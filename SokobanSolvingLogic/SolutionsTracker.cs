using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;

namespace Sokoban.SokobanSolvingLogic
{
    public class SolutionsTracker
    {
         List<Solution> UnRepeatedSolutions = new List<Solution>();
        public  bool IsSolutionRepeated(Solution NewSolution)
        {
            
            foreach (Solution _Solution in UnRepeatedSolutions)
            {
                List<SokobanObject> RelevantObjects = _Solution.DerivedObjects.FindAll ( delegate(SokobanObject obj) { return (obj.Type == UnitType.Box || obj.Type == UnitType.BoxOnTarget ); });
                foreach (SokobanObject _Object in RelevantObjects)
                {
                    SokobanObject CompareObject = NewSolution.DerivedObjects.Find(delegate(SokobanObject obj) { return (obj.Type != _Object.Type && obj.Position.CompareTo(_Object.Position) == 1); });
                    if (CompareObject != null)
                    {
                        goto TryAnotherSolution;
                    }
                }
                if(! IsCarrierPositionRelevant(NewSolution, _Solution) ) return true;//solution is repeated
            TryAnotherSolution: ;
            }
            UnRepeatedSolutions.Insert(0, NewSolution); //most important repeated solutions are at the beginning of the list
                                                                                                        //performance jumped 50% faster after i used insert instead of add

           

            PerformanceDetails.UnRepeatedSolutions = UnRepeatedSolutions.Count;
            

            return false;
        }

        private static bool IsCarrierPositionRelevant(Solution NewSolution,Solution CompareSolution)
        {
            SokobanObject NewCarrier = NewSolution.DerivedObjects.Find(delegate(SokobanObject obj) { return (obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget); });
            SokobanObject CompareCarrier = CompareSolution.DerivedObjects.Find(delegate(SokobanObject obj) { return (obj.Type == UnitType.Carrier || obj.Type == UnitType.CarrierOnTarget); });

            if (NewCarrier.Position.CompareTo(CompareCarrier.Position) == 1) return false;
       
            if (CarrierPathTracker.CheckForValidPath(CompareSolution.DerivedObjects, NewCarrier.Position).valid)
            return false ;
            else
            return true ;

        }

        public  void InitializeSolutions()
        {
            UnRepeatedSolutions.Clear();
        }
    }
}
