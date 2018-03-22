using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.ObjectModel;

namespace WebApiSergio.Models
{
    public class Queries : Interface1
    {
        private static string connectionStringDb = ConfigurationManager.ConnectionStrings["smartDb"].ConnectionString;

        public ObservableCollection<Cliente> ListaClientes(int? Id)
        {
            using (MySqlConnection smartConexaoDb = new MySqlConnection(connectionStringDb.ToString()))
            {
                string query;
                if (Id == null)
                {
                    query = "SELECT id,nome, cpf_cnpj from cliente";
                }
                else
                {
                    query = "SELECT id,nome, cpf_cnpj from cliente where id=@id";
                }
                
                using (MySqlCommand cmd = new MySqlCommand(query, smartConexaoDb))
                {

                    cmd.Parameters.AddWithValue("@id", Id);
                    smartConexaoDb.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        var listCliente = new ObservableCollection<Cliente>();

                        while (dr.Read())
                        {
                            listCliente.Add
                                (new Cliente()
                                {
                                    id = dr.GetInt32(0),
                                    Nome = dr.GetString(1),
                                    Cpf = dr.GetString(2)
                                });
                        }
                        return listCliente;
                    }
                }
            }
        }
    }
}