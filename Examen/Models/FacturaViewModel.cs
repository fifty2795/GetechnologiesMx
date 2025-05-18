namespace Examen.Models
{
    public class FacturaViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int IdPersona { get; set; }
        public PersonaDTO? Persona { get; set; }
    }  

    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public int Identificacion { get; set; }
    }
}
