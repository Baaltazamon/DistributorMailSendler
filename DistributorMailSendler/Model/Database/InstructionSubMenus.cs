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
    
    public partial class InstructionSubMenus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstructionSubMenus()
        {
            this.InstructionFiles = new HashSet<InstructionFiles>();
        }
    
        public int Id { get; set; }
        public int Main_Id { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructionFiles> InstructionFiles { get; set; }
        public virtual InstructionMainMenus InstructionMainMenus { get; set; }
    }
}