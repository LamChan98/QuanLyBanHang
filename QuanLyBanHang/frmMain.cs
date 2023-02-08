using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Classs.Functions.Connect();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Classs.Functions.Disconnect();
            Application.Exit();
        }

        private void mnuDanhMuc_Click(object sender, EventArgs e)
        {

        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham frm = new frmSanPham();
            frm.ShowDialog();
        }

        private void mnuLoaiSanPham_Click(object sender, EventArgs e)
        {
            frmLoaiSP frm = new frmLoaiSP();
            frm.ShowDialog();
        }

        private void mnuTKND_Click(object sender, EventArgs e)
        {
            frmTaiKhoan frm = new frmTaiKhoan();
            frm.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.ShowDialog();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            frm.ShowDialog();
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            frmTimKiemHoaDon frm = new frmTimKiemHoaDon();
            frm.ShowDialog();
        }

        private void mnuTonKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoTonKho frm = new frmBaoCaoTonKho();
            frm.ShowDialog();
        }

        private void tồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTonKho frm = new frmTonKho();
            frm.ShowDialog();
        }

        private void mnuDoanhThu_Click(object sender, EventArgs e)
        {
            frmBaoCaoDoanhThu frm = new frmBaoCaoDoanhThu();
            frm.ShowDialog();
        }

        private void giaoDịchKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGiaoDich frm = new frmGiaoDich();
            frm.ShowDialog();
        }

        private void nợPhảiThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNoPhaiThu frm = new frmNoPhaiThu();
            frm.ShowDialog();
        }

        private void mnuFindKhachHang_Click(object sender, EventArgs e)
        {
            frmTimKiemKhachHang frm = new frmTimKiemKhachHang();
            frm.ShowDialog();
        }

        private void mnuFindSanPham_Click(object sender, EventArgs e)
        {
            frmTimKiemSanPham frm = new frmTimKiemSanPham();
            frm.ShowDialog();
        }

        private void backupDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackup frm = new frmBackup();
            frm.ShowDialog();
        }
    }
}
