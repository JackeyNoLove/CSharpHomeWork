using System;
using System.Collections.Generic;
using System.Text;

namespace homework3
{
    class Triangle:Shape
    {
        public double width, height;
        public Triangle(double w, double h)
        {
            if (w > 0 && h > 0)
            {
                width = w;
                height = h;
            }
            else
            {
                throw new Exception("Not a triangle!");
            }
        }
        public Triangle(double a, double b,double c)
        {
            if (a>0&&b>0&&c>0&&a+b>c&&a+c>b&&b+c>a)
            {
                width = a;
                height = Math.Sqrt(b * b - (a * a + b * b - c * c) * (a * a + b * b - c * c) / (4 * a * a));
            }
            else
            {
                throw new Exception("Not a triangle!");
            }
        }
        public double Area => width * height / 2;
        public void Print()
        {
            Console.WriteLine("Triangle  width:"+width+",heigth:"+height+",area:"+Area);
        }

    }
}
