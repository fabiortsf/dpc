using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    public class PedidoItem
    {
        public int Id { get; set; }

        public int IdPedido { get; set; }

        public string codProduto { get; set; }

        public string codProdutoSKU { get; set; }

        public decimal Valor { get; set; }
    }
}
