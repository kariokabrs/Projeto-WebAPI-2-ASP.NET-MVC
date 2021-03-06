﻿using System.Collections.ObjectModel;
using System.Net.Security;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApiSergio.Models
{

    //Aqui implemento o ServiceContract par adizer quais métodos da classe queries serão usados para o cliente que consumir esta WebAPi. Para isso basta colocar o OperatorContract em cada me´todo definido na classe. POde ser na interface ou diretamente na classe.Não esquecer de referenciar a DLL no Projet System.ServiceModel.

    [ServiceContract(Name = "Interface", Namespace = "http://alphaebetacom.wordpress.com", ProtectionLevel = ProtectionLevel.EncryptAndSign)]
    interface Interface1
    {
        [OperationContract]
        Task<ObservableCollection<Clientes>> ListaClientesAsync(int? Id);
        [OperationContract]
        Task InserirClienteAsync(string nome, string cpf_cnpj);
        [OperationContract]
        Task AtualizarClienteAsync(int clienteid, string nome, string cpf_cnpj);
        [OperationContract]
        Task DeletarClienteAsync(int clienteid);
    }

}
