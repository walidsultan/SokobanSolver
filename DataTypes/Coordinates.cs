using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class Coordinates
    {
        public Coordinates(int _X,int _Y)
        {
            x = _X;
            y = _Y;
        }
        public Coordinates()
        {
            
        }
        public int x;
        public int y;
    }
}
