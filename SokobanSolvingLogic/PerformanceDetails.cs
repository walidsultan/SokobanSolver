using System;
using System.Collections.Generic;

using System.Text;

namespace Sokoban.SokobanSolvingLogic
{
     public  class PerformanceDetails
    {
          static  int _recursiveSolutions;
         static  int _unRepeatedSolutions;
         static int _StuckSolutions;
         static int _Pushes;
         static bool _IsDirectPath;
         static float  _PathTime;
         static int _PossibleSolutions;
         static int _CurrentSolution;

         public static void Intitialize()
         {
             _recursiveSolutions = 0;
             _StuckSolutions = 0;
             _unRepeatedSolutions = 0;
             _Pushes = 0;
             _PossibleSolutions = 0;
             _CurrentSolution = 0;
         }

         public static int RecursiveSolutions
         {
             get
             {
                 return _recursiveSolutions;
             }
             set
             {

                 _recursiveSolutions = value;
             }
         }
         public static int PossibleSolutions
         {
             get
             {
                 return _PossibleSolutions;
             }
             set
             {

                 _PossibleSolutions = value;
             }
         }
         public static int CurrentSolution
         {
             get
             {
                 return _CurrentSolution;
             }
             set
             {

                 _CurrentSolution = value;
             }
         }

         public static int UnRepeatedSolutions
         {
             get
             {
                 return _unRepeatedSolutions;
             }
             set
             {

                 _unRepeatedSolutions = value;
             }
         }

         public static int Pushes
         {
             get
             {
                 return _Pushes;
             }
             set
             {

                 _Pushes = value;
             }
         }

         public static int StuckSolutions
         {
             get
             {
                 return _StuckSolutions;
             }
             set
             {

                 _StuckSolutions = value;
             }
         }

         public static float PathTime
         {
             get
             {
                 return _PathTime;
             }
             set
             {

                 _PathTime = value;
             }
         }
         public static bool  IsDirectPath
         {
             get
             {
                 return _IsDirectPath;
             }
             set
             {

                 _IsDirectPath = value;
             }
         }



    }
}
