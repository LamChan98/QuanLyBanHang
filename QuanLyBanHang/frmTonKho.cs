using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Classs;
using System.Globalization;

namespace QuanLyBanHang
{
    public partial class frmTonKho : Form
    {
        DataTable tblTK;
        public frmTonKho()
        {
            InitializeComponent();
        }

        private void TonKho_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            txtSoLuong.Enabled = false;
            dtpNgayNhapHang.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT b.MaSP, b.TenSP, SoLuongTK, NgayNhapHang FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP";
            tblTK = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvTonKho.DataSource = tblTK; //Nguồn dữ liệu            
            dgvTonKho.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvTonKho.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvTonKho.Columns[2].HeaderText = "Số lượng";
            dgvTonKho.Columns[3].HeaderText = "Ngày nhập hàng";

            dgvTonKho.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvTonKho.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void ResetValue()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "0";
            dtpNgayNhapHang.Value = DateTime.Now;
        }

        private void btnXemDS_Click(object sender, EventArgs e)
        {
            frmSanPham frm = new frmSanPham();
            frm.Show();
        }

        private void dgvTonKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tblTK.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaSP.Text =
        dgvTonKho.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text =
        dgvTonKho.CurrentRow.Cells["TenSP"].Value.ToString();
            txtSoLuong.Text =
        dgvTonKho.CurrentRow.Cells["SoLuongTK"].Value.ToString();
            dtpNgayNhapHang.Value = (DateTime)dgvTonKho.CurrentRow.Cells["NgayNhapHang"].Value;
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = false;
            txtSoLuong.Enabled = true;
            dtpNgayNhapHang.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = false;
            txtSoLuong.Enabled = true;
            dtpNgayNhapHang.Enabled = true;
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValue();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaSP.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            
            sql = "Select MaSP From tblTonKho where MaSP=N'" + txtMaSP.Text.Trim() + "' and NgayNhapHang='"+dtpNgayNhapHang.Value+"'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Sản phẩm nhập này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "INSERT INTO tblTonKho VALUES(N'" +
                txtMaSP.Text + "','" + dtpNgayNhapHang.Value + "','" + txtSoLuong.Text + "')";
                Functions.RunSQL(sql); //Thực hiện câu lệnh sql
                LoadDataGridView(); //Nạp lại DataGridView
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblTK.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "UPDATE tblTonKho SET SoLuongTK= '" + txtSoLuong.Text + "' WHERE MaSP=N'" + txtMaSP.Text + "' and NgayNhapHang='" + dtpNgayNhapHang.Value + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();

                btnBoQua.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblTK.Rows.Count == 0)
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
                sql = "DELETE tblTonKho WHERE MaSP=N'" + txtMaSP.Text + "'and NgayNhapHang='" + dtpNgayNhapHang.Value + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            txtSoLuong.Enabled = false;
            dtpNgayNhapHang.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            ResetValue();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng cửa sổ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
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
            sql = "SELECT b.MaSP, b.TenSP, SoLuongTK, NgayNhapHang FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP WHERE b.MaSP='" + txtTim.Text+"'";
            tblTK = Functions.GetDataToTable(sql);
            if (tblTK.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblTK.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTonKho.DataSource = tblTK;
            ResetValue();
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT b.MaSP, b.TenSP, SoLuongTK, NgayNhapHang FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP";
            tblTK = Functions.GetDataToTable(sql);
            dgvTonKho.DataSource = tblTK;
        }
    }
    }

