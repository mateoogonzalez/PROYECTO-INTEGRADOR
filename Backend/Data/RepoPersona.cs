// Importamos una parte del proyecto que contiene los modelos de datos, como la clase "Persona"
using Backend.Models;

// Importamos una herramienta que permite trabajar con bases de datos MySQL en C#
using MySql.Data.MySqlClient;

// Definimos un espacio donde se agrupa este código (una forma de organizar todo lo relacionado al "Backend")
namespace Backend.Data
{
    // Creamos una clase llamada "RepoGrado" que hereda (usa) la clase "DB_Conexion"
    // Esto le permite conectarse a la base de datos usando el método AbrirConexion()
    public class RepoPersona: DB_Conexion
    {
        // Creamos un método que devuelve una lista de grados desde la base de datos
        public List<Persona> Mostrar()
        {
            // Creamos una lista vacía donde vamos a guardar los grados que traemos desde la base de datos
            List<Persona> personas = new List<Persona>();

            // Usamos la conexión a la base de datos (se abre automáticamente y se cierra al terminar)
            using (var connection = AbrirConexion())
            {
                // Creamos un comando SQL para seleccionar los datos de la tabla "grado"
                using (var command = new MySqlCommand("SELECT id_persona, nombre, apellido, nro_dni FROM persona", connection))
                {
                    // Ejecutamos el comando y leemos los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        // Mientras haya datos por leer...
                        while (reader.Read())
                        {
                            // Creamos un nuevo objeto de tipo Grado con los datos obtenidos
                            personas.Add(new persona
                            {
                                _id_persona = reader.GetInt32("id_persona"),
                                _nombre = reader.GetString("nombre"),
                                _apellido = reader.GetString("apellido")
                                _nro_dni = reader.GetString("nro_dni")
                            });
                        }
                    }
                }
            }

            // Devolvemos la lista con todos los grados encontrados
            return personas; 
        }
    }
}