﻿using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using SendGen.Domain.OpaSuiteDomains.OpaSuiteTemplate;
using SendGen.Domain.SendGenDomains.Data;

namespace SendGen.Web.Models
{
    public class FiltroAgendamentoView
    {
        public List<FiltroDB> Filtros { get; set; }
        public List<TemplateGetData> Templates { get; set; }
        public List<CanalGetData> Canais { get; set; }
        public List<Agendamento> Agendamentos { get; set; }

    }

}