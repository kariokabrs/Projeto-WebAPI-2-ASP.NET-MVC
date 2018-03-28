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
                using (MySqlCommand Cmd = new MySqlCommand("sp_GetCliente", MyConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", Id);
                    MyConexaoDb.Open();

                    using (MySqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        var ListCliente = new ObservableCollection<Cliente>();

                        while (Dr.Read())
                        {
                            ListCliente.Add
                                (new Cliente()
                                {
                                    id = Dr.GetInt32(0),
                                    Nome = Dr.GetString(1),
                                    Cpf = Dr.GetString(2)
                                });
                        }
                        return ListCliente;
                    }
                }
            }
        }
    }
}