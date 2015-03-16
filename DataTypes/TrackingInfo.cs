using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class TrackingInfo
    {
        public TrackingInfo()
        {
            TrackPath = new Path();
            BoxPosition = new PositionIndex();
            TargetPosition = new PositionIndex();
        }
        public Path  TrackPath;//A virtual Path used in path tracking for "Direct path Solver"
        public PositionIndex BoxPosition;//A virtual position used  in box tracking for "Direct path solver"
        public PositionIndex TargetPosition;//A virtual position used  in box tracking for "Direct path solver"
    }
}
