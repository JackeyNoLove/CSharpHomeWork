using System;
using System.Collections.Generic;
using System.Text;

namespace Orders
{
    public class OrderItem
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public int Amount{ get; set; }
        public double UnitPrice { get; set; }

        public OrderItem() { }
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
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ItemID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ItemName);
            return hashCode;
        }

        public override string ToString()
        {
            return "商品ID：" + ItemID + "商品名："+ItemName+"，数量："+Amount+"，单价："+UnitPrice+"，总价："+TotalPrice;
        }
    }
}
