// Importamos una parte del proyecto que contiene los modelos de datos, como la clase "ArmEsp"
using Backend.Models;

// Importamos una herramienta que permite trabajar con bases de datos MySQL en C#
using MySql.Data.MySqlClient;

// Definimos un espacio donde se agrupa este código (una forma de organizar todo lo relacionado al "Backend")
namespace Backend.Data
{
    // Creamos una clase llamada "RepoArmEsp" que hereda (usa) la clase "DB_Conexion"
    // Esto le permite conectarse a la base de datos usando el método AbrirConexion()
    public class RepoArmEsp : DB_Conexion
    {
        // Creamos un método que devuelve una lista de grados desde la base de datos
        public List<ArmEsp> Mostrar()
        {
            // Creamos una lista vacía donde vamos a guardar los grados que traemos desde la base de datos
            List<ArmEsp> ArmEsp = new List<ArmEsp>();

            // Usamos la conexión a la base de datos (se abre automáticamente y se cierra al terminar)
            using (var connection = AbrirConexion())
            {
                // Creamos un comando SQL para seleccionar los datos de la tabla "grado"
                using (var command = new MySqlCommand("SELECT id_armesp, abreviatura, armesp_completo FROM armesp", connection))
                {
                    // Ejecutamos el comando y leemos los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        // Mientras haya datos por leer...
                        while (reader.Read())
                        {
                            // Creamos un nuevo objeto de tipo ArmEsp con los datos obtenidos
                            ArmEsp.Add(new ArmEsp
                            {
                                Id = reader.GetInt32("id_armesp"),
                                Descripcion = reader.GetString("abreviatura"),
                                ArmEspCompleto = reader.GetString("armesp_completo")
                            });
                        }
                    }
                }
            }

            // Devolvemos la lista con todos las ArmEsp encontradas
            return ArmEsp;
        }

        // Método para insertar un nuevo grado en la base de datos
        public bool Insertars(int id_armesp, string abreviatura, string armesp_completo)
        {
            // Abrimos conexión con la base de datos
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para insertar los datos (esta línea contiene un error: más abajo lo explico)
                using (var command = new MySqlCommand("INSERT INTO armesp (id_armesp, abreviatura, armesp_completo) VALUES (@abreviatura, '@armesp_completo') WHERE id_armesp = @idarmesp", connection))
                {
                    // Asociamos los valores a los parámetros definidos en el comando SQL
                    command.Parameters.AddWithValue("@idarmesp", id_armesp);
                    command.Parameters.AddWithValue("@abreviatura", abreviatura);
                    command.Parameters.AddWithValue("@armesp_completo", armesp_completo);

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
                        Console.WriteLine($"Error al modificar armesp: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para modificar un grado existente en la base de datos
        public bool Modificar(int id_armesp, string abreviatura, string armesp_completo)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para actualizar el registro que tenga el id indicado
                using (var command = new MySqlCommand("UPDATE armesp SET id_armesp = @idarmesp, abreviatura = @abreviatura, armesp_completo = @armesp_completo WHERE id_armesp = @idarmesp", connection))
                {
                    // Asociamos los valores a los parámetros
                    command.Parameters.AddWithValue("@idarmesp", id_armesp);
                    command.Parameters.AddWithValue("@abreviatura", abreviatura);
                    command.Parameters.AddWithValue("@armesp_completo", armesp_completo);

                    try
                    {
                        // Ejecutamos el comando y vemos cuántas filas se modificaron
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hay error, lo mostramos y devolvemos falso
                        Console.WriteLine($"Error al modificar armesp: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para eliminar un grado de la base de datos
        public bool Eliminar(int idarmesp)
        {
            // Abrimos conexión
            using (var connection = AbrirConexion())
            {
                // Creamos el comando SQL para eliminar un grado según su id
                using (var command = new MySqlCommand("DELETE FROM armesp WHERE id_armesp = @idarmesp", connection))
                {
                    // Asociamos el parámetro con el valor recibido
                    command.Parameters.AddWithValue("@idarmesp", idarmesp);

                    try
                    {
                        // Ejecutamos el comando y verificamos cuántas filas se eliminaron
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        // Si hay error, lo mostramos y devolvemos falso
                        Console.WriteLine($"Error al eliminar armesp: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}