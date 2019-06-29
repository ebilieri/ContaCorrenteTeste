using Conta.Domain.Entities;
using Conta.Domain.Enumerators;
using Conta.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conta.Repository.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly string StringConexao = @"Server=(localdb)\\mssqllocaldb;Database=ContaCorrenteDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly IDbConnection conexao;

        public MovimentacaoRepository()
        {
            conexao = new SqlConnection(StringConexao);
            conexao.Open();
        }


        public void Adicionar(Movimentacao entity)
        {
            try
            {
                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "INSERT INTO Movimentacao (ContaId, DataMovimentacao, TipoOperacao, Valor, ValorAtual) VALUES (@ContaId, @DataMovimentacao, @TipoOperacao, @Valor, @ValorAtual); SELECT SCOPE_IDENTITY();";

                IDbDataParameter paramContaId = new SqlParameter("ContaId", entity.ContaId);
                IDbDataParameter paramDataMovimentacao = new SqlParameter("DataMovimentacao", entity.DataMovimentacao);
                IDbDataParameter paramTipoOperacao = new SqlParameter("TipoOperacao", entity.TipoOperacao);
                IDbDataParameter paramValor = new SqlParameter("Valor", entity.Valor);
                IDbDataParameter paramValorAutal = new SqlParameter("ValorAtual", entity.ValorAtual);

                cmd.Parameters.Add(paramContaId);
                cmd.Parameters.Add(paramDataMovimentacao);
                cmd.Parameters.Add(paramTipoOperacao);
                cmd.Parameters.Add(paramValor);
                cmd.Parameters.Add(paramValorAutal);

                entity.IdMovimentacao = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public IEnumerable<Movimentacao> ObterPorIdConta(int idConta)
        {
            try
            {
                var listaMovimentacaos = new List<Movimentacao>();

                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "select * from Movimentacao where ContaId = @IdConta";

                IDbDataParameter paramIdConta = new SqlParameter("IdConta", idConta);

                cmd.Parameters.Add(paramIdConta);

                IDataReader resultado = cmd.ExecuteReader();

                while (resultado.Read())
                {
                    var movimentacao = new Movimentacao
                    {
                        IdMovimentacao = Convert.ToInt32(resultado["IdMovimentacao"]),
                        ContaId = Convert.ToInt32(resultado["ContaId"]),
                        DataMovimentacao = Convert.ToDateTime(resultado["DataMovimentacao"]),
                        TipoOperacao = (TipoOperacaoEnum)Convert.ToInt32(resultado["TipoOperacao"]),
                        Valor = Convert.ToInt32(resultado["Valor"]),
                        ValorAtual = Convert.ToDecimal(resultado["ValorAtual"])
                    };

                    listaMovimentacaos.Add(movimentacao);
                }

                return listaMovimentacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
