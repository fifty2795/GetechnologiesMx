using System;
using System.Collections.Generic;

namespace Examen.API.Data.Models;

public partial class TblFactura
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Monto { get; set; }

    public int IdPersona { get; set; }

    public virtual TblPersona IdPersonaNavigation { get; set; } = null!;
}
