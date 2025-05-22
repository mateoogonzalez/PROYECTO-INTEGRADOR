// Usamos una librería (conjunto de herramientas) que nos permite trabajar con bases de datos MySQL en C#
using MySql.Data.MySqlClient;

// Definimos un "espacio de nombres", una forma de organizar el código.
// Aquí se agrupa todo el código relacionado con los datos.
namespace Backend.Data
{
    // Creamos una clase (una especie de molde para crear objetos) que se llama "DB_Conexion"
    // Esta clase va a encargarse de conectarse a una base de datos.
    public class DB_Conexion
    {
        // Creamos una variable privada (solo accesible dentro de esta clase) que guarda los datos
        // necesarios para conectarse a la base de datos: el servidor, el nombre de la base, el usuario y la contraseña.
        private string connectionString = "server=localhost; database=db_app_cps; user=root; password=;";

        // Creamos un método público (algo que se puede usar desde fuera de la clase) que se llama "AbrirConexion"
        // Este método sirve para abrir la conexión con la base de datos.
        public MySqlConnection AbrirConexion()
        {
            // Creamos un nuevo objeto de tipo MySqlConnection usando los datos de conexión que guardamos antes
            MySqlConnection connection = new MySqlConnection(connectionString);

            // Abrimos la conexión con la base de datos
            connection.Open();
            Console.WriteLine("conexion exitosa");

            // Devolvemos la conexión ya abierta para que se pueda usar en otra parte del programa
            return connection;
        }
    }
}
