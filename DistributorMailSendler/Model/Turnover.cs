using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorMailSendler.Model
{
    internal class Turnover
    {
        public string DocNumberDistr { get; set; }
        public string AgentCodeDistr { get; set; }
        public Guid? GUID { get; set; }
        public string Notes { get; set; }
        public DateTime OperDate { get; set; }
        public string OutletDistr { get; set; }
        public Guid PartnerFromCodeDistr { get; set; }
        public string PartnerToCodeDistr { get; set; }
        public ObservableCollection<Position> Positions { get; set; }
        public int TypeOper { get; set; }
    }
}
