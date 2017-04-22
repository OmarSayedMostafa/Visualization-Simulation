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
    public static class DrawCell
    {
       //public static Color[] colors = { Color.GreenYellow, Color.Black, Color.GreenYellow,Color.Red, Color.Blue };

        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush, float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillRect(this Graphics g, Brush brush, float X, float Y,int size,double value,int type)//0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        {
            double[] C = ColorOperations.ValueToColor(value, ColorOperations.s_min, ColorOperations.s_max);

            //Color CO = Color.FromArgb(255,(byte)(C[0] ), (byte)(C[1]), (byte)(C[2]));

            brush = new SolidBrush(Color.FromArgb(255, (int)(C[0]*255.0), (int)(C[1]*255.0), (int)(C[2]*255.0)));

            g.FillRectangle(brush, X, Y, size, size);
            if(type==2)//window
            {
                g.DrawRectangle(new Pen(Color.Black), X, Y, size, size);
            }
        }

        
    }
}
