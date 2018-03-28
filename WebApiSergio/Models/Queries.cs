using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;

namespace WebApiSergio.Models
{
    public class Queries : Interface1
    {
        private static string ConnectionStringDb = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;

        public ObservableCollection<Cliente> ListaClientes(int? Id)
        {
            using (MySqlConnection MyConexaoDb = new MySqlConnection(ConnectionStringDb.ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_GetCliente", MyConexaoDb))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteid", Id);
                    MyConexaoDb.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        var ListCliente = new ObservableCollection<Cliente>();

                        while (dr.Read())
                        {
                            ListCliente.Add
                                (new Cliente()
                                {
                                    id = dr.GetInt32(0),
                                    Nome = dr.GetString(1),
                                    Cpf = dr.GetString(2)
                                });
                        }
                        return ListCliente;
                    }
                }
            }
        }
    }
}