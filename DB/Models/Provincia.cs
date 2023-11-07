using System;
using System.Collections.Generic;

namespace DB.Models;

public class Provincia
{
    public int Id { get; set; }

    public string? NombreProvincia { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual ICollection<Trabajadores> Trabajadores { get; set; } = new List<Trabajadores>();
}
