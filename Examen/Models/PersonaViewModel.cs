namespace Examen.Models
{
    public class PersonaViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string? ApellidoMaterno { get; set; }

        public int Identificacion { get; set; }

        public string? Token { get; set; }
    }
}
