using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    // Esta es una clase controladora de API para manejar operaciones relacionadas con "Grados"
    [Route("api/[controller]")]  // Establece la ruta base para los endpoints como "api/grado"
    [ApiController]              // Indica que esta clase es un controlador de API con comportamientos predeterminados
    public class GradoController : ControllerBase // Hereda de ControllerBase (base para controladores API)
    {
        // Instancia del repositorio para operaciones con Grados
        private readonly RepoGrado _repository = new RepoGrado();

        // Endpoint HTTP GET para obtener todos los grados
        [HttpGet] //Mostrar
        public ActionResult<IEnumerable<Grado>> Get()
        {
            // Retorna una respuesta HTTP 200 (OK) con la lista de grados obtenida del repositorio
            return Ok(_repository.Mostrar());
        }

        // Endpoint HTTP DELETE para eliminar un grado por su ID
        [HttpDelete("{id}")] //Eliminar
        public ActionResult Delete(int id)
        {
            // Llama al método Eliminar del repositorio
            _repository.Eliminar(id);
            // Retorna una respuesta HTTP 200 (OK) con mensaje de confirmación
            return Ok("Grado eliminado correctamente.");
        }

        // Endpoint HTTP POST para agregar un nuevo grado
        [HttpPost] //Agregar
        public ActionResult Post([FromBody] Grado nuevoGrado)
        {
            // Intenta insertar el nuevo grado usando los datos del cuerpo de la solicitud
            bool resultado = _repository.Insertar(nuevoGrado.Id, nuevoGrado.Descripcion, nuevoGrado.GradoCompleto);
            
            if(resultado)
            {
                // Si la inserción fue exitosa, retorna HTTP 200 (OK)
                return Ok("Grado insertado.");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al insertar el grado");
            }
        }

        // Endpoint HTTP PUT para modificar un grado existente
        [HttpPut("{id}")] //Modificar
        public ActionResult Put(int id, [FromBody] Grado grado)
        {
            // Intenta modificar el grado con los datos proporcionados
            bool resultado = _repository.Modificar(grado.Id, grado.Descripcion, grado.GradoCompleto);
            
            if(resultado)
            {
                // Si la modificación fue exitosa, retorna HTTP 200 (OK)
                return Ok("Grado modificado");
            }
            else
            {
                // Si hubo error, retorna HTTP 400 (Bad Request)
                return BadRequest("Error al midificar el grado.");
            }
        }
    }
}