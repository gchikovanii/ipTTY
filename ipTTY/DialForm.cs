using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ipTTY
{
    public partial class DialForm : Form
    {

        public DialForm()
        {
            InitializeComponent();
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
           if(phoneNumberTb.Text.Length > 0)
           {
                this.Close();
            }
           else
           {
                Close();
           }
        }
    }
}
