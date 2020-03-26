using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders
{
    class Program
    {
        public static void Update(ref Order order)
        {
            while (true)
            {
                Console.WriteLine("选择一个操作符：c-添加商品，d-删除商品，q-退出");
                var op = Console.ReadLine();
                if (op.Length == 0) { continue; }
                switch (op[0])
                {
                    case 'c':
                        string itemID, itemName;
                        int itemAmount;
                        double itemPrice;
                        OrderItem newitem;
                        do
                        {
                            Console.WriteLine("请输入一份商品的详细信息");
                            Console.WriteLine("请输入商品ID");
                            itemID = Console.ReadLine();
                            Console.WriteLine("请输入商品名称");
                            itemName = Console.ReadLine();
                            Console.WriteLine("请输入商品单价");
                            while (!double.TryParse(Console.ReadLine(), out itemPrice))
                            {
                                Console.WriteLine("请输入正确的商品单价");
                            }
                            Console.WriteLine("请输入商品数量");
                            while (!int.TryParse(Console.ReadLine(), out itemAmount))
                            {
                                Console.WriteLine("请输入正确的商品数量");
                            }
                            newitem = new OrderItem(itemID, itemName, itemAmount, itemPrice);
                            order.AddItem(newitem);
                            Console.WriteLine("输入y继续添加商品，否则停止添加");
                        } while (Console.ReadLine() == "y");
                        break;
                    case 'd':
                        Console.WriteLine("请输入商品ID");
                        itemID = Console.ReadLine();
                        order.DeleteItem(new OrderItem(itemID,"",0,0));
                        Console.WriteLine("已删除商品");
                        break;
                    case 'q':
                        return;
                    default:
                        Console.WriteLine("错误的操作符！");
                        break;
                }
            }
        }

        public static void Read()
        {
            var orders = new List<Order>();
            var service = new OrderService();
            while (true)
            {
                Console.WriteLine("选择一个操作符: c-添加订单,d-删除订单,u-修改订单,s-查询订单,q-退出");
                var op = Console.ReadLine();
                if (op.Length == 0) { continue; }

                switch (op[0])
                {
                    case 'c':
                        while(true)
                        {
                            string orderID, customerName, itemID, itemName;
                            int itemAmount;
                            double itemPrice;

                            Console.WriteLine("请输入订单ID");
                            orderID = Console.ReadLine(); 
                            Console.WriteLine("请输入用户名");
                            customerName = Console.ReadLine();
                            OrderItem newitem;
                            Order neworder = new Order(orderID,customerName);

                            do
                            {
                                Console.WriteLine("请输入一份商品的详细信息");
                                Console.WriteLine("请输入商品ID");
                                itemID = Console.ReadLine();
                                Console.WriteLine("请输入商品名称");
                                itemName = Console.ReadLine();
                                Console.WriteLine("请输入商品单价");
                                while (!double.TryParse(Console.ReadLine(), out itemPrice))
                                {
                                    Console.WriteLine("请输入正确的商品单价");
                                }
                                Console.WriteLine("请输入商品数量");
                                while (!int.TryParse(Console.ReadLine(), out itemAmount))
                                {
                                    Console.WriteLine("请输入正确的商品数量");
                                }
                                newitem = new OrderItem(itemID, itemName, itemAmount, itemPrice);
                                neworder.AddItem(newitem);
                                Console.WriteLine("输入y继续添加商品，否则停止添加");
                            } while (Console.ReadLine() == "y");
                            try
                            {
                                service.Add(neworder);
                                Console.WriteLine("新订单"+orderID+"已添加");
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                        break;
                    case 'd':
                        Console.WriteLine("请输入需要删除的订单ID");
                        try
                        {
                            string ID = Console.ReadLine();
                            service.Delete(new Order(ID,""));
                            Console.WriteLine("订单"+ID+"已删除");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 'u':
                        Console.WriteLine("请输入需要修改的订单ID");
                        string id = Console.ReadLine();
                        service.Get(id, out Order order, out int index);
                        if (index == -1) { Console.WriteLine("找不到相应订单！"); }
                        else
                        {
                            try
                            {
                                Update(ref order);
                                service.Edit(order,index);
                                Console.WriteLine("订单" + id + "已修改");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    case 's':
                        string op1;
                        do
                        {
                            Console.WriteLine("选择一个操作符: i-按订单号查询,n-按客户名查询,l-列出全部订单,q-退出");
                            op1 = Console.ReadLine();
                            if (op1.Length == 0) { Console.WriteLine("请输入操作符！"); continue; }
                            switch (op1[0])
                            {
                                case 'i':
                                    try
                                    {
                                        Console.WriteLine("请输入订单号");
                                        service.SelectByOrderID(Console.ReadLine()); 
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case 'n':
                                    try 
                                    { 
                                        Console.WriteLine("请输入客户名");
                                        service.SelectByCustomer(Console.ReadLine()); break;
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case 'l':
                                    service.Sort();
                                    break;
                            }
                        } while (op1[0] != 'q');
                        break;
                    case 'q':
                        return;
                    default:
                        Console.WriteLine("错误的操作符！");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Read();
        }
    }
}
