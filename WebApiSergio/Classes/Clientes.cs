using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiSergio
{
    //Para usar o DataContract tem que referenciar a DLL System.Runtime.Serialization no projeto!!
    //DataContract aqui diz que o posso determinar que apareça como respota a webapi no formato Json. Ou seja quais as propriodades apareceram e para isso , basta colocar um [DataMeber] acima de cada propriedade que quer que apareça. Caso não coloque, a propriedade não será chamada. 

    [DataContract(Name = "Clientes", Namespace = "http:/alphabetacom.wordpres.com")]
    public class Clientes
    {
        //Se especificar um nome no DataMember o nome da coluna da propriedade não será mostrado e sim o nome que especifiquei no DataMember. 

        [DataMember(Name ="ClienteId")]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]

        //Pode-se também o usar apenas o atributo [JsonIgnore] para omitir a propriedade
        //[JsonIgnore]

        public string Cpf { get; set; }
    }
}