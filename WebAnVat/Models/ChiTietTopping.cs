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
    
    public partial class ChiTietTopping
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietTopping()
        {
            this.CTMA_Topp = new HashSet<CTMA_Topp>();
        }
    
        public int ID_Topping { get; set; }
        public string Loai_Topping { get; set; }
        public string Ten_Topping { get; set; }
        public Nullable<decimal> GiaTopp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTMA_Topp> CTMA_Topp { get; set; }
    }
}
