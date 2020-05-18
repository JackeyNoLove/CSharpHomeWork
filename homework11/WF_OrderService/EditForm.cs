using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orders;

namespace WF_OrderService
{
    public partial class EditForm : Form
    {
        public Order CurrentOrder { get; set; }

        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(Order order,bool edit = false) : this()
        {
            CurrentOrder = order;
            OrderBindingSource.DataSource = CurrentOrder;
            txtOrderID.DataBindings.Add("Text", OrderBindingSource, "ID");
            txtCustomer.DataBindings.Add("Text", OrderBindingSource, "Customer");
            txtCustomer.Enabled = !edit;
            txtOrderID.Enabled = !edit;
            this.Text = edit ? "修改订单" : "添加订单";
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ItemEditForm form2 = new ItemEditForm(new OrderItem());
            try
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    CurrentOrder.AddItem(form2.CurrentItem);
                    OrderBindingSource.ResetBindings(true);
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            OrderItem item = ItemBindingSource.Current as OrderItem;
            if (item == null)
            {
                MessageBox.Show("请选择一件商品进行修改");
                return;
            }
            ItemEditForm form2 = new ItemEditForm(item, true);
            if (form2.ShowDialog() == DialogResult.OK)
            {
                CurrentOrder.AddItem(item);
                OrderBindingSource.ResetBindings(true);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            OrderItem item = ItemBindingSource.Current as OrderItem;
            if (item == null)
            {
                MessageBox.Show("请选择一件商品进行删除");
                return;
            }
            CurrentOrder.DeleteItem(item);
            OrderBindingSource.ResetBindings(true);
        }
    }
}
