using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public class Result
    {
        public bool Succeeded { get; set; }

        public string ResultMessage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Result Object</typeparam>
    /// <typeparam name="U">Result Type</typeparam>
    public class Result<T, U>
    {
        public T ResultObject { get; set; }

        public U ResultType { get; set; }
    }

    public class Result<T>
    {
        public List<T> Reasons { get; set; }

        public bool Succeeded { get; set; }

    }
}
