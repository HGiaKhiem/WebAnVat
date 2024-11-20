//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAnVat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mon()
        {
            this.CTMA_Size = new HashSet<CTMA_Size>();
            this.CTMA_Topp = new HashSet<CTMA_Topp>();
            this.ChiTietDonHang = new HashSet<ChiTietDonHang>();
            this.GioHang = new HashSet<GioHang>();
        }
    
        public int ID_Mon { get; set; }
        public string TenMon { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public int ID_LoaiMonAn { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> KhuyenMai { get; set; }
        public Nullable<decimal> GiaSauKhiGiam { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTMA_Size> CTMA_Size { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTMA_Topp> CTMA_Topp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHang { get; set; }
        public virtual LoaiMonAn LoaiMonAn { get; set; }
    }
}
