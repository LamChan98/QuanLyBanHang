namespace QuanLyBanHang
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuTepTin = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoaiSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTKND = new System.Windows.Forms.ToolStripMenuItem();
            this.tồnKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDonBan = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nợPhảiThuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giaoDịchKháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTimKiem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.côngNợToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTepTin,
            this.mnuDanhMuc,
            this.mnuHoaDon,
            this.cToolStripMenuItem,
            this.mnuTimKiem,
            this.mnuBaoCao,
            this.mnuTroGiup});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1235, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuTepTin
            // 
            this.mnuTepTin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thoátToolStripMenuItem,
            this.backupDữLiệuToolStripMenuItem});
            this.mnuTepTin.Name = "mnuTepTin";
            this.mnuTepTin.Size = new System.Drawing.Size(72, 24);
            this.mnuTepTin.Text = "Tệp Tin";
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // backupDữLiệuToolStripMenuItem
            // 
            this.backupDữLiệuToolStripMenuItem.Name = "backupDữLiệuToolStripMenuItem";
            this.backupDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.backupDữLiệuToolStripMenuItem.Text = "Backup dữ liệu";
            this.backupDữLiệuToolStripMenuItem.Click += new System.EventHandler(this.backupDữLiệuToolStripMenuItem_Click);
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSanPham,
            this.mnuLoaiSanPham,
            this.mnuNhanVien,
            this.mnuKhachHang,
            this.mnuTKND,
            this.tồnKhoToolStripMenuItem});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(94, 24);
            this.mnuDanhMuc.Text = "Danh Mục ";
            this.mnuDanhMuc.Click += new System.EventHandler(this.mnuDanhMuc_Click);
            // 
            // mnuSanPham
            // 
            this.mnuSanPham.Name = "mnuSanPham";
            this.mnuSanPham.Size = new System.Drawing.Size(242, 26);
            this.mnuSanPham.Text = "Sản Phẩm";
            this.mnuSanPham.Click += new System.EventHandler(this.mnuSanPham_Click);
            // 
            // mnuLoaiSanPham
            // 
            this.mnuLoaiSanPham.Name = "mnuLoaiSanPham";
            this.mnuLoaiSanPham.Size = new System.Drawing.Size(242, 26);
            this.mnuLoaiSanPham.Text = "Loại Sản Phẩm";
            this.mnuLoaiSanPham.Click += new System.EventHandler(this.mnuLoaiSanPham_Click);
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(242, 26);
            this.mnuNhanVien.Text = "Nhân Viên";
            this.mnuNhanVien.Click += new System.EventHandler(this.mnuNhanVien_Click);
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(242, 26);
            this.mnuKhachHang.Text = "Khách Hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuTKND
            // 
            this.mnuTKND.Name = "mnuTKND";
            this.mnuTKND.Size = new System.Drawing.Size(242, 26);
            this.mnuTKND.Text = "Tài Khoản Người Dùng";
            this.mnuTKND.Click += new System.EventHandler(this.mnuTKND_Click);
            // 
            // tồnKhoToolStripMenuItem
            // 
            this.tồnKhoToolStripMenuItem.Name = "tồnKhoToolStripMenuItem";
            this.tồnKhoToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.tồnKhoToolStripMenuItem.Text = "Tồn Kho";
            this.tồnKhoToolStripMenuItem.Click += new System.EventHandler(this.tồnKhoToolStripMenuItem_Click);
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHoaDonBan});
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(83, 24);
            this.mnuHoaDon.Text = "Hóa Đơn";
            // 
            // mnuHoaDonBan
            // 
            this.mnuHoaDonBan.Name = "mnuHoaDonBan";
            this.mnuHoaDonBan.Size = new System.Drawing.Size(181, 26);
            this.mnuHoaDonBan.Text = "Hóa Đơn Bán";
            this.mnuHoaDonBan.Click += new System.EventHandler(this.mnuHoaDonBan_Click);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nợPhảiThuToolStripMenuItem,
            this.giaoDịchKháchHàngToolStripMenuItem});
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.cToolStripMenuItem.Text = "Công nợ";
            // 
            // nợPhảiThuToolStripMenuItem
            // 
            this.nợPhảiThuToolStripMenuItem.Name = "nợPhảiThuToolStripMenuItem";
            this.nợPhảiThuToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.nợPhảiThuToolStripMenuItem.Text = "Nợ phải thu";
            this.nợPhảiThuToolStripMenuItem.Click += new System.EventHandler(this.nợPhảiThuToolStripMenuItem_Click);
            // 
            // giaoDịchKháchHàngToolStripMenuItem
            // 
            this.giaoDịchKháchHàngToolStripMenuItem.Name = "giaoDịchKháchHàngToolStripMenuItem";
            this.giaoDịchKháchHàngToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.giaoDịchKháchHàngToolStripMenuItem.Text = "Giao dịch khách hàng";
            this.giaoDịchKháchHàngToolStripMenuItem.Click += new System.EventHandler(this.giaoDịchKháchHàngToolStripMenuItem_Click);
            // 
            // mnuTimKiem
            // 
            this.mnuTimKiem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFindHoaDon,
            this.mnuFindSanPham,
            this.mnuFindKhachHang});
            this.mnuTimKiem.Name = "mnuTimKiem";
            this.mnuTimKiem.Size = new System.Drawing.Size(86, 24);
            this.mnuTimKiem.Text = "Tìm Kiếm";
            // 
            // mnuFindHoaDon
            // 
            this.mnuFindHoaDon.Name = "mnuFindHoaDon";
            this.mnuFindHoaDon.Size = new System.Drawing.Size(239, 26);
            this.mnuFindHoaDon.Text = "Tìm Kiếm Hóa Đơn";
            this.mnuFindHoaDon.Click += new System.EventHandler(this.mnuFindHoaDon_Click);
            // 
            // mnuFindSanPham
            // 
            this.mnuFindSanPham.Name = "mnuFindSanPham";
            this.mnuFindSanPham.Size = new System.Drawing.Size(239, 26);
            this.mnuFindSanPham.Text = "Tìm Kiếm Sản Phẩm";
            this.mnuFindSanPham.Click += new System.EventHandler(this.mnuFindSanPham_Click);
            // 
            // mnuFindKhachHang
            // 
            this.mnuFindKhachHang.Name = "mnuFindKhachHang";
            this.mnuFindKhachHang.Size = new System.Drawing.Size(239, 26);
            this.mnuFindKhachHang.Text = "Tìm Kiếm Khách Hàng";
            this.mnuFindKhachHang.Click += new System.EventHandler(this.mnuFindKhachHang_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoanhThu,
            this.mnuTonKho,
            this.côngNợToolStripMenuItem});
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(79, 24);
            this.mnuBaoCao.Text = "Báo Cáo";
            // 
            // mnuDoanhThu
            // 
            this.mnuDoanhThu.Name = "mnuDoanhThu";
            this.mnuDoanhThu.Size = new System.Drawing.Size(164, 26);
            this.mnuDoanhThu.Text = "Doanh Thu";
            this.mnuDoanhThu.Click += new System.EventHandler(this.mnuDoanhThu_Click);
            // 
            // mnuTonKho
            // 
            this.mnuTonKho.Name = "mnuTonKho";
            this.mnuTonKho.Size = new System.Drawing.Size(164, 26);
            this.mnuTonKho.Text = "Tồn Kho";
            this.mnuTonKho.Click += new System.EventHandler(this.mnuTonKhoToolStripMenuItem_Click);
            // 
            // côngNợToolStripMenuItem
            // 
            this.côngNợToolStripMenuItem.Name = "côngNợToolStripMenuItem";
            this.côngNợToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.côngNợToolStripMenuItem.Text = "Công Nợ";
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(79, 24);
            this.mnuTroGiup.Text = "Trợ Giúp";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.helpToolStripMenuItem.Text = "Hướng dẫn";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1235, 623);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 651);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chương trình quản lý bán hàng sơn JOTUN";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuTepTin;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuLoaiSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDonBan;
        private System.Windows.Forms.ToolStripMenuItem mnuTimKiem;
        private System.Windows.Forms.ToolStripMenuItem mnuFindHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuFindSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuFindKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuTKND;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem backupDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTonKho;
        private System.Windows.Forms.ToolStripMenuItem côngNợToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tồnKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nợPhảiThuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem giaoDịchKháchHàngToolStripMenuItem;
    }
}