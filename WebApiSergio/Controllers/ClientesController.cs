using System.Collections.ObjectModel;
using System.Web.Http;
using WebApiSergio.Models;

namespace WebApiSergio.Controllers
{
    public class ClientesController : ApiController
    {
        Queries query = new Queries();

        // GET: api/Clientes
        public ObservableCollection<Cliente> Get()
        {
            var listCliente = query.ListaClientes(null);
            return listCliente;
        }
        
        // GET: api/Clientes/5
        public ObservableCollection<Cliente> Get(int id)
        {
            var listCliente = query.ListaClientes(id);
            return listCliente;
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
