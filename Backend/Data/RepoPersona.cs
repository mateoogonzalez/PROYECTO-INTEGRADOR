// Importamos una parte del proyecto que contiene los modelos de datos, como la clase "Persona"
using Backend.Models;

// Importamos una herramienta que permite trabajar con bases de datos MySQL en C#
using MySql.Data.MySqlClient;

// Definimos un espacio donde se agrupa este código (una forma de organizar todo lo relacionado al "Backend")
namespace Backend.Data
{
    // Creamos una clase llamada "RepoGrado" que hereda (usa) la clase "DB_Conexion"
    // Esto le permite conectarse a la base de datos usando el método AbrirConexion()
    public class RepoPersona : DB_Conexion
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
                            personas.Add(new Persona
                            {
                                Id = reader.GetInt32("id_persona"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                DNI = reader.GetString("nro_dni")
                            });
                        }
                    }
                }
            }

            // Devolvemos la lista con todos los grados encontrados
            return personas;
        }
    


// Método para insertar un nuevo grado en la base de datos
        public bool Insertar(int id_persona, int id_grado, int id_armesp, string nombre, string apellido, string nro_dni)
        {
            // Abrimos conexión con la base de datos
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para insertar los datos (esta línea contiene un error: más abajo lo explico)
                using (var command = new MySqlCommand("INSERT INTO persona (id_grado, id_persona, id_armesp, nombre, apellido, nro_dni) VALUES (@idGrado, @idPersona, @idArmesp, @nombre, @apellido, @nroDni') WHERE id_´persona = @idPersona", connection))
                {
                    // Asociamos los valores a los parámetros definidos en el comando SQL
                    command.Parameters.AddWithValue("@id_persona", id_persona);
                    command.Parameters.AddWithValue("@id_Grado", id_grado);
                    command.Parameters.AddWithValue("@Grado", id_grado); // cambiar
                    command.Parameters.AddWithValue("@id_armesp", id_armesp);
                    command.Parameters.AddWithValue("@Armesp", id_grado); // cambiar
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@nro_dni", nro_dni);

                    try
                    {
                        // Ejecutamos el comando y obtenemos cuántas filas fueron afectadas
                        int rowsAffected = command.ExecuteNonQuery();

                        // Si se modificó al menos una fila, devolvemos verdadero (éxito)
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hubo un error, lo mostramos en consola y devolvemos falso
                        Console.WriteLine($"Error al modificar grado: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para modificar un grado existente en la base de datos
        public bool Modificar(int id_persona, string id_grado, string id_armesp, string nombre, string apellido, string nro_dni)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para actualizar el registro que tenga el id indicado
                using (var command = new MySqlCommand("UPDATE persona SET id_persona = @idPersona, id_grado = @idGrado, id_armesp = @idArmesp, nombre = @nombre, apellido = @apellido, nro_dni = @nroDni, WHERE id_persona = @idPersona", connection))
                {
                    // Asociamos los valores a los parámetros
                    command.Parameters.AddWithValue("@idPersona", id_persona);
                    command.Parameters.AddWithValue("@idGrado", id_grado);
                    command.Parameters.AddWithValue("@idArmesp", id_armesp);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@nroDni", nro_dni);

                    try
                    {
                        // Ejecutamos el comando y vemos cuántas filas se modificaron
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hay error, lo mostramos y devolvemos falso
                        Console.WriteLine($"Error al modificar persona: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para eliminar un grado de la base de datos
        public bool Eliminar(int idPersona)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para eliminar un grado según su id
                using (var command = new MySqlCommand("DELETE FROM persona WHERE id_persona = @idPersona", connection))
                {
                    // Asociamos el parámetro con el valor recibido
                    command.Parameters.AddWithValue("@idPersona", idPersona);

                    try
                    {
                        // Ejecutamos el comando y verificamos cuántas filas se eliminaron
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hay error, lo mostramos y devolvemos falso
                        Console.WriteLine($"Error al eliminar grado: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        internal bool Modificar(int id, string nombre, string apellido, string dNI)
        {
            throw new NotImplementedException();
        }
    }
}