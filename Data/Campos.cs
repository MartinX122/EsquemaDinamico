using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EsquemaDinamico.Data
{
    public class Campos
    {
        [Key]
        public int id { set; get; }


        [ForeignKey("Tabla")]
        public string NombreTabla { set; get; }

        public Tabla Tabla { set; get; }


        public string Nombre { set; get; }

        public string Desc { set; get; }

        [Column("Tipo")]
        [Display(Name = "Tipo")]
        public int TipoID { set; get; }

        [NotMapped]
        public Tipo Tipo { set => TipoID = (int)value; get => (Tipo)this.TipoID; }

        [NotMapped]
        public string NombreAnterior { set; get; }
        
        public static string GetTipoKey(Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.TEXT:
                    return "TEXT";

                case Tipo.NUMERIC:
                    return "NUMERIC";

                default:
                    throw new IndexOutOfRangeException("Valor no soportado");
            }
        }

    }


    public enum Tipo : int
    {

        [Display(Name = "Numero")]
        NUMERIC,
        [Display(Name = "Texto")]
        TEXT

       
    }
}
