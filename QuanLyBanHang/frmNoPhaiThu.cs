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
    public partial class frmNoPhaiThu : Form
    {
        DataTable tblKH;
        public frmNoPhaiThu()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKH.Enabled = true;
            txtMaKH.Focus();
            txtTenKH.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtDienThoai.Enabled = true;
            txtEmail.Enabled = true;
            txtDiaChi.Enabled = true;
        }
        //Khởi tạo các giá trị trên form
        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtGioiTinh.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblKhachHang WHERE MaKH=N'" + txtMaKH.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có muốn sửa bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "UPDATE tblKhachHang SET NoPhaiThu= '" + txtNoPhaiThu.Text + "' WHERE MaKH=N'" + txtMaKH.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                //ResetValues();
                btnBoQua.Enabled = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT MaKH FROM tblKhachHang WHERE MaKH=N'" + txtMaKH.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Chèn thêm
                //sql = "INSERT INTO tblKhachHang VALUES (N'" + txtMaKH.Text.Trim() +
                //"',N'" + txtTenKH.Text.Trim() + "',N'" + txtGioiTinh.Text.Trim() + "',N'" + txtDienThoai.Text.Trim() + "',N'" + txtEmail.Text.Trim() + "',N'" + txtDiaChi.Text + "','" + dtpNgayTao.Value + "',0)";
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

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtDienThoai.Enabled = false;
            txtEmail.Enabled = false;
            txtDiaChi.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtDienThoai.Enabled = false;
            txtEmail.Enabled = false;
            txtDiaChi.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaKH, TenKH, GioiTinhKH, SDTKH, EmailKH, DiaChiKH, NoPhaiThu FROM tblKhachHang";
            tblKH = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Nguồn dữ liệu            
            dgvKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns[2].HeaderText = "Giới tính";
            dgvKhachHang.Columns[3].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns[4].HeaderText = "Email";
            dgvKhachHang.Columns[5].HeaderText = "Địa Chỉ ";
            dgvKhachHang.Columns[6].HeaderText = "Nợ Phải Thu";

            /*dgvKhachHang.Columns[0].Width = 100;
            dgvKhachHang.Columns[1].Width = 200;
            dgvKhachHang.Columns[2].Width = 250;
            dgvKhachHang.Columns[3].Width = 300;*/
            dgvKhachHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKH"].Value.ToString();
            txtGioiTinh.Text = dgvKhachHang.CurrentRow.Cells["GioiTinhKH"].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells["SDTKH"].Value.ToString();
            txtEmail.Text = dgvKhachHang.CurrentRow.Cells["EmailKH"].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChiKH"].Value.ToString();
            txtNoPhaiThu.Text = dgvKhachHang.CurrentRow.Cells["NoPhaiThu"].Value.ToString();
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtDienThoai.Enabled = true;
            txtEmail.Enabled = true;
            txtDiaChi.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng cửa sổ này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void txtMaKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
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
            sql = "SELECT * from tblKhachHang WHERE 1=1";
            if (cboTim.SelectedItem.ToString() == "Mã khách hàng")
                sql += " AND MaKH LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Tên khách hàng")
                sql += " AND TenKH LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Điện thoại")
                sql += " AND SDTKH LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Email")
                sql += " AND EmailKH LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Địa chỉ")
                sql += " AND DiaChiKH LIKE N'%" + txtTim.Text + "%'";
            tblKH = Functions.GetDataToTable(sql);
            if (tblKH.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblKH.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvKhachHang.DataSource = tblKH;
            ResetValues();
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaKH, TenKH, GioiTinhKH, SDTKH, EmailKH, DiaChiKH, NgayTaoKH FROM tblKhachHang";
            tblKH = Functions.GetDataToTable(sql);
            dgvKhachHang.DataSource = tblKH;
        }
    }
}
