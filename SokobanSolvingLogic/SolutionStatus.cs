using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
namespace Sokoban.SokobanSolvingLogic
{
    public class SolutionStatus
    {

        public static bool IsSolutionStuck(Solution _Solution)
        {

            List<SokobanObject> AllBoxes = _Solution.DerivedObjects.FindAll(delegate(SokobanObject obj) { return obj.Type == UnitType.Box; });

            //Check if any "Box" is completely stuck
            foreach (SokobanObject Box in AllBoxes)
            {
                if (IsObjectStuck(_Solution.DerivedObjects, Box))
                    return true;
                else
                    Box.TrackType = UnitType.Floor;
            }

            //the following is another check for stuck procedure added on 1st november 2008
            //List<SokobanObject> AllBoxesOnTargets = _Solution.DerivedObjects.FindAll(delegate(SokobanObject obj) { return obj.Type == UnitType.BoxOnTarget; });
            //foreach (SokobanObject BoxOnTarget in AllBoxesOnTargets)
            //{
            //    if (IsObjectStuck(_Solution.DerivedObjects, BoxOnTarget))
            //        BoxOnTarget.TrackType = UnitType.Wall;
            //    else
            //    {
            //        BoxOnTarget.TrackType = UnitType.Target;
            //    }
            //}


            //List<SokobanObject> AllFloor = _Solution.DerivedObjects.FindAll(delegate(SokobanObject obj) { return obj.Type == UnitType.Floor; });
            //foreach (SokobanObject _Object in AllFloor)
            //{
            //    _Object.TrackType = UnitType.Floor;

            //}

            //SokobanObject CarrierOnTarget = _Solution.DerivedObjects.Find(delegate(SokobanObject obj) { return (obj.Type == UnitType.CarrierOnTarget || obj.Type == UnitType.Carrier) ; });
            //if (CarrierOnTarget != null) CarrierOnTarget.TrackType = UnitType.Carrier;

            //AllBoxes.AddRange(AllBoxesOnTargets);
            //List<SokobanObject> AllTargets = _Solution.DerivedObjects.FindAll(delegate(SokobanObject obj) { return obj.TrackType == UnitType.Target || obj.Type == UnitType.Target; });
            //foreach (SokobanObject Box in AllBoxes)
            //{
            //    if (Box.TrackType != UnitType.Wall)
            //    {
            //        bool ValidPath = false;
            //        Box.TrackType = Box.Type;
            //        foreach (SokobanObject target in AllTargets)
            //        {
            //            Path PossiblePath = DeliverBox.CheckForValidPath(_Solution.DerivedObjects, new PositionIndex(target.Position), new PositionIndex(Box.Position));
            //            if (PossiblePath.valid)
            //            {
            //                ValidPath = true;
            //                break;
            //            }
            //        }
            //        if (Box.TrackType == UnitType.BoxOnTarget)
            //            Box.Type = UnitType.Target;
            //        else
            //            Box.Type = UnitType.Floor;
            //        if (!ValidPath) return true;
            //    }
            //}
            return false;
        }

        private static bool IsObjectStuck(List<SokobanObject> AllObjects, SokobanObject TargetBox)
        {

            if (!IsObjectMovable(AllObjects, TargetBox))
            {
                int Obstacles = 0;
                List<SokobanObject> SurroundingObjects = GetSurroundingObjects(AllObjects, TargetBox.Position);
                List<SokobanObject> ObstaclesList = new List<SokobanObject>();
                foreach (SokobanObject _SurroundingObject in SurroundingObjects)
                {
                    if (_SurroundingObject.Type == UnitType.Box || _SurroundingObject.Type == UnitType.BoxOnTarget || _SurroundingObject.Type == UnitType.Wall)
                    {
                        Obstacles++;
                        ObstaclesList.Add(_SurroundingObject);
                    }
                }
                int NotMovableObstacles = 0;
                List<SokobanObject> NotMovableObstaclesList = new List<SokobanObject>();
                foreach (SokobanObject _Obstacle in ObstaclesList)
                {
                    if (!IsObjectMovable(AllObjects, _Obstacle))
                    {
                        NotMovableObstacles++;
                        NotMovableObstaclesList.Add(_Obstacle);
                    }
                }

                if (NotMovableObstacles > 2)
                {
                    return true;
                }
                else if (NotMovableObstacles == 2)
                {
                    //check if these 2 obstacles are adjactent
                    if (NotMovableObstaclesList[0].Position.xIndex != NotMovableObstaclesList[1].Position.xIndex &&
               NotMovableObstaclesList[0].Position.yIndex != NotMovableObstaclesList[1].Position.yIndex)
                    {   //   $$
                        //#$     we must first get the object at the corner before judging that the solution is stuck
                        if (NotMovableObstaclesList[0].Type != UnitType.Wall || NotMovableObstaclesList[1].Type != UnitType.Wall)
                        {
                            PositionIndex Relative0 = new PositionIndex(TargetBox.Position.xIndex - NotMovableObstaclesList[0].Position.xIndex, TargetBox.Position.yIndex - NotMovableObstaclesList[0].Position.yIndex);
                            PositionIndex Relative1 = new PositionIndex(TargetBox.Position.xIndex - NotMovableObstaclesList[1].Position.xIndex, TargetBox.Position.yIndex - NotMovableObstaclesList[1].Position.yIndex);
                            PositionIndex RelativePosition = new PositionIndex(Relative0.xIndex + Relative1.xIndex, Relative0.yIndex + Relative1.yIndex);
                            PositionIndex CornerPosition = new PositionIndex(TargetBox.Position.xIndex - RelativePosition.xIndex, TargetBox.Position.yIndex - RelativePosition.yIndex);

                            SokobanObject CornerObject = CarrierPathTracker.GetObjectByPositionIndex(AllObjects, CornerPosition);

                            if (CornerObject.Type == UnitType.Box || CornerObject.Type == UnitType.BoxOnTarget || CornerObject.Type == UnitType.Wall)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }


                    }

                }

            }
           
                return false;
               
        
        }

        private static bool IsObjectMovable(List<SokobanObject> AllObjects, SokobanObject TargetBox)
        {
            if (TargetBox.Type == UnitType.Box || TargetBox.Type == UnitType.BoxOnTarget)
            {
                List<SokobanObject> SurroundingObjects = GetSurroundingObjects(AllObjects, TargetBox.Position);
                List<SokobanObject> ObstaclesList = new List<SokobanObject>();
                int Obstacles = 0;
                foreach (SokobanObject _Object in SurroundingObjects)
                {
                    if (_Object.Type == UnitType.Box || _Object.Type == UnitType.BoxOnTarget || _Object.Type == UnitType.Wall)
                    {
                        Obstacles++;
                        ObstaclesList.Add(_Object);
                    }
                }
                if (Obstacles == 3)
                {
                    return false;
                }
                else if (Obstacles == 2)
                {
                    //check if the two obstacles are adjacent and both of them are of wall type
                    if (ObstaclesList[0].Position.xIndex != ObstaclesList[1].Position.xIndex &&
                        ObstaclesList[0].Position.yIndex != ObstaclesList[1].Position.yIndex)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }

        }
        public  static bool IsLevelSolved(List<SokobanObject> AllObjects)
        {
            foreach (SokobanObject _Object in AllObjects)
            {
                if (_Object.Type == UnitType.Box) return false;
            }

            return true;
        }

        private static List<SokobanObject> GetSurroundingObjects(List<SokobanObject> AllObjects, PositionIndex SurroundedIndex)
        {
            PositionIndex LeftIndex = new PositionIndex(SurroundedIndex.xIndex - 1, SurroundedIndex.yIndex);
            PositionIndex RightIndex = new PositionIndex(SurroundedIndex.xIndex + 1, SurroundedIndex.yIndex);
            PositionIndex UpIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex - 1);
            PositionIndex DownIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex + 1);


            List<SokobanObject> SurrouningObjects = AllObjects.FindAll(delegate(SokobanObject obj)
            {
                return (obj.Position.CompareTo(LeftIndex) == 1 || obj.Position.CompareTo(RightIndex) == 1
                               || obj.Position.CompareTo(UpIndex) == 1 || obj.Position.CompareTo(DownIndex) == 1);
            });



            foreach (SokobanObject _Object in SurrouningObjects)
            {
                if (_Object.Position.CompareTo(LeftIndex) == 1)
                {
                    _Object.TrackPath.Directions.Add(Direction.Left);

                }
                if (_Object.Position.CompareTo(RightIndex) == 1)
                {
                    _Object.TrackPath.Directions.Add(Direction.Right);

                }
                if (_Object.Position.CompareTo(UpIndex) == 1)
                {
                    _Object.TrackPath.Directions.Add(Direction.Up);

                }


                if (_Object.Position.CompareTo(DownIndex) == 1)
                {
                    _Object.TrackPath.Directions.Add(Direction.Down);

                }

            }
            return SurrouningObjects;
        }
    }
}
