using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSergio.Classes;
using WebApiSergio.Models;

namespace WebApiSergio.Controllers
{
    /// <summary>
    /// WebAPI para CRUD da tabela Clientes do MySQL.
    /// </summary>
    public class ClientesController : ApiController
    {
        Queries query = new Queries();
        //Os 3 /// abaixo é que gera a documentação XML para mostrar na página de HELP desta WebAPI que contém as intruções , classes e verbos disponíveis para esta WebAPI como sua documentação básica
        /// GET: api/Clientes a chamada foi em método Async porque o médoto da classe Queries é Asysnc
        [HttpGet]
        public async Task<ObservableCollection<Clientes>> GetAsync()
        {
            try
            {
                var listCliente = await query.ListaClientesAsync(null);
                return listCliente;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        /// GET: api/Clientes/ pelo id do cliente
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            IHttpActionResult response;
            //Código para retornar um link de retono caso não haja resultado pelo ID do cliente;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMsg.Headers.Location = new Uri("http://www.alphaebetacom.wordpress.com");
            response = ResponseMessage(responseMsg);
            try
            {
                var listCliente = await query.ListaClientesAsync(id);
                if (listCliente.Count == 0)
                {
                    //Aqui retorna um direcionamento de página.
                    //return response;

                    //Aqui mostra caso o banco de dados não tenha resultado a palavra null em JSON;
                    //return listCliente = null;

                    //Aqui retorno um erro 404
                    //return NotFound();

                    //Aqui retona um formato de Texto baseado na Classe TextResult
                    return new TextResult("hello", Request);
                }
                else
                {
                    return Ok(listCliente);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        /// POST(Insert): api/Clientes
        public async Task<IHttpActionResult> PostAync([FromBody]Clientes value)
        {
            try
            {
                await query.InserirClienteAsync(value.Nome, value.Cpf);
                //Task.Run(() => query.InserirClienteAsync(value.Nome, value.Cpf));
            }

            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        /// PUT(Update): api/Clientes/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody]Clientes value)
        {
            try
            {
                await query.AtualizarClienteAsync(id, value.Nome, value.Cpf);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        // DELETE: api/Clientes/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            try
            {
                await query.DeletarClienteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }
    }
}
