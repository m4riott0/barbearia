using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly List<Appointment> _appointments;

        public AppointmentsController()
        {
            // Inicialize a lista de agendamentos
            _appointments = new List<Appointment>();
        }

        // Obtém todos os agendamentos
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _appointments;
        }

        // Obtém um agendamento pelo ID
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointmentById(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }

        // Cria um novo agendamento
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment appointment)
        {
            appointment.Id = _appointments.Count + 1;
            _appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

        // Atualiza um agendamento existente
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment.CustomerName = updatedAppointment.CustomerName;
            appointment.Time = updatedAppointment.Time;
            return NoContent();
        }

        // Deleta um agendamento
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            _appointments.Remove(appointment);
            return NoContent();
        }
    }

    // Classe modelo para representar um agendamento
    public class Appointment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Time { get; set; }
    }
}