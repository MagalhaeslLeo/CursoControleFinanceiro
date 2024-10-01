using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFInanceiro.BLL.Models
{
    public class Tipo
    {
        public int TipoID { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }
    }
}
