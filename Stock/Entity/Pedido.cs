using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Entity
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime DataPedido { get; set; }

        public int IdTipo { get; set; }

        public decimal Valor { get; set; }

        public List<PedidoItem> Itens { get; set; }
    }
}
