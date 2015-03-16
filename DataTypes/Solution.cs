using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class Solution
    {
        public Solution(List<SokobanObject > _DerivedObjects )
        {
            DerivedObjects=  _DerivedObjects;
        }
        public Solution()
        {

            SolutionHistory = new List<TrackingInfo>();

        }
        public List<TrackingInfo> SolutionHistory; //used with "Direct path" solver
        public Path SolutionPath; //used with "Heuristics" solver
        public List<SokobanObject> DerivedObjects; 
        
    }
}
