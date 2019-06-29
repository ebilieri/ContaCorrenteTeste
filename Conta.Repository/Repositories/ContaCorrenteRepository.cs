using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conta.Repository.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly string StringConexao = @"Server=(localdb)\\mssqllocaldb;Database=ContaCorrenteDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly IDbConnection conexao;

        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public ContaCorrenteRepository(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
            conexao = new SqlConnection(StringConexao);
            conexao.Open();
        }

        public void Adicionar(ContaCorrente entity)
        {
            try
            {
                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "INSERT INTO ContaCorrente (ValorAtual) VALUES (@ValorAtual); SELECT SCOPE_IDENTITY();";

                IDbDataParameter paramValorAutal = new SqlParameter("ValorAtual", entity.ValorAtual);

                cmd.Parameters.Add(paramValorAutal);

                entity.IdConta = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Atualizar(ContaCorrente entity)
        {
            try
            {
                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();

                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "update ContaCorrente set ValorAtual = @ValorAtual where IdConta = @IdConta;";

                IDbDataParameter paramValorAutal = new SqlParameter("ValorAtual", entity.ValorAtual);
                IDbDataParameter paramIdConta = new SqlParameter("IdConta", entity.IdConta);

                cmd.Parameters.Add(paramValorAutal);
                cmd.Parameters.Add(paramIdConta);


                cmd.ExecuteNonQuery();

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

        public void CriarSaldoInicial(Movimentacao entity)
        {
            _movimentacaoRepository.Adicionar(entity);
        }

        public ContaCorrente ObterPorId(int id)
        {
            try
            {
                ContaCorrente conta = null;

                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "select * from ContaCorrente where IdConta = @IdConta";

                IDbDataParameter paramIdConta = new SqlParameter("IdConta", id);

                cmd.Parameters.Add(paramIdConta);

                IDataReader resultado = cmd.ExecuteReader();

                while (resultado.Read())
                {
                    conta = new ContaCorrente
                    {
                        IdConta = Convert.ToInt32(resultado["IdConta"]),
                        ValorAtual = Convert.ToDecimal(resultado["ValorAtual"])
                    };
                }

                return conta;
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

        public IEnumerable<ContaCorrente> ObterTodos()
        {
            try
            {
                var listaContas = new List<ContaCorrente>();

                IDbCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "select * from ContaCorrente";
                IDataReader resultado = cmd.ExecuteReader();

                while (resultado.Read())
                {
                    var conta = new ContaCorrente
                    {
                        IdConta = Convert.ToInt32(resultado["IdConta"]),
                        ValorAtual = Convert.ToDecimal(resultado["ValorAtual"])
                    };

                    listaContas.Add(conta);
                }

                return listaContas;
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
