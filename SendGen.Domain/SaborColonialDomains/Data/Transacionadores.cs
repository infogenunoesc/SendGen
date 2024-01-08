using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.SaborColonialDomains.Data
{
    //objeto do banco
    public class Transacionadores
    {
        public int TraCod { get; set; }

        public string? TraNom { get; set; }
        public string? TraCelular { get; set; }
        public DateTime? TraDatNasc { get; set; }
    }
}
