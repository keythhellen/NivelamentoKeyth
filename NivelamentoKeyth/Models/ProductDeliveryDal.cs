using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using NivelamentoKeyth.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    public class ProductDeliveryDal : IProductDeliveryDal
    {
        readonly string _connectionString;

        public ProductDeliveryDal(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<string> Validate(string path)
        {
            List<string> errors = new List<string>();
            List <ProductDelivery> deliveries = new List<ProductDelivery>();

            try
            {
                string StrConexao = String.Format(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1';", path);
                OleDbConnection Conexao = new OleDbConnection();
                Conexao.ConnectionString = StrConexao;
                Conexao.Open();
                object[] restricoes = { null, null, null, "TABLE" };
                DataTable DTSchema = Conexao.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restricoes); 

                if(DTSchema.Rows.Count > 0)
                {
                    string Sheet = DTSchema.Rows[0]["TABLE_NAME"].ToString();
                    OleDbCommand Comando = new OleDbCommand("SELECT * FROM [" + Sheet + "]", Conexao);
                    DataTable Dados = new DataTable();
                    OleDbDataAdapter Adapter = new OleDbDataAdapter(Comando);
                    Adapter.Fill(Dados);
                    Conexao.Close();

                    foreach (DataRow row in Dados.Rows)
                    {
                        ProductDelivery entrega = new ProductDelivery();
                        if ((Convert.ToDateTime(row[0].ToString()) == DateTime.MinValue) || (Convert.ToDateTime(row[0].ToString()) <= DateTime.Today))
                            errors.Add(string.Format("Data da entrega deve ser informada e depois de hoje. Linha: {0}", Dados.Rows.IndexOf(row)));
                        if (string.IsNullOrEmpty(row[1].ToString()))
                            errors.Add(string.Format("Nome do produto deve ser informado. Linha: {0}", Dados.Rows.IndexOf(row)));
                        if(row[1].ToString().Length > 50)
                            errors.Add(string.Format("Nome do produto tem mais de 50 caracteres. Linha: {0}", Dados.Rows.IndexOf(row)));
                        if (Convert.ToInt32(row[2].ToString()) <= 0)
                            errors.Add(string.Format("Quantidade deve ser informado com valor maior do que zero. Linha: {0}", Dados.Rows.IndexOf(row)));
                        if (Convert.ToInt32(row[3].ToString()) <= 0)
                            errors.Add(string.Format("Valor Unitário deve ser informado com valor maior do que zero. Linha: {0}", Dados.Rows.IndexOf(row)));

                        entrega.DeliveryDate = Convert.ToDateTime(row[0].ToString());
                        entrega.ProductName = row[1].ToString();
                        entrega.Amount = Convert.ToInt32(row[2].ToString());
                        entrega.UnitaryValue = Convert.ToDecimal(row[3].ToString());

                        if (errors.Count == 0)
                            deliveries.Add(entrega);
                    }
                }
                if(errors.Count == 0)
                {
                    Insert(deliveries);
                    errors.Add("Importação realizada com sucesso.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao acessar os dados: " + e.Message);
            }
            return errors;
        }

        public int Insert(List<ProductDelivery> deliveries)
        {
            int status = 0;

            foreach(ProductDelivery delivery in deliveries)
            {
                Create(delivery);
            }
            return status;
        }

        public int Create(ProductDelivery entregaProduto)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = @$"INSERT INTO ProductDelivery(DeliveryDate, ProductName, Amount, UnitaryValue) 
                                        VALUES(@DataEntrega, @NomeProduto, @Quantidade, @ValorUnitario)";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@DataEntrega", entregaProduto.DeliveryDate);
                    cmd.Parameters.AddWithValue("@NomeProduto", entregaProduto.ProductName);
                    cmd.Parameters.AddWithValue("@Quantidade", entregaProduto.Amount);
                    cmd.Parameters.AddWithValue("@ValorUnitario", entregaProduto.UnitaryValue);

                    con.Open();
                    var status = cmd.ExecuteNonQuery();
                    con.Close();

                    return status;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ProductDelivery GetImportById(int id)
        {
            var entregaProduto = new ProductDelivery();
            
            using (var con = new SqlConnection(_connectionString))
            {
                var query = $"SELECT Id, DeliveryDate, ProductName, Amount, UnitaryValue FROM ProductDelivery WHERE Id = {id}";
                var cmd = new SqlCommand(query, con);

                con.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        entregaProduto.Id = Convert.ToInt32(reader["Id"]);
                        entregaProduto.DeliveryDate = Convert.ToDateTime(reader["DataEntrega"]);
                        entregaProduto.ProductName = reader["NomeProduto"].ToString();
                        entregaProduto.Amount = Convert.ToInt32(reader["Quantidade"]);
                        entregaProduto.UnitaryValue = Convert.ToDecimal(reader["ValorUnitario"]);
                    }
                }
                else
                    return null;
                con.Close();
            }
            return entregaProduto;
        }

        public ProductDeliveryViewModel GetAllImports()
        {
            ProductDeliveryViewModel productDeliveryViewModel = new ProductDeliveryViewModel();
            var ProductDeliveryList = new List<ProductDelivery>();
            using (var con = new SqlConnection(_connectionString))
            {
                var query = $"SELECT Id, DeliveryDate, ProductName, Amount, UnitaryValue FROM ProductDelivery";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var entregaProduto = new ProductDelivery
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                        ProductName = reader["ProductName"].ToString(),
                        Amount = Convert.ToInt32(reader["Amount"]),
                        UnitaryValue = Convert.ToDecimal(reader["UnitaryValue"])
                    };

                    ProductDeliveryList.Add(entregaProduto);
                }
                productDeliveryViewModel.SetProductDeliveryList(ProductDeliveryList);

                if(ProductDeliveryList.Count>0)
                    productDeliveryViewModel.OldestDelivery = productDeliveryViewModel.ProductDeliveryList.OrderBy(p => p.DeliveryDate).FirstOrDefault().DeliveryDate;
                con.Close();
            }
            return productDeliveryViewModel;
        }
    }
}
