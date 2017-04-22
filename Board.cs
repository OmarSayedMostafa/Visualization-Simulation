using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualizaton_Project
{
    class Board
    {
        //0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        public int Rows, Coloums, CellSize;
        public Cell[,] boardCells;
        public Cell[,] NewBoardCells;///temp

        public Board(Size size, int CellSize)
        {
            this.CellSize = CellSize;
            Coloums = size.Width / CellSize;
            Rows = size.Height / CellSize;
            boardCells = new Cell[Rows, Coloums];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Coloums; j++)
                {
                    Cell normCell = new Cell(0, CellSize, new Point(i, j));
                    boardCells[i, j] = normCell;
                }
        }

        public void AddNewCell(int CellType, Point ClickPos, Point boardLocation, Size BoardSize, int size)
        {
            Point Coordinates = calculate_coordinates(ClickPos, boardLocation, BoardSize);
            if (Coordinates.X > Rows - 1)
                Coordinates.X--;
            if (Coordinates.Y > Coloums - 1)
                Coordinates.Y--;

            Cell newCell = new Cell(CellType, size, Coordinates);
            boardCells[Coordinates.X, Coordinates.Y] = newCell;

        }
        public Point calculate_coordinates(Point ClickPos, Point boardLocation, Size BoardSize)
        {

            int C_number = (int)(((double)(ClickPos.X - boardLocation.X) / (double)((boardLocation.X + BoardSize.Width) - boardLocation.X)) * (Coloums));
            int rowNumber = (int)(((double)(ClickPos.Y - boardLocation.Y) / (double)((boardLocation.Y + BoardSize.Height) - boardLocation.Y)) * (Rows));

            return new Point(rowNumber, C_number);
        }



        public void updateCellValues(bool notifier,int numofThreads)//0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        {
            
            
            //========================================================================
            NewBoardCells = new Cell[Rows, Coloums];
            //========================================================================
            
            #region looping on board

            #region parallel
            if (notifier)//parallel
            {
                //for (int i = 0; i < Rows; i++)
                Parallel.For(0, Rows, new ParallelOptions { MaxDegreeOfParallelism = numofThreads }, i =>
                {
                    for (int j = 0; j < Coloums; j++)
                    {
                        //NewBoardCells = new Cell[Rows, Coloums];
                        double totalValue = 0;
                        int TotalCount = 0;
                        totalValue = boardCells[i, j].value;
                        TotalCount = 1;
                        if (boardCells[i, j].Type == 0)//normal cell
                        {
                            #region check neighbours

                            #region up
                            //---------------------------up
                            try
                            {
                                if (boardCells[i - 1, j].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region down
                            //----------------------------down
                            try
                            {
                                if (boardCells[i + 1, j].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region left
                            //----------------------------left
                            try
                            {
                                if (boardCells[i, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i, j - 1].value;
                                    TotalCount++;
                                }

                            }
                            catch { }
                            #endregion

                            #region right
                            //----------------------------right
                            try
                            {
                                if (boardCells[i, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner up left
                            //----------------------------up left
                            try
                            {
                                if (boardCells[i - 1, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j - 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner down left
                            //----------------------------down left
                            try
                            {
                                if (boardCells[i + 1, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j - 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner up right
                            //----------------------------up right
                            try
                            {
                                if (boardCells[i - 1, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner down right
                            //----------------------------down right
                            try
                            {
                                if (boardCells[i + 1, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion




                            //----------------------------end

                            #endregion

                            Cell normCell = new Cell(0, CellSize, new Point(i, j));
                            NewBoardCells[i, j] = normCell;

                            NewBoardCells[i, j].value = totalValue / (double)TotalCount;

                            // boardCells[i, j].value = totalValue / (double)TotalCount;
                        }
                        else
                        {
                            //  Cell normCell = new Cell(boardCells[i, j].Type, CellSize, new Point(i, j));
                            NewBoardCells[i, j] = boardCells[i, j];
                        }

                    }

                });



                boardCells = NewBoardCells;
            }
            #endregion

            #region sequntional
            else
            {

                //NewBoardCells = new Cell[Rows, Coloums];
                double totalValue = 0;
                int TotalCount = 0;
                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Coloums; j++)
                    {
                        totalValue = boardCells[i, j].value;
                        TotalCount = 1;
                        if (boardCells[i, j].Type == 0)//normal cell
                        {
                            #region check neighbours

                            #region up
                            //---------------------------up
                            try
                            {
                                if (boardCells[i - 1, j].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region down
                            //----------------------------down
                            try
                            {
                                if (boardCells[i + 1, j].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region left
                            //----------------------------left
                            try
                            {
                                if (boardCells[i, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i, j - 1].value;
                                    TotalCount++;
                                }

                            }
                            catch { }
                            #endregion

                            #region right
                            //----------------------------right
                            try
                            {
                                if (boardCells[i, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner up left
                            //----------------------------up left
                            try
                            {
                                if (boardCells[i - 1, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j - 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner down left
                            //----------------------------down left
                            try
                            {
                                if (boardCells[i + 1, j - 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j - 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner up right
                            //----------------------------up right
                            try
                            {
                                if (boardCells[i - 1, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i - 1, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion

                            #region corner down right
                            //----------------------------down right
                            try
                            {
                                if (boardCells[i + 1, j + 1].Type != 1) //block
                                {
                                    totalValue += boardCells[i + 1, j + 1].value;
                                    TotalCount++;
                                }
                            }
                            catch { }
                            #endregion




                            //----------------------------end

                            #endregion

                            Cell normCell = new Cell(0, CellSize, new Point(i, j));
                            NewBoardCells[i, j] = normCell;

                            NewBoardCells[i, j].value = totalValue / (double)TotalCount;

                            // boardCells[i, j].value = totalValue / (double)TotalCount;
                        }
                        else
                        {
                            //  Cell normCell = new Cell(boardCells[i, j].Type, CellSize, new Point(i, j));
                            NewBoardCells[i, j] = boardCells[i, j];
                        }

                    }

                boardCells = NewBoardCells;
            }
            #endregion



            #endregion

        }


        public void updateBoardColor(double oldSmin,double oldSmax,double NewSmin,double NewSmax)
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Coloums; j++)
                {

                    boardCells[i, j].value = ColorOperations.ColorConverter(oldSmin, oldSmax, boardCells[i, j].value,NewSmin,NewSmax);
                }
        }
    }

}
