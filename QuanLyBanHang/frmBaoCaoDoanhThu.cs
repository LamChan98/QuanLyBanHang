using QuanLyBanHang.Classs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMExcel = Microsoft.Office.Interop.Excel; 
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        DataTable tblDT;
        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cboTim.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn điều kiện tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sql;
            double tong;
            if (cboTim.SelectedItem.ToString() == "Ngày") 
            {
                sql = "SELECT * from tblHoaDonBan WHERE NgayLap='" + dtpNgay.Value + "'";
                tblDT = Functions.GetDataToTable(sql);
                if (tblDT.Rows.Count != 0)
                {
                    tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE NgayLap = '" + dtpNgay.Value + "'"));
                    txtTongDoanhThu.Text = tong.ToString();
                }
            }

            if (cboTim.SelectedItem.ToString() == "Tháng")
            {
                sql = "SELECT * from tblHoaDonBan WHERE MONTH(NgayLap)='" + txtThang.Text + "' AND YEAR(NgayLap)='"+txtThangNam.Text+"'";
                tblDT = Functions.GetDataToTable(sql);
                if (tblDT.Rows.Count != 0)
                {
                    tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE MONTH(NgayLap)='" + txtThang.Text + "' AND YEAR(NgayLap)='" + txtThangNam.Text + "'"));
                    txtTongDoanhThu.Text = tong.ToString();
                }
            }
            
            if (cboTim.SelectedItem.ToString() == "Quý")
            {
                if (cboQuy.SelectedItem.ToString() == "Quý 1")
                {
                    sql = "SELECT * from tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 1 AND 3 AND YEAR(NgayLap)='" + txtNam.Text + "'";
                    tblDT = Functions.GetDataToTable(sql);
                    if (tblDT.Rows.Count != 0)
                    {
                        tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 1 AND 3 AND YEAR(NgayLap)='" + txtNam.Text + "'"));
                        txtTongDoanhThu.Text = tong.ToString();
                    }
                }
                
                if (cboQuy.SelectedItem.ToString() == "Quý 2")
                {
                    sql = "SELECT * from tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 4 AND 6 AND YEAR(NgayLap)='" + txtNam.Text + "'";
                    tblDT = Functions.GetDataToTable(sql);
                    if (tblDT.Rows.Count != 0)
                    {
                        tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 4 AND 6 AND YEAR(NgayLap)='" + txtNam.Text + "'"));
                        txtTongDoanhThu.Text = tong.ToString();
                    }
                }

                if (cboQuy.SelectedItem.ToString() == "Quý 3")
                {
                    sql = "SELECT * from tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 7 AND 9 AND YEAR(NgayLap)='" + txtNam.Text + "'";
                    tblDT = Functions.GetDataToTable(sql);
                    if (tblDT.Rows.Count != 0)
                    {
                        tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 7 AND 9 AND YEAR(NgayLap)='" + txtNam.Text + "'"));
                        txtTongDoanhThu.Text = tong.ToString();
                    }
                }

                if (cboQuy.SelectedItem.ToString() == "Quý 4")
                {
                    sql = "SELECT * from tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 10 AND 12 AND YEAR(NgayLap)='" + txtNam.Text + "'";
                    tblDT = Functions.GetDataToTable(sql);
                    if (tblDT.Rows.Count != 0)
                    {
                        tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE MONTH(NgayLap) BETWEEN 10 AND 12 AND YEAR(NgayLap)='" + txtNam.Text + "'"));
                        txtTongDoanhThu.Text = tong.ToString();
                    }
                }
            }

            if (cboTim.SelectedItem.ToString() == "Khác")
            {
                sql = "SELECT * from tblHoaDonBan WHERE NgayLap BETWEEN'" + dtpTuNgay.Value + "' AND '"+dtpDenNgay.Value+"'";
                tblDT = Functions.GetDataToTable(sql);
                if (tblDT.Rows.Count != 0)
                {
                    tong = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE NgayLap BETWEEN'" + dtpTuNgay.Value + "' AND '" + dtpDenNgay.Value + "'"));
                    txtTongDoanhThu.Text = tong.ToString();
                }
            }

            if (tblDT.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblDT.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDoanhThu.DataSource = tblDT;
            dgvDoanhThu.Columns[0].HeaderText = "Mã hóa đơn";
            dgvDoanhThu.Columns[1].HeaderText = "Mã nhân viên";
            dgvDoanhThu.Columns[2].HeaderText = "Ngày tạo";
            dgvDoanhThu.Columns[3].HeaderText = "Mã khách hàng";
            dgvDoanhThu.Columns[4].HeaderText = "Tổng tiền";

            btnIn.Enabled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cboTim.SelectedItem.ToString() == "Khác")
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
                exRange.Range["C2:E2"].Value = "BÁO CÁO DOANH THU";
            
                //Lấy thông tin các mặt hàng
                sql = "SELECT * from tblHoaDonBan WHERE NgayLap BETWEEN'" + dtpTuNgay.Value + "' AND '" + dtpDenNgay.Value + "'";
                tblThongtinHang = Functions.GetDataToTable(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A6:F6"].Font.Bold = true;
                exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C6:F6"].ColumnWidth = 14;
                exRange.Range["A6:A6"].Value = "STT";
                exRange.Range["B6:B6"].Value = "Mã hóa đơn";
                exRange.Range["C6:C6"].Value = "Mã nhân viên";
                exRange.Range["D6:D6"].Value = "Ngày bán";
                exRange.Range["E6:E6"].Value = "Mã khách hàng";
                exRange.Range["F6:F6"].Value = "Tổng tiền";
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
                //Đóng khung bảng
                //exRange.Range["A11:F14"].Cells.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
                exRange = exSheet.Cells[cot][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = "Tổng cộng:";
                exRange = exSheet.Cells[cot + 1][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE NgayLap BETWEEN'" + dtpTuNgay.Value + "' AND '" + dtpDenNgay.Value + "'"));
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
                exSheet.Name = "Báo cáo doanh thu";
                exApp.Visible = true;
            }
            if (cboTim.SelectedItem.ToString() == "Ngày")
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
                exRange.Range["C2:E2"].Value = "BÁO CÁO DOANH THU";

                //Lấy thông tin các mặt hàng
                sql = "SELECT * from tblHoaDonBan WHERE NgayLap='" + dtpNgay.Value + "'";
                tblThongtinHang = Functions.GetDataToTable(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A6:F6"].Font.Bold = true;
                exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C6:F6"].ColumnWidth = 14;
                exRange.Range["A6:A6"].Value = "STT";
                exRange.Range["B6:B6"].Value = "Mã hóa đơn";
                exRange.Range["C6:C6"].Value = "Mã nhân viên";
                exRange.Range["D6:D6"].Value = "Ngày bán";
                exRange.Range["E6:E6"].Value = "Mã khách hàng";
                exRange.Range["F6:F6"].Value = "Tổng tiền";
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
                //Đóng khung bảng
                //exRange.Range["A11:F14"].Cells.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
                exRange = exSheet.Cells[cot][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = "Tổng cộng:";
                exRange = exSheet.Cells[cot + 1][hang + 9];
                exRange.Font.Bold = true;
                exRange.Value2 = Convert.ToDouble(Functions.GetFieldValues("SELECT SUM(TongTien) FROM tblHoaDonBan WHERE NgayLap = '" + dtpNgay.Value + "'"));
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
                exSheet.Name = "Báo cáo doanh thu";
                exApp.Visible = true;
            }

            btnIn.Enabled = false;
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            btnIn.Enabled = false;
            dtpNgay.Visible = false;
            txtThang.Visible = false;
            lblThangNam.Visible = false;
            txtThangNam.Visible = false;
            cboQuy.Visible = false;
            lblNam.Visible = false;
            txtNam.Visible = false;
            lblTuNgay.Visible = false;
            dtpTuNgay.Visible = false;
            lblDenNgay.Visible = false;
            dtpDenNgay.Visible = false;
        }

        private void cboTim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTim.SelectedItem.ToString() =="Ngày")
            {
                dtpNgay.Visible = true;
                txtThang.Visible = false;
                lblThangNam.Visible = false;
                txtThangNam.Visible = false;
                cboQuy.Visible = false;
                lblNam.Visible = false;
                txtNam.Visible = false;
                lblTuNgay.Visible = false;
                dtpTuNgay.Visible = false;
                lblDenNgay.Visible = false;
                dtpDenNgay.Visible = false;
            }
            if (cboTim.SelectedItem.ToString() == "Tháng")
            {
                dtpNgay.Visible = false;
                txtThang.Visible = true;
                lblThangNam.Visible = true;
                txtThangNam.Visible = true;
                cboQuy.Visible = false;
                lblNam.Visible = false;
                txtNam.Visible = false;
                lblTuNgay.Visible = false;
                dtpTuNgay.Visible = false;
                lblDenNgay.Visible = false;
                dtpDenNgay.Visible = false;
            }
            if (cboTim.SelectedItem.ToString() == "Quý")
            {
                dtpNgay.Visible = false;
                txtThang.Visible = false;
                lblThangNam.Visible = false;
                txtThangNam.Visible = false;
                cboQuy.Visible = true;
                lblNam.Visible = true;
                txtNam.Visible = true;
                lblTuNgay.Visible = false;
                dtpTuNgay.Visible = false;
                lblDenNgay.Visible = false;
                dtpDenNgay.Visible = false;
            }
            if (cboTim.SelectedItem.ToString() == "Khác")
            {
                dtpNgay.Visible = false;
                txtThang.Visible = false;
                lblThangNam.Visible = false;
                txtThangNam.Visible = false;
                cboQuy.Visible = false;
                lblNam.Visible = false;
                txtNam.Visible = false;
                lblTuNgay.Visible = true;
                dtpTuNgay.Visible = true;
                lblDenNgay.Visible = true;
                dtpDenNgay.Visible = true;
            }
        }
    }
}
