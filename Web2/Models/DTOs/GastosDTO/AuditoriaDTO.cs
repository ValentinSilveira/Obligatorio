namespace Web.Models.DTOs.GastosDTO
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Entidad { get; set; }
        public string Operacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
    }
}
