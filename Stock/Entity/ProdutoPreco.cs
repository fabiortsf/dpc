using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    [DataContract(Name = "ProdutoPreco")]
    public class ProdutoPreco
    {
        [DataMember]
        public string CodigoBarras { get; set; }

        [DataMember]
        public string CodTabelaPreco { get; set; }

        [DataMember]
        public decimal Preco { get; set; }

        [DataMember]
        public char Ativo { get; set; }
    }
}
