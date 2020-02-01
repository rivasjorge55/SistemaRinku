using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Trabajador;
using Sistema.Web.Models.Trabajadores;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public TrabajadoresController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Trabajadores/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<TrabajadorViewModel>> Listar()
        {
            var trabajador = await _context.Trabajadores.ToListAsync();
            return trabajador.Select(t => new TrabajadorViewModel
            {
                id = t.id,
                nombre = t.nombre,
                idrol = t.idrol,
                idtipo = t.idtipo,
                activo = t.activo
            });
        }

        // GET: api/Trabajadores/vwtrabajadores
        [HttpGet("[action]")]
        public async Task<IEnumerable<vw_trabajadores>> vwtrabajadores()
        {
            var trabajador = await _context.vw_Trabajadores.ToListAsync();
            return trabajador;
        }

        // GET: api/Trabajadores/Mostrar/3
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);

            if (trabajador == null)
            {
                return NotFound();
            }

            return Ok(new TrabajadorViewModel {
                id=trabajador.id,
                nombre=trabajador.nombre,
                idrol=trabajador.idrol,
                idtipo=trabajador.idtipo,
                activo=trabajador.activo
            });
        }

        // PUT: api/Trabajadores/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <=0)
            {
                return BadRequest();
            }

            var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.id == model.id);

            if (trabajador == null)
            {
                return NotFound();
            }

            trabajador.nombre = model.nombre;
            trabajador.idrol = model.idrol;
            trabajador.idtipo = model.idtipo;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Trabajadores/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trabajador trabajador = new Trabajador
            {
                nombre = model.nombre,
                idtipo = model.idtipo,
                idrol = model.idrol,
                activo = true
            };

            _context.Trabajadores.Add(trabajador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }


            return Ok();
        }

        // DELETE: api/Trabajadores/Eliminar/3
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            _context.Trabajadores.Remove(trabajador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            

            return Ok(trabajador);
        }

        // PUT: api/Trabajadores/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.id == id);

            if (trabajador == null)
            {
                return NotFound();
            }
            trabajador.activo = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Trabajadores/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.id == id);

            if (trabajador == null)
            {
                return NotFound();
            }
            trabajador.activo = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.id == id);
        }
    }
}