using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Models
{
    public class FacturaFormViewModel
    {
        public FacturaViewModel Factura { get; set; } = new();
        public List<SelectListItem> Personas { get; set; } = new();
    }
}
