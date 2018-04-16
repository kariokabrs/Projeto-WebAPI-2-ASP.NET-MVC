using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace WebApiSergio.Models
{
    
    interface Interface1
    {
        ObservableCollection<Clientes> ListaClientes(int? Id);
    }
    
}
