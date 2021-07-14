using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsquemaDinamico.Data
{
    public class Tabla
    {
        [Key]
        public string Name { set; get; }

    }
}
