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

    public async Task<ActionResult> Index(int? filtroID)
    {
        FiltroDB filtro = await _context.FiltroDB.FirstOrDefaultAsync(e => e.ID == filtroID);

        var templateController = new TemplateController(_utilitiesApiRepository);
        var canaisController = new CanaisController(_utilitiesApiRepository);

        Console.WriteLine("Filtro ID: " + filtroID);
        Console.WriteLine("Filtro: " + filtro.ToJson());

        string? filtroCondicao = filtro?.Condicao ?? "Select * from [SendGen].[dbo].[Cliente]";

        List<Cliente> listaClientes = _utilitiesApiRepository.BuscaEntidadeDB<Cliente>(filtroCondicao);

        List<TemplateGetData> templates = await templateController.templateGet(null, null, 0, 100);

        canaisGetFilter filtroCanais = new canaisGetFilter
        {
            filter = new filterCanais { },
            options = new options { }
        };

        List<CanaisGetData> canais = await canaisController.canaisGet(filtroCanais);

        var viewModel = new FiltroAgendamentoView
        {
            Filtro = filtro,
            Clientes = listaClientes,
            Templates = templates,
            Canais = canais
        };

        return View(viewModel);
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

    public async Task Salvar(string condicao)
    {
        FiltroDB filtro = new FiltroDB
        {
            Condicao = condicao
        };

        _context.FiltroDB.Add(filtro);
        await _context.SaveChangesAsync();

        Console.WriteLine("Salvar: " + condicao);
    }

    private Timer _timer;

    public void IniciarVerificacao()
    {
        
            TemplateRepository envioTemplate = new TemplateRepository();

            _timer = new Timer(async (e) =>
            {
                Console.WriteLine("Iniciando Verificação.");


                List<Agendamento> listaAgendamentos = _utilitiesApiRepository.BuscaEntidadeDB<Agendamento>("Select * from [SendGen].[dbo].[Agendamento]");
            List<FiltroDB> listaFiltros = _utilitiesApiRepository.BuscaEntidadeDB<FiltroDB>("Select * from [SendGen].[dbo].[FiltroDB]");

                foreach (var item in listaFiltros)
                {
                    Console.WriteLine("FiltroID: " + item.ID + "/ Condição: " + item.Condicao);
                }

                foreach (var item in listaAgendamentos)
                {
                    Console.WriteLine("Agendamento ID: " + item.ID + "/ Tipo: " + item.Tipo);
                }

                DateTime DataAtual = DateTime.Now;

            List<int> frequenciaHoras = new List<int> { 1, 2, 3, 4 };

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
                                    
                                     // await envioTemplate.Send(cliente.Celular, cliente.Nome, agendamento.CanalID, agendamento.TemplateID);
                            }


                            }
                        break;
                    case "Recado":
                            Console.WriteLine("Recado sendo checado: " + agendamento.ID);
                        TimeSpan diferencaData = (DataAtual - agendamento.UltimaExecucao);

                            Console.WriteLine("Diferença data: " + diferencaData);

                            foreach (var intervalo in frequenciaHoras)
                        {
                            if (agendamento.IntervaloExecucao == intervalo &&
                            (diferencaData.TotalMinutes > (intervalo * 60) || agendamento.UltimaExecucao == null))
                            {
                                    Console.WriteLine("Recado sendo enviado..." );

                                    filtrarListaClientes(listaFiltros, agendamento.FiltroID)
                                    .ForEach(async cliente => await envioTemplate.Send(cliente.Celular, cliente.Nome, agendamento.CanalID, agendamento.TemplateID));

                                }
                            }                           

                        break;

                    default:
                        break;
                }                                         
            }

            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

    public List<Cliente> filtrarListaClientes(List<FiltroDB> listaFiltros, int idFiltro)
    {
        var filtro = listaFiltros.FirstOrDefault(filtroObj => filtroObj.ID == idFiltro);

        List<Cliente> listaClientes = _utilitiesApiRepository.BuscaEntidadeDB<Cliente>(filtro.Condicao);
        
        return listaClientes.Where(cliente => cliente.Celular != null).ToList();
    }

    public async Task enviarMensagem()
    {
        TemplateRepository envioTemplate = new TemplateRepository();

        await envioTemplate.Send("+55049988190017", "Morgane", "64f09e6332843f9dada2d0d6", "6500b9df32843f9dada2e6be");
    }


}
