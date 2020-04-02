using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Orders.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        OrderService service = new OrderService();
        OrderItem apple = new OrderItem("1", "apple", 80, 10.0);
        OrderItem egg = new OrderItem("2", "eggs", 200, 1.2);
        OrderItem milk = new OrderItem("3", "milk", 10, 50);

        [TestInitialize()]
        public void init()
        {
            Order order1 = new Order("1", "Customer1", new List<OrderItem> { apple, egg, milk });
            Order order2 = new Order("2", "Customer2", new List<OrderItem> { egg, milk });
            Order order3 = new Order("3", "Customer2", new List<OrderItem> { apple, milk });
            service = new OrderService();
            service.AddOrders(order1);
            service.AddOrders(order2);
            service.AddOrders(order3);
        }

        [TestMethod()]
        public void AddOrdersTest()
        {
            Order order4 = new Order("4", "Customer2", new List<OrderItem> { milk });
            service.AddOrders(order4);
            Assert.AreEqual(4, service.GetOrders.Count);
        }

        [TestMethod()]
        public void DeleteOrdersTest1()
        {
            Order order3 = new Order("3", "", null);
            service.DeleteOrders(order3);
            Assert.AreEqual(2, service.GetOrders.Count);
        }
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void DeleteOrdersTest2()
        {
            Order order5 = new Order("5", "", null);
            service.DeleteOrders(order5);
            Assert.AreEqual(2, service.GetOrders.Count);
        }

        [TestMethod()]
        public void EditOrdersTest()
        {
            Order order2 = new Order("2", "Customer2", new List<OrderItem> { egg, milk });
            service.Get("2",out Order o,out int i);
            Assert.IsNotNull(o);
            Assert.AreEqual(order2, o);
            service.Get("4", out o, out i);
            Assert.IsNull(o);
        }

        [TestMethod()]
        public void SelectByOrderIDTest()
        {
            Assert.AreEqual(1, service.SelectByOrderID("1").Count);
        }

        [TestMethod()]
        public void SelectByCustomerTest()
        {
            Assert.AreEqual(2, service.SelectByCustomer("Customer2").Count);
        }

        [TestMethod()]
        public void ExportTest()
        {
            String file = "./temp.xml";
            service.Export(file);
            Assert.IsTrue(File.Exists(file));
            List<String> expectLines = File.ReadLines("a.xml").ToList();
            List<String> outputLines = File.ReadLines(file).ToList();
            Assert.AreEqual(expectLines.Count, outputLines.Count);
            for (int i = 0; i < expectLines.Count; i++)
            {
                Assert.AreEqual(expectLines[i].Trim(), outputLines[i].Trim());
            }
        }

        [TestMethod()]
        public void ImportTest1()
        {
            OrderService os = new OrderService();
            os.Import("./a.xml");
            Assert.AreEqual(3, os.GetOrders.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportTest2()
        {
            OrderService os = new OrderService();
            os.Import("./b.xml");
            Assert.AreEqual(3, os.GetOrders.Count);
        }
    }
}
