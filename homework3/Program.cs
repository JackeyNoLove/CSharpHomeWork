using System;

namespace homework3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShapeFactory SF = new ShapeFactory();
                Random r = new Random();
                double Sum = 0;
                Shape[] Graphics = new Shape[10];
                for(int i = 0; i < 10; i++)
                {
                    switch (r.Next(1, 4))
                    {
                        case 1:
                            Graphics[i]=SF.Creat("triangle");
                            break;
                        case 2:
                            Graphics[i]=SF.Creat("rectangle");
                            break;
                        case 3:
                            Graphics[i]=SF.Creat("square");
                            break;
                    }
                }
                for(int i = 0; i < 10; i++)
                {
                    Graphics[i].Print();
                    Sum += Graphics[i].Area;
                }
                Console.WriteLine("AreaSum:" + Sum);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
