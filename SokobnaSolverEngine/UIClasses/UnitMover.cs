using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using Sokoban.DataTypes;

namespace SokobnaSolverEngine
{
    public class UnitMover
    {

        private static Coordinates CalculateTargetCoordinates(Coordinates currentCoordinates, Direction _Direction)
        {
            switch (_Direction)
            { 
                case Direction.Up :
                    return new Coordinates(currentCoordinates.x, currentCoordinates.y - UnitBuilder.objectSize);
                    
            case Direction.Down :
                    return new Coordinates(currentCoordinates .x,currentCoordinates.y+UnitBuilder.objectSize );
                    
                    case Direction.Left :
                    return new Coordinates(currentCoordinates .x-UnitBuilder.objectSize,currentCoordinates.y );
                    
                    case Direction.Right:
                    return new Coordinates(currentCoordinates .x+UnitBuilder.objectSize,currentCoordinates.y );
                    
                default :
                    throw new NotSupportedException();

            }
        }

        public  static Direction  GetoppositeDirection(Direction _Direction)
        {
            switch (_Direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new NotImplementedException();
            
            }
        }
        private  static bool  PushBox(Form  frmInstance,Coordinates CurrentCoordinates,UnitType BoxType ,Direction PushDirection)
        {

            string MovingBox = BoxType.ToString() + CurrentCoordinates.x + "Top" + CurrentCoordinates.y;
            foreach (Control ctrl in frmInstance.Controls)
            {
                if (ctrl.Name == MovingBox)//delete the box and drawcarrier
                {

                    Coordinates TargetCoordinates = CalculateTargetCoordinates(CurrentCoordinates, PushDirection);
                    //if the target is object "BoxTarget" then draw box over the target else draw an ordinary box
                    if (UnitDisposer.DisposeControl(frmInstance, UnitType.Floor, TargetCoordinates))
                    {
                        UnitBuilder.AddObject(frmInstance, UnitType.Box, TargetCoordinates);
                        ctrl.Dispose();
                        if (BoxType == UnitType.Box)
                          UnitBuilder.AddObject(frmInstance, UnitType.Carrier, CurrentCoordinates);
                        else if (BoxType == UnitType.BoxOnTarget)
                            UnitBuilder.AddObject(frmInstance, UnitType.CarrierOnTarget, CurrentCoordinates);
                        //delete the old carrier
                        Coordinates OppositeCoordinates = CalculateTargetCoordinates(CurrentCoordinates, GetoppositeDirection(PushDirection));
                        SwapOldCarrier(frmInstance,OppositeCoordinates );
                        return true;
                    }
                    else if (UnitDisposer.DisposeControl(frmInstance, UnitType.Target, TargetCoordinates))
                    {
                        UnitBuilder.AddObject(frmInstance, UnitType.BoxOnTarget, TargetCoordinates);
                        ctrl.Dispose();
                        if (BoxType == UnitType.Box)
                            UnitBuilder.AddObject(frmInstance, UnitType.Carrier, CurrentCoordinates);
                        else if (BoxType == UnitType.BoxOnTarget)
                            UnitBuilder.AddObject(frmInstance, UnitType.CarrierOnTarget, CurrentCoordinates);
                        //delete the old carrier
                        Coordinates OppositeCoordinates = CalculateTargetCoordinates(CurrentCoordinates, GetoppositeDirection(PushDirection));
                        SwapOldCarrier(frmInstance, OppositeCoordinates);
                        return true;
                    }
                 
                }
            }

            return false;

        }

        private   static UnitType GetObjectType(Form frmInstance, Coordinates _Coordinates)
        {
            Dictionary <string,UnitType  > ObjectsList = new Dictionary <string,UnitType>();
            ObjectsList.Add(UnitType.Box.ToString() + _Coordinates.x + "Top" + _Coordinates.y,UnitType.Box);
            ObjectsList.Add(UnitType.Target.ToString() + _Coordinates.x + "Top" + _Coordinates.y,UnitType.Target);
            ObjectsList.Add(UnitType.Floor.ToString() + _Coordinates.x + "Top" + _Coordinates.y,UnitType.Floor);
            ObjectsList.Add( UnitType.Wall.ToString() + _Coordinates.x + "Top" + _Coordinates.y,UnitType.Wall );
            ObjectsList.Add(UnitType.BoxOnTarget.ToString() + _Coordinates.x + "Top" + _Coordinates.y,UnitType.BoxOnTarget );

            foreach (Control ctrl in frmInstance.Controls)
            {
                foreach (KeyValuePair<string,UnitType> de in ObjectsList)
                {
                    if (de.Key == ctrl.Name)
                    {
                        return de.Value; 
                    }
                }
            
            }

            return UnitType.NotSupported;


        }

        private static void SwapOldCarrier (Form frmInstance,Coordinates _Coordinates)
        {
            

            if (UnitDisposer.DisposeControl(frmInstance, UnitType.Carrier, _Coordinates))
            {
                UnitBuilder.AddObject(frmInstance, UnitType.Floor, _Coordinates);

            }
            else if (UnitDisposer.DisposeControl(frmInstance, UnitType.CarrierOnTarget, _Coordinates))
            {
                UnitBuilder.AddObject(frmInstance, UnitType.Target, _Coordinates);
            }
        }


        public static bool  MoveCarrier(Form frmInstance, Coordinates CurrentCoordinates, Direction MoveDirection)
        {
            Coordinates TargetCoordinates = CalculateTargetCoordinates(CurrentCoordinates, MoveDirection);
            Coordinates OppositeCoordinates = CalculateTargetCoordinates(CurrentCoordinates , GetoppositeDirection(MoveDirection));
            UnitType TargetObjectType = GetObjectType(frmInstance, TargetCoordinates);      

            
            switch (TargetObjectType)
            { 
                case UnitType.Box :
                    return PushBox(frmInstance, TargetCoordinates,UnitType.Box , MoveDirection);
                case UnitType .BoxOnTarget :
                    return PushBox(frmInstance, TargetCoordinates, UnitType.BoxOnTarget, MoveDirection);
                case UnitType.Floor :
                    UnitDisposer.DisposeControl(frmInstance,UnitType.Floor,TargetCoordinates);
                    UnitBuilder.AddObject(frmInstance,UnitType.Carrier ,TargetCoordinates);
                    SwapOldCarrier(frmInstance, CurrentCoordinates );
                    return true;
                case UnitType.Target :
                    UnitDisposer.DisposeControl(frmInstance, UnitType.Target, TargetCoordinates);
                    UnitBuilder.AddObject(frmInstance, UnitType.CarrierOnTarget, TargetCoordinates);
                    SwapOldCarrier(frmInstance, CurrentCoordinates);
                    return true;
                case UnitType.Wall :
                    return false ;

                default:
                    return false;
            }
            
        }

    }
}
