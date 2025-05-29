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
        [Range(typeof(DateTime), "01/01/1920", "31/12/2025", ErrorMessage = "Fecha fuera de rango (\"01/01/2025\" - \"31/12/2025\")")]
        public DateTime FechaNacimiento { get; set; } = DateTime.ParseExact("01/01/1920", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        [Required(ErrorMessage = "El campo \"Correo Electronico\" no puede quedar vacio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo \"Telefono\" no puede quedar vacío.")]
        [RegularExpression(@"^\d{3}-\d{4}-\d{3}$", ErrorMessage = "El formato debe ser XXX-XXXX-XXX.")]
        public string? Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
