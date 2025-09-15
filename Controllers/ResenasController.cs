using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("resenas")]
public class ResenasController : ControllerBase
{
    private readonly AppDbContext _db;
    public ResenasController(AppDbContext db) => _db = db;
    [HttpGet("producto/{productoId}")]
    public async Task<IEnumerable<ResenaDto>> PorProducto(int productoId)
    => await _db.Resenas
            .Where(r => r.ProductoId == productoId)
            .AsNoTracking()
            .Select(r => new ResenaDto(r.Id, r.ProductoId,
            r.Producto, r.Rating, r.Comentario, r.Fecha))
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<ResenaDto>> Crear(CrearResenaDto dto)
    {
        var r = new Resena
        {
            ProductoId = dto.ProductoId,
            Producto = dto.Producto,
            Rating = dto.Rating,
            Comentario = dto.Comentario
        };
        _db.Resenas.Add(r); await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(PorProducto), 
        new { productoId = r.ProductoId},
            new ResenaDto(r.Id, r.ProductoId, r.Producto,
            r.Rating, r.Comentario, r.Fecha));
    }
}