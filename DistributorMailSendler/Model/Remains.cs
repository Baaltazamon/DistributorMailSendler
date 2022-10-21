using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorMailSendler.Model
{
    internal class Remains
    {
        public DateTime OperDate { get; set; }
        public Guid PartnerFromCodeDistr { get; set; }
        public int TypeOper { get; set; }
        public ObservableCollection<Position> Positions { get; set; }
    }
}
