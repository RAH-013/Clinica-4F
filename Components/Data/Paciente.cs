using System.ComponentModel.DataAnnotations;

namespace Clinica.Components.Data
{
    public class Paciente
    {
        [Key]
        public string IdPaciente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Nombre Completo\" no puede quedar vacio.")]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Fecha de Nacimiento\" no puede quedar vacio.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo \"Correo Electronico\" no puede quedar vacio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo \"Telefono\" no puede quedar vacio.")]
        [StringLength(10)]
        public string? Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
