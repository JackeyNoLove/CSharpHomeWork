using System;
using System.Collections.Generic;
using System.Text;

namespace homework3
{
    class Square : Shape
    {
        public double width;
        public Square(double w)
        {
            if (w > 0)
            {
                width = w;
            }
            else
            {
                throw new Exception("Not a square!");
            }
        }
        public double Area => width * width;
        public void Print()
        {
            Console.WriteLine("Square    width:" + width+",area:"+Area);
        }
    }
}
