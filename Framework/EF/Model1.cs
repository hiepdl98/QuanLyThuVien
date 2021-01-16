namespace Framework.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        public virtual DbSet<ChiTietPhieuNhapKho> ChiTietPhieuNhapKhoes { get; set; }
        public virtual DbSet<LoaiSach> LoaiSaches { get; set; }
        public virtual DbSet<NhanBanSach> NhanBanSaches { get; set; }
        public virtual DbSet<NXB> NXBs { get; set; }
        public virtual DbSet<PhieuMuonTra> PhieuMuonTras { get; set; }
        public virtual DbSet<PhieuNhapKho> PhieuNhapKhoes { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietPhieuMuon>()
                .Property(e => e.Nhanbanthu)
                .IsFixedLength();

            modelBuilder.Entity<NhanBanSach>()
                .Property(e => e.Nhanbanthu)
                .IsFixedLength();

            modelBuilder.Entity<PhieuNhapKho>()
                .HasMany(e => e.ChiTietPhieuNhapKhoes)
                .WithRequired(e => e.PhieuNhapKho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuNhapKhoes)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);
        }
    }
}
