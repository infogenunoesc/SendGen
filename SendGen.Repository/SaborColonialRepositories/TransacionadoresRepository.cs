﻿using Dapper;
using Microsoft.Data.SqlClient;
using SendGen.Domain.SaborColonialDomains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Repository.SaborColonialRepositories
{
    public class TransacionadoresRepository
    {

        public List<Transacionadores> ObterClientes()
        {
            string connectionString = "Server=localhost; Database=SaborColonial; Integrated Security=True;TrustServerCertificate=True;";

            // Inicializa a conexão
            using (var con = new SqlConnection(connectionString))
            {
                // Abre a conexão
                con.Open();

                List<Transacionadores> lista = con
                    .Query<Transacionadores>(@"SELECT TraCod, TraNom, TraCelular, TraDatNasc FROM [dbo].[TRANSACIONADORES]")
                    .ToList();


                return lista;
            }
        }
    }
}
