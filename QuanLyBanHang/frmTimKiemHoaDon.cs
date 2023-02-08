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
    public partial class frmTimKiemHoaDon : Form
    {
        DataTable tblHDB; //Hoá đơn bán
        public frmTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmTimKiemHoaDon_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtTim.Focus();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaHDB, a.MaNV, c.TenNV, NgayLap, a.MaKH, b.TenKH,TongTien FROM tblHoaDonBan as a join tblKhachHang as b on a.MaKH =b.MaKH join tblNhanVien as c on a.MaNV= c.MaNV";
            tblHDB = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvTKHoaDon.DataSource = tblHDB; //Nguồn dữ liệu
            dgvTKHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvTKHoaDon.Columns[2].HeaderText = "Tên nhân viên";
            dgvTKHoaDon.Columns[3].HeaderText = "Ngày tạo";
            dgvTKHoaDon.Columns[4].HeaderText = "Mã khách hàng";
            dgvTKHoaDon.Columns[5].HeaderText = "Tên khách hàng";
            dgvTKHoaDon.Columns[6].HeaderText = "Tổng tiền";

            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtTim.Text == "")
            {
                MessageBox.Show("Bạn hãy nhập nội dung tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTim.Focus();
                return;
            }
            if (cboTim.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn điều kiện tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "SELECT MaHDB, a.MaNV, c.TenNV, NgayLap, a.MaKH, b.TenKH,TongTien FROM tblHoaDonBan as a join tblKhachHang as b on a.MaKH =b.MaKH join tblNhanVien as c on a.MaNV= c.MaNV WHERE 1=1";
            if (cboTim.SelectedItem.ToString() == "Mã hóa đơn")
                sql += " AND a.MaHDB LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Mã nhân viên")
                sql += " AND a.MaNV LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Tên nhân viên")
                sql += " AND c.TenNV LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Ngày lập")
                sql += " AND a.NgayLap LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Mã khách hàng")
                sql += " AND a.MaKH LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Tên khách hàng")
                sql += " AND b.TenKH LIKE N'%" + txtTim.Text + "%'";
            tblHDB = Functions.GetDataToTable(sql);
            if (tblHDB.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblHDB.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTKHoaDon.DataSource = tblHDB;
            ResetValues();
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void txtTong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dgvTKHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dgvTKHoaDon.CurrentRow.Cells["MaHDB"].Value.ToString();
                frmHoaDon frm = new frmHoaDon();
                txtTim.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtThang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
