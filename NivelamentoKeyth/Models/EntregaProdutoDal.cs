using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    public class EntregaProdutoDal : IEntregaProdutoDal
    {
        readonly string _connectionString;

        public EntregaProdutoDal(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public int AtualizarEntregaProduto(EntregaProduto entregaProduto)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = @$" UPDATE EntregaProduto 
                                        SET DataEntrega = @DataEntrega, NomeProduto = @NomeProduto, Quantidade = @Quantidade, ValorUnitario = @ValorUnitario
                                    WHERE Id = @Id";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", entregaProduto.Id);
                    cmd.Parameters.AddWithValue("@DataEntrega", entregaProduto.DataEntrega);
                    cmd.Parameters.AddWithValue("@NomeProduto", entregaProduto.NomeProduto);
                    cmd.Parameters.AddWithValue("@Quantidade", entregaProduto.Quantidade);
                    cmd.Parameters.AddWithValue("@ValorUnitario", entregaProduto.ValorUnitario);

                    con.Open();
                    var status = cmd.ExecuteNonQuery();
                    con.Close();
                    return status;
                }
            }
            catch (Exception e)
            {
                string erro = e.Message;
                return 0;
            }
        }

        public int ExcluirEntregaProduto(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var query = $" DELETE FROM EntregaProduto WHERE Id = @Id";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                var status = cmd.ExecuteNonQuery();
                con.Close();

                return status;
            }
        }

        public int IncluirEntregaProduto(EntregaProduto entregaProduto)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = @$"INSERT INTO EntregaProduto(DataEntrega, NomeProduto, Quantidade, ValorUnitario) 
                                        VALUES(@DataEntrega, @NomeProduto, @Quantidade, @ValorUnitario)";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@DataEntrega", entregaProduto.DataEntrega);
                    cmd.Parameters.AddWithValue("@NomeProduto", entregaProduto.NomeProduto);
                    cmd.Parameters.AddWithValue("@Quantidade", entregaProduto.Quantidade);
                    cmd.Parameters.AddWithValue("@ValorUnitario", entregaProduto.ValorUnitario);

                    con.Open();
                    var status = cmd.ExecuteNonQuery();
                    con.Close();

                    return status;
                }
            }
            catch(Exception)
            {
            return 0;
            }
        }

        public EntregaProduto ObterEntregaPorId(int id)
        {
            var entregaProduto = new EntregaProduto();

            using (var con = new SqlConnection(_connectionString))
            {
                var query = $"SELECT Id, DataEntrega, NomeProduto, Quantidade, ValorUnitario FROM EntregaProduto WHERE Id = {id}";
                var cmd = new SqlCommand(query, con);

                con.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        entregaProduto.Id = Convert.ToInt32(reader["Id"]);
                        entregaProduto.DataEntrega = Convert.ToDateTime(reader["DataEntrega"]);
                        entregaProduto.NomeProduto = reader["NomeProduto"].ToString();
                        entregaProduto.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                        entregaProduto.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"]);
                    }
                }
                else
                    return null;
                con.Close();
            }
            return entregaProduto;
        }

        public IEnumerable<EntregaProduto> ObterEntregasProdutos()
        {
            var entregasLista = new List<EntregaProduto>();
            using (var con = new SqlConnection(_connectionString))
            {
                var query = $"SELECT Id, DataEntrega, NomeProduto, Quantidade, ValorUnitario FROM EntregaProduto";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var entregaProduto = new EntregaProduto
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DataEntrega = Convert.ToDateTime(reader["DataEntrega"]),
                        NomeProduto = reader["NomeProduto"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"]),
                        ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"])
                    };

                    entregasLista.Add(entregaProduto);
                }
                con.Close();
            }
            return entregasLista;
        }
    }
}
