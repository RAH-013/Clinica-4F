﻿using System.ComponentModel.DataAnnotations;

namespace Clinica.Components.Data
{
    public class Medico
    {
        [Key]
        public string IdMedico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Nombre Completo\" no puede quedar vacio.")]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Especialidad\" no puede quedar vacio.")]
        public string Especialidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Correo Electronico\" no puede quedar vacio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo \"Telefono\" no puede quedar vacío.")]
        [RegularExpression(@"^\d{3}-\d{4}-\d{3}$", ErrorMessage = "El formato debe ser XXX-XXXX-XXX.")]
        public string? Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public override string ToString()
        {
            return IdMedico;
        }
    }
}
