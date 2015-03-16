using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;
using System.Threading;
using System.Configuration;
namespace SokobnaSolverEngine
{
    public partial class frmMain:Form 
    {
        Coordinates CurrentPosition = new Coordinates();
        static  Path _SolutionPath = new Path();//used for animation
        static int LoopCount = 0;//used for animation
        Path PlayerPath = new Path();//to keep track of player moves
        DateTime StartDate;
        frmProcessing _frmProcessing = new frmProcessing();
       public  VerticalProgressBar ProcessingBar = new VerticalProgressBar();

        public frmMain()
        {
            Mutex theMutex = null;
            try //try to open the mutex
            {
                theMutex = Mutex.OpenExisting("SokokobanMutex");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                //cannot open the mutex because it doesn't exist
            }

            //create it if it doesn't exist
            if (theMutex == null)
            {
                theMutex = new Mutex(false, "SokokobanMutex");

            }

            InitializeComponent();

            this.Show();
         //   testInterface();
          // TestStuckProcedure();
            UnitBuilder.LoadImages();
            pnlStatistics.Left = this.Width - pnlStatistics.Width;
            GenerateMenu();
            LoadSetting();
            lblMsg.Text = XMLHandler.LoadLevel(this, Settings.StartupLevel );
            CurrentPosition = XMLHandler.GetCarrierCoordinates(this);
            this.Opacity = .99;


            ProcessingBar.Visible = false;
            this.Controls.Add(ProcessingBar);

        }

        public  static  Path  SolutionPath
        {
            get
            {
                return (_SolutionPath);
            }
            set
            {

                _SolutionPath = value;
            }
        }

        private void testInterface()
        {
            Coordinates CurrentCoordinates = new Coordinates();
            //draw wall frame
            for (int i = 0; i < 40; i++)
            {
                UnitBuilder.AddObject(this, UnitType.Wall, CurrentCoordinates);
                //AddObject( UnitBuilder.CreateWall(CurrentCoordinates));
                if (i < 10) CurrentCoordinates.x += UnitBuilder.objectSize;
                if (i > 10 && i < 20) CurrentCoordinates.y += UnitBuilder.objectSize;
                if (i > 20 && i < 31) CurrentCoordinates.x -= UnitBuilder.objectSize;
                if (i > 30 && i < 41) CurrentCoordinates.y -= UnitBuilder.objectSize;
            }

            CurrentCoordinates.x = 0;
            //draw Floor
            for (int i = 0; i < 9; i++)
            {
                CurrentCoordinates.x += UnitBuilder.objectSize;
                CurrentCoordinates.y = UnitBuilder.objectSize;
                for (int j = 0; j < 8; j++)
                {
                    UnitBuilder.AddObject(this, UnitType.Floor, CurrentCoordinates);
                    CurrentCoordinates.y += UnitBuilder.objectSize;
                }
            }

            //draw boxes
            CurrentCoordinates.x = UnitBuilder.objectSize * 4;
            CurrentCoordinates.y = UnitBuilder.objectSize * 6;
            UnitBuilder.AddObject(this, UnitType.Box, CurrentCoordinates);


            CurrentCoordinates.x = UnitBuilder.objectSize * 7;
            CurrentCoordinates.y = UnitBuilder.objectSize * 6;
            UnitBuilder.AddObject(this, UnitType.Box, CurrentCoordinates);


            //draw carrier
            CurrentCoordinates.x = UnitBuilder.objectSize * 4;
            CurrentCoordinates.y = UnitBuilder.objectSize * 7;
            UnitBuilder.AddObject(this, UnitType.Carrier, CurrentCoordinates);


            //draw Targets
            CurrentCoordinates.x = UnitBuilder.objectSize * 1;
            CurrentCoordinates.y = UnitBuilder.objectSize * 2;
            UnitBuilder.AddObject(this, UnitType.Target, CurrentCoordinates);


            CurrentCoordinates.x = UnitBuilder.objectSize * 6;
            CurrentCoordinates.y = UnitBuilder.objectSize * 1;
            UnitBuilder.AddObject(this, UnitType.Target, CurrentCoordinates);







            //draw box on target 
            CurrentCoordinates.x = UnitBuilder.objectSize * 3;
            CurrentCoordinates.y = UnitBuilder.objectSize * 6;
            UnitBuilder.AddObject(this, UnitType.BoxOnTarget, CurrentCoordinates);



        }

        public   void LoadSetting()
        {
          Settings.Speed  =int.Parse( ConfigurationSettings.AppSettings["Speed"]);
          Settings.ZoomFactor = int.Parse(ConfigurationSettings.AppSettings["ZoomFactor"]);
          Settings.StartupLevel = ConfigurationSettings.AppSettings["StartupLevel"];
          Settings.DirectPathMaxTargets =int.Parse( ConfigurationSettings.AppSettings["DirectPathMaxTargets"]);
          tAnimate.Interval = Settings.Speed;
        }

        private void LoadLevel_Click(object sender, EventArgs e)
        {
            ofdSelectFile.ShowDialog();
            if (ofdSelectFile.FileName != string.Empty)
            {
                _SolutionPath.Directions.Clear();
                lblMsg.Text = XMLHandler.LoadLevel(this, ofdSelectFile.FileName);
                CurrentPosition = XMLHandler.GetCarrierCoordinates(this);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XMLHandler.ClearAllObjects(this);
        }




      

        private void lblKeyBoardhandler_TextChanged(object sender, EventArgs e)
        {

           

        }

        private void button1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            lblKeyBoardhandler.Text = "";
            lblKeyBoardhandler.Text = e.KeyChar.ToString();
        }


        private void btnDirectSolve_Click(object sender, EventArgs e)
        {
         
                _SolutionPath.Directions.Clear();
       
                StartDate = DateTime.Now;


               Thread Animate = new Thread(AnimateOpacity);
                Animate.Start();
               

                if (!_frmProcessing.Created ) _frmProcessing = new frmProcessing();
                _frmProcessing.Show(this);
               
             
            DirectPathSolver Solver =new DirectPathSolver();
           
             Thread SolvingThread = new Thread(Solver.Solve);

             Solver.Solved += new DirectPathSolver.SolvedHandler(Solver_Solved);
              
                SolvingThread.Start((object)ExportLevelToSolvingLogic.GetLevelObjects(this));
                SolvingThread.Name ="MyThread";
            
        

            this.Focus();


        }


        private void btnSolve_Click(object sender, EventArgs e)
        {
            
            
            if (btnSolve.Text == "Solve")
            {
                ProcessingBar.Visible = true ;
                _SolutionPath.Directions.Clear();
                HeuristicsSolver.RequestStop(false);
                StartDate = DateTime.Now;


               Thread Animate = new Thread(AnimateOpacity);
                Animate.Start();
                //AnimateOpacity();

                if (!_frmProcessing.Created ) _frmProcessing = new frmProcessing();
                _frmProcessing.Show(this);
                _frmProcessing.FormClosing += new FormClosingEventHandler(_frmProcessing_FormClosing);
                 

                button1.Enabled = false;
                
                HeuristicsSolver Solver = new HeuristicsSolver();
                Thread SolvingThread = new Thread(Solver.Solve);
                Solver.Solved += new HeuristicsSolver.SolvedHandler(Solver_Solved);
              
                SolvingThread.Start((object)ExportLevelToSolvingLogic.GetLevelObjects(this));
                SolvingThread.Name ="MyThread";
            }
            else if (btnSolve.Text == "Stop")
            {
                

                btnSolve.Text = "Solve";
                button1.Enabled = true;
                HeuristicsSolver.RequestStop(true  );
                btnSolve.Visible = false ;
            }

            this.Focus();


        }

        void _frmProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessingBar.Visible = false;
        }



        void Solver_Solved(object unInformedSolver, SolutionInfoEventArgs SolutionInformation)
        {

            ProcessingBar.BeginInvoke(new EventHandler(delegate { ProcessingBar.Visible = false; }));
            if (SolutionInformation.SolutionPath.valid == false)
            {
                _frmProcessing.BeginInvoke(new EventHandler(delegate { _frmProcessing.Close(); }));
                MessageBox.Show("Unsolvable level" , "Sokoban Solver");
                this.BeginInvoke(new EventHandler(delegate { this.Opacity = .99; }));
              

                _SolutionPath.valid = true;
            }
            else
            {
                DateTime SolvedDate = DateTime.Now;
                TimeSpan TimeTaken = SolvedDate.Subtract(StartDate);
                _frmProcessing.BeginInvoke(new EventHandler(delegate { _frmProcessing.Close(); }));
                MessageBox.Show( "Level Solved successfully, Time Taken = " + (TimeTaken.Seconds + TimeTaken.Minutes * 60 + Math.Round((decimal)TimeTaken.Milliseconds / 1000, 1)) + " s.", "Sokoban Solver",MessageBoxButtons.OK );
                this.BeginInvoke(new EventHandler(delegate { this.Opacity = .99; }));
                
               
                _SolutionPath = new Path(SolutionInformation.SolutionPath);
                _SolutionPath.valid = SolutionInformation.SolutionPath.valid;
            }
            
       }



        private void tAnimate_Tick(object sender, EventArgs e)
        {
            lblUnrepeatedSolution.Text = PerformanceDetails.UnRepeatedSolutions.ToString();
            lblRecursiveSolution.Text = PerformanceDetails.RecursiveSolutions.ToString();
            lblStuckSolutions.Text = PerformanceDetails.StuckSolutions.ToString();
            lblIsDirectPath.Text = PerformanceDetails.IsDirectPath.ToString();
            lblPathTime.Text  = PerformanceDetails.PathTime.ToString();
            lblPushes.Text = PerformanceDetails.Pushes.ToString();
            lblPossibleSolutions.Text = PerformanceDetails.PossibleSolutions.ToString();
            lblCurrentSolution.Text = PerformanceDetails.CurrentSolution.ToString();
            ProcessingBar.Maximum = PerformanceDetails.PossibleSolutions;
            ProcessingBar.Value = PerformanceDetails.CurrentSolution;
            if (PerformanceDetails.CurrentSolution > 0)
            {
                float  ConvergenceRatio = PerformanceDetails.RecursiveSolutions / PerformanceDetails.CurrentSolution;
                if (ConvergenceRatio > 1.1)
                {
                    ProcessingBar.Color  = Color.Red;
                }
                else if (ConvergenceRatio > .9 && ConvergenceRatio < 1.1)
                {
                    ProcessingBar.Color = Color.Blue;
                }
                else
                {
                    ProcessingBar.Color = Color.Green;
                }
            }
            if (_SolutionPath.valid)
            {
                if (LoopCount == _SolutionPath.Directions.Count)
                {
                    LoopCount = 0;
                    _SolutionPath.valid = false;
                    button1.Enabled = true;
                    btnSolve.Enabled = true;
                    btnSolve.Text = "Solve";
                    btnSolve.Visible = false;
                    return;
                }


                Direction _Direction = _SolutionPath.Directions[LoopCount];

                switch (_Direction)
                {
                    case Direction.Up:
                        if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Up))
                        {

                            CurrentPosition.y -= UnitBuilder.objectSize;

                        }
                        break;

                    case Direction.Right:
                        if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Right))
                        {

                            CurrentPosition.x += UnitBuilder.objectSize;
                        }
                        break;

                    case Direction.Left:
                        if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Left))
                        {

                            CurrentPosition.x -= UnitBuilder.objectSize;
                        }
                        break;

                    case Direction.Down:
                        if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Down))
                        {

                            CurrentPosition.y += UnitBuilder.objectSize;
                        }
                        break;
                }


                LoopCount++;
            }


        }



        private bool Test(string  LevelPath,string Solution)
        {
            XMLHandler.LoadLevel(this, LevelPath);
            List<SokobanObject> AllObjects = ExportLevelToSolvingLogic.GetLevelObjects(this);
            string strPath = Solution;
            Path TrackPath = new Path();
            Solution TestSolution = new Solution(AllObjects);
            TestSolution.SolutionPath = new Path();
            foreach (char ch in strPath)
            {
                switch (ch.ToString().ToLower())
                {
                    case "u":
                        TrackPath.Directions.Add(Direction.Up);
                        break;
                    case "d":
                        TrackPath.Directions.Add(Direction.Down);
                        break;
                    case "l":
                        TrackPath.Directions.Add(Direction.Left);
                        break;
                    case "r":
                        TrackPath.Directions.Add(Direction.Right);
                        break;

                }



                if (HeuristicsSolver.ApplySolution(TrackPath, TestSolution.DerivedObjects, TestSolution.SolutionPath).DerivedObjects != null)
                {
                    TestSolution = HeuristicsSolver.ApplySolution(TrackPath, TestSolution.DerivedObjects, TestSolution.SolutionPath);
                    //  TestSolution.SolutionPath.Directions.AddRange(TrackPath.Directions );
                    TrackPath.Directions.Clear();
                    if (TestSolution.DerivedObjects != null)
                    {
                        if (SolutionStatus.IsSolutionStuck(TestSolution))
                        {
                            throw new Exception("Problem with the stuck procedure, either the level is unsolvable or Stuck Procedure is buggy");
                        }
                    }
                }

            }
            return true;//stuck procedure is ok
        }

        private void  TestStuckProcedure()
        {
            string strPath = "dllldRddrrUULrddlluUrrddrddllUdrruuluullddRRuuluurDDDuurrddLDLUUdrruulLdllddRddrrUUddlluuluurrurrddLdddlluuRlluurrurrddlDuruullulDrrrddldllluuluRdddrrruruulldLruulDlDurrdLurrrddldlUUdrruulLdlUlldRurrdddllUdrddrrUUluuurrddLdlU";
            Test("Test Levels\\Test1.gam", strPath);

            string strPath2 = "RlddRdrruLuUUruulDDDlluuRlddrruruulDllddddLdlluRuuuuRRddLruulldDrrddLdlluRUUrrddLdlUrrRdrruLLLuurrDullddrdrruLuuUruulDDDlluuRlddrruruulD";
            Test("Test Levels\\Test2.gam", strPath2); 
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HeuristicsSolver.RequestStop(true );
        }

        private void GenerateMenu()
        {
            MainMenu menu = new MainMenu();
            MenuItem File = menu.MenuItems.Add("&File");
            File.MenuItems.Add(new MenuItem("&Open",new EventHandler(LoadLevel_Click) ));
            File.MenuItems.Add(new MenuItem("&Reload Level",new EventHandler(Reload_clicked)));
            File.MenuItems.Add(new MenuItem("-"));
            File.MenuItems.Add(new MenuItem("&Exit", new EventHandler(Exit_clicked)));
            MenuItem Solve = menu.MenuItems.Add("&Solver");
            Solve.MenuItems.Add(new MenuItem("&Solve",new EventHandler(btnSolve_Click)));
            Solve.MenuItems.Add(new MenuItem("&DirectSolve", new EventHandler(btnDirectSolve_Click)));
            Solve.MenuItems.Add(new MenuItem("&Trace"));
            MenuItem Tools = menu.MenuItems.Add("&Tools");
            Tools.MenuItems.Add(new MenuItem("&Options", new EventHandler(btnOptions_Click)));
            MenuItem About = menu.MenuItems.Add("&Help");
            About.MenuItems.Add(new MenuItem("&Sokoban Help", new EventHandler(btnHelp_Click)));
            About.MenuItems.Add(new MenuItem("-"));
            About.MenuItems.Add(new MenuItem("&About"));

            this.Menu = menu;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use directions to move and press \"S\" to solve.", "Sokoban Solver");
        }
        private void btnOptions_Click(object sender, EventArgs e)
        {

            frmSetting frmSettingInstance = new frmSetting();

            frmSettingInstance.SettingsChanged += new frmSetting.SettingChangedHandler(frmSettingInstance_SettingsChanged);
            frmSettingInstance.Show(this);
        }

        void frmSettingInstance_SettingsChanged()
        {
            tAnimate.Interval = Settings.Speed;
        }
        private void Exit_clicked(object sender, EventArgs e)
        {
            HeuristicsSolver.RequestStop(true);
            this.Close();
        }
        private void Reload_clicked(object sender, EventArgs e)
        {
            _SolutionPath.Directions.Clear();
            if (ofdSelectFile.FileName != string.Empty)
            {
                lblMsg.Text = XMLHandler.LoadLevel(this, ofdSelectFile.FileName);
            }
            else
            {
                lblMsg.Text = XMLHandler.LoadLevel(this, Settings.StartupLevel );
            }


            CurrentPosition = XMLHandler.GetCarrierCoordinates(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:

                    if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Up))
                    {
                        PlayerPath.Directions.Add(Direction.Up);
                        CurrentPosition.y -= UnitBuilder.objectSize;
                    }

                    break;

                case Keys.Right:


                    if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Right))
                    {
                        PlayerPath.Directions.Add(Direction.Right);
                        CurrentPosition.x += UnitBuilder.objectSize;
                    }

                    break;

                case Keys.Left:

                    if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Left))
                    {
                        PlayerPath.Directions.Add(Direction.Left);
                        CurrentPosition.x -= UnitBuilder.objectSize;
                    }

                    break;

                case Keys.Down:

                    if (UnitMover.MoveCarrier(this, CurrentPosition, Direction.Down))
                    {
                        PlayerPath.Directions.Add(Direction.Down);
                        CurrentPosition.y += UnitBuilder.objectSize;
                    }

                    break;

                case Keys.R:

                    Reload_clicked(this, e);

                    break;

                case Keys.S :
                    btnSolve_Click(this, e);
                    

                    break;
                case Keys.D:
                    btnDirectSolve_Click(this, e);
                        break;
                case Keys.Back:

                    if (PlayerPath.Directions.Count > 0)
                    {
                        Direction BackDirection = UnitMover.GetoppositeDirection(PlayerPath.Directions.FindLast (delegate (Direction direction) {return true ;}));
                        if (UnitMover.MoveCarrier(this, CurrentPosition, BackDirection))
                        {

                            PlayerPath.Directions.RemoveAt(PlayerPath.Directions.Count - 1);

                            //not the best way to undo last move
                            Reload_clicked(this, e);
                            //switch (BackDirection)
                            //{
                            //    case Direction.Down:
                            //        CurrentPosition.y += UnitBuilder.objectSize;
                            //        break;

                            //    case Direction.Up:
                            //        CurrentPosition.y -= UnitBuilder.objectSize;
                            //        break;

                            //    case Direction.Right:
                            //        CurrentPosition.x += UnitBuilder.objectSize;
                            //        break;

                            //    case Direction.Left:
                            //        CurrentPosition.x -= UnitBuilder.objectSize;
                            //        break;
                            //}
                        }
                    }

                    break;
            }

        }

     

        private void AnimateOpacity()
        {
            //Animate Opacity
            for (int LoopCount = 0; LoopCount < 50; LoopCount++)
            {
                this.BeginInvoke(new EventHandler(delegate { this.Opacity -= .005; }));
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
                Application.DoEvents();
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //int OldObjectSize = UnitBuilder.objectSize;
            //UnitBuilder.CalculateObjectSize(this, loadLevelFromXML.GetLevelSize(this), 5);
            //foreach (Control ctrl in this.Controls)
            //{
            //    if (ctrl.GetType() == typeof(PictureBox))
            //    {
            //        ctrl.Width = UnitBuilder.objectSize;
            //        ctrl.Height = UnitBuilder.objectSize;
            //        ctrl.Left += UnitBuilder.objectSize - OldObjectSize;
            //        ctrl.Top  += UnitBuilder.objectSize - OldObjectSize;
            //    }
            //}
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            Thread Animate = new Thread(AnimateOpacity);
            Animate.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XMLHandler.WriteSetting("Speed", "500");
        }

        private void pnlStatistics_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

      
 

      

    }
}
