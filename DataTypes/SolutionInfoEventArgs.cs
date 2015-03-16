using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
namespace Sokoban.SokobanSolvingLogic
{
    public class SolutionInfoEventArgs : EventArgs
    {

        public SolutionInfoEventArgs(Path _SolutionPath)
            {
                this.SolutionPath = new Path(_SolutionPath);
            }
            public readonly Path  SolutionPath;
            
            
        
    }
}
