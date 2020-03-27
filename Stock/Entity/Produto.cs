using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    [DataContract(Name = "Produto")]
    public class Produto
    {
        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public int IdTipo { get; set; }

        [DataMember]
        public List<ProdutoSKU> Grades { get; set; }

        [DataMember]
        public List<ProdutoPreco> Precos { get; set; }


        [DataMember]
        public ProdutoEstoque Estoque { get; set; }
    }
}
