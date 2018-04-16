using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSergio.Models;

namespace WebApiSergio.Controllers
{
    public class ClientesController : ApiController
    {
        Queries query = new Queries();

        // GET: api/Clientes
        public ObservableCollection<Clientes> Get()
        {
            var listCliente = query.ListaClientes(null);
            return listCliente;
        }
        
        // GET: api/Clientes/5
        public IHttpActionResult Get(int id)
        {
            IHttpActionResult response;
            //Código para retornar um link eterno caso não haja resultado pelo ID do cliente;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMsg.Headers.Location = new Uri("http://www.microsoft.com");
            response = ResponseMessage(responseMsg);
       
            var listCliente = query.ListaClientes(id);
            if (listCliente.Count == 0)
            {
                listCliente = null;
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
