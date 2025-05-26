using System;

namespace Backend.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int GradoId { get; set; }
        public int ArmEspId { get; set; }
        public string DNI { get; set; }
        public string Dirección { get; set; }
    }
}