using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    // Esta es una clase controladora de API para manejar operaciones relacionadas con "Personal"
    [Route("api/[controller]")]  // Establece la ruta base para los endpoints como "api/personal"
    [ApiController]              // Indica que esta clase es un controlador de API con comportamientos predeterminados
    public class PersonalController : ControllerBase // Hereda de ControllerBase (base para controladores API)
    {
        // Instancia del repositorio para operaciones con Personal
        private readonly RepoPersona _repository = new RepoPersona();

        // Endpoint HTTP GET para obtener todos los personal
        [HttpGet] //Mostrar
        public ActionResult<IEnumerable<Persona>> Get()
        {
            // Retorna una respuesta HTTP 200 (OK) con la lista de personal obtenida del repositorio
            return Ok(_repository.Mostrar());
        }

        // Endpoint HTTP DELETE para eliminar un personal por su ID
        [HttpDelete("{id}")] //Eliminar
        public ActionResult Delete(int id)
        {
            // Llama al método Eliminar del repositorio
            _repository.Eliminar(id);
            // Retorna una respuesta HTTP 200 (OK) con mensaje de confirmación
            return Ok("Personal eliminado correctamente.");
        }

        // Endpoint HTTP POST para agregar un nuevo personal
        [HttpPost] //Agregar
        public ActionResult Post([FromBody] Persona nuevoPersonal)
        {
            // Intenta insertar el nuevo personal usando los datos del cuerpo de la solicitud
            bool resultado = _repository.Insertar(nuevoPersonal.Id, nuevoPersonal.GradoId, nuevoPersonal.ArmEspId, nuevoPersonal.Nombre, nuevoPersonal.Apellido, nuevoPersonal.DNI);

            if (resultado)
            {
                // Si la inserción fue exitosa, retorna HTTP 200 (OK)
                return Ok("Personal insertado.");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al insertar el personal");
            }
        }

        // Endpoint HTTP PUT para modificar un personal existente
        [HttpPut("{id}")] //Modificar
        public ActionResult Put(int id, [FromBody] Persona personal)
        {
            // Intenta modificar el personal con los datos proporcionados
            bool resultado = _repository.Modificar(personal.Id, personal.Nombre, personal.Apellido, personal.DNI);            
            if(resultado)
            {
                // Si la modificación fue exitosa, retorna HTTP 200 (OK)
                return Ok("Personal modificado");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al modificar el personal");
            }
        }
    }
}