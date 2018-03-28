using System.Collections.ObjectModel;

namespace WebApiSergio.Models
{
    interface Interface1
    {
        ObservableCollection<Cliente> ListaClientes(int? Id);
    }

    public class Cliente
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

}
