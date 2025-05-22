// Importamos una parte del proyecto que contiene los modelos de datos, como la clase "Grado"
using Backend.Models;

// Importamos una herramienta que permite trabajar con bases de datos MySQL en C#
using MySql.Data.MySqlClient;

// Definimos un espacio donde se agrupa este código (una forma de organizar todo lo relacionado al "Backend")
namespace Backend.Data
{
    // Creamos una clase llamada "RepoGrado" que hereda (usa) la clase "DB_Conexion"
    // Esto le permite conectarse a la base de datos usando el método AbrirConexion()
    public class RepoGrado : DB_Conexion
    {
        // Creamos un método que devuelve una lista de grados desde la base de datos
        public List<Grado> Mostrar()
        {
            // Creamos una lista vacía donde vamos a guardar los grados que traemos desde la base de datos
            List<Grado> grados = new List<Grado>();

            // Usamos la conexión a la base de datos (se abre automáticamente y se cierra al terminar)
            using (var connection = AbrirConexion())
            {
                // Creamos un comando SQL para seleccionar los datos de la tabla "grado"
                using (var command = new MySqlCommand("SELECT id_grado, abreviatura, gradocompleto FROM grado", connection))
                {
                    // Ejecutamos el comando y leemos los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        // Mientras haya datos por leer...
                        while (reader.Read())
                        {
                            // Creamos un nuevo objeto de tipo Grado con los datos obtenidos
                            grados.Add(new Grado
                            {
                                Id = reader.GetInt32("id_grado"),
                                Descripcion = reader.GetString("abreviatura"),
                                GradoCompleto = reader.GetString("gradocompleto")
                            });
                        }
                    }
                }
            }

            // Devolvemos la lista con todos los grados encontrados
            return grados;
        }

        // Método para insertar un nuevo grado en la base de datos
        public bool Insertar(int idGrado, string abreviatura, string gradoCompleto)
        {
            // Abrimos conexión con la base de datos
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para insertar los datos (esta línea contiene un error: más abajo lo explico)
                using (var command = new MySqlCommand("INSERT INTO grado (abreviatura, gradocompleto) VALUES (@abreviatura, '@gradoCompleto') WHERE id_grado = @idGrado", connection))
                {
                    // Asociamos los valores a los parámetros definidos en el comando SQL
                    command.Parameters.AddWithValue("@idGrado", idGrado);
                    command.Parameters.AddWithValue("@abreviatura", abreviatura);
                    command.Parameters.AddWithValue("@gradoCompleto", gradoCompleto);

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
        public bool Modificar(int idGrado, string abreviatura, string gradoCompleto)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para actualizar el registro que tenga el id indicado
                using (var command = new MySqlCommand("UPDATE grado SET abreviatura = @abreviatura, grado_completo = @grado_completo WHERE id_grado = @idGrado", connection))
                {
                    // Asociamos los valores a los parámetros
                    command.Parameters.AddWithValue("@idGrado", idGrado);
                    command.Parameters.AddWithValue("@abreviatura", abreviatura);
                    command.Parameters.AddWithValue("@gradoCompleto", gradoCompleto);

                    try
                    {
                        // Ejecutamos el comando y vemos cuántas filas se modificaron
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hay error, lo mostramos y devolvemos falso
                        Console.WriteLine($"Error al modificar grado: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para eliminar un grado de la base de datos
        public bool Eliminar(int idGrado)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para eliminar un grado según su id
                using (var command = new MySqlCommand("DELETE FROM grado WHERE id_grado = @idGrado", connection))
                {
                    // Asociamos el parámetro con el valor recibido
                    command.Parameters.AddWithValue("@idGrado", idGrado);

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
    }
}
