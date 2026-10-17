using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiAplicacionEF.DB;
namespace MiAplicacionEF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductosController(AppDbContext context){
        _context = context;
    }

    // GET: /api/productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> ObtenerTodos(){
        var productos = await _context.Productos.AsNoTracking().OrderBy(p => p.Id).ToListAsync();
        return Ok(productos);
    }

    // GET: /api/productos/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Producto>> ObtenerPorId(int id)
    {
        var producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (producto is null){
            return NotFound();
        }
        return Ok(producto);
    }

    // POST: /api/productos
    [HttpPost]
    public async Task<ActionResult<Producto>> Crear(Producto producto){
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
    }

    // PUT: /api/productos/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Producto producto){
        if (id != producto.Id){
            return BadRequest("El ID de la URL no coincide con el ID del producto.");
        }

        var existente = await _context.Productos.FindAsync(id);

        if (existente is null){
            return NotFound();
        }
        existente.Nombre = producto.Nombre;
        existente.Precio = producto.Precio;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: /api/productos/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id){
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null){
            return NotFound();
        }

        _context.Productos.Remove(producto);

        await _context.SaveChangesAsync();
        return NoContent();
    }
}