using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
namespace Sokoban.SokobanSolvingLogic
{
    public class HeuristicsSolver
    {

        public delegate void SolvedHandler(object unInformedSolver, SolutionInfoEventArgs SolutionInformation);

        public event SolvedHandler Solved;

        private static bool _requestStop = false;

        public Path Solve(List<SokobanObject> levelObjects,System.Threading.CancellationToken cancellationToken)
        {
            List<Solution> PossibleSolutions = new List<Solution>();
            List<Path> PossiblePaths = CarrierPathTracker.GetPossiblePaths(levelObjects);
            SolutionsTracker AssociatedSolutionsTracker = new SolutionsTracker();

            //Initialize Solution Tracker
            AssociatedSolutionsTracker.InitializeSolutions();

            PerformanceDetails.Intitialize();

            foreach (Path possiblePath in PossiblePaths)
            {
                Solution PossibleSolution = ApplySolution(possiblePath, levelObjects, new Path());
                if (SolutionStatus.IsLevelSolved(PossibleSolution.DerivedObjects))
                {
                    //Level Solved 
                    PossibleSolution.SolutionPath.valid = true;
                    return PossibleSolution.SolutionPath;
                }
                if (!SolutionStatus.IsSolutionStuck(PossibleSolution) && !AssociatedSolutionsTracker.IsSolutionRepeated(PossibleSolution))
                {
                    PossibleSolutions.Add(PossibleSolution);
                }

            }

            PerformanceDetails.Pushes++;

            List<Solution> RecursiveSolutions = new List<Solution>();
            while (!cancellationToken.IsCancellationRequested)
            {
                PerformanceDetails.Pushes++;
                foreach (Solution PossibleSolution in PossibleSolutions)
                {
                    PerformanceDetails.CurrentSolution = PossibleSolutions.IndexOf(PossibleSolution);
                    if (cancellationToken.IsCancellationRequested) break;
                    PossiblePaths = CarrierPathTracker.GetPossiblePaths(PossibleSolution.DerivedObjects);
                    foreach (Path possiblePath in PossiblePaths)
                    {
                        Solution _PossibleSolution = ApplySolution(possiblePath, PossibleSolution.DerivedObjects, PossibleSolution.SolutionPath);
                        if (SolutionStatus.IsLevelSolved(_PossibleSolution.DerivedObjects))
                        {
                            //Level Solved 
                            _PossibleSolution.SolutionPath.valid = true;
                            return _PossibleSolution.SolutionPath;
                        }

                        if (!SolutionStatus.IsSolutionStuck(_PossibleSolution))
                        {
                            if (!AssociatedSolutionsTracker.IsSolutionRepeated(_PossibleSolution))
                            {
                                RecursiveSolutions.Add(_PossibleSolution);
                                PerformanceDetails.RecursiveSolutions = RecursiveSolutions.Count;
                            }
                        }
                        else
                        {
                            PerformanceDetails.StuckSolutions++;
                        }
                    }
                }
                PossibleSolutions.Clear();
                PossibleSolutions.AddRange(RecursiveSolutions);
                PerformanceDetails.PossibleSolutions = PossibleSolutions.Count;
                if (RecursiveSolutions.Count == 0)
                {
                    //Level failed ;
                    return new Path() { valid = false };
                }
                RecursiveSolutions.Clear();

            }

              return new Path() { valid = false };
        }

        protected void OnSolved(object unInformedSolver, SolutionInfoEventArgs SolutionInformation)
        {
            if (Solved != null)
            {
                Solved(unInformedSolver, SolutionInformation);
            }
        }

        public static Solution ApplySolution(Path ApplyPath, List<SokobanObject> AllObjects, Path RootPath)
        {
            List<SokobanObject> DerivedObjects = new List<SokobanObject>();
            foreach (SokobanObject _NativeObject in AllObjects)
            {
                DerivedObjects.Add((SokobanObject)_NativeObject.Clone());
            }

            SokobanObject CarrierObject = DerivedObjects.Find(delegate (SokobanObject obj) { return (obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget); });
            //Move Carrier to destination
            PositionIndex TargetPosition = new PositionIndex(CarrierObject.Position);
            foreach (Direction _direction in ApplyPath.Directions)
            {
                switch (_direction)
                {
                    case Direction.Left:
                        TargetPosition.xIndex -= 1;
                        break;
                    case Direction.Down:
                        TargetPosition.yIndex += 1;
                        break;
                    case Direction.Up:
                        TargetPosition.yIndex -= 1;
                        break;
                    case Direction.Right:
                        TargetPosition.xIndex += 1;
                        break;
                }
            }




            SokobanObject BoxObject = CarrierPathTracker.GetObjectByPositionIndex(DerivedObjects, TargetPosition);
            if (BoxObject.Type != UnitType.Box && BoxObject.Type != UnitType.BoxOnTarget) return new Solution(); //this line for test stuck procedure
            PositionIndex PushIndex = new PositionIndex(TargetPosition);
            //push box
            switch (ApplyPath.Directions.FindLast(delegate (Direction direction) { return true; }))
            {
                case Direction.Up:
                    PushIndex.yIndex--;
                    break;
                case Direction.Down:
                    PushIndex.yIndex++;
                    break;
                case Direction.Left:
                    PushIndex.xIndex--;
                    break;
                case Direction.Right:
                    PushIndex.xIndex++;
                    break;

            }


            SokobanObject TargetObject = CarrierPathTracker.GetObjectByPositionIndex(DerivedObjects, PushIndex);
            if (TargetObject.Type == UnitType.Target)
            {
                TargetObject.Type = UnitType.BoxOnTarget;

            }
            else if (TargetObject.Type == UnitType.Floor)
            {
                TargetObject.Type = UnitType.Box;
            }
            else if (TargetObject.Type == UnitType.Carrier)
            {
                TargetObject.Type = UnitType.Box;
            }
            else if (TargetObject.Type == UnitType.CarrierOnTarget)
            {
                TargetObject.Type = UnitType.BoxOnTarget;
            }
            else
            {
                new Exception("Abnoraml logic Flow");
            }


            //delete old carrier 
            if (CarrierObject.Type == UnitType.Carrier)
            {
                CarrierObject.Type = UnitType.Floor;
            }
            else if (CarrierObject.Type == UnitType.CarrierOnTarget)
            {
                CarrierObject.Type = UnitType.Target;
            }
            else
            {
                new Exception("Abnoraml logic Flow");
            }


            if (BoxObject.Type == UnitType.BoxOnTarget)
            {
                BoxObject.Type = UnitType.CarrierOnTarget;
            }
            else if (BoxObject.Type == UnitType.Box)
            {
                BoxObject.Type = UnitType.Carrier;
            }
            else
            {
                new Exception("Abnoraml logic Flow");
            }


            Solution AppliedSolution = new Solution();
            AppliedSolution.DerivedObjects = DerivedObjects;
            AppliedSolution.SolutionPath = new Path(RootPath);
            AppliedSolution.SolutionPath.Directions.AddRange(ApplyPath.Directions);
            return AppliedSolution;
        }

        public static void RequestStop(bool value)
        {
            _requestStop = value;
        }


    }
}
