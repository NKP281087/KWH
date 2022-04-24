using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.Infrastrcture
{
    public class RequestViewModel<T>
    {
        public string Token { get; set; }
        public T ModelObject { get; set; }
    }
}
