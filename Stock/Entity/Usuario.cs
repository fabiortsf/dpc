using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    [DataContract(Name = "Usuario")]
    public class Usuario
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        public List<Perfil> Perfis { get; set; }
    }
}
