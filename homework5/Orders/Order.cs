using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Orders
{
    class Order
    {
        public string ID { get; }
        public string Customer { get; }
        public List<OrderItem> Items;

        public Order(string id,string customername)
        {
            ID = id;
            Customer = customername;
            Items = new List<OrderItem>();
        }

        public double TotalPrice => Items.Sum(x=>x.TotalPrice);

        public void AddItem(OrderItem item)
        {
            var index = Items.FindIndex(x => x.Equals(item));
            if (index == -1)
            {
                Items.Add(item);
            }
            else
            {
                if (Items[index].ItemName != item.ItemName || Items[index].UnitPrice != item.UnitPrice)
                {
                    throw new Exception("错误的商品数据!");
                }
                else
                {
                    Items[index].Amount += item.Amount;
                }
            }
        }

        public void DeleteItem(OrderItem item)
        {
            Items = Items.Where(x => !x.Equals(item)).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is Order order && ID == order.ID ;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public void Table()
        {
            Console.WriteLine("订单ID："+ID);
            Console.WriteLine("客户名："+Customer);
            Console.WriteLine("商品：");
            Items.ForEach(x => Console.WriteLine("      "+x.ToString())) ;
        }
    }
}
