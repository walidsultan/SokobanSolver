using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class Path
    {
        public Path()
        {
            Directions = new List<Direction>();
        }
        public Path(Path _Path)
        {
            Directions =new List<Direction>();
            Directions.AddRange(_Path.Directions); //to clone the source the source path instead of both having the same refrence
                                                                                             //because list are refrence type.       
        }
        public List<Direction> Directions;
        public bool valid;
    }
}
