using System;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Components.Data
{
    public class Cita
    {
        [Key]
        public string IdCita { get; set; } = string.Empty;
        public string IdPaciente { get; set; } = string.Empty;
        public string Paciente { get; set; } = string.Empty;
        public string IdMedico { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo \"Fecha de la Cita\" no puede quedar vacío.")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "El campo \"Hora de inicio\" no puede quedar vacío.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "El campo \"Hora de fin\" no puede quedar vacío.")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El campo \"Estado\" no puede quedar vacío.")]
        [RegularExpression("pendiente|confirmada|cancelada|completada", ErrorMessage = "El estado no es válido.")]
        public string Estado { get; set; } = "pendiente";
    }
}
