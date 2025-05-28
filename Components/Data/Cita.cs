using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Components.Data
{
    public class Cita
    {
        [Key]
        public string IdCita { get; set; } = string.Empty;
        [ForeignKey(nameof(IdPaciente))]
        [Required(ErrorMessage = "El campo \"Paciente\" no puede quedar vacio.")]
        public string IdPaciente { get; set; } = string.Empty;
        public string Paciente { get; set; } = string.Empty;
        [ForeignKey(nameof(IdMedico))]
        [Required(ErrorMessage = "El campo \"Medico\" no puede quedar vacio.")]
        public string IdMedico { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo \"Fecha de la Cita\" no puede quedar vacío.")]
        [Range(typeof(DateTime), "01/01/2025", "31/12/2025", ErrorMessage = "Fecha fuera de rango")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "El campo \"Hora\" no puede quedar vacío.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "El campo \"Estado\" no puede quedar vacío.")]
        [RegularExpression("pendiente|confirmada|cancelada|completada", ErrorMessage = "El estado no es válido.")]
        public string Estado { get; set; } = "pendiente";
    }
}
