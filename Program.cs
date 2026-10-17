using Microsoft.EntityFrameworkCore;
using MiAplicacionEF.DB;

var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores de la API.
builder.Services.AddControllers();

// Registrar Entity Framework Core y PostgreSQL.
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

// Configurar Swagger.
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


// Registrar los endpoints de los controladores.
app.MapControllers();

app.Run();