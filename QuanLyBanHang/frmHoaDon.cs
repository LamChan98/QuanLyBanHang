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
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace QuanLyBanHang
{
    public partial class frmHoaDon : Form
    {
        DataTable tblChiTietHoaDonBan; //Bảng chi tiết hóa đơn
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;
            txtMaHD.Enabled = false;
            dtpNgayBan.Enabled = false;
            cboMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            cboMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            mtbDienThoai.Enabled = false;
            cboMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            txtDonGia.Enabled = false;
            txtVAT.Enabled = false;
            txtSoLuong.Enabled = false;
            txtGiamGia.Enabled = false;
            txtThanhTien.Enabled = false;
            txtTong.Enabled = false;
            txtGiamGia.Text = "0";
            txtVAT.Text = "0";
            txtTong.Text = "0";
            Functions.FillCombo("SELECT MaKH, TenKH FROM tblKhachHang", cboMaKH, "MaKH", "MaKH");
            cboMaKH.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaSP, TenSP FROM tblSanPham", cboMaSP, "MaSP", "MaSP");
            cboMaSP.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHD.Text != "")
            {
                LoadInfoHoaDon();
                btnHuyHD.Enabled = true;
                btnInHD.Enabled = true;
            }
            LoadDataGridView();
        }
        //Nạp dữ liệu lên lưới
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.MaSP, b.TenSP, a.SoLuong, b.DonGiaBan, a.GiamGia, a.VAT, a.ThanhTien FROM tblChiTietHoaDonBan AS a, " +
                "tblSanPham AS b WHERE a.MaHDB = N'" + txtMaHD.Text + "' AND a.MaSP=b.MaSP";
            tblChiTietHoaDonBan = Functions.GetDataToTable(sql);
            dgvHDBanHang.DataSource = tblChiTietHoaDonBan;
            dgvHDBanHang.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvHDBanHang.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvHDBanHang.Columns[2].HeaderText = "Số lượng";
            dgvHDBanHang.Columns[3].HeaderText = "Đơn giá";
            dgvHDBanHang.Columns[4].HeaderText = "Giảm giá %";
            dgvHDBanHang.Columns[5].HeaderText = "VAT";
            dgvHDBanHang.Columns[6].HeaderText = "Thành tiền";
            /*dgvHDBanHang.Columns[0].Width = 100;
            dgvHDBanHang.Columns[1].Width = 150;
            dgvHDBanHang.Columns[2].Width = 150;
            dgvHDBanHang.Columns[3].Width = 150;
            dgvHDBanHang.Columns[4].Width = 100;
            dgvHDBanHang.Columns[5].Width = 150;*/
            //Canh lề phải số của DataGridView
            dgvHDBanHang.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHDBanHang.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHDBanHang.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHDBanHang.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHDBanHang.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Thêm đơn vị "đồng" vào datagridview
            dgvHDBanHang.Columns[3].DefaultCellStyle.Format = "c2";
            dgvHDBanHang.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi");
            dgvHDBanHang.Columns[6].DefaultCellStyle.Format = "c2";
            dgvHDBanHang.Columns[6].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi");

            dgvHDBanHang.AllowUserToAddRows = false;
            dgvHDBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NgayLap FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
            dtpNgayBan.Value = DateTime.Parse(Functions.GetFieldValues(str));
            str = "SELECT MaNV FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
            cboMaNV.Text = Functions.GetFieldValues(str);
            str = "SELECT MaKH FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
            cboMaKH.Text = Functions.GetFieldValues(str);
            str = "SELECT TongTien FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
            txtTong.Text = Functions.GetFieldValues(str);
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(Double.Parse(txtTong.Text));
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            btnHuyHD.Enabled = false;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            btnThemHD.Enabled = false;

            txtMaHD.Enabled = true;
            dtpNgayBan.Enabled = true;
            cboMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            cboMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtDiaChi.Enabled = true;
            mtbDienThoai.Enabled = true;
            cboMaSP.Enabled = true;
            txtTenSP.Enabled = true;
            txtDonGia.Enabled = true;
            txtVAT.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiamGia.Enabled = true;
            txtThanhTien.Enabled = true;
            txtTong.Enabled = true;
            ResetValues();
            LoadDataGridView();
            string chuoi1 = "";
            int chuoi2 = 0;
            chuoi1 = Convert.ToString(Functions.GetFieldValues("SELECT TOP 1 MaHDB FROM tblHoaDonBan order by MaHDB desc"));
            chuoi2 = Convert.ToInt32(chuoi1.Remove(0, 2));
            if (chuoi2 + 1 < 10)
            {
                txtMaHD.Text = "HD00" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 100)
            {
                txtMaHD.Text = "HD0" + (chuoi2 + 1).ToString();
            }
            else if (chuoi2 + 1 < 1000)
            {
                txtMaHD.Text = "HD" + (chuoi2 + 1).ToString();
            }
        }

        private void ResetValues()
        {
            txtMaHD.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboMaNV.Text = "";
            cboMaKH.Text = "";
            txtTong.Text = "0";
            lblBangChu.Text = "Bằng chữ: ";
            cboMaSP.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtVAT.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi, slmua, a;
            sql = "SELECT MaHDB FROM tblHoaDonBan WHERE MaHDB=N'" + txtMaHD.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                /*if (txtNgayBan.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNgayBan.Focus();
                    return;
                }*/
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKH.Focus();
                    return;
                }
                sql = "INSERT INTO tblHoaDonBan(MaHDB, NgayLap, MaNV, MaKH, TongTien) VALUES (N'" + txtMaHD.Text.Trim() + "','" +
                        dtpNgayBan.Value + "',N'" + cboMaNV.SelectedValue + "',N'" +
                        cboMaKH.SelectedValue + "'," + txtTong.Text + ")";
                Functions.RunSQL(sql);
            }
            // Lưu thông tin của các mặt hàng
            if (cboMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaSP.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MaSP FROM tblChiTietHoaDonBan WHERE MaSP=N'" + cboMaSP.SelectedValue + "' AND MaHDB = N'" + txtMaHD.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboMaSP.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?

            //sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblSanPham WHERE MaSP = N'" + cboMaSP.SelectedValue + "'"));
   
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT Sum(SoLuongTK) FROM tblTonKho WHERE MaSP = N'" + cboMaSP.SelectedValue + "'"));
            
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
                sql = "INSERT INTO tblChiTietHoaDonBan(MaHDB,MaSP,SoLuong,DonGia, GiamGia, VAT, ThanhTien) VALUES(N'" + txtMaHD.Text.Trim() + "',N'" + cboMaSP.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtGiamGia.Text + "," + txtVAT.Text + "," + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            //SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            //sql = "UPDATE tblSanPham SET SoLuong =" + SLcon + " WHERE MaSP= N'" + cboMaSP.SelectedValue + "'";
            slmua = Convert.ToDouble(txtSoLuong.Text);
            a = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuongTK FROM tblTonKho WHERE MaSP = N'" + cboMaSP.SelectedValue + "'and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + cboMaSP.SelectedValue + "')"));
            SLcon = a - slmua;
            while (SLcon < 0)
            {
                sql = "DELETE FROM tblTonKho WHERE MaSP = N'" + cboMaSP.SelectedValue + "'and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + cboMaSP.SelectedValue + "')";
                Functions.RunSQL(sql);
                a = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuongTK FROM tblTonKho WHERE MaSP = N'" + cboMaSP.SelectedValue + "'and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + cboMaSP.SelectedValue + "')"));
                SLcon = a + SLcon;
            }
            sql = "UPDATE tblTonKho SET SoLuongTK =" + SLcon + " WHERE MaSP= N'" + cboMaSP.SelectedValue + "' " +
                "and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + cboMaSP.SelectedValue + "')";

            Functions.RunSQL(sql);
           
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'"));
            //Tongmoi = (tong + Convert.ToDouble(txtThanhTien.Text)) + ((tong + Convert.ToDouble(txtThanhTien.Text)) * Convert.ToDouble(txtVAT.Text) / 100);
            Tongmoi = (tong + Convert.ToDouble(txtThanhTien.Text))+((tong + Convert.ToDouble(txtThanhTien.Text))*Convert.ToDouble(txtVAT.Text)/100);
            sql = "UPDATE tblHoaDonBan SET TongTien =" + Tongmoi + " WHERE MaHDB = N'" + txtMaHD.Text + "'";
            Functions.RunSQL(sql);

            txtTong.Text = Tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(Double.Parse(Tongmoi.ToString()));

            //Cập nhật nợ phải thu
            sql = "UPDATE tblKhachHang SET NoPhaiThu = NoPhaiThu+'" + txtThanhTien.Text +"' WHERE MaKH = N'" + cboMaKH.Text + "'";
            Functions.RunSQL(sql);

            ResetValuesHang();
            btnHuyHD.Enabled = true;
            btnThemHD.Enabled = true;
            btnInHD.Enabled = true;
        }
        private void ResetValuesHang()
        {
            cboMaSP.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaSP.Text == "")
            {
                txtTenSP.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TenSP FROM tblSanPham WHERE MaSP =N'" + cboMaSP.SelectedValue + "'";
            txtTenSP.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM tblSanPham WHERE MaSP =N'" + cboMaSP.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                mtbDienThoai.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKH from tblKhachHang where MaKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = Functions.GetFieldValues(str);
            str = "Select DiaChiKH from tblKhachHang where MaKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select SDTKH from tblKhachHang where MaKH= N'" + cboMaKH.SelectedValue + "'";
            mtbDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg, vat;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtVAT.Text == "")
                vat = 0;
            else
                vat = Convert.ToDouble(txtVAT.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - (sl * dg * gg / 100) + sl * dg * vat / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg, vat;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtVAT.Text == "")
                vat = 0;
            else
                vat = Convert.ToDouble(txtVAT.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - (sl * dg * gg / 100) + sl * dg * vat / 100;
            txtThanhTien.Text = tt.ToString();
        }
        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg, vat;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtVAT.Text == "")
                vat = 0;
            else
                vat = Convert.ToDouble(txtVAT.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - (sl * dg * gg / 100) + sl * dg * vat / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cboMaHD_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHDB FROM tblHoaDonBan", cboMaHD, "MaHDB", "MaHDB");
            cboMaHD.SelectedIndex = -1;
        }
        

        private void btnInHD_Click(object sender, EventArgs e)
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
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDB, a.NgayLap, a.TongTien, b.TenKH, b.DiaChiKH, b.SDTKH, c.TenNV FROM tblHoaDonBan AS a, tblKhachHang AS b, tblNhanVien AS c WHERE a.MaHDB = N'" + txtMaHD.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
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
            exRange.Range["C9:E9"].Cells.HorizontalAlignment= COMExcel.XlHAlign.xlHAlignLeft;
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenSP, a.SoLuong, b.DonGiaBan, a.GiamGia, a.VAT, a.ThanhTien " +
                  "FROM tblChiTietHoaDonBan AS a , tblSanPham AS b WHERE a.MaHDB = N'" +
                  txtMaHD.Text + "' AND a.MaSP = b.MaSP";
            tblThongtinHang = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 20;
            //exRange.Range["B11:B11"].Columns.AutoFit();
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "VAT";
            exRange.Range["G11:G11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            //Đóng khung bảng
            //exRange.Cells[1][hang+11].Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(Double.Parse(tblThongtinHD.Rows[0][2].ToString()));
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Thành Phố Hồ Chí Minh, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn bán hàng";
            exApp.Visible = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đóng cửa sổ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaSP,SoLuong FROM tblChiTietHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
                DataTable tblHang = Functions.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuongTK FROM tblTonKho WHERE MaSP = N'" + tblHang.Rows[hang][0].ToString() + "' " +
                "and NgayNhapHang = (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + tblHang.Rows[hang][0].ToString() + "')"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    
                    //sql = "UPDATE tblSanPham SET SoLuong =" + slcon + " WHERE MaSP= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    
                    sql = "UPDATE tblTonKho SET SoLuongTK = " + slcon + " WHERE MaSP = N'" + tblHang.Rows[hang][0].ToString() + "' " +
                "and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + tblHang.Rows[hang][0].ToString() + "')";
                    Functions.RunSQL(sql);
                }

                //Cập nhật nợ phải thu
                sql = "UPDATE tblKhachHang SET NoPhaiThu = NoPhaiThu -" + txtTong.Text + " WHERE MaKH = N'" + cboMaKH.Text + "'";
                Functions.RunSQL(sql);

                //Xóa chi tiết hóa đơn
                sql = "DELETE tblChiTietHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
                Functions.RunSQL(sql);

                //Xóa hóa đơn
                sql = "DELETE tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'";
                Functions.RunSQL(sql);
                ResetValues();
                LoadDataGridView();
                btnHuyHD.Enabled = false;
                btnInHD.Enabled = false;
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
            {
                txtTenKH.Text = "";
            }
            else
            {
                str = "Select TenNV from tblNhanVien where MaNV = N'" + cboMaNV.SelectedValue + "'";
                txtTenNV.Text = Functions.GetFieldValues(str);
            } 
        }

        private void dgvHDBanHang_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (tblChiTietHoaDonBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvHDBanHang.CurrentRow.Cells["MaSP"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvHDBanHang.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvHDBanHang.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE tblChiTietHoaDonBan WHERE MaHDB=N'" + txtMaHD.Text + "' AND MaSP = N'" + MaHangxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng                
                //sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblSanPham WHERE MaSP = N'" + MaHangxoa + "'"));
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuongTK FROM tblTonKho WHERE MaSP = N'" + MaHangxoa + "' " +
                "and NgayNhapHang = (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + MaHangxoa + "')"));
                slcon = sl + SoLuongxoa;

                //sql = "UPDATE tblSanPham SET SoLuong =" + slcon + " WHERE MaSP= N'" + MaHangxoa + "'";

                sql = "UPDATE tblTonKho SET SoLuongTK = " + slcon + " WHERE MaSP = N'" + MaHangxoa + "' " +
                "and NgayNhapHang= (SELECT min(NgayNhapHang) from tblTonKHo WHERE MaSP= N'" + MaHangxoa + "')";
                Functions.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblHoaDonBan WHERE MaHDB = N'" + txtMaHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE tblHoaDonBan SET TongTien =" + tongmoi + " WHERE MaHDB = N'" + txtMaHD.Text + "'";
                Functions.RunSQL(sql);
                txtTong.Text = tongmoi.ToString();
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(tongmoi);
                //Cập nhật nợ phải thu
                sql = "UPDATE tblKhachHang SET NoPhaiThu = NoPhaiThu -" + ThanhTienxoa + " WHERE MaKH = N'" + cboMaKH.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHD.Focus();
                return;
            }
            txtMaHD.Text = cboMaHD.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnHuyHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            cboMaHD.SelectedIndex = -1;
        }

        private void btnLoadSP_Click(object sender, EventArgs e)
        {
            string str;
            if (cboMaSP.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn mã sản phẩm để import", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            str = "SELECT TenSP FROM tblSanPham WHERE MaSP =N'" + cboMaSP.Text + "'";
            txtTenSP.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM tblSanPham WHERE MaSP =N'" + cboMaSP.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void btnLoadKH_Click(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn mã khách hàng để import", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKH from tblKhachHang where MaKH = N'" + cboMaKH.Text + "'";
            txtTenKH.Text = Functions.GetFieldValues(str);
            str = "Select DiaChiKH from tblKhachHang where MaKH = N'" + cboMaKH.Text + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select SDTKH from tblKhachHang where MaKH= N'" + cboMaKH.Text + "'";
            mtbDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void btnXemHD_Click(object sender, EventArgs e)
        {
            frmTimKiemHoaDon frm = new frmTimKiemHoaDon();
            frm.Show();
        }

        private void btnXemSP_Click(object sender, EventArgs e)
        {
            frmTimKiemSanPham frm = new frmTimKiemSanPham();
            frm.Show();
        }

        private void btnXemKH_Click(object sender, EventArgs e)
        {
            frmTimKiemKhachHang frm = new frmTimKiemKhachHang();
            frm.Show();
        }
    }
}
