using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.SendGenDomains.Data
{
    public class RespostaAPI<T>
    {
        public string status { get; set; }
        public int code { get; set; }
        public List<T> data { get; set; }
    }
}
