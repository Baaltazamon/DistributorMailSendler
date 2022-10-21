using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorMailSendler.Model
{
    internal class Outlet
    {
        public double CodeDistr { get; set; }
		public string PartnerCode { get; set; }
		public string PartnerINN { get; set; }
		public string PartnerKPP { get; set; }
		public string PartnerName { get; set; }
		public string PartnerAddress { get; set; }
		public string OutletCode { get; set; }
		public string OutletKPP { get; set; }
		public string OutletName { get; set; }
		public string OutletAddress { get; set; }
    }
}
