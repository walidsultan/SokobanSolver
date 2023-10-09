using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class PositionIndex : IComparable, ICloneable
    {
        public PositionIndex(int _xIndex, int _yIndex)
        {
            xIndex = _xIndex;
            yIndex = _yIndex;
        }
        [JsonConstructor]
        public PositionIndex(PositionIndex _Index)
        {
            xIndex = _Index.xIndex;
            yIndex = _Index.yIndex;
        }
        public PositionIndex()
        {
            xIndex = 0;
            yIndex = 0;
        }
        public int xIndex { get; set; }
        public int yIndex { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is PositionIndex)
            {
                PositionIndex otherPositionIndex = (PositionIndex)obj;
                if (this.xIndex.CompareTo(otherPositionIndex.xIndex) == 0 && this.yIndex.CompareTo(otherPositionIndex.yIndex) == 0)
                {
                    return 1;
                }
                else
                    return 0;
            }
            else
            {
                throw new ArgumentException("Object is not a PositionIndex");
            }
        }

        #endregion


        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
