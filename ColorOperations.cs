using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Visualizaton_Project
{
    class ColorOperations
    {
        public static double s_min, s_max, s;
        static int index, n = 5;

        public static Color[] colors = { Color.Blue, Color.SkyBlue, Color.FromArgb(0, 255, 0), Color.Orange, Color.Red };

        //---------------------------------------------convert from value to color
        public static double[] ValueToColor(double s, double smin, double smax)
        {
            s_max = smax;
            s_min = smin;
            double delta_s, ds, alfa;
            double [] c={0,0,0};


            Color CS=Color.Black;
            if (s > s_max)
            {
                s=smax;
                //MessageBox.Show("wrong value");
                //return c;
            }
            else if(s < s_min)
            {
                s = s_min;
            }
            else
            {
                delta_s = (s_max - s_min) / (n - 1);
                ds = (s - s_min) / delta_s;
                index = System.Math.Abs((int)ds);
                alfa = ds - (double)index;
                if(index+1==n)
                {
                    c[0] = colors[index].R/255 ;
                    c[1] = colors[index].G/255;
                    c[2] =colors[index].B/255;
                }
                //***************************************************************************************
                else
                {
                    c[0] = (colors[index].R + (alfa * (colors[index + 1].R - colors[index].R)))/255.0;
                    c[1] = (colors[index].G + (alfa * (colors[index + 1].G - colors[index].G)))/255.0;
                    c[2] = (colors[index].B + (alfa * (colors[index + 1].B - colors[index].B)))/255.0;
                }
                //****************************************************************************************
            }
           
            return c;
        }
        //--------------------------------------------------------------------------
        public static double getValue(int cellType)//0 -> normal cell  // 1 -> block  // 2 -> window  // 3 -> heat // 4 -> cold
        {
            if(cellType==0 || cellType == 2 )//normal cell || window
            {
                return (s_max + s_min) / 2.0;
            }
            else if (cellType == 1)//block
            {
                return -1;
            }
            else if(cellType == 3)
            {
                return s_max;
            }
            else
            {
                return s_min;
            }

        }
        //--------------------------------------------------------------------------
        public static double ColorConverter(double old_SMin,double Old_smax,double s,double NEWSMIN,double NEWSMAX)
        {
            double newValue = 0;
            newValue = (s - old_SMin) / (Old_smax - old_SMin);

            newValue *= (NEWSMAX - NEWSMIN);

            newValue += NEWSMIN;

            return newValue;
        }
    }
}
