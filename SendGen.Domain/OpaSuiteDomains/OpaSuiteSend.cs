using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.OpaSuiteDomains
{
    public class OpaSuiteContato
    {
        public string canalCliente { get; set; }
    }

    public class OpaSuiteSend
    {
        public OpaSuiteContato contato { get; set; }
        public OpaSuiteTemplateSend template { get; set; }
        public string canal { get; set; }
    }

    public class OpaSuiteTemplateSend
    {
        public string _id { get; set; }
        public List<string> variaveis { get; set; }
    }


}
