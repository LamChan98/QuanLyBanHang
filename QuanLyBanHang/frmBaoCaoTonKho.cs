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
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanHang
{
    public partial class frmBaoCaoTonKho : Form
    {
        DataTable tblTK;
        public frmBaoCaoTonKho()
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
            sql = "SELECT b.MaSP, b.TenSP, NgayNhapHang, b.DonViTinh, SoLuongTK FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP";
            tblTK = Classs.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvTonKho.DataSource = tblTK; //Nguồn dữ liệu            
            dgvTonKho.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvTonKho.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvTonKho.Columns[2].HeaderText = "Ngày nhập hàng";
            dgvTonKho.Columns[3].HeaderText = "Đơn vị tính";
            dgvTonKho.Columns[4].HeaderText = "Số lượng";

            dgvTonKho.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            sql = "SELECT * from tblTonKho WHERE MaSP like'%"+txtTim.Text+"%'";
            tblTK = Functions.GetDataToTable(sql);
            if (tblTK.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblTK.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTonKho.DataSource = tblTK;
            ResetValue();
            btnIn.Enabled = true;
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT b.MaSP, b.TenSP, SoLuongTK, NgayNhapHang FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP";
            tblTK = Functions.GetDataToTable(sql);
            dgvTonKho.DataSource = tblTK;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
            // Định dạng chung
            exRange = (COMExcel.Range)exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa hàng sơn JOTUN";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Trần Phú - Quận 5";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)38526419";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 1; //Màu đen
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "BÁO CÁO TỒN KHO";
            // Biểu diễn thông tin chung của hóa đơn bán
            /*sql = "SELECT a.MaHDB, a.NgayLap, a.TongTien, b.TenKH, b.DiaChiKH, b.SDTKH, c.TenNV FROM tblHoaDonBan AS a, tblKhachHang AS b, tblNhanVien AS c WHERE a.MaHDB = N'" + txtTongDoanhThu.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            exRange.Range["C9:E9"].Cells.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;*/
            if (txtTim.Text != "")
            {
                //Lấy thông tin các mặt hàng
                sql = "SELECT a.MaSP, b.TenSP, a.NgayNhapHang, b.DonViTinh, a.SoLuongTK from tblTonKho as a join tblSanPham as b on a.MaSP= b.MaSP WHERE a.MaSP='" + txtTim.Text + "'";
                tblThongtinHang = Functions.GetDataToTable(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A6:F6"].Font.Bold = true;
                exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C6:F6"].ColumnWidth = 20;
                exRange.Range["A6:A6"].Value = "STT";
                exRange.Range["B6:B6"].Value = "Mã sản phẩm";
                exRange.Range["C6:C6"].Value = "Tên sản phẩm";
                exRange.Range["D6:D6"].Value = "Ngày nhập hàng";
                exRange.Range["E6:E6"].Value = "Đơn vị tính";
                exRange.Range["F6:F6"].Value = "Số lượng";
                for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 7] = hang + 1;
                    for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 7] = tblThongtinHang.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 7] = tblThongtinHang.Rows[hang][cot].ToString();
                    }
                }
            } else
            {
                //Lấy thông tin các mặt hàng
                sql = "SELECT b.MaSP, b.TenSP, NgayNhapHang, b.DonViTinh, SoLuongTK FROM tblTonKho as a join tblSanPham as b on a.MaSP=b.MaSP";
                tblThongtinHang = Functions.GetDataToTable(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A6:F6"].Font.Bold = true;
                exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C6:F6"].ColumnWidth = 20;
                exRange.Range["A6:A6"].Value = "STT";
                exRange.Range["B6:B6"].Value = "Mã sản phẩm";
                exRange.Range["C6:C6"].Value = "Tên sản phẩm";
                exRange.Range["D6:D6"].Value = "Ngày nhập hàng";
                exRange.Range["E6:E6"].Value = "Đơn vị tính";
                exRange.Range["F6:F6"].Value = "Số lượng";
                for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 7] = hang + 1;
                    for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 7] = tblThongtinHang.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 7] = tblThongtinHang.Rows[hang][cot].ToString();
                    }
                }
            }
            
            //Đóng khung bảng
            //exRange.Range["A11:F14"].Cells.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
            exRange = exSheet.Cells[cot][hang + 9];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng cộng:";
            if (txtTim.Text != "")
            {
                exRange = exSheet.Cells[cot + 1][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(SoLuongTK) FROM tblTonKho WHERE MaSP = '" + txtTim.Text + "'"));
            } else
            {
                exRange = exSheet.Cells[cot + 1][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(SoLuongTK) FROM tblTonKho"));
            }
            exRange = exSheet.Cells[1][hang + 10]; //Ô A1 
            exRange.Range["D1:F1"].MergeCells = true;
            exRange.Range["D1:F1"].Font.Italic = true;
            exRange.Range["D1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["D1:F1"].Value = "Thành Phố Hồ Chí Minh, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["D2:F2"].MergeCells = true;
            exRange.Range["D2:F2"].Font.Italic = true;
            exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D2:F2"].Value = "Người báo cáo";
            exRange.Range["D6:F6"].MergeCells = true;
            exRange.Range["D6:F6"].Font.Italic = true;
            exRange.Range["D6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D6:F6"].Value = "(Ký tên)";
            exSheet.Name = "Báo cáo tồn kho";
            exApp.Visible = true;

            btnIn.Enabled = false;
        }
    }
    }

