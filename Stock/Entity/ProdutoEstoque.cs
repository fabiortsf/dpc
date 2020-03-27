using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    [DataContract(Name = "ProdutoEstoque")]
    public class ProdutoEstoque
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string CodigoFilial { get; set; }

        [DataMember]
        public string CodigoDeBarras { get; set; }

        [DataMember]
        public int Quantidade { get; set; }

        [DataMember]
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
