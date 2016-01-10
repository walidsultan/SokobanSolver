using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
using System.Threading;
namespace Sokoban.SokobanSolvingLogic
{
   public class DeliverBox
    {
       static Path _SolutionPath = new Path();

       public static List<TrackingInfo > GetAllPossibleDeliverPaths(List<SokobanObject> AllObjects)
       {
           List<SokobanObject> AllTargets = AllObjects.FindAll(delegate(SokobanObject obj) { return obj.Type == UnitType.Target || obj.Type == UnitType.CarrierOnTarget ; });
           List<SokobanObject> AllBoxes = AllObjects.FindAll(delegate(SokobanObject obj) { return (obj.Type == UnitType.Box ); });
           List<TrackingInfo > AllPossiblePaths = new List<TrackingInfo >();
           
           //get a valid path for each box to all targets
           foreach (SokobanObject target in AllTargets)
           {
               foreach (SokobanObject box in AllBoxes)
               {
                   TrackingInfo BoxTargetPath = new TrackingInfo();
                     BoxTargetPath.TrackPath  = GetDeliverPath(AllObjects, box.Position, (PositionIndex) target.Position.Clone(), false,false   ); //remember to pass allboxes and alltargets instead of allboxes

                     if (BoxTargetPath.TrackPath.valid == true)
                   {
                       BoxTargetPath.BoxPosition = new PositionIndex(box.Position);
                       BoxTargetPath.TargetPosition = new PositionIndex(target.Position);
                       AllPossiblePaths.Add(BoxTargetPath);
                   }
                }
           }
           return AllPossiblePaths;
       }  
       
       public static Path GetDeliverPath(List<SokobanObject> AllObjects,PositionIndex StartPosition,PositionIndex TargetPosition,bool UseUnInformedSolver,bool GetCarrierPath)
       {
           #region Initialize Objects
           List<SokobanObject> AllBoxes = AllObjects.FindAll(delegate(SokobanObject obj) { return ((obj.Type == UnitType.Box || obj.Type == UnitType.BoxOnTarget)&&obj.Position.CompareTo(StartPosition)!=1)  ; });
           foreach (SokobanObject _Object in AllBoxes )
           {
               _Object.TrackType  = UnitType.Wall;
           
           }
           List<SokobanObject> AllTargets = AllObjects.FindAll(delegate(SokobanObject obj) { return ((obj.Type == UnitType.Target  ) && obj.Position.CompareTo(TargetPosition ) != 1) ||obj.Type == UnitType.Floor ; });
           foreach (SokobanObject _Object in AllTargets )
           {
               _Object.TrackType = UnitType.Floor;

           }

           SokobanObject CarrierOnTarget = AllObjects.Find(delegate(SokobanObject obj) { return ((obj.Type == UnitType.CarrierOnTarget || obj.Type == UnitType.Carrier ) && obj.Position.CompareTo(TargetPosition) != 1); });
           if(CarrierOnTarget!=null)  CarrierOnTarget.TrackType  = UnitType.Carrier;


           SokobanObject  TargetObject = AllObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(TargetPosition) == 1; });
           if (TargetObject.Type != UnitType.Carrier && TargetObject.Type != UnitType.CarrierOnTarget)
           {
               TargetObject.TrackType = UnitType.Target;
           }
           else
           {
               TargetObject.TrackType = UnitType.CarrierOnTarget;
           }

           SokobanObject StartObject = AllObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(StartPosition ) == 1; });
           //if (StartObject.TrackType == UnitType.BoxOnTarget)
           //{
               StartObject.TrackType = UnitType.Box;
               //}
           #endregion

           #region Check for direct path
           //################*************************Check for direct path************************###################
           DateTime StartTime = DateTime.Now;
           List<SokobanObject> CopyOfAllObjects = new List<SokobanObject>();
           if (UseUnInformedSolver)
           {
               //copy all objects to be used with Heuristics solver
               foreach (SokobanObject obj in AllObjects) 
               {
                   SokobanObject CloneObj = (SokobanObject)obj.Clone();
                   CloneObj.Type = CloneObj.TrackType; // the heuristics solver uses "UnitType" unlike the DirectPathSolver 
                   CopyOfAllObjects.Add(CloneObj); 
               }
           }
           Path DirectPath = CheckForValidPath(AllObjects, TargetPosition, StartPosition);
           if (DirectPath.valid)
           {
               if (GetCarrierPath)
               {
                   Path Carrierpath = GetPushPath(AllObjects, DirectPath, StartPosition);
                   if (Carrierpath.valid)
                   {
                       _SolutionPath = Carrierpath;
                       PerformanceDetails.IsDirectPath = true;
                       PerformanceDetails.PathTime = DateTime.Now.Subtract(StartTime).Milliseconds + DateTime.Now.Subtract(StartTime).Seconds * 1000;
                       return _SolutionPath;
                   }
               }
               else
               {
                   return DirectPath;
               }
           }
           //################**************************************************************************###################
           #endregion

           #region Use Heuristics solver
           //#######**************Use Heuristics solver**************#######
           if (UseUnInformedSolver)
           {
               StartTime = DateTime.Now;

               HeuristicsSolver Solver = new HeuristicsSolver();
             //  Thread BoxDeleveringThread = new Thread(Solver.Solve);
               //BoxDeleveringThread.Start(CopyOfAllObjects);
               //Solver.Solved += new HeuristicsSolver.SolvedHandler(Solver_Solved);
               //BoxDeleveringThread.Join();

               PerformanceDetails.IsDirectPath = false;
               PerformanceDetails.PathTime = DateTime.Now.Subtract(StartTime).Milliseconds + DateTime.Now.Subtract(StartTime).Seconds * 1000;
           }
           //#######**************************************************#######
           #endregion

           return _SolutionPath;
       }

       public static Path GetPushPath( List<SokobanObject>AllObjects, Path BoxPath,PositionIndex  BoxPosition )
       { 
           PositionIndex TargetPosition=new PositionIndex(BoxPosition);
           PositionIndex CarrierIndex = GetCarrierIndex(AllObjects);
           Path CarrierPath = new Path();
           foreach (Direction _direction in BoxPath.Directions )
           {
               switch (_direction )
               {
                   case Direction.Up:
                       TargetPosition.yIndex++;
                       break;
                   case Direction.Down:
                       TargetPosition.yIndex--;
                       break;
                   case Direction.Left:
                       TargetPosition.xIndex++;
                       break;
                   case Direction.Right:
                       TargetPosition.xIndex--;
                       break;
               }

             

            Path PushPath=CarrierPathTracker.CheckForValidPath(AllObjects, TargetPosition);
            if (PushPath.valid)
            {
                CarrierPath.Directions.AddRange(ReversePath(PushPath).Directions);
                CarrierPath.Directions.Add(_direction);
            }
            else
            {
                return new Path();
            }


                SokobanObject BoxObject = GetObjectByPositionIndex(AllObjects, BoxPosition);
                SokobanObject CarrierObject = GetObjectByPositionIndex(AllObjects, CarrierIndex);
                BoxObject.Type = UnitType.Carrier;
                CarrierObject.Type = UnitType.Floor;

                CarrierIndex = new PositionIndex(TargetPosition);
                switch (CarrierPath.Directions.FindLast(delegate(Direction direction) { return true; }))
                {
                    case Direction.Up:
                        TargetPosition.yIndex -= 2;
                        CarrierIndex.yIndex--;
                        BoxPosition.yIndex--;
                        break;
                    case Direction.Down:
                        TargetPosition.yIndex += 2;
                        CarrierIndex.yIndex++;
                        BoxPosition.yIndex++;
                        break;
                    case Direction.Left:
                        TargetPosition.xIndex -= 2;
                        CarrierIndex.xIndex--;
                        BoxPosition.xIndex--;
                        break;
                    case Direction.Right:
                        TargetPosition.xIndex += 2;
                        CarrierIndex.xIndex++;
                        BoxPosition.xIndex++;
                        break;
                }
                SokobanObject NewBoxObject = GetObjectByPositionIndex(AllObjects, BoxPosition);
                NewBoxObject.Type = UnitType.Box;
           }
           CarrierPath.valid = true;
           return CarrierPath;
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
               return _Object.Position;
           }

       }

       /// <summary>
       /// Check for direct path between any selected target and any box
       /// </summary>
       /// <param name="AllObjects">All object in the system</param>
       /// <param name="TargetIndex">Index of the target.</param>
       /// <param name="BoxIndex">Index of the box.</param>
       /// <returns>The path between the target and the box if there is valid path</returns>
       public static Path CheckForValidPath(List<SokobanObject> AllObjects, PositionIndex TargetIndex, PositionIndex BoxIndex)
       {
           ClearAllTrackPaths(AllObjects);
           if (TargetIndex.CompareTo(BoxIndex) == 1)
           {// there no need to calculate the path because the carrier is already on the target
               Path _path = new Path();
               _path.valid = true;
               return _path;
           }
           List<SokobanObject> SurroundingObjects = GetSurroundingObjectsOfTypeFloorOrTarget(AllObjects,new SokobanObject( BoxIndex ));
           while (true)
           {
               List<SokobanObject> RecursiveObjects = new List<SokobanObject>();
               foreach (SokobanObject _Object in SurroundingObjects)
               {

                   if (_Object.TrackType != UnitType.Target && _Object.TrackType != UnitType.CarrierOnTarget)
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
       public static SokobanObject GetObjectByPositionIndex(List<SokobanObject> AllObjects, PositionIndex Index)
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
       private static List<SokobanObject> GetSurroundingObjectsOfTypeFloorOrTarget(List<SokobanObject> AllObjects, SokobanObject SurroundedObject)
       {
           PositionIndex SurroundedIndex = new PositionIndex(SurroundedObject.Position.xIndex, SurroundedObject.Position.yIndex);
           PositionIndex LeftIndex = new PositionIndex(SurroundedIndex.xIndex - 1, SurroundedIndex.yIndex);
           PositionIndex RightIndex = new PositionIndex(SurroundedIndex.xIndex + 1, SurroundedIndex.yIndex);
           PositionIndex UpIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex - 1);
           PositionIndex DownIndex = new PositionIndex(SurroundedIndex.xIndex, SurroundedIndex.yIndex + 1);
           List<SokobanObject> SurroundingObjects = new List<SokobanObject>();
           List<SokobanObject> RelevantObjects = AllObjects.FindAll(delegate(SokobanObject obj) { return (obj.TrackType == UnitType.Floor || obj.TrackType == UnitType.Target || obj.TrackType == UnitType.Carrier || obj.TrackType == UnitType.CarrierOnTarget || obj.TrackType == UnitType.Box); });


           SokobanObject LeftObject = RelevantObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(LeftIndex) == 1; });
           SokobanObject RightObject = RelevantObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(RightIndex) == 1; });
           SokobanObject TopObject = RelevantObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(UpIndex) == 1; });
           SokobanObject BottomObject = RelevantObjects.Find(delegate(SokobanObject obj) { return obj.Position.CompareTo(DownIndex) == 1; });


           if (LeftObject != null && RightObject != null)
           {
               if (LeftObject.TrackPath.Directions.Count == 0 && LeftObject.TrackType != UnitType.Box)
               {
                   LeftObject.TrackPath = new Path(SurroundedObject.TrackPath);
                   LeftObject.TrackPath.Directions.Add(Direction.Left);
                   SurroundingObjects.Add(LeftObject);
               }

               if (RightObject.TrackPath.Directions.Count == 0 && RightObject.TrackType != UnitType.Box)
               {
                   RightObject.TrackPath = new Path(SurroundedObject.TrackPath);
                   RightObject.TrackPath.Directions.Add(Direction.Right);
                   SurroundingObjects.Add(RightObject);
               }
           }


           if (TopObject != null && BottomObject != null)
           {
               if (TopObject.TrackPath.Directions.Count == 0 && TopObject.TrackType != UnitType.Box)
               {
                   TopObject.TrackPath = new Path(SurroundedObject.TrackPath);
                   TopObject.TrackPath.Directions.Add(Direction.Up);
                   SurroundingObjects.Add(TopObject);
               }

               if (BottomObject.TrackPath.Directions.Count == 0 && BottomObject.TrackType != UnitType.Box)
               {
                   BottomObject.TrackPath = new Path(SurroundedObject.TrackPath);
                   BottomObject.TrackPath.Directions.Add(Direction.Down);
                   SurroundingObjects.Add(BottomObject);
               }
           }



           return SurroundingObjects;
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

       private static void ClearAllTrackPaths(List<SokobanObject> AllObjects)
       {
           foreach (SokobanObject _Object in AllObjects)
           {
               _Object.TrackPath = new Path();
           }

       }
       static void Solver_Solved(object unInformedSolver, SolutionInfoEventArgs SolutionInformation)
       {
           if (SolutionInformation.SolutionPath.valid == true )
           {
               _SolutionPath = new Path(SolutionInformation.SolutionPath);
               _SolutionPath.valid = SolutionInformation.SolutionPath.valid;
           }

       }
    }
}
