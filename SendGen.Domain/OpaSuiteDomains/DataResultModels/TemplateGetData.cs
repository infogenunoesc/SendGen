using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.OpaSuiteDomains.DataResultModels
{
    public class TemplateGetData
    {
        public string _id { get; set; }
        public string texto { get; set; }
        public string atalho { get; set; }
        public string tipo_mensagem { get; set; }
        public List<string> departamentos { get; set; }
    }
}