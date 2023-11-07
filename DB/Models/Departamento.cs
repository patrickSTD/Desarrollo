using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models;

public class Departamento
{

    public int Id { get; set; }

    public string? NombreDepartamento { get; set; }

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();

    public virtual ICollection<Trabajadores> Trabajadores { get; set; } = new List<Trabajadores>();
}
