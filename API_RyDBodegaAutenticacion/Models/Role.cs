using System;
using System.Collections.Generic;

namespace API_RyDBodegaAutenticacion.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public string NombreRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
