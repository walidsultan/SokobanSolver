using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
namespace Sokoban.SokobanSolvingLogic
{
    public class DirectPathSolver
    {
        public delegate void SolvedHandler(object unInformedSolver, SolutionInfoEventArgs SolutionInformation);

        public event SolvedHandler Solved;

        private static bool _requestStop = false;

        

        public void Solve(object AllObjectsParametrized)
        {
            List<SokobanObject> AllObjects = (List<SokobanObject>)AllObjectsParametrized;
            List<Solution> PossibleSolutions = new List<Solution>();
            List<TrackingInfo > AllTargetPaths = DeliverBox .GetAllPossibleDeliverPaths (AllObjects);
            int LevelOrder = AllObjects.FindAll(delegate(SokobanObject obj) { return obj.Type == UnitType.Target || obj.Type == UnitType.CarrierOnTarget; }).Count;
            int MaxRecursiveSolutions = int.Parse(Math.Pow(Factorial(Settings.DirectPathMaxTargets), 2).ToString());  // (n!)^2


            if (LevelOrder > Settings.DirectPathMaxTargets)
            {
                SolutionInfoEventArgs SolutionInformation = new SolutionInfoEventArgs(new Path());
                OnSolved(new HeuristicsSolver(), SolutionInformation);
                return;
                //number of targets will generate too many recurisive solutions 
            }
            
                int CurrentOrder = 2;
           // PerformanceDetails.Intitialize();

            foreach (TrackingInfo possiblePath in AllTargetPaths)
            {
                Solution PossibleSolution = ApplySolution(AllObjects , possiblePath );
                if (SolutionStatus.IsLevelSolved(PossibleSolution.DerivedObjects))  
                {
                    PossibleSolution .SolutionHistory.Add(possiblePath);
                    Path SolutionPath = TransformLevelHistoryToPath(AllObjects, PossibleSolution );
                    if (SolutionPath.valid)
                    {
                        SolutionInfoEventArgs SolutionInformation = new SolutionInfoEventArgs(SolutionPath);
                        SolutionInformation.SolutionPath.valid = true;
                        OnSolved(new DirectPathSolver(), SolutionInformation);
                        return;
                        //Level Solved 
                    } 

                }
                
                    PossibleSolution.SolutionHistory.Add(possiblePath);
                    PossibleSolutions.Add(PossibleSolution);
                

            
            }

            List<Solution> RecursiveSolutions = new List<Solution>();
            while (_requestStop == false)
            {
                foreach (Solution PossibleSolution in PossibleSolutions)
                {

                    AllTargetPaths = DeliverBox.GetAllPossibleDeliverPaths(PossibleSolution.DerivedObjects );
                    foreach (TrackingInfo possiblePath in AllTargetPaths)
                    {
                        Solution _PossibleSolution = ApplySolution(PossibleSolution.DerivedObjects, possiblePath);
                        _PossibleSolution.SolutionHistory.AddRange(PossibleSolution.SolutionHistory); 
                        _PossibleSolution.SolutionHistory.Add(possiblePath);
                     

                     
                                RecursiveSolutions.Add(_PossibleSolution);
                                //PerformanceDetails.RecursiveSolutions = RecursiveSolutions.Count;
                                if (RecursiveSolutions.Count > MaxRecursiveSolutions) OnSolved(this, new SolutionInfoEventArgs(new Path()));

                                if (CurrentOrder == LevelOrder)
                                {
                                    if (SolutionStatus.IsLevelSolved(_PossibleSolution.DerivedObjects))
                                    {
                                        Path SolutionPath = TransformLevelHistoryToPath(AllObjects, _PossibleSolution);
                                        if (SolutionPath.valid)
                                        {
                                            SolutionInfoEventArgs SolutionInformation = new SolutionInfoEventArgs(SolutionPath);
                                            SolutionInformation.SolutionPath.valid = true;
                                            OnSolved(new DirectPathSolver(), SolutionInformation);
                                            return;
                                            //Level Solved 
                                        }

                                    }
                                }

                    }
                }
                PossibleSolutions.Clear();
                PossibleSolutions.AddRange(RecursiveSolutions);

                if (RecursiveSolutions.Count == 0)
                {
                    SolutionInfoEventArgs SolutionInformation = new SolutionInfoEventArgs(new Path());
                    OnSolved(new HeuristicsSolver(), SolutionInformation);
                    return;
                    //Level failed ;
                }
                RecursiveSolutions.Clear();
                CurrentOrder++;
            }


        }

        public static long Factorial(long val)
        {
            return val == 1 ? val : val * Factorial(val - 1);
        }
        public Path TransformLevelHistoryToPath(List<SokobanObject>NativeObjects,   Solution _Solution)
        {
            List<SokobanObject> DerivedObjects = new List<SokobanObject>();
            foreach (SokobanObject _NativeObject in NativeObjects)
            {
                DerivedObjects.Add((SokobanObject)_NativeObject.Clone());
            }

            Path SolutionPath=new Path();
            foreach (TrackingInfo PossiblePath in _Solution.SolutionHistory)
            {
                Path BoxPath = DeliverBox.GetPushPath(DerivedObjects, PossiblePath.TrackPath, new PositionIndex( PossiblePath.BoxPosition));
               if (BoxPath.valid)
               {
                   SolutionPath.Directions.AddRange(BoxPath.Directions );
               }
               else
               {
                   return new Path();  //solution is not valid
               }

            }
            SolutionPath.valid = true;
            return SolutionPath;
        
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

        public static Solution ApplySolution( List<SokobanObject> AllObjects, TrackingInfo _TrackingInfo)
        {
            List<SokobanObject> DerivedObjects = new List<SokobanObject>();
            foreach (SokobanObject _NativeObject in AllObjects)
            {
                DerivedObjects.Add((SokobanObject)_NativeObject.Clone());
            }


            SokobanObject BoxObject = GetObjectByPositionIndex(DerivedObjects, _TrackingInfo.BoxPosition);
            SokobanObject TargetObject = GetObjectByPositionIndex(DerivedObjects, _TrackingInfo.TargetPosition);
            SokobanObject CarrierObject = GetObjectByPositionIndex(DerivedObjects, GetCarrierIndex(DerivedObjects));

            TargetObject.Type = UnitType.BoxOnTarget;
            BoxObject.Type = UnitType.Floor;
            if (CarrierObject.Type == UnitType.Carrier)
                CarrierObject.Type = UnitType.Floor;
            else if (CarrierObject.Type == UnitType.CarrierOnTarget)
                CarrierObject.Type = UnitType.Target;

            Direction LastPushDirection = _TrackingInfo.TrackPath.Directions.FindLast(delegate(Direction direction) { return true; });
            PositionIndex NewCarrierIndex ;

            //get carrier index from last box push direction
            switch (LastPushDirection)
            {
                case Direction.Down:
                    NewCarrierIndex = new PositionIndex(_TrackingInfo.TargetPosition.xIndex, _TrackingInfo.TargetPosition.yIndex-1);
                    break;
                case Direction.Up:
                    NewCarrierIndex = new PositionIndex(_TrackingInfo.TargetPosition.xIndex, _TrackingInfo.TargetPosition.yIndex+1);
                    break;
                case Direction.Left:
                    NewCarrierIndex = new PositionIndex(_TrackingInfo.TargetPosition.xIndex+1, _TrackingInfo.TargetPosition.yIndex);
                    break;
                case Direction.Right:
                    NewCarrierIndex = new PositionIndex(_TrackingInfo.TargetPosition.xIndex-1, _TrackingInfo.TargetPosition.yIndex);
                    break;
                default :
                    throw  new Exception("Direction not supported");
            }

            SokobanObject newCarrierObject = GetObjectByPositionIndex(DerivedObjects, NewCarrierIndex);
            if(newCarrierObject.Type ==UnitType.Floor )
                newCarrierObject.Type =UnitType.Carrier;
            else if (newCarrierObject.Type ==UnitType.Target )
                newCarrierObject.Type = UnitType.CarrierOnTarget;


            Solution AppliedSolution = new Solution();
              AppliedSolution.DerivedObjects = DerivedObjects;
              return AppliedSolution;









          //  SokobanObject CarrierObject = DerivedObjects.Find(delegate(SokobanObject obj) { return (obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget); });
          //  //Move Carrier to destination
          // // PositionIndex TargetPosition = new PositionIndex(CarrierObject.Position);
          //  //foreach (Direction _direction in ApplyPath.Directions)
          //  //{
          //  //    switch (_direction)
          //  //    {
          //  //        case Direction.Left:
          //  //            TargetPosition.xIndex -= 1;
          //  //            break;
          //  //        case Direction.Down:
          //  //            TargetPosition.yIndex += 1;
          //  //            break;
          //  //        case Direction.Up:
          //  //            TargetPosition.yIndex -= 1;
          //  //            break;
          //  //        case Direction.Right:
          //  //            TargetPosition.xIndex += 1;
          //  //            break;
          //  //    }
          //  //}

          //  SokobanObject BoxObject = CarrierPathTracker.GetObjectByPositionIndex(DerivedObjects, TargetPosition);
          ////  if (BoxObject.Type != UnitType.Box && BoxObject.Type != UnitType.BoxOnTarget) return new Solution(); //this line for test stuck procedure
          //  PositionIndex PushIndex = new PositionIndex(TargetPosition);
          //  //push box
          //  //switch (ApplyPath.Directions.Last())
          //  //{
          //  //    case Direction.Up:
          //  //        PushIndex.yIndex--;
          //  //        break;
          //  //    case Direction.Down:
          //  //        PushIndex.yIndex++;
          //  //        break;
          //  //    case Direction.Left:
          //  //        PushIndex.xIndex--;
          //  //        break;
          //  //    case Direction.Right:
          //  //        PushIndex.xIndex++;
          //  //        break;

          //  //}


          //  SokobanObject TargetObject = CarrierPathTracker.GetObjectByPositionIndex(DerivedObjects, PushIndex);
          //  if (TargetObject.Type == UnitType.Target)
          //  {
          //      TargetObject.Type = UnitType.BoxOnTarget;

          //  }
          //  else if (TargetObject.Type == UnitType.Floor)
          //  {
          //      TargetObject.Type = UnitType.Box;
          //  }
          //  else if (TargetObject.Type == UnitType.Carrier)
          //  {
          //      TargetObject.Type = UnitType.Box;
          //  }
          //  else if (TargetObject.Type == UnitType.CarrierOnTarget)
          //  {
          //      TargetObject.Type = UnitType.BoxOnTarget;
          //  }
          //  else
          //  {
          //      new Exception("Abnoraml logic Flow");
          //  }


          //  //delete old carrier 
          //  if (CarrierObject.Type == UnitType.Carrier)
          //  {
          //      CarrierObject.Type = UnitType.Floor;
          //  }
          //  else if (CarrierObject.Type == UnitType.CarrierOnTarget)
          //  {
          //      CarrierObject.Type = UnitType.Target;
          //  }
          //  else
          //  {
          //      new Exception("Abnoraml logic Flow");
          //  }


          //  if (BoxObject.Type == UnitType.BoxOnTarget)
          //  {
          //      BoxObject.Type = UnitType.CarrierOnTarget;
          //  }
          //  else if (BoxObject.Type == UnitType.Box)
          //  {
          //      BoxObject.Type = UnitType.Carrier;
          //  }
          //  else
          //  {
          //      new Exception("Abnoraml logic Flow");
          //  }


          //  Solution AppliedSolution = new Solution();
          //  AppliedSolution.DerivedObjects = DerivedObjects;
          //  AppliedSolution.SolutionPath = new Path(RootPath);
          ////  AppliedSolution.SolutionPath.Directions.AddRange(ApplyPath.Directions);
          //  return AppliedSolution;
        }


        protected void OnSolved(object unInformedSolver, SolutionInfoEventArgs SolutionInformation)
        {
            if (Solved != null)
            {
                Solved(unInformedSolver, SolutionInformation);
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
                return _Object.Position;
            }

        }
    }
}
