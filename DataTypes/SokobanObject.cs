using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class SokobanObject : ICloneable
    {
        [JsonConstructor]
        public SokobanObject(PositionIndex _Position)
        {
            Position = new PositionIndex(_Position);
            TrackPath = new Path();
        }

        public SokobanObject()
        {

            Position = new PositionIndex();
            TrackPath = new Path();
        }

        public UnitType Type { get; set; }
        public PositionIndex Position { get; set; }
        public Path TrackPath;//used in path tracking for both "Heuristics" and "Direct path" solvers
        public UnitType TrackType;//A virtual type used in path tracking for "Direct path Solver"
        public Path SolutionPath;//used  to track solution

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
