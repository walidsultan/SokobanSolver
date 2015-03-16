using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using Sokoban.DataTypes;
namespace SokobnaSolverEngine
{
    public class UnitDisposer
    {
        private delegate bool DisposeControlDelegate(Form frmInstance, UnitType ControlType, Coordinates ControlCoordinates);

        public static bool DisposeControl(Form frmInstance, UnitType ControlType, Coordinates ControlCoordinates)
        {
            string ControlToBeDisposed = ControlType.ToString() + ControlCoordinates.x + "Top" + ControlCoordinates.y;
            foreach (Control ctrl in frmInstance.Controls)
            {
                if (ctrl.Name == ControlToBeDisposed)
                {
                  
                    ctrl.Dispose();
                    return true;
                }

            }
            return false;
        }

        

    }
}
