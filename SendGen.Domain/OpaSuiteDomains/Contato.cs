using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Domain.OpaSuiteDomains
{
    public class Contato
    {
        public int? id_contato { get; set; }
        public int? id_cliente { get; set; }
        public int? id_fornecedor { get; set; }
        public string nome { get; set; }
        public string? classificacao { get; set; }
        public string? foneResidencialCompleto { get; set; }
        public string? foneComercialCompleto { get; set; }
        public string? celularCompleto { get; set; }
        public string? WhatsappCompleto { get; set; }
        public bool requerAutenticacaoSempre { get; set; }
        public bool habilitarAlerta { get; set; }
        public bool lead { get; set; }
        public bool historico_email { get; set; }
        public string? email_principal { get; set; }
        public string? mensagemAlerta { get; set; }
        public string senha { get; set; }
        public string repetirSenha { get; set; }

    }
}
