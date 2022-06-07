using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ipTTY
{
    public partial class SettingsForm : Form
    {
       
        public SettingsForm()
        {
            InitializeComponent();
            chekActivity();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void playCb_CheckedChanged(object sender, EventArgs e)
        {
            chekActivity();
        }



        public void chekActivity()
        {
            if (playCb.Checked == true)
            {
                uploadPb.Enabled = true;
                uploadTb.Enabled = true;
                repeatCb.Enabled = true;
                secondsTb.Enabled = true;
            }
            else
            {
                uploadPb.Enabled = false;
                uploadTb.Enabled = false;
                repeatCb.Enabled = false;
                secondsTb.Enabled = false;
            }
        }

        private void uploadPb_Click(object sender, EventArgs e)
        {
            Upload();
        }

        private void greetingRb_CheckedChanged(object sender, EventArgs e)
        {
            if (greetingRb.Checked)
            {
                inputCB.Enabled = true;
                inputRTB.Enabled = true;
            }
            else
            {
                inputCB.Enabled = false;
                inputRTB.Enabled = false;
            }
        }

        private void saveConversationAutoRb_CheckedChanged(object sender, EventArgs e)
        {
            if (saveConversationAutoRb.Checked)
            {
                uploadAutoTb.Enabled = true;
                uploadAutoPB.Enabled = true;
                nameConventionTb.Enabled = true;
            }
            else
            {
                uploadAutoTb.Enabled = false;
                uploadAutoPB.Enabled = false;
                nameConventionTb.Enabled = false;
            }
        }

        private void uploadAutoPB_Click(object sender, EventArgs e)
        {
            Upload();
        }


        public void Upload()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Document (*.txt) |*.txt",
                DefaultExt = "*.txt",
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = File.OpenText(openFileDialog.FileName);
                uploadTb.Text = openFileDialog.FileName;
                reader.Close();
            }
        }

        private void enableCB_CheckedChanged(object sender, EventArgs e)
        {
            if (enableCB.Checked)
            {
                speakerCB.Enabled = true;
                micCB.Enabled = true;
            }
            else
            {
                speakerCB.Enabled = false;
                micCB.Enabled = false;
            }
        }

        private void rfc4103CB_CheckedChanged(object sender, EventArgs e)
        {
            if (rfc4103CB.Checked)
            {
                rtpBasePortTB.Enabled = true;
                bufferTb.Enabled = true;
                maxCpsTb.Enabled = true;
                redGenTb.Enabled = true;
                allowInboundCB.Enabled = true;
            }
            else
            {
                rtpBasePortTB.Enabled = false;
                bufferTb.Enabled = false;
                maxCpsTb.Enabled = false;
                redGenTb.Enabled = false;
                allowInboundCB.Enabled = false;
            }
        }

        private void speakerCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
