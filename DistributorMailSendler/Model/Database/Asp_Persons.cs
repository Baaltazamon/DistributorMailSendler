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
    
    public partial class Asp_Persons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsIT { get; set; }
        public string WorkTime { get; set; }
        public string Skype { get; set; }
        public string String { get; set; }
        public Nullable<int> Distributor_Id { get; set; }
    
        public virtual Asp_SpravDistr Asp_SpravDistr { get; set; }
    }
}
