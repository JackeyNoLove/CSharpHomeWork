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
    public partial class ItemEditForm : Form
    {
        public OrderItem CurrentItem { get; set; }

        public ItemEditForm()
        {
            InitializeComponent();
        }

        public ItemEditForm(OrderItem orderItem, bool edit = false) : this()
        {
            CurrentItem = orderItem;
            ItemBindingSource.DataSource = orderItem;
            txtAmount.DataBindings.Add("Text", ItemBindingSource, "Amount");
            txtItemID.DataBindings.Add("Text", ItemBindingSource, "ItemID");
            txtItemName.DataBindings.Add("Text", ItemBindingSource, "ItemName");
            txtUnitPrice.DataBindings.Add("Text", ItemBindingSource, "UnitPrice");
            txtItemID.Enabled = !edit;
            txtItemName.Enabled = !edit;
        }
    }
}
