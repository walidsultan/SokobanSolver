using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
namespace Sokoban.SokobanSolvingLogic
{
    public class CarrierPathTracker  
    {
        public   static     Path CheckForValidPath(List<SokobanObject> AllObjects, PositionIndex TargetIndex)
        {
            ClearAllTrackPaths(AllObjects);
            PositionIndex CarrierIndex = GetCarrierIndex(AllObjects );
            if (TargetIndex.CompareTo(CarrierIndex) == 1)
            {// there no need to calculate the path because the carrier is already on the target
                Path _path = new Path();
                _path.valid = true;
                return _path;
            }
            List<SokobanObject> SurroundingObjects = GetSurroundingObjectsOfTypeFloorOrTarget(AllObjects, new SokobanObject( TargetIndex));
            while (true)
            {
                List<SokobanObject> RecursiveObjects = new List<SokobanObject>();
                foreach (SokobanObject _Object in SurroundingObjects)
                {

                    if (_Object.Type != UnitType.Carrier && _Object.Type != UnitType.CarrierOnTarget)
                    {
                        RecursiveObjects.AddRange(GetSurroundingObjectsOfTypeFloorOrTarget(AllObjects, _Object));
                    }
                    else
                    {
                        _Object.TrackPath.valid = true;
                        return _Object.TrackPath;
                    }
                }
                SurroundingObjects.Clear();
                SurroundingObjects.AddRange(RecursiveObjects);

                if (RecursiveObjects.Count == 0) break;
            }

            return new Path();// return Invalid path
        }
        public  static List<Path> GetPossiblePaths(List<SokobanObject> AllObjects)
        {
            List<Path> PossiblePaths = new List<Path>();
            List<SokobanObject> AllBoxes = AllObjects.FindAll(delegate(SokobanObject obj) { return (obj.Type == UnitType.Box || obj.Type == UnitType.BoxOnTarget); });
            
            foreach (SokobanObject Box in AllBoxes)
            {
                ClearAllTrackPaths(AllObjects);
                List<SokobanObject> SurroundingObjects = GetSurroundingObjectsOfTypeFloorOrTarget(AllObjects, Box);
                foreach (SokobanObject _SurroundingObject in SurroundingObjects)
                {
                    //path to the surrounding objects will be important in case there is no obstacle at the other side of the box
                    PositionIndex OppositeObjectIndex = new PositionIndex(_SurroundingObject.Position);
                    Direction OppositeDirection = GetPushDirection(_SurroundingObject, Box);
                    switch (OppositeDirection)
                    {
                        case Direction.Down:
                            OppositeObjectIndex.yIndex -= 2;
                            break;
                        case Direction.Up:
                            OppositeObjectIndex.yIndex += 2;
                            break;
                        case Direction.Right:
                            OppositeObjectIndex.xIndex -= 2;
                            break;
                        case Direction.Left:
                            OppositeObjectIndex.xIndex += 2;
                            break;
                    }

                    SokobanObject OppositeObject = new SokobanObject();
                    try
                    {
                        OppositeObject = GetObjectByPositionIndex(SurroundingObjects, OppositeObjectIndex);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Object not Found")
                            OppositeObject.Type = UnitType.Wall;
                    }

                    if (OppositeObject.Type != UnitType.Wall && OppositeObject.Type != UnitType.Box && OppositeObject.Type != UnitType.BoxOnTarget)
                    {
                        Path _path = CheckForValidPath(AllObjects, _SurroundingObject.Position);
                        if (_path.valid)
                        {
                            _path = ReversePath(_path);
                            _path.Directions.Add(GetPushDirection(Box, _SurroundingObject));
                            PossiblePaths.Add(_path);
                        }
                    }
                }
            }
            return PossiblePaths;
        }
        private static void ClearAllTrackPaths(List<SokobanObject> AllObjects)
        {
            foreach (SokobanObject _Object in AllObjects)
            {
                _Object.TrackPath = new Path();
            }

        }
        private static List<SokobanObject> GetSurroundingObjectsOfTypeFloorOrTarget(List<SokobanObject> AllObjects, SokobanObject SurroundedObject)
        {
            PositionIndex SurroundedIndex = new PositionIndex(SurroundedObject.Position.xIndex, SurroundedObject.Position.yIndex);
            PositionIndex LeftIndex = new PositionIndex(SurroundedIndex.xIndex - 1, SurroundedIndex.yIndex);
            PositionIndex RightIndex = new PositionIndex(SurroundedIndex.xIndex + 1, SurroundedIndex.yIndex);
            PositionIndex UpIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex - 1);
            PositionIndex DownIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex + 1);
            List<SokobanObject> SurrouningObjects = new List<SokobanObject>();
            List<SokobanObject> RelevantObjects = AllObjects.FindAll(delegate(SokobanObject obj) { return (obj.Type == UnitType.Floor || obj.Type == UnitType.Target || obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget) && obj.TrackPath.Directions.Count == 0; });
            foreach (SokobanObject _Object in RelevantObjects)
            {
                if (_Object.Position.CompareTo(LeftIndex) == 1)// && ExcludeDirection != Direction.Left)
                {
                    _Object.TrackPath = new Path(SurroundedObject.TrackPath);
                    _Object.TrackPath.Directions.Add(Direction.Left);
                    SurrouningObjects.Add(_Object);
                }
                if (_Object.Position.CompareTo(RightIndex) == 1 )//&& ExcludeDirection != Direction.Right)
                {
                    _Object.TrackPath = new Path(SurroundedObject.TrackPath);
                    _Object.TrackPath.Directions.Add(Direction.Right);
                    SurrouningObjects.Add(_Object);
                }
                if (_Object.Position.CompareTo(UpIndex) == 1 )//&& ExcludeDirection != Direction.Up)
                {
                    _Object.TrackPath = new Path(SurroundedObject.TrackPath);
                    _Object.TrackPath.Directions.Add(Direction.Up);
                    SurrouningObjects.Add(_Object);
                }


                if (_Object.Position.CompareTo(DownIndex) == 1)// && ExcludeDirection != Direction.Down)
                {
                    _Object.TrackPath = new Path(SurroundedObject.TrackPath);
                    _Object.TrackPath.Directions.Add(Direction.Down);
                    SurrouningObjects.Add(_Object);
                }

            }

            return SurrouningObjects;
        }

        private static Direction GetoppositeDirection(Direction _Direction)
        {
            switch (_Direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new NotImplementedException();

            }
        }
        private static Direction GetPushDirection(SokobanObject BoxObject, SokobanObject PushingObject)
        {
            PositionIndex BoxPosition = new PositionIndex(BoxObject.Position);
            PositionIndex PushingObjectPosition = new PositionIndex(PushingObject.Position);
            int relativeX = BoxPosition.xIndex - PushingObjectPosition.xIndex;
            int relativeY = BoxPosition.yIndex - PushingObjectPosition.yIndex;
            if (relativeX == -1 && relativeY == 0)
            {
                return Direction.Left;
            }
            else if (relativeX == 1 && relativeY == 0)
            {
                return Direction.Right;
            }
            else if (relativeX == 0 && relativeY == -1)
            {
                return Direction.Up;
            }
            else if (relativeX == 0 && relativeY == 1)
            {
                return Direction.Down;
            }
            else
            {
                throw new Exception("Objects aren't near each other");// error in computing logic
            }

        }
        public  static SokobanObject GetObjectByPositionIndex(List<SokobanObject> AllObjects, PositionIndex Index)
        {

            SokobanObject _Object = AllObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(Index) == 1; });

            if (_Object == null)
            {
                throw new Exception("Object not Found");
            }
            else
            {
                return _Object;
            }

        }
        private static PositionIndex GetCarrierIndex(List<SokobanObject> AllObjects)
        {

            SokobanObject _Object = AllObjects.Find(delegate(SokobanObject obj) { return (obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget); });

            if (_Object == null)
            {
                throw new Exception("Carrier was not found");
            }
            else
            {
                return _Object.Position ;
            }

        }
        private static Path ReversePath(Path _path)
        {
            Path ReversedPath = new Path();
            ReversedPath.valid = _path.valid;
            for (int PathIndex = _path.Directions.Count - 1; PathIndex > -1; PathIndex--)
            {
                //ReversedPath.Directions.Add(GetoppositeDirection(_path.Directions.ElementAt(PathIndex)));
                //impelentation of the above statement for dot net framework 2
                ReversedPath.Directions.Add(GetoppositeDirection(_path.Directions[PathIndex]));
                
            }
            return ReversedPath;
        }
    }
}
