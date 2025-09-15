using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedArtesanias.Api.DTOs;
using System.Threading.Tasks;


[ApiController]
[Route("productos")]

public class ProductosController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async 
    Task<ActionResult<IEnumerable<ProductoDto>>> Listar()
    {
        var data = await _db.Productos
        .Include(p => p.Fotos)
        .AsNoTracking()
        .Select(p => new ProductoDto(
            p.Id, p.Nombre, p.Precio, p.Descripcion,
            p.ArtesanoId, p.Fotos.Select(f => f.Url).ToList()))
        .ToListAsync();
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDto>> Obtener(int id)
    {
        var p = await _db.Productos.Include(x => x.Fotos).FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();
        return new ProductoDto(p.Id, p.Nombre, p.Precio, p.Descripcion, p.ArtesanoId, p.Fotos.Select(f => f.Url).ToList());
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDto>>
    Crear([FromBody] CrearProductoDto dto)
    {
        var p = new Producto
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            Descripcion = dto.Descripcion,
            ArtesanoId = dto.ArtesanoId,
            Fotos = (dto.Fotos ?? new()).Select(u => new Foto
            {
                Url = u
            }).ToList()
        };
        // Corregido: se usa la variable _db en lugar de _dbProductos
        _db.Productos.Add(p);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obtener), new { id = p.Id },
            new ProductoDto(p.Id, p.Nombre, p.Precio,
            p.Descripcion, p.ArtesanoId, p.Fotos.Select(f => f.Url).ToList()));

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Actualizar(int id, [FromBody]
    CrearProductoDto dto)
    {
        var p = await _db.Productos.Include(x => x.Fotos).FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();
        p.Nombre = dto.Nombre;
        p.Precio = dto.Precio;
        p.Descripcion = dto.Descripcion;
        p.ArtesanoId = dto.ArtesanoId;
        p.Fotos.Clear();
        foreach (var u in dto.Fotos ?? new())
            p.Fotos.Add(new Foto { Url = u });
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var p = await _db.Productos.FindAsync(id);
        if (p is null) return NotFound();
        _db.Productos.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}