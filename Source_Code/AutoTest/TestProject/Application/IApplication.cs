using AutoTestEngine.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Application
{
    public interface IApplication
    {
        InvoiceModel SendInvoice(InvoiceModel invoice);
    }
}
