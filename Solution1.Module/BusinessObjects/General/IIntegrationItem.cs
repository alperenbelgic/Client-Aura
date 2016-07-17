using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public interface IIntegrationItem
    {
        string IntegrationSource { get; set; }

        string IntegrationCode { get; set; }
    }
}
