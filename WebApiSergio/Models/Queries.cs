using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;

namespace WebApiSergio.Models
{
    public class Queries : Interface1
    {
        private static string ConnectionStringDb = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;

        public ObservableCollection<Clientes> ListaClientes(int? Id)
        {
            using (MySqlConnection MyConexaoDb = new MySqlConnection(ConnectionStringDb.ToString()))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_GetCliente", MyConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", Id);
                    MyConexaoDb.Open();

                    using (MySqlDataReader dr = Cmd.ExecuteReader())
                    {
                        var ListCliente = new ObservableCollection<Clientes>();

                        while (dr.Read())
                        {
                            if (!(dr.IsDBNull(0)))
                            {
                                ListCliente.Add
                                (new Clientes()
                                {
                                    Id = dr.GetInt32(0),
                                    Nome = dr.GetString(1),
                                    Cpf = dr.GetString(2)
                                });
                            }
                        }
                        return ListCliente;
                    }
                }
            }
        }
    }
}