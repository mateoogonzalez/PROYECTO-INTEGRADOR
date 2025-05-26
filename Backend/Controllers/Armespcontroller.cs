using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    // Esta es una clase controladora de API para manejar operaciones relacionadas con "Personal"
    [Route("api/[controller]")]  // Establece la ruta base para los endpoints como "api/personal"
    [ApiController]              // Indica que esta clase es un controlador de API con comportamientos predeterminados
    public class ArmespController : ControllerBase // Hereda de ControllerBase (base para controladores API)
    {
        // Instancia del repositorio para operaciones con Personal
        private readonly RepoArmEsp _repository = new RepoArmEsp();

        // Endpoint HTTP GET para obtener todos los personal
        [HttpGet] //Mostrar
        public ActionResult<IEnumerable<ArmEsp>> Get()
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
            return Ok("Arm/Esp eliminado correctamente.");
        }

        // Endpoint HTTP POST para agregar un nuevo personal
        [HttpPost] //Agregar
        public ActionResult Post([FromBody] ArmEsp nuevoArmEsp)
        {
            // Intenta insertar el nuevo personal usando los datos del cuerpo de la solicitud
            bool resultado = _repository.Insertar(nuevoArmEsp.Id, nuevoArmEsp.Descripcion, nuevoArmEsp.ArmEspCompleto);

            if (resultado)
            {
                // Si la inserción fue exitosa, retorna HTTP 200 (OK)
                return Ok("Arm/Esp insertado.");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al insertar el Arm/Esp");
            }
        }

        // Endpoint HTTP PUT para modificar un personal existente
        [HttpPut("{id}")] //Modificar
        public ActionResult Put(int id, [FromBody] Persona personal)
        {
            // Intenta modificar el personal con los datos proporcionados
            bool resultado = _repository.Modificar(nuevoarmEsp.Id, nuevoArmEsp.Descripcion, nuevoArmEsp.ArmEspCompleto);            
            if(resultado)
            {
                // Si la modificación fue exitosa, retorna HTTP 200 (OK)
                return Ok("Arm/Esp modificado");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al modificar el Arm/Esp");
            }
        }
    }
}