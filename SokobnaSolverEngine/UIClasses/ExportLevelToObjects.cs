using System;
using System.Collections.Generic;

using System.Text;
using Sokoban.DataTypes;
using System.Windows.Forms;
namespace SokobnaSolverEngine
{
    public class ExportLevelToSolvingLogic
    {

        

        public  static UnitType  ExtractObjectType(Control  _pictureBox)
        {
            
            if (_pictureBox.Name.Contains(UnitType.CarrierOnTarget.ToString()))
            {
                return UnitType.CarrierOnTarget;
            }
            else if (_pictureBox.Name.Contains(UnitType.BoxOnTarget.ToString()))
            {
                return UnitType.BoxOnTarget;
            }
            else if (_pictureBox.Name.Contains(UnitType.Box.ToString()))
            {
                return UnitType.Box;
            }
            else if (_pictureBox.Name.Contains(UnitType.Carrier.ToString()))
            {
                return UnitType.Carrier;
            }
            else if (_pictureBox.Name.Contains(UnitType.Floor.ToString()))
            {
                return UnitType.Floor;
            }
            else if (_pictureBox.Name.Contains(UnitType.Target.ToString()))
            {
                return UnitType.Target;
            }
            else if (_pictureBox.Name.Contains(UnitType.Wall.ToString()))
            {
                return UnitType.Wall;
            }
            else
            {
                return UnitType.NotSupported;
            }

            
        }

        public static List<SokobanObject>   GetLevelObjects(Form frmInstance)
        {
            List<SokobanObject> AllObjects = new List<SokobanObject>();
             foreach (Control ctrl in frmInstance.Controls)
            {
                SokobanObject _Object = new SokobanObject();
                if (ctrl.GetType() == typeof(PictureBox))
                {
                    _Object.Type = ExtractObjectType(ctrl);
                    int offSet=_Object.Type.ToString().Length  ;
                    int TopIndex=ctrl.Name.IndexOf("Top");
                    _Object.Position.xIndex  = int.Parse( ctrl.Name.Substring(offSet, TopIndex - offSet))/UnitBuilder.objectSize ;
                    _Object.Position.yIndex  = int.Parse(ctrl.Name.Substring(TopIndex + 3)) / UnitBuilder.objectSize;
                    AllObjects.Add(_Object);
                }
               
            }
             return AllObjects;
        }
    }
}
