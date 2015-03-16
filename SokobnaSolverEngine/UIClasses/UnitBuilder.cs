using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;
using System.Drawing;
namespace SokobnaSolverEngine
{
    public  class UnitBuilder
    {
        static int _ObjectSize;
        static int _WidthOffSet;
        static int _HeightOffSet;
        public static int objectSize
        {
            get
            {
                return _ObjectSize;
            }
      

        }
        public static int WidthOffSet
        {
            get
            {
                return _WidthOffSet;
            }

        }
        public static int HeightOffSet
        {
            get
            {
                return _HeightOffSet;
            }

        }
       static  PositionIndex StatrtupPosition, TargetPosition;//used to deliver box automatically 

         static  Image imgCarrier, imgBox, imgBoxOnTarget, imgCarrierOnTarget, imgFloor, imgTarget, imgWall;
        public static  void LoadImages()
        {
             imgCarrier = Bitmap.FromFile (Application.StartupPath+ "\\images\\Carrier.JPG");
             imgBox =  Bitmap.FromFile(Application.StartupPath + "\\images\\Box.JPG");
             imgBoxOnTarget =  Bitmap.FromFile(Application.StartupPath + "\\images\\BoxOnTarget.JPG");
             imgCarrierOnTarget =  Bitmap.FromFile(Application.StartupPath + "\\images\\CarrierOnTarget.JPG");
             imgTarget =  Bitmap.FromFile(Application.StartupPath + "\\images\\Target.JPG");
             imgWall =  Bitmap.FromFile(Application.StartupPath + "\\images\\Wall.JPG");
             imgFloor =  Bitmap.FromFile(Application.StartupPath + "\\images\\Floor.JPG");
            
        }
        private  static PictureBox CreateUnit(UnitType ObjectType, Coordinates _Coordinates)
        {
            PictureBox _picInstance = new PictureBox();
            _picInstance.Left = _Coordinates.x+WidthOffSet ;
            _picInstance.Top = _Coordinates.y+HeightOffSet ;
            _picInstance.Width = objectSize;
            _picInstance.Height = objectSize;
            _picInstance.SizeMode = PictureBoxSizeMode.StretchImage;

            //_picInstance.ImageLocation = Application.StartupPath + "\\images\\"+ ObjectType.ToString()+".jpg";
            switch (ObjectType)
            {
                case UnitType.Box:
                    _picInstance.Image = imgBox;
                    break;
                case UnitType.BoxOnTarget:
                    _picInstance.Image = imgBoxOnTarget;
                    break;
                case UnitType.Floor:
                    _picInstance.Image = imgFloor;
                    break;
                case UnitType.Target:
                    _picInstance.Image = imgTarget;
                    break;
                case UnitType.Wall:
                    _picInstance.Image = imgWall;
                    break;
                case UnitType.Carrier:
                    _picInstance.Image = imgCarrier;
                    break;
                case UnitType.CarrierOnTarget:
                    _picInstance.Image = imgCarrierOnTarget;
                    break;

            }
            _picInstance.Name = ObjectType.ToString() + _Coordinates.x + "Top" + _Coordinates.y;
            _picInstance.Click += new EventHandler(_picInstance_Click);
            return _picInstance;
        }

        static void _picInstance_Click(object sender, EventArgs e)
        {
            PictureBox ClickedObject = (PictureBox)sender;
            UnitType ObjectType = ExportLevelToSolvingLogic.ExtractObjectType(ClickedObject);
            if (ObjectType == UnitType.Box || ObjectType == UnitType.BoxOnTarget)
            {
                Form frmInstance = (Form)ClickedObject.GetContainerControl();
                frmInstance.Cursor = Cursors.PanNorth;
                if (StatrtupPosition != null)
                { StatrtupPosition = null; frmInstance.Cursor = Cursors.Default; }
                else
                {
                    StatrtupPosition = new PositionIndex((ClickedObject.Left - UnitBuilder._WidthOffSet) / UnitBuilder._ObjectSize, (ClickedObject.Top - UnitBuilder.HeightOffSet) / UnitBuilder.objectSize);
                }
            }
            else if (ObjectType == UnitType.Target||ObjectType ==UnitType.Floor ||ObjectType==UnitType.Carrier ||ObjectType==UnitType.CarrierOnTarget )
            {
                if (StatrtupPosition != null)
                {
                    TargetPosition = new PositionIndex((ClickedObject.Left - UnitBuilder._WidthOffSet) / UnitBuilder._ObjectSize, (ClickedObject.Top - UnitBuilder.HeightOffSet) / UnitBuilder.objectSize);
                    Form frmInstance = (Form)ClickedObject.GetContainerControl();
                    frmInstance.Cursor = Cursors.Default ;
                    Path SolutionPath= DeliverBox.GetDeliverPath(ExportLevelToSolvingLogic.GetLevelObjects(frmInstance), StatrtupPosition, TargetPosition,true  ,true       );
                    frmMain.SolutionPath = SolutionPath;
                    StatrtupPosition = null;
                    TargetPosition  = null;
                }
            }

        }

        public static void AddObject(Form frmInstance,UnitType ObjectType,Coordinates ObjectCoordinates)
        {
            //Delete floor first before adding in any other object
            if (!(ObjectType == UnitType.Floor || ObjectType == UnitType.Wall ))
            {
                UnitDisposer.DisposeControl(frmInstance, UnitType.Floor,ObjectCoordinates);
            }

            frmInstance.Controls.Add(CreateUnit(ObjectType,ObjectCoordinates));

        }

        public static void CalculateObjectSize(Form frmInstance, LevelSize CurrentLevelSize,int ZoomFactor)
        {
            
            _ObjectSize = frmInstance.Height / (CurrentLevelSize.Height + ZoomFactor);
            _WidthOffSet = (frmInstance.Width - CurrentLevelSize.Width * _ObjectSize) / 2;
            _HeightOffSet = (frmInstance.Height - (CurrentLevelSize.Height+1)* _ObjectSize) / 2;
            if (_ObjectSize * (CurrentLevelSize.Width + ZoomFactor) > frmInstance.Width)
            {
                _ObjectSize = frmInstance.Width / (CurrentLevelSize.Width + ZoomFactor);
                _WidthOffSet = (frmInstance.Width - CurrentLevelSize.Width * _ObjectSize) / 2;
                _HeightOffSet = (frmInstance.Height - (CurrentLevelSize.Height) * _ObjectSize) / 2;
            }
            
        }

    }
}
