namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanBanSach")]
    public partial class NhanBanSach
    {
        [Key]
        [StringLength(5)]
        public string Masach { get; set; }

        [StringLength(10)]
        public string Nhanbanthu { get; set; }

        [StringLength(50)]
        public string Nhansach { get; set; }
    }
}
