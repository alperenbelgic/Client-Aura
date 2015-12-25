using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects.General
{
    public interface IHaveIsDeletedMember
    {
        bool IsDeleted { get; set; }
    }
}
