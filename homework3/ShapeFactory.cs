using System;
using System.Collections.Generic;
using System.Text;

namespace homework3
{
    class ShapeFactory
    {
        Random r = new Random();
        Shape temp = null;
        public Shape Creat(string shape)
        {
            switch (shape)
            {
                case "triangle":
                    temp = new Triangle(r.Next(1,100)/10.0, r.Next(1,100)/10.0);
                    break;
                case "rectangle":
                    temp = new Rectangle(r.Next(1,100)/10.0, r.Next(1,100)/10.0);
                    break;
                case "square":
                    temp = new Square(r.Next(1,100)/10.0);
                    break;
                default:throw new Exception("Unknown shape");
            }
            return temp;
        }
    }
}
