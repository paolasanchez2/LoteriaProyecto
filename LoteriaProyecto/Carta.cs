using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaProyecto
{
    public class Carta
    {
        // Propiedades de la clase (Encapsulamiento)
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public string RutaSonido { get; set; }

        // Constructor para inicializar la carta
        public Carta(int id, string nombre, string rutaImagen, string rutaSonido)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.RutaImagen = rutaImagen;
            this.RutaSonido = rutaSonido;
        }
    }
}
