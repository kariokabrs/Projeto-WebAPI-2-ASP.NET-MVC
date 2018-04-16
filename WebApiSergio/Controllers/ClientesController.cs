using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSergio.Models;

namespace WebApiSergio.Controllers
{
    public class ClientesController : ApiController
    {
        Queries query = new Queries();

        // GET: api/Clientes a chamada foi em método Async porque o médoto da classe Queries é Asysnc
        public async Task<ObservableCollection<Clientes>> GetAsync()
        {
            var listCliente = await query.ListaClientesAsync(null);
            return listCliente;
        }

        // GET: api/Clientes/5 
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            IHttpActionResult response;
            //Código para retornar um link etrno caso não haja resultado pelo ID do cliente;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMsg.Headers.Location = new Uri("http://www.microsoft.com");
            response = ResponseMessage(responseMsg);

            var listCliente = await query.ListaClientesAsync(id);
            if (listCliente.Count == 0)
            {
                return response;
            }
            return Ok(listCliente);
        }

        // POST: api/Clientes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clientes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clientes/5
        public void Delete(int id)
        {
        }


    }
}
