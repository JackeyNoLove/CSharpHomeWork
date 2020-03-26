using System;
using System.Collections.Generic;
using System.Text;

namespace Orders
{
    class OrderItem
    {
        public string ItemID { get; }
        public string ItemName { get; }
        public int Amount;
        public double UnitPrice;

        public OrderItem(string id,string name,int num,double price)
        {
            ItemID = id;
            ItemName = name;
            Amount = num;
            UnitPrice = price;
        }

        public double TotalPrice => UnitPrice * Amount;

        public override bool Equals(object obj)
        {
            return obj is OrderItem item && ItemID == item.ItemID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemName, ItemID);
        }

        public override string ToString()
        {
            return "商品ID：" + ItemID + "商品名："+ItemName+"，数量："+Amount+"，单价："+UnitPrice+"，总价："+TotalPrice;
        }
    }
}
