using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFInanceiro.BLL.Models
{
    public class Mes
    {
        public int MesID { get; set; }
        public string Nome { get; set;}
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<Ganho> Ganhos { get; set; }
    }
}
