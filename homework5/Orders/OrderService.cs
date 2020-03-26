using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Orders
{
    class OrderService
    {
        private List<Order> orders = new List<Order>();

        public void Add(Order order)
        {
            if (orders.Exists(x => x.Equals(order)))
            {
                throw new Exception("订单已存在！");
            }
            orders.Add(order);
        }

        public void Delete(Order order)
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
        
        public void Edit(Order order,int index)
        {
            if (index > orders.Count|| orders[index].ID != order.ID && orders.Exists(x => x.ID == order.ID))
            {
                throw new Exception("订单号未匹配");
            }
            orders[index] = order;
        }

        public void SelectByOrderID(string orderid)
        {
            int index = orders.FindIndex(x => x.ID == orderid);
            if (index == -1)
            {
                throw new Exception("按订单ID找不到相应订单！");
            }
            orders[index].Items.OrderBy(x => x.TotalPrice);
            orders[index].Table();
        }

        public void SelectByCustomer(string name)
        {
            int index = orders.FindIndex(x => x.Customer == name);
            if (index == -1)
            {
                throw new Exception("按客户名找不到相应订单！");
            }
            orders[index].Items.OrderBy(x => x.TotalPrice);
            orders[index].Table();
        }

        public void Sort()
        {
            orders.OrderBy(x => x.TotalPrice);
            orders.ForEach(x=>x.Table());
        }
    }
}
