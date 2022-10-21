//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DistributorMailSendler.Model.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asp_SpravDistr
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asp_SpravDistr()
        {
            this.Asp_Persons = new HashSet<Asp_Persons>();
            this.Faults = new HashSet<Faults>();
            this.Recons = new HashSet<Recons>();
            this.StatusUpdates = new HashSet<StatusUpdates>();
            this.Asp_Warehouses = new HashSet<Asp_Warehouses>();
            this.Comments = new HashSet<Comments>();
        }
    
        public int Id { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public long DistributorCode { get; set; }
        public string INN { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string TypeSystem { get; set; }
        public string TypeDataTransmission { get; set; }
        public bool UsesTabletPC { get; set; }
        public bool UsesCodeMoney { get; set; }
        public string ShortName { get; set; }
        public bool TZ { get; set; }
        public System.DateTime LastDocumentDateTime { get; set; }
        public System.DateTime LastReviseDateTime { get; set; }
        public System.DateTime IgnoreToDate { get; set; }
        public bool PrimarySales { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asp_Persons> Asp_Persons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faults> Faults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recons> Recons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StatusUpdates> StatusUpdates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asp_Warehouses> Asp_Warehouses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
