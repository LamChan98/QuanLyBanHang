using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QuanLyBanHang.Classs; //Sử dụng classs Functions.cs

namespace QuanLyBanHang
{
    public partial class frmTaiKhoan : Form
    {
        DataTable tblTK;

        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaNV, TaiKhoan, MatKhau FROM tblTaiKhoan";
            tblTK = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvTaiKhoan.DataSource = tblTK; //Nguồn dữ liệu            
            
            dgvTaiKhoan.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvTaiKhoan.Columns[0].HeaderText = "Tài khoản";
            dgvTaiKhoan.Columns[1].HeaderText = "Mật Khẩu";
            //dgvTaiKhoan.Columns[0].Width = 300;
            //dgvTaiKhoan.Columns[1].Width = 500;
        }

        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
