namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhapKho")]
    public partial class ChiTietPhieuNhapKho
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Maphieunhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string Masach { get; set; }

        public int? Soluong { get; set; }

        [StringLength(50)]
        public string Dongia { get; set; }

        public virtual PhieuNhapKho PhieuNhapKho { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
