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
    public partial class MainForm : Form
    {
        OrderService service = new OrderService();
        BindingSource fieldsBS = new BindingSource();

        public MainForm()
        {
            InitializeComponent();
            service.AddOrders(new Order("1", "Customer1", new List<OrderItem> { new OrderItem("1", "apple", 80, 10.0), new OrderItem("2", "eggs", 200, 1.2), new OrderItem("3", "milk", 10, 50) }));
            service.AddOrders(new Order("2", "Customer2", new List<OrderItem> { new OrderItem("2", "eggs", 200, 1.2),new OrderItem("3", "milk", 10, 50) }));
            service.AddOrders(new Order("3", "Customer2", new List<OrderItem> { new OrderItem("1", "apple", 80, 10.0),new OrderItem("3", "milk", 10, 50) }));
            OrderBindingSource.DataSource = service.GetOrders;
            ModeList.SelectedIndex = 0;
        }

        private void ModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ModeList.SelectedIndex)
            {
                case 0:
                    OrderBindingSource.DataSource = service.GetOrders;
                    OptionList.SelectedIndex = -1;
                    break;
                case 1:
                    List<string> id = new List<string>();
                    service.GetOrders.ForEach( x => { if (!id.Contains(x.ID)) id.Add(x.ID); } );
                    id.Sort();
                    OptionList.DataSource = id;
                    break;
                case 2:
                    List<string> customer = new List<string>();
                    service.GetOrders.ForEach(x => { if (!customer.Contains(x.Customer)) customer.Add(x.Customer); });
                    customer.Sort();
                    OptionList.DataSource = customer;
                    break;
            }
        }

        private void OptionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ModeList.SelectedIndex)
            {
                case 1:
                    OrderBindingSource.DataSource = service.SelectByOrderID(OptionList.Text);
                    break;
                case 2:
                    OrderBindingSource.DataSource = service.SelectByCustomer(OptionList.Text);
                    break;
            }
            OrderBindingSource.ResetBindings(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditForm form1 = new EditForm(new Order());
            if (form1.ShowDialog() == DialogResult.OK ) 
            {
                service.AddOrders(form1.CurrentOrder);
                OrderBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Order order = OrderBindingSource.Current as Order;
            service.Get(order.ID, out Order t, out int index);
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行修改");
                return;
            }
            EditForm form1 = new EditForm(order, true);
            if (form1.ShowDialog() == DialogResult.OK)
            {
                service.EditOrders(order, index);
                OrderBindingSource.ResetBindings(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Order order = OrderBindingSource.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            service.DeleteOrders(order);
            OrderBindingSource.ResetBindings(true);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = saveFileDialog.FileName;
                service.Export(fileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                String fileName = openFileDialog.FileName;
                service.Import(fileName);
                OrderBindingSource.ResetBindings(true);
            }
        }
    }
}
