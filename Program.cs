using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


//EF Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

//CORS
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(p =>
    p.WithOrigins("http://localhost:8080")//, "http://localhost:8000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

//JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        var cfg = builder.Configuration.GetSection("Jwt");
        opt.TokenValidationParameters = new()
        {
            ValidIssuer = cfg["Issuer"],
            ValidAudience = cfg["Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(cfg["Key"]!)
                )
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();//habilita wwwroot como raíz estática

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();


    // Añadir artesanos si la tabla está vacía
    if (!db.Artesanos.Any())
    {
        db.Artesanos.AddRange(
            new Artesano { Nombre = "Ana", Bio = "Técnicas de trenzado de caña flecha." },
            new Artesano { Nombre = "Luis", Bio = "Tejidos Wayúu con diseños personalizados." },
            new Artesano { Nombre = "María", Bio = "Cerámica utilitaria y decorativa." }
        );
        db.SaveChanges();
    }

    // Añadir productos si la tabla está vacía
    if (!db.Productos.Any())
    {
        db.Productos.AddRange(
            new Producto { Nombre = "Sombrero Vueltiao", Precio = 120000, ArtesanoId = 1, Descripcion = "Hecho a mano por artesanos Zenú." },
            new Producto { Nombre = "Mochila Wayúu", Precio = 180000, ArtesanoId = 2, Descripcion = "Tejido tradicional de La Guajira." },
            new Producto { Nombre = "Cerámica Negra", Precio = 95000, ArtesanoId = 3, Descripcion = "De La Chamba, Tolima." }
        );
        db.SaveChanges();
    }
}

app.Run();
