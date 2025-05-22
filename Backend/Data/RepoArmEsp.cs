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
    }
}