using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Visualizaton_Project
{
    public partial class Form1 : Form
    {
        #region Data
        int   start=1;
        float STEPX=25, STEPY = 25,X,Y;
        Graphics G;
        Pen p = new Pen(Color.DarkGray);
        SolidBrush brush = new SolidBrush(Color.Green);
        Board board;

        int CellType = 0;//0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        int CellSize;

        double sMin = 0, sMax = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();
            G = panelBoard.CreateGraphics();

            

            CellSize = int.Parse(textBoxCellSize.Text.ToString());

            ColorOperations.s_min = double.Parse(textBoxmini.Text.ToString());
            ColorOperations.s_max = double.Parse(textBoxmaxi.Text.ToString());

            board = new Board(panelBoard.Size,CellSize);



            STEPX = float.Parse(textBoxCellSize.Text.ToString());
            STEPY = STEPX;

            radioButtonSelectcell.Checked = true;
            comboBox1.Enabled = false;
            comboBox1.Hide();

            draw();
            
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {
            
            //=============================================================================

        }

        public void draw()
        {

            #region create board graphics

          //  ColorOperations.s_min = double.Parse(textBoxmini.Text.ToString());
         //   ColorOperations.s_max = double.Parse(textBoxmaxi.Text.ToString());
            double[] C = ColorOperations.ValueToColor(ColorOperations.getValue(0), ColorOperations.s_min, ColorOperations.s_max);
            double[] CC = userControl11.ValueToColor(ColorOperations.getValue(0), ColorOperations.s_min, ColorOperations.s_max);
            

            panelBoard.BackColor = Color.FromArgb(255, (int)(C[0] * 255.0), (int)(C[1] * 255.0), (int)(C[2] * 255.0));
            #endregion


            if (board != null)
            {
                #region draw cells
                for (int i = 0; i < board.Rows; i++)
                {
                    for (int j = 0; j < board.Coloums; j++)
                    {
                        if (board.boardCells[i, j] != null)
                        {
                            X = (j * CellSize) + panelBoard.Location.X;
                            Y = (i * CellSize) + panelBoard.Location.Y;

                            DrawCell.FillRect(G, brush, X, Y, board.boardCells[i, j].size, board.boardCells[i, j].value, board.boardCells[i, j].Type);
                        }
                    }
                }
                #endregion
            }
          

            
        }


        private void buttonupdate_Click(object sender, EventArgs e)
        {
            CellSize = int.Parse(textBoxCellSize.Text.ToString());
            board = new Board(panelBoard.Size, CellSize);
        }

        #region mouse events
        //----------------------------------------------------------------------------------------------
        private void panelBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if(CellType!=-1)
            {
                Point clickPos = new Point(e.X, e.Y);
                ColorOperations.s_min = double.Parse(textBoxmini.Text.ToString());
                ColorOperations.s_max = double.Parse(textBoxmaxi.Text.ToString());

                sMin = double.Parse(textBoxmini.Text.ToString());
                sMax = double.Parse(textBoxmaxi.Text.ToString());

                board.AddNewCell(CellType, clickPos, panelBoard.Location, panelBoard.Size, CellSize);
                draw();
            }
            else
            {
                Point clickPos = new Point(e.X, e.Y);
                Point coordinates = board.calculate_coordinates(clickPos, panelBoard.Location, panelBoard.Size);

                //MessageBox.Show("Cell Type : " + board.boardCells[coordinates.X, coordinates.Y].CellName + "\n");

                ToolTip tt = new ToolTip();
                tt.Show("Cell Type : " + board.boardCells[coordinates.X, coordinates.Y].CellName + "\n cell value " + board.boardCells[coordinates.X, coordinates.Y].value.ToString(), panelBoard, 10000);

            }
           


        } 
        //----------------------------------------------------------------------------------------------
        private void panelBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && CellType!=-1)
            {
                try
                {
                    Point clickPos = new Point(e.X, e.Y);

                    board.AddNewCell(CellType, clickPos, panelBoard.Location, panelBoard.Size, CellSize);

                    draw();
                }
                catch { }
               // panelBoard.Invalidate();
            }
            else
            {

            }
        }
        //----------------------------------------------------------------------------------------------
        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {
            board.updateCellValues(checkBoxParrallel.Checked,int.Parse(textBoxNumOfThread.Text.ToString()));
            draw();
           // panelBoard.Invalidate();
        }

     
        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            start *= -1;

            if (start == -1)
            {
                draw();
                buttonStartStop.Text = "stop";
                timer1.Interval = int.Parse(textBoxPeriod.Text.ToString());
                timer1.Start();
            }

            else
            {
                buttonStartStop.Text = "start";
                timer1.Stop();
            }

           
     

        }

        private void buttonupdateKey_Click(object sender, EventArgs e)
        {
            double oldsmin = ColorOperations.s_min;
            double oldsmax = ColorOperations.s_max;

            ColorOperations.s_min = double.Parse(textBoxmini.Text.ToString());
            ColorOperations.s_max = double.Parse(textBoxmaxi.Text.ToString());

            board.updateBoardColor(oldsmin, oldsmax, ColorOperations.s_min, ColorOperations.s_max);

            draw();

            //ColorOperations.s_min = smin;
            //ColorOperations.s_max = smax;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CellType = comboBox1.SelectedIndex; //0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.Show();
        }

        private void radioButtonSelectcell_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox1.Hide();
            CellType = -1;
        }

    






    }
}
