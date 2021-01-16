namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuMuon")]
    public partial class ChiTietPhieuMuon
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(5)]
        public string Masach { get; set; }

        [StringLength(50)]
        public string Maphieumuon { get; set; }

        [StringLength(50)]
        public string Tensach { get; set; }

        [StringLength(10)]
        public string Nhanbanthu { get; set; }

        public virtual PhieuMuonTra PhieuMuonTra { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
