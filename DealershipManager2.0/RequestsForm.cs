using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DealershipManager2._0
{
    public partial class RequestsForm : Form
    {
        public RequestsForm()
        {
            InitializeComponent();
        }

        private void RequestsForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Parent.Size.Width * 2, Parent.Size.Height * 2);
        }
    }
}
