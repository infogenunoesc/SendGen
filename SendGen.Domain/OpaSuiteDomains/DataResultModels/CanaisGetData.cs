using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.OpaSuiteDomains.DataResultModels
{
    public class CanaisGetData
    {
        public string _id { get; set; }
        public string nome { get; set; }
        public string id_atendente { get; set; }
        public string status { get; set; }
        public string canal { get; set; }
        public string integracao { get; set; }
        public int prioridadeListagemAtendimentos { get; set; }

    }

}