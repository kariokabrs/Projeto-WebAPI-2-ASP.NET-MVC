using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System;

namespace WebApiSergio.Models
{
    public class Queries : Interface1
    {
        private static string ConnectionStringDb = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;
        //Se o método é Async a chamada também tem que ser

        //GET e GET(id) (Select)
        public async Task<ObservableCollection<Clientes>> ListaClientesAsync(int? Id)
        {
            using (MySqlConnection MyConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_ReadCliente", MyConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", Id);
                    await MyConexaoDb.OpenAsync();

                    //Neste caso MySQL tem um bug para o método Asyncrono ExecuteReader mesmo no connectro versão 8.0
                    // using (MySqlDataReader dr = await Cmd.ExecuteReaderAsync())
                    using (MySqlDataReader dr = Cmd.ExecuteReader())
                    {
                        var ListCliente = new ObservableCollection<Clientes>();

                        while (await dr.ReadAsync())
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
        //Método completamente asyncronous desde o Open até o Execute. 
        //Post (Insert)
        public async Task InserirClienteAsync(string nome, string cpf_cnpj)
        {
            using (MySqlConnection smartConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_CreateCliente", smartConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@_nome", nome);
                    Cmd.Parameters.AddWithValue("@_cpf_cnpj", cpf_cnpj);

                    try
                    {
                        await smartConexaoDb.OpenAsync();
                        await Cmd.ExecuteNonQueryAsync();
                    }
                    catch (MySqlException)
                    {
                        throw;
                    }
                }
            }
        }

        //PUT (Update)
        public async Task AtualizarClienteAsync(int clienteid, string nome, string cpf_cnpj)
        {
            using (MySqlConnection smartConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_UpdateCliente", smartConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", clienteid);
                    Cmd.Parameters.AddWithValue("@_nome", nome);
                    Cmd.Parameters.AddWithValue("@_cpf_cnpj", cpf_cnpj);

                    try
                    {
                        await smartConexaoDb.OpenAsync();
                        await Cmd.ExecuteNonQueryAsync();

                        //int atualizado = await Cmd.ExecuteNonQueryAsync();

                        //if (atualizado == 0)
                        //{
                        //    return "Dado não foi atualizado!";
                        //}
                        //else
                        //{
                        //    return "Dado atualizado!";
                        //}
                    }
                    catch (MySqlException)
                    {
                        throw;
                    }
                }
            }
        }

        //Delete (Delete)
        public async Task DeletarClienteAsync(int clienteid)
        {
            using (MySqlConnection smartConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_DeleteCliente", smartConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", clienteid);

                    try
                    {
                        await smartConexaoDb.OpenAsync();
                        await Cmd.ExecuteNonQueryAsync();
                    }
                    catch (MySqlException)
                    {
                        throw;
                    }
                }
            }
        }
    }
}