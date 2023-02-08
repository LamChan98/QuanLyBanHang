using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyBanHang.Classs;

namespace QuanLyBanHang
{
    public partial class frmBackup : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.QLBanHangConnectionString);
        public frmBackup()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = dlg.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            if(txtLocation.Text== string.Empty)
            {
                MessageBox.Show("Please enter backup file location");
            }
            else
            {
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK = '" + txtLocation.Text + "\\" + "QLCuahang" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
                con.Open();
                SqlCommand command = new SqlCommand(cmd, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Database backup done successfully");
                con.Close();
                btnBackup.Enabled = false;
            }
        }
    }
}
