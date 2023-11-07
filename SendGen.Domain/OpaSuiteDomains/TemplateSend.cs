using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.OpaSuiteDomains
{
    public class TemplateSend
    {
        public Contato contato { get; set; }
        public Template template { get; set; }
        public string canal { get; set; }
    }

    public class Template
    {
        public string _id { get; set; }
        public List<string> variaveis { get; set; }
    }


}
