using System;
using System.Collections.Generic;
using System.Text;

namespace homework3
{
    class Rectangle:Shape
    {
        public double width,height;
        public Rectangle(double w,double h)
        {
            if (w > 0 && h > 0)
            {
                width = w;
                height = h;
            }
            else
            {
                throw new Exception("Not a rectangle!");
            }
        }
        public double Area => width * height;
        public void Print()
        {
            Console.WriteLine("Rectangle width:" + width + ",heigth:" + height+",area:"+Area);
        }
    }
}
