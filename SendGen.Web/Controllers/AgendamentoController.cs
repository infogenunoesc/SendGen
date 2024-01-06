using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.SendGenRepositories;
using SendGen.Web.Models;
using NuGet.Protocol;
using SendGen.Web.Controllers.API;
using JetBrains.Annotations;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Repository.OpaSuiteRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace SendGen.Web.Controllers;

public class AgendamentoController : Controller
{
    private readonly SendGenContexto _context;
    private IUtilitiesApiRepository _utilitiesApiRepository;
    private ITemplateRepository _templateRepository;

    public AgendamentoController(IUtilitiesApiRepository utilitiesApiRepository, SendGenContexto context, ITemplateRepository templateRepository)
    {
        _utilitiesApiRepository = utilitiesApiRepository;
        _context = context;
        _templateRepository = templateRepository;
    }

    public async Task<ActionResult> Index()
    {
        var templateController = new TemplateController(_utilitiesApiRepository);
        var canaisController = new CanaisController(_utilitiesApiRepository);

        List<TemplateGetData> templates = await templateController.templateGet(null, null, 0, 100);

        canaisGetFilter filtroCanais = new canaisGetFilter
        {
            filter = new filterCanais { },
            options = new options { }
        };

        List<CanaisGetData> canais = await canaisController.canaisGet(filtroCanais);

        List<Agendamento> agendamentos = _context.Agendamento.ToList();

        List<FiltroDB> filtros = _context.FiltroDB.ToList();

        var viewModel = new FiltroAgendamentoView
        {
            Filtros = filtros,
            Templates = templates,
            Canais = canais,
            Agendamentos = agendamentos            
        };

        return View(viewModel);
    }

    private Timer _timer;

    public void IniciarVerificacao()
    {
        TemplateRepository envioTemplate = new TemplateRepository();
        
        _timer = new Timer(async (e) =>
        {
            Console.WriteLine("Iniciando Verificação...");

            List<Agendamento> listaAgendamentos = _utilitiesApiRepository.BuscaEntidadeDB<Agendamento>("Select * from [SendGen].[dbo].[Agendamento]");
            List<FiltroDB> listaFiltros = _utilitiesApiRepository.BuscaEntidadeDB<FiltroDB>("Select * from [SendGen].[dbo].[FiltroDB]");

            DateTime DataAtual = DateTime.Now;
  
            foreach (var agendamento in listaAgendamentos)
            {
                switch (agendamento.Tipo)
                {
                    case "Aniversário":
                        var listaClientes = filtrarListaClientes(listaFiltros, agendamento.FiltroID);
                        foreach (var cliente in listaClientes)
                        {
                            if (cliente.DataNascimento != null && 
                            (cliente.DataNascimento.Value.Day == DataAtual.Day &&
                            cliente.DataNascimento.Value.Month == DataAtual.Month))
                            {
                                Console.WriteLine("Aniversário encontrado");

                                Console.WriteLine("Cliente Aniversário: " + cliente.ToJson());

                                // await envioTemplate.Send(cliente.Celular, cliente.Nome, agendamento.CanalID, agendamento.TemplateID);

                                await AtualizarAgendamento(DataAtual, agendamento.ID);
                            }
                        }
                        break;
                    case "Recado":
                        TimeSpan diferencaData = DataAtual - (agendamento.UltimaExecucao ?? DateTime.MinValue);                          
                    
                        if ((diferencaData.TotalMinutes > agendamento.IntervaloExecucao) || agendamento.UltimaExecucao == null)
                        {
                            Console.WriteLine("Recado sendo enviado..." );

                            filtrarListaClientes(listaFiltros, agendamento.FiltroID)
                            .ForEach(cliente => Console.WriteLine("Cliente Aniversário: " + cliente.ToJson()));

                            //filtrarListaClientes(listaFiltros, agendamento.FiltroID)
                            //.ForEach(async cliente => await envioTemplate.Send(cliente.Celular, cliente.Nome, agendamento.CanalID, agendamento.TemplateID));

                            await AtualizarAgendamento(DataAtual, agendamento.ID);

                        }                         
                        break;
                    default:
                        Console.WriteLine("Tipo de agendamento não encontrado.");
                        break;
                }                                         
            }
            Console.WriteLine("Verificação concluída");  
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }

    [HttpPost]
    public async Task SalvarAgendamento(int filtroID, string templateID, string canalID, int intervaloExecucao, string tipo)
    {
        Agendamento agendamento = new Agendamento
        {
            FiltroID = filtroID,
            TemplateID = templateID,
            CanalID = canalID,
            IntervaloExecucao = intervaloExecucao,
            Tipo = tipo
        };

        _context.Agendamento.Add(agendamento);
        await _context.SaveChangesAsync();

        Console.WriteLine("Agendamento Salvo");
    }

    [HttpPut]
    public async Task AtualizarAgendamento(DateTime data, int id)
    {
        using (var context = new SendGenContexto())
        {
            var agendamento = await context.Agendamento.FirstOrDefaultAsync(a => a.ID == id);

            agendamento.UltimaExecucao = data;
            await context.SaveChangesAsync();

            Console.WriteLine("Ultima Execução: " + data.ToJson());
        }
    }

    public List<Cliente> filtrarListaClientes(List<FiltroDB> listaFiltros, int idFiltro)
    {
        var filtro = listaFiltros.FirstOrDefault(filtroObj => filtroObj.ID == idFiltro);

        List<Cliente> listaClientes = _utilitiesApiRepository.BuscaEntidadeDB<Cliente>(filtro.Condicao);
        
        return listaClientes.Where(cliente => cliente.Celular != null).ToList();
    }

}
