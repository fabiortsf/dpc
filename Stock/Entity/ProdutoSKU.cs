using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    [DataContract(Name = "ProdutoSKU")]
    public class ProdutoSKU
    {
        [DataMember]
        public string CodigoDeBarras { get; set; }

        [DataMember]
        public string CodigoProduto { get; set; }

    }
}
