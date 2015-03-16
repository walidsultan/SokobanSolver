using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using Sokoban.DataTypes;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
namespace SokobnaSolverEngine
{
   public  class XMLHandler
    {
      
       public static string   LoadLevel(Form frmInstance, string LevelPath)
       {
           try
           {
               UnitBuilder.CalculateObjectSize(frmInstance, GetLevelSize(LevelPath), Settings.ZoomFactor);
               XmlTextReader _XmlReader = new XmlTextReader(LevelPath);
               bool StartReading = false;
               string LevelName=string.Empty ;
               int yIndex = 0;
               ClearAllObjects(frmInstance);
               while (_XmlReader.Read())
               {
                   switch (_XmlReader.NodeType)
                   {
                       case XmlNodeType.Element: // The node is an element.
                           if (_XmlReader.Name == "L")
                               StartReading = true;
                           else if (_XmlReader.Name == "Level")
                           {
                               LevelName = _XmlReader.GetAttribute(0);
                           }
                           break;
                       case XmlNodeType.Text: //Display the text in each element.
                           if (StartReading)
                           {
                               DrawLine(frmInstance, yIndex, _XmlReader.Value );
                               yIndex++;
                           }
                         
                           break;
                       case XmlNodeType.EndElement: //Display the end of the element.
                           if (_XmlReader.Name == "L") StartReading = false ;
                           break;
                   }
               }
               return LevelName;

           }

           catch (Exception ex)
           {
   
               return ex.Message;
           }

       
       }


       public static void WriteSetting(string key, string value)
       {
           // load config document for current assembly
           XmlDocument doc = loadConfigDocument();

           // retrieve appSettings node
           XmlNode node = doc.SelectSingleNode("//appSettings");

           if (node == null)
               throw new InvalidOperationException("appSettings section not found in config file.");

           try
           {
               // select the 'add' element that contains the key
               XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

               if (elem != null)
               {
                   // add value for key
                   elem.SetAttribute("value", value);
               }
               else
               {
                   // key was not found so create the 'add' element 
                   // and set it's key/value attributes 
                   elem = doc.CreateElement("add");
                   elem.SetAttribute("key", key);
                   elem.SetAttribute("value", value);
                   node.AppendChild(elem);
               }
               doc.Save(getConfigFilePath());
           }
           catch
           {
               throw;
           }
       }


       private static XmlDocument loadConfigDocument()
       {
           XmlDocument doc = null;
           try
           {
               doc = new XmlDocument();
               doc.Load(getConfigFilePath());
               return doc;
           }
           catch (System.IO.FileNotFoundException e)
           {
               throw new Exception("No configuration file found.", e);
           }
       }

       private static string getConfigFilePath()
       {
           return Assembly.GetExecutingAssembly().Location + ".config";
       }

       private static LevelSize GetLevelSize(string LevelPath)
       {
           LevelSize CurrentLevel = new LevelSize();
           try
           {
               XmlTextReader _XmlReader = new XmlTextReader(LevelPath);
               bool StartReading = false;
               while (_XmlReader.Read())
               {
                   switch (_XmlReader.NodeType)
                   {
                       case XmlNodeType.Element: // The node is an element.
                           if (_XmlReader.Name == "L") StartReading = true;
                           break;
                       case XmlNodeType.Text: //Display the text in each element.
                           if (StartReading)
                           {
                               CurrentLevel.Height++;
                               if (CurrentLevel.Width < _XmlReader.Value.Length) CurrentLevel.Width = _XmlReader.Value.Length;
                           }
                           break;
                       case XmlNodeType.EndElement: //Display the end of the element.
                           if (_XmlReader.Name == "L") StartReading = false;
                           break;
                   }
               }

               return CurrentLevel;
           }

           catch (Exception ex)
           {

               throw ex;
           }
    
       }


       public  static LevelSize GetLevelSize(Form frmInstance)
       {
           LevelSize CurrentLevel = new LevelSize();
           int LeftPostition=int.MaxValue ;
           int RightPostition=0;
           int UpPostition=int.MaxValue;
           int DownPostition=0 ;
           foreach (Control ctrl in frmInstance.Controls)
           {
               if (ctrl.Left < LeftPostition) LeftPostition = ctrl.Left;
               if (ctrl.Left >RightPostition ) LeftPostition = ctrl.Left;
               if (ctrl.Top < UpPostition) UpPostition = ctrl.Top ;
               if (ctrl.Top > DownPostition) DownPostition = ctrl.Top;
           }

           CurrentLevel.Width = (RightPostition - LeftPostition) / UnitBuilder.objectSize;
           CurrentLevel.Height = (DownPostition - UpPostition) / UnitBuilder.objectSize;
           return CurrentLevel;

       }

       private static void  DrawLine(Form frmInstance,int yIndex ,string Line)
       {
           int xIndex=0;
           int ObjectSize=UnitBuilder.objectSize ;
           bool StartDrawing = false;   //Start drawing any object after drawing a wall object
           foreach (char ch in Line)
           {
               if (ch.ToString() == "#") StartDrawing = true;
               if (StartDrawing)
               {
                   switch (ch.ToString())
                   {
                       case " ":
                           UnitBuilder.AddObject(frmInstance, UnitType.Floor, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case "#":
                           UnitBuilder.AddObject(frmInstance, UnitType.Wall, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case ".":
                           UnitBuilder.AddObject(frmInstance, UnitType.Target, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case "$":
                           UnitBuilder.AddObject(frmInstance, UnitType.Box, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case "*":
                           UnitBuilder.AddObject(frmInstance, UnitType.BoxOnTarget, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case "@":
                           UnitBuilder.AddObject(frmInstance, UnitType.Carrier, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;
                       case "+":
                           UnitBuilder.AddObject(frmInstance, UnitType.CarrierOnTarget, new Coordinates(xIndex * ObjectSize, yIndex * ObjectSize));
                           break;

                   }
               }
                   xIndex++;

               
           }
       
       }
       public static  void ClearAllObjects(Form frmInstance)
       {
         
           LoopStart: foreach (Control ctrl in frmInstance.Controls)
               {
                   string s = ctrl.GetType().ToString();

                   if (ctrl.GetType() == typeof(PictureBox))
                   {
                       ctrl.Dispose();
                       goto LoopStart;
                   }
               }
           
       }

       public static Coordinates GetCarrierCoordinates(Form frmInstance)
       {
           int xPosition = 0; int yPosition = 0;
           foreach (Control ctrl in frmInstance.Controls)
           {
               
             if (ctrl.Name.Contains(UnitType.CarrierOnTarget.ToString()))
               {
                   int topIndex = ctrl.Name.IndexOf("Top");
                   int OffSet = UnitType.CarrierOnTarget.ToString().Length;
                   xPosition = int.Parse(ctrl.Name.Substring(OffSet, topIndex - OffSet));
                   yPosition = int.Parse(ctrl.Name.Substring(topIndex+3));
               }else
                 if (ctrl.Name.Contains(UnitType.Carrier.ToString()))
                 {
                     int topIndex = ctrl.Name.IndexOf("Top");
                     int OffSet = UnitType.Carrier.ToString().Length;
                     xPosition = int.Parse(ctrl.Name.Substring(OffSet, topIndex - OffSet));
                     yPosition = int.Parse(ctrl.Name.Substring(topIndex + 3));
                 }
           }

           if (xPosition == 0 || yPosition == 0)
               throw new Exception("Level has no player, please select file with prober fromat");
           else
               return new Coordinates(xPosition, yPosition);
       }
    }
}
