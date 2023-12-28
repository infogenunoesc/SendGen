using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using SendGen.Domain.SendGenDomains.Data;

namespace SendGen.Web.Models
{
    public class FiltroAgendamentoView
    {
        public FiltroDB Filtro { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public List<TemplateGetData> Templates { get; set; }
        public List<CanaisGetData> Canais { get; set; }

    }

}