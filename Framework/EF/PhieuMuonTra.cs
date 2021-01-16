namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuMuonTra")]
    public partial class PhieuMuonTra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMuonTra()
        {
            ChiTietPhieuMuons = new HashSet<ChiTietPhieuMuon>();
        }

        [Key]
        [StringLength(50)]
        public string Maphieumuon { get; set; }

        [StringLength(50)]
        public string Tensach { get; set; }

        [StringLength(5)]
        public string Masinhvien { get; set; }

        [StringLength(50)]
        public string Hinhthucmuon { get; set; }

        [StringLength(50)]
        public string Ngaymuon { get; set; }

        [StringLength(50)]
        public string Ngaytra { get; set; }

        [StringLength(50)]
        public string Nopphat { get; set; }

        [StringLength(50)]
        public string Thuoctinhloai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
