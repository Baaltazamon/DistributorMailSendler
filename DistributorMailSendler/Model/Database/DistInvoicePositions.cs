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
    
    public partial class DistInvoicePositions
    {
        public int Id { get; set; }
        public string Articul { get; set; }
        public Nullable<double> CodeEAN { get; set; }
        public Nullable<int> DocRecNo { get; set; }
        public double MaterCode { get; set; }
        public double Price { get; set; }
        public double PriceWithVAT { get; set; }
        public double Quantity { get; set; }
    }
}
