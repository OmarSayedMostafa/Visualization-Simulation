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
    class Cell
    {
        public int Type;
        public int size;
        Point position;
        public Color color;
        public double value;
        public String CellName;




        public Cell(int Type,int size,Point P)
        {
            color = new Color();
           // color = DrawCell.colors[Type];
            this.Type = Type;
            position = new Point();
            position = P;
            this.size = size;
            value = ColorOperations.getValue(Type);
            findCellname();
        }

        public void setValue(double Value)
        {
            this.value = Value;
        }
        public void findCellname()
        {
            if (Type == 0)
                CellName = "Normal Cell";
            else if(Type==1)
                CellName = "Block Cell";
            else if (Type == 2)
                CellName = "Window Cell";
            else if (Type == 3)
                CellName = "Heat Cell";
            else if (Type == 4)
                CellName = "Cold Cell";
        }

    
        
    }
}
