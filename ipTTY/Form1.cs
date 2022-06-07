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
using System.Data.SqlClient;

namespace ipTTY
{
    public partial class MainPage : Form
    {
        #region Header
        Stopwatch stopwatch;
        string path = @"Data Source=chiko\SQLEXPRESS;Initial Catalog=ipTTY;Integrated Security = true";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sdpt;
        DataTable dt;
        int id;
        #endregion
        public MainPage()
        {
            InitializeComponent();
            #region Events
            save.Click += saveBtn_Click;
            saveAs.Click += saveAsBtn_Click;
            chatPanel.Hide();
            panel6.Click += ShowChatPannel;
            panel8.Click += ShowInfoPanel;
            chatPb.Click += ShowChatPannel;
            logPb.Click += ShowInfoPanel;
            chatRB.Click += timer_start;
            #endregion
            #region Set Colors
            panel1.BackColor = Color.FromArgb(212, 226, 234);
            infoLb.ForeColor = Color.FromArgb(11, 44, 64);
            settingsLb.ForeColor = Color.FromArgb(11, 44, 64);
            speedDialLb.ForeColor = Color.FromArgb(11, 44, 64);
            statusLb.ForeColor = Color.FromArgb(2, 48, 71);
            chatLb.ForeColor = Color.FromArgb(2, 48, 71);
            callLogLb.ForeColor = Color.FromArgb(2, 48, 71);
            panel8.BackColor = Color.FromArgb(217, 217, 217);
            callLogLb.BackColor = Color.FromArgb(217, 217, 217);
            panel9.BackColor = Color.FromArgb(154, 165, 182);
            callLogLb.ForeColor = Color.FromArgb(2, 48, 71);
            


            #endregion
            #region etc
            stopwatch = new Stopwatch();
            transferBtn.Hide();
            hangupBtn.Hide();
            con = new SqlConnection(path);
            RefreshTable();
            #endregion
        }
        private void font_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                outPutRb.Font = fontDialog.Font;
                chatRB.Font = fontDialog.Font;
            }
        }
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dialBtn_Click(object sender, EventArgs e)
        {
            DialForm dialForm = new DialForm();
            dialForm.ShowDialog();
        }

        private void dialPb_Click(object sender, EventArgs e)
        {
            SpeedDialForm speedDialForm = new SpeedDialForm();
            speedDialForm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void aboutUs_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void output_Click(object sender, EventArgs e)
        {
            if(output.Checked == false)
            {
                outPutRb.Hide();
                panel9.Hide();
            }
            if (output.Checked == true)
            {
                outPutRb.Show();
                panel9.Show();
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            outPutRb.SelectAll();
            chatRB.SelectAll();
        }

        private void tenacity_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tenacitycorp.com/iptty/");
        }


        #region Display Panels
        private void ShowInfoPanel(object sender, EventArgs e)
        {
            panel6.BackColor = Color.White;
            panel8.BackColor = Color.FromArgb(217, 217, 217);
            callLogLb.BackColor = Color.FromArgb(217, 217, 217);
            callLogLb.ForeColor = Color.FromArgb(2, 48, 71);
            chatLb.BackColor = Color.White;
            pictureBox5.Show();
            informationDGV.Show();
            dialBtn.Show();

            chatPanel.Hide();
            hangupBtn.Hide();
            transferBtn.Hide();

            transferPB.Visible = false;
            hangupPB.Visible = false;
        }
        private void ShowChatPannel(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(217, 217, 217); ;
            panel8.BackColor = Color.White;
            chatLb.BackColor = Color.FromArgb(217, 217, 217);
            callLogLb.BackColor = Color.White;
            chatLb.ForeColor = Color.FromArgb(2, 48, 71);
            informationDGV.Hide();
            dialBtn.Hide();
            pictureBox5.Hide();
            chatPanel.Show();
            hangupBtn.Show();
            transferBtn.Show();
            transferPB.Visible = true;
            hangupPB.Visible = true;
        }

      

        #endregion
        #region Save Buttons
        private void saveBtn_Click(object sender, EventArgs e)
        {
            string onPath = @"D:\Full-Stack\Visual studio 2022\FreelanceProjects\ipTTY\Saved";
            using (StreamWriter writer = new StreamWriter(onPath))
            {
                //writer.Write(inputTb.Text);
                writer.Close();
            }
        }

        private void saveAsBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Text Document (*.txt) |*.txt",
                DefaultExt = "*.txt",
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream stream = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(stream);
                //sw.Write(inputTb.Text);
                sw.Close();
                stream.Close();
            }
        }

        #endregion
        #region Copy-Paste
        private void copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(outPutRb.Text);
        }

        private void paste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                outPutRb.Text += Clipboard.GetText();
            }
        }

        #endregion
        #region Timer

        private void timer_start(object sender, EventArgs e)
        {
            stopwatch.Start();
        }
        private void timer_stop(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }
        private void timer_reset(object sender, EventArgs e)
        {
            stopwatch.Reset();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timerLb.Text = String.Format("{0:hh\\:mm\\:ss\\.ff}", stopwatch.Elapsed);
        }
        #endregion
        #region Search

      

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Received Calls")
            {
                string rec = "Received";
                dt = new DataTable();
                con.Open();
                sdpt = new SqlDataAdapter($"select * from info_db where Type like '%{rec}%'", con);
                sdpt.Fill(dt);
                informationDGV.DataSource = dt;
                con.Close();
            }
            else if (comboBox1.Text == "Missed Calls")
            {
                string missed = "Missed";
                dt = new DataTable();
                con.Open();
                sdpt = new SqlDataAdapter($"select * from info_db where Type like '%{missed}%'", con);
                sdpt.Fill(dt);
                informationDGV.DataSource = dt;
                con.Close();
            }
            else if (comboBox1.Text == "Dialed Calls")
            {
                string dissabled = "Dissabled";
                dt = new DataTable();
                con.Open();
                sdpt = new SqlDataAdapter($"select * from info_db where Type like '%{dissabled}%'", con);
                sdpt.Fill(dt);
                informationDGV.DataSource = dt;
                con.Close();
            }
            else
            {
                dt = new DataTable();
                con.Open();
                sdpt = new SqlDataAdapter($"select * from info_db", con);
                sdpt.Fill(dt);
                informationDGV.DataSource = dt;
                con.Close();

            }
        }
        public void RefreshTable()
        {
            dt = new DataTable();
            con.Open();
            sdpt = new SqlDataAdapter("select * from info_db", con);
            sdpt.Fill(dt);
            informationDGV.DataSource = dt;
            con.Close();
        }



        #endregion
        private void chatRB_TextChanged(object sender, EventArgs e)
        {
            soundBtn.BackColor = Color.FromArgb(240, 67, 113);
        }

        private void hangupBtn_Click_1(object sender, EventArgs e)
        {
            hangupBtn.Click += timer_stop;
            hangupPB.Click += timer_stop;
        }

        private void transferBtn_Click_1(object sender, EventArgs e)
        {
            transferBtn.Click += timer_stop;
            transferPB.Click += timer_stop;
            transferPB.Click += timer_reset;
            transferBtn.Click += timer_reset;

        }

        private void content_Click(object sender, EventArgs e)
        {
            Lisence lisence = new Lisence();
            lisence.ShowDialog();
        }

      

        
    }
}
