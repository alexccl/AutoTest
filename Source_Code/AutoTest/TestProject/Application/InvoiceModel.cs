using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Application
{
    public class InvoiceModel
    {
        public Guid InvoiceId { get; set; }
        public bool IsProcessed { get; set; }

    }
}
