using System;
using System.Collections.Generic;

namespace Examen.API.Data.Models;

public partial class TblPersona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public int Identificacion { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
}
