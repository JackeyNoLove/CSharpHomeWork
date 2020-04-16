using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Orders
{
    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        public List<Order> GetOrders { get => orders; }

        public void AddOrders(Order order)
        {
            if (orders.Exists(x => x.Equals(order)))
            {
                throw new Exception("订单已存在！");
            }
            orders.Add(order);
        }

        public void DeleteOrders(Order order)
        {
            if (!orders.Exists(x => x.Equals(order)))
            {
                throw new Exception("找不到相应订单！");
            }
            orders.Remove(order);
        }

        public void Get(string id,out Order order,out int index)
        {
            index = orders.FindIndex(x => x.ID==id);
            order = index == -1 ? null : orders[index];
        }
        
        public void EditOrders(Order order,int index)
        {
            if (index > orders.Count|| orders[index].ID != order.ID && orders.Exists(x => x.ID == order.ID))
            {
                throw new Exception("订单号未匹配");
            }
            orders[index] = order;
        }

        public List<Order> SelectByOrderID(string orderid)
        {
            int index = orders.FindIndex(x => x.ID == orderid);
            if (index == -1)
            {
                throw new Exception("按订单ID找不到相应订单！");
            }
            List<Order> temp = new List<Order>();
            orders.ForEach(x => { if (x.ID == orderid) temp.Add(x); });
            return temp;
        }

        public List<Order> SelectByCustomer(string name)
        {
            int index = orders.FindIndex(x => x.Customer == name);
            if (index == -1)
            {
                throw new Exception("按客户名找不到相应订单！");
            }
            List<Order> temp = new List<Order>();
            orders.ForEach(x => { if (x.Customer == name) temp.Add(x); });
            return temp;
        }

        public void Sort()
        {
            orders.OrderBy(x => x.TotalPrice);
            orders.ForEach(x=>x.Table());
        }

        public void Export(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xml.Serialize(fs, orders);
            }
        }

        public void Import(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var temp = (List<Order>)xml.Deserialize(fs);
                temp.ForEach(x => { if (!orders.Exists(a => a.Equals(x))){ orders.Add(x); } });
            }
        }
    }
}
