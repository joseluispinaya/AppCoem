using System.ComponentModel.DataAnnotations;

namespace AppCoem.Mobile.Modelos
{
    public class EAfiliado
    {
        [Key]
        public int IdAfiliado { get; set; }
        public int Idasoci { get; set; }
        public string? NroCI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Celular { get; set; }
        public bool Estado { get; set; }
    }
}
