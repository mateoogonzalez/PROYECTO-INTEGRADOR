// Usamos el espacio de nombres donde está definida la clase DB_Conexion
using Backend.Data;

// Creamos el "builder" de la aplicación web, que permite configurar servicios y middlewares
var builder = WebApplication.CreateBuilder(args);

// Configuramos CORS (Cross-Origin Resource Sharing) para permitir solicitudes desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", // Definimos una política llamada "AllowAll"
        policy =>
        {
            // Esta política permite cualquier origen, método y cabecera HTTP
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Registramos el servicio de controladores (para usar [ApiController] y rutas con atributos)
builder.Services.AddControllers();

// Registramos la clase DB_Conexion como un singleton
// Esto significa que se creará solo una instancia durante toda la vida de la aplicación
builder.Services.AddSingleton<DB_Conexion>();

// Construimos la aplicación con la configuración definida arriba
var app = builder.Build();

// Usamos la política de CORS que definimos anteriormente ("AllowAll")
app.UseCors("AllowAll");

// Redirige automáticamente las solicitudes HTTP a HTTPS si es posible
app.UseHttpsRedirection();

// Middleware para manejar la autorización (aunque no haya autenticación configurada aún)
app.UseAuthorization();

// Mapea los controladores con sus rutas definidas ([Route], [HttpGet], etc.)
app.MapControllers();

// Ejecuta la aplicación
app.Run();
