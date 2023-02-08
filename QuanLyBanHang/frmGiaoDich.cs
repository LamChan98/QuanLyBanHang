using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Classs;

namespace QuanLyBanHang
{
    public partial class frmGiaoDich : Form
    {
        DataTable tblGD;
        public frmGiaoDich()
        {
            InitializeComponent();
        }
        private void frmGiaoDich_Load(object sender, EventArgs e)
        {
            txtMaGD.Enabled = false;
            dtpNgayGiaoDich.Enabled = false;
            txtTienThanhToan.Enabled = false;
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtDTKH.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            ResetValues();
        }
        private void ResetValues()
        {
            txtMaGD.Text = "";
            dtpNgayGiaoDich.Value = DateTime.Now;
            txtTienThanhToan.Text = "0";
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDTKH.Text = "";
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaGD, NgayGD, b.MaKH, b.TenKH, b.SDTKH , TienThanhToan,GhiChu FROM tblGiaoDich as a join tblKhachHang as b on a.MaKH=b.MaKH ";
            tblGD = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvGiaoDich.DataSource = tblGD; //Nguồn dữ liệu            
            dgvGiaoDich.Columns[0].HeaderText = "Mã Giao Dịch";
            dgvGiaoDich.Columns[1].HeaderText = "Ngày Giao Dịch";
            dgvGiaoDich.Columns[2].HeaderText = "Mã Khách Hàng";
            dgvGiaoDich.Columns[3].HeaderText = "Tên Khách Hàng";
            dgvGiaoDich.Columns[4].HeaderText = "SDT Khách Hàng";
            dgvGiaoDich.Columns[5].HeaderText = "Tiền Thanh Toán";
            dgvGiaoDich.Columns[6].HeaderText = "Ghi Chú";

            //canh lề phải cho Datagridview
            dgvGiaoDich.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Thêm đơn vị "đồng" vào datagridview
            dgvGiaoDich.Columns[5].DefaultCellStyle.Format = "c2";
            dgvGiaoDich.Columns[5].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi");

            dgvGiaoDich.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvGiaoDich.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaGD.Enabled = true;
            dtpNgayGiaoDich.Enabled = true;
            txtTienThanhToan.Enabled = true;
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = false;
            txtDTKH.Enabled = false;
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValues();
            string chuoi1 = "";
            int chuoi2 = 0;
            chuoi1 = Convert.ToString(Functions.GetFieldValues("SELECT TOP 1 MaGD FROM tblGiaoDich order by MaGD desc"));
            chuoi2 = Convert.ToInt32(chuoi1.Remove(0, 2));
            if (chuoi2 + 1 < 10)
            {
                txtMaGD.Text = "GD00" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 100)
            {
                txtMaGD.Text = "GD0" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 1000)
            {
                txtMaGD.Text = "GD" + (chuoi2 + 1).ToString();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaGD.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã giao dịch", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaGD.Focus();
                return;
            }
            if (txtMaKH.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTienThanhToan.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tiền thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = "Select MaGD From tblGiaoDich where MaGD=N'" + txtMaGD.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã giao dịch này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "INSERT INTO tblGiaoDich VALUES(N'" +
                txtMaGD.Text + "','" + txtMaKH.Text + "','" + dtpNgayGiaoDich.Value + "','" + txtTienThanhToan.Text + "','" + txtGhiChu.Text + "')";
                Functions.RunSQL(sql); //Thực hiện câu lệnh sql
                sql = "UPDATE tblKhachHang SET NoPhaiThu = NoPhaiThu-'" + txtTienThanhToan.Text + "' WHERE MaKH = N'" + txtMaKH.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblGD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaGD.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập mã giao dịch", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTienThanhToan.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tiền thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "UPDATE tblGiaoDich SET MaGD= '" + txtMaGD.Text + "', MaKH= '" + txtMaKH.Text + "', NgayGD= '" + dtpNgayGiaoDich.Value + "', TienThanhToan= '" + txtTienThanhToan.Text + "', GhiChu= '" + txtGhiChu.Text + "'" +
                    "WHERE MaGD=N'" + txtMaGD.Text + "'";
                Functions.RunSQL(sql);
                
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblGD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaGD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblGiaoDich WHERE MaGD=N'" + txtMaGD.Text + "'";
                Functions.RunSQL(sql);
                sql = "UPDATE tblKhachHang SET NoPhaiThu = NoPhaiThu+'" + txtTienThanhToan.Text + "' WHERE MaKH = N'" + txtMaKH.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
           
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtMaGD.Enabled = false;
            dtpNgayGiaoDich.Enabled = false;
            txtTienThanhToan.Enabled = false;
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtDTKH.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng cửa sổ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void dgvGiaoDich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tblGD.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaGD.Text =
        dgvGiaoDich.CurrentRow.Cells["MaGD"].Value.ToString();
            dtpNgayGiaoDich.Value = (DateTime)dgvGiaoDich.CurrentRow.Cells["NgayGD"].Value;
            txtMaKH.Text =
       dgvGiaoDich.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text =
       dgvGiaoDich.CurrentRow.Cells["TenKH"].Value.ToString();
            txtDTKH.Text =
       dgvGiaoDich.CurrentRow.Cells["SDTKH"].Value.ToString();
            txtTienThanhToan.Text =
       dgvGiaoDich.CurrentRow.Cells["TienThanhToan"].Value.ToString();
            txtGhiChu.Text =
       dgvGiaoDich.CurrentRow.Cells["GhiChu"].Value.ToString();
            txtMaGD.Enabled = true;
            dtpNgayGiaoDich.Enabled = true;
            txtTienThanhToan.Enabled = true;
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = false;
            txtDTKH.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
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
            sql = "SELECT MaGD, NgayGD, b.MaKH, b.TenKH, b.SDTKH , TienThanhToan,GhiChu FROM tblGiaoDich as a join tblKhachHang as b on a.MaKH=b.MaKH WHERE 1=1";
            if (cboTim.SelectedItem.ToString() == "Mã giao dịch")
                sql += " AND MaGD LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Ngày giao dịch")
                sql += " AND NgayGD LIKE N'%" + txtTim.Text + "%'";
            if (cboTim.SelectedItem.ToString() == "Mã khách hàng")
                sql += " AND MaKH LIKE N'%" + txtTim.Text + "%'";

            tblGD = Functions.GetDataToTable(sql);
            if (tblGD.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblGD.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvGiaoDich.DataSource = tblGD;
            ResetValues();
        }
    }
}
