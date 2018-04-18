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
        public Task<ObservableCollection<Clientes>> ListaClientesAsync(int? Id)
        {
            using (MySqlConnection MyConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_ReadCliente", MyConexaoDb))
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
                        return Task.Run(() => ListCliente);
                    }
                }
            }
        }

        //Post (Insert)
        public string InserirCliente(string nome, string cpf_cnpj)
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
                        smartConexaoDb.Open();

                        int atualizado = Cmd.ExecuteNonQuery();

                        if (atualizado == 0)
                        {
                            return "Dado não foi lançado!";
                        }
                        else
                        {
                            return "Dado lançado!";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        return ("Erro para inserir novo dado" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }

        //PUT (Update)
        public string AtualizarCliente(int clienteid,string nome, string cpf_cnpj)
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
                        smartConexaoDb.Open();

                        int atualizado = Cmd.ExecuteNonQuery();

                        if (atualizado == 0)
                        {
                            return "Dado não foi atualizado!";
                        }
                        else
                        {
                            return "Dado atualizado!";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        return ("Erro para atualizar o dado" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }

        //Delete (Delete)
        public string  DeletarCliente(int clienteid)
        {
            using (MySqlConnection smartConexaoDb = new MySqlConnection(ConnectionStringDb))
            {
                using (MySqlCommand Cmd = new MySqlCommand("sp_DeleteCliente", smartConexaoDb))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@clienteid", clienteid);
                    
                    try
                    {
                        smartConexaoDb.Open();

                        int atualizado = Cmd.ExecuteNonQuery();

                        if (atualizado == 0)
                        {
                            return "Dado não foi deletado!";
                        }
                        else
                        {
                            return "Dado deletado!";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        return ("Erro para deletar o dado" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }
    }
}