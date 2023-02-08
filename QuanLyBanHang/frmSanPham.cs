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
using System.Globalization;

namespace QuanLyBanHang
{
    public partial class frmSanPham : Form
    {
        DataTable tblSP;

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * from tblLoaiSP";
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            cboMaLoaiSP.Enabled = false;
            txtThuongHieu.Enabled = false;
            txtTheTich.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonViTinh.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            dtpNgayNhap.Enabled = false;
            txtMoTa.Enabled = false;
            txtGhiChu.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboMaLoaiSP, "MaLoaiSP", "TenLoaiSP");
            cboMaLoaiSP.SelectedIndex = -1;
            ResetValues();
        }

        //Phương thức nạp dữ liệu
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaSP, TenSP, MaLoaiSP, ThuongHieu, TheTich, SoLuong, DonGiaNhap, DonGiaBan, DonViTinh, NgayNhap, Mota, GhiChu FROM tblSanPham";
            tblSP = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvSanPham.DataSource = tblSP; //Nguồn dữ liệu            
            dgvSanPham.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvSanPham.Columns[2].HeaderText = "Mã Loại Sản Phẩm";
            dgvSanPham.Columns[3].HeaderText = "Thương hiệu";
            dgvSanPham.Columns[4].HeaderText = "Thể tích";
            dgvSanPham.Columns[5].HeaderText = "Số Lượng";
            dgvSanPham.Columns[6].HeaderText = "Đơn Giá Nhập";
            dgvSanPham.Columns[7].HeaderText = "Đơn Giá Bán";
            dgvSanPham.Columns[8].HeaderText = "Đơn vị tính";
            dgvSanPham.Columns[9].HeaderText = "Ngày nhập";
            dgvSanPham.Columns[10].HeaderText = "Mô tả";
            dgvSanPham.Columns[11].HeaderText = "Ghi Chú";
            /*dgvSanPham.Columns[0].Width = 100;
            dgvSanPham.Columns[1].Width = 150;
            dgvSanPham.Columns[2].Width = 50;
            dgvSanPham.Columns[3].Width = 50;
            dgvSanPham.Columns[4].Width = 100;
            dgvSanPham.Columns[5].Width = 100;
            dgvSanPham.Columns[6].Width = 100;*/

            //canh lề phải cho Datagridview
            dgvSanPham.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSanPham.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSanPham.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Thêm đơn vị "đồng" vào datagridview
            dgvSanPham.Columns[6].DefaultCellStyle.Format = "c2";
            dgvSanPham.Columns[6].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi");
            dgvSanPham.Columns[7].DefaultCellStyle.Format = "c2";
            dgvSanPham.Columns[7].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi");

            dgvSanPham.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        //Khởi tạo lại giá trị
        private void ResetValues()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cboMaLoaiSP.Text = "";
            txtThuongHieu.Text = "";
            txtTheTich.Text = "";
            txtSoLuong.Text = "0";
            txtDonViTinh.Text = "";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            dtpNgayNhap.Value = DateTime.Now;
            txtMoTa.Text = "";
            txtGhiChu.Text = "";
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaSanPham;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaSP.Text = dgvSanPham.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells["TenSP"].Value.ToString();
            cboMaLoaiSP.Text = dgvSanPham.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
            sql = "SELECT TenLoaiSP FROM tblLoaiSP WHERE MaLoaiSP=N'" + cboMaLoaiSP.Text + "'";
            cboMaLoaiSP.Text = Functions.GetFieldValues(sql);
            txtThuongHieu.Text= dgvSanPham.CurrentRow.Cells["ThuongHieu"].Value.ToString();
            txtTheTich.Text = dgvSanPham.CurrentRow.Cells["TheTich"].Value.ToString();
            txtSoLuong.Text = dgvSanPham.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonViTinh.Text = dgvSanPham.CurrentRow.Cells["DonViTinh"].Value.ToString();
            txtDonGiaNhap.Text = dgvSanPham.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvSanPham.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            dtpNgayNhap.Value = (DateTime)dgvSanPham.CurrentRow.Cells["NgayNhap"].Value;
            sql = "SELECT MoTa FROM tblSanPham WHERE MaSP = N'" + txtMaSP.Text + "'";
            txtMoTa.Text = Functions.GetFieldValues(sql);
            sql = "SELECT GhiChu FROM tblSanPham WHERE MaSP = N'" + txtMaSP.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = true;
            cboMaLoaiSP.Enabled = true;
            txtThuongHieu.Enabled = true;
            txtTheTich.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonViTinh.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            dtpNgayNhap.Enabled = true;
            txtMoTa.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaSP.Enabled = true;
            txtMaSP.Focus();
            txtTenSP.Enabled = true;
            cboMaLoaiSP.Enabled = true;
            txtThuongHieu.Enabled = true;
            txtTheTich.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonViTinh.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            dtpNgayNhap.Enabled = true;
            txtMoTa.Enabled = true;
            txtGhiChu.Enabled = true;

            string chuoi1 = "";
            int chuoi2 = 0;
            chuoi1 = Convert.ToString(Functions.GetFieldValues("SELECT TOP 1 MaSP FROM tblSanPham order by MaSP desc"));
            chuoi2 = Convert.ToInt32(chuoi1.Remove(0, 2));
            if (chuoi2 + 1 < 10)
            {
                txtMaSP.Text = "SP00" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 100)
            {
                txtMaSP.Text = "SP0" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 1000)
            {
                txtMaSP.Text = "SP" + (chuoi2 + 1).ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (cboMaLoaiSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiSP.Focus();
                return;
            }
            sql = "SELECT MaSP FROM tblSanPham WHERE MaSP=N'" + txtMaSP.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại, bạn phải chọn mã sản phẩm khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "INSERT INTO tblSanPham(MaSP,TenSP,MaLoaiSP,ThuongHieu, TheTich, SoLuong, DonViTinh, DonGiaNhap, DonGiaBan, NgayNhap, MoTa, GhiChu) VALUES(N'"
                + txtMaSP.Text.Trim() + "',N'" + txtTenSP.Text.Trim() +
                "',N'" + cboMaLoaiSP.SelectedValue.ToString() +
                "',N'" + txtThuongHieu.Text.Trim() + "'," + txtTheTich.Text + "," + txtSoLuong.Text + ",N'" + txtDonViTinh.Text.Trim() + "', " + txtDonGiaNhap.Text + "," + txtDonGiaBan.Text + ",'" + dtpNgayNhap.Value + "',N'" + txtMoTa.Text.Trim() + "',N'" + txtGhiChu.Text.Trim() + "')";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnBoQua.Enabled = true;
                btnLuu.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (cboMaLoaiSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiSP.Focus();
                return;
            }

            if (MessageBox.Show("Bạn có muốn sửa thông tin sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "UPDATE tblSanPham SET TenSP=N'" + txtTenSP.Text.Trim().ToString() + "',MaLoaiSP=N'" + cboMaLoaiSP.SelectedValue.ToString() +
                "',ThuongHieu=N'" + txtThuongHieu.Text.Trim().ToString() + "',TheTich='" + txtTheTich.Text + "',SoLuong='" + txtSoLuong.Text + "',DonViTinh=N'" + txtDonViTinh.Text.Trim().ToString() + "',DonGiaNhap='" + txtDonGiaNhap.Text + "',DonGiaBan='" + txtDonGiaBan.Text + "',NgayNhap='" + dtpNgayNhap.Value + "',MoTa=N'" + txtMoTa.Text.Trim().ToString() + "',GhiChu=N'" + txtGhiChu.Text.Trim().ToString() + "'WHERE MaSP=N'" + txtMaSP.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
                btnBoQua.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblSanPham WHERE MaSP=N'" + txtMaSP.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            cboMaLoaiSP.Enabled = false;
            txtThuongHieu.Enabled = false;
            txtTheTich.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonViTinh.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            dtpNgayNhap.Enabled = false;
            txtMoTa.Enabled = false;
            txtGhiChu.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnTKiem_Click(object sender, EventArgs e)
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
            sql = "SELECT * from tblSanPham WHERE 1=1";
            if (cboTim.SelectedItem.ToString() == "Mã sản phẩm")
                sql += " AND MaSP LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Tên sản phẩm")
                sql += " AND TenSP LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Loại sản phẩm")
                sql += " AND MaLoaiSP LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Thương hiệu")
                sql += " AND ThuongHieu LIKE N'%" + txtTim.Text + "%'";
            tblSP = Functions.GetDataToTable(sql);
            if (tblSP.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblSP.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvSanPham.DataSource = tblSP;
            ResetValues();
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaSP, TenSP, MaLoaiSP, ThuongHieu, TheTich, SoLuong, DonGiaNhap, DonGiaBan, DonViTinh, NgayNhap, Mota, GhiChu FROM tblSanPham";
            tblSP = Functions.GetDataToTable(sql);
            dgvSanPham.DataSource = tblSP;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng cửa sổ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void cboMaLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
