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
    public partial class frmLoaiSP : Form
    {
        DataTable tblLoaiSP;

        public frmLoaiSP()
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
            ResetValue(); //Xoá trắng các textbox
            txtMaLoaiSP.Enabled = true; //cho phép nhập mới
            txtMaLoaiSP.Focus();
            string chuoi1 = "";
            int chuoi2 = 0;
            chuoi1 = Convert.ToString(Functions.GetFieldValues("SELECT TOP 1 MaLoaiSP FROM tblLoaiSP order by MaLoaiSP desc"));
            chuoi2 = Convert.ToInt32(chuoi1.Remove(0, 1));
            if (chuoi2 + 1 < 10)
            {
                txtMaLoaiSP.Text = "L00" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 100)
            {
                txtMaLoaiSP.Text = "L0" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 1000)
            {
                txtMaLoaiSP.Text = "L" + (chuoi2 + 1).ToString();
            }
        }

        private void ResetValue()
        {
            txtMaLoaiSP.Text = "";
            txtTenLoaiSP.Text = "";
        }

        private void frmLoaiSP_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaLoaiSP, TenLoaiSP FROM tblLoaiSP";
            tblLoaiSP = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvLoaiSanPham.DataSource = tblLoaiSP; //Nguồn dữ liệu            
            dgvLoaiSanPham.Columns[0].HeaderText = "Mã Loại Sản Phẩm";
            dgvLoaiSanPham.Columns[1].HeaderText = "Tên Loại Sản Phẩm";
            dgvLoaiSanPham.Columns[0].Width = 300;
            dgvLoaiSanPham.Columns[1].Width = 300;
            dgvLoaiSanPham.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvLoaiSanPham.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void dgvLoaiSanPham_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiSP.Focus();
                return;
            }
            if (tblLoaiSP.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaLoaiSP.Text = 
        dgvLoaiSanPham.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
            txtTenLoaiSP.Text = 
        dgvLoaiSanPham.CurrentRow.Cells["TenLoaiSP"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void dgvLoaiSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaLoaiSP.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiSP.Focus();
                return;
            }
            if (txtTenLoaiSP.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoaiSP.Focus();
                return;
            }
            sql = "Select MaLoaiSP From tblLoaiSP where MaLoaiSP=N'" + txtMaLoaiSP.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiSP.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "INSERT INTO tblLoaiSP VALUES(N'" +
                txtMaLoaiSP.Text + "',N'" + txtTenLoaiSP.Text + "')";
                Functions.RunSQL(sql); //Thực hiện câu lệnh sql
                LoadDataGridView(); //Nạp lại DataGridView
                ResetValue();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaLoaiSP.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiSP.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenLoaiSP.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn cập nhật bản ghi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "UPDATE tblLoaiSP SET TenLoaiSP=N'" +
                txtTenLoaiSP.Text.ToString() +
                "' WHERE MaLoaiSP=N'" + txtMaLoaiSP.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();

                btnBoQua.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiSP.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblLoaiSP WHERE MaLoaiSP=N'" + txtMaLoaiSP.Text + "'";
                Classs.Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLoaiSP.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaLoaiSP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
