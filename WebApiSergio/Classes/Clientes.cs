using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApiSergio
{
    //Para usar o DataContract tem que referenciar a DLL System.Runtime.Serialization no projeto!!
    //DataContract aqui diz que o posso determinar que apareça como respota a webapi no formato Json. Ou seja quais as propriedades apareceram e para isso , basta colocar um [DataMeber] acima de cada propriedade que quer que apareça. Caso não coloque, a propriedade não será chamada. 
    /// <summary>
    /// Classe Principal
    /// </summary>
    [DataContract(Name = "Clientes", Namespace = "http:/alphabetacom.wordpres.com")]
    public class Clientes
    {
        //Se especificar um nome no DataMember o nome da coluna da propriedade não será mostrado e sim o nome que especifiquei no DataMember. 
        //[DataMember(Name = "ClienteId")]
        public int Id { get; set; }
        //[DataMember]
        public string Nome { get; set; }
        public List<_Cpf> Cpf { get; set; }
      
        public class _Cpf 
        {
            public string Cpf { get; set; }
        }

    }
}

