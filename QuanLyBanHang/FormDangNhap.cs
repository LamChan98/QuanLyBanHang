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

namespace QuanLyBanHang
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        public static SqlConnection Con;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Con = new SqlConnection();   //Khởi tạo đối tượng
            Con.ConnectionString = Properties.Settings.Default.QLBanHangConnectionString;
            try
            {
                Con.Open();
                string sql = "select * from tblTaiKhoan where TaiKhoan='" + txtTaiKhoan.Text + "' and MatKhau= '" + txtMatKhau.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Con);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    frmMain frm = new frmMain();
                    frm.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            } 
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    

