using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.DataTypes
{
    public class Settings
    {
        static int _speed;
        static int _ZoomFactor;
        static string _StartupLevel;
        static int _directPathMaxTargets;
        public static int Speed
        {
            get
            {
                return _speed;
            }
            set
            {

                _speed = value;
            }
        }
        public static string StartupLevel
        {
            get
            {
                return _StartupLevel;
            }
            set
            {

                _StartupLevel = value;
            }
        }

        public static int ZoomFactor
        {
            get
            {
                return _ZoomFactor;
            }
            set
            {

                _ZoomFactor = value;
            }
        }
        public static int DirectPathMaxTargets
        {
            get
            {
                return _directPathMaxTargets;
            }
            set
            {

                _directPathMaxTargets = value;
            }
        }
    }
}
