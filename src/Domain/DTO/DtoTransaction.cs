using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DtoTransaction : IDtoTransaction
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
