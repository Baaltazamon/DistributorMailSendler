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
    
    public partial class Recons
    {
        public int Id { get; set; }
        public System.DateTime Recon { get; set; }
        public bool Obtained { get; set; }
        public Nullable<System.DateTime> ObtainedDate { get; set; }
        public bool Success { get; set; }
        public Nullable<System.DateTime> SuccessDate { get; set; }
        public Nullable<int> Distributor_Id { get; set; }
    
        public virtual Asp_SpravDistr Asp_SpravDistr { get; set; }
    }
}