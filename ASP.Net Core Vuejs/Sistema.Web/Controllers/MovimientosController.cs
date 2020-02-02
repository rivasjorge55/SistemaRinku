using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Nominas;
using Sistema.Entidades.Registros;
using Sistema.Web.Models.Movimientos;
using Sistema.Web.Models.Nominas;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public MovimientosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Movimientos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MovimientosViewModel>> Listar()
        {
            var trabajador = await _context.Movimientos.ToListAsync();
            return trabajador.Select(m => new MovimientosViewModel
            {
                id = m.id,
                idtrabajador=m.idtrabajador,
                idrol=m.idrol,
                fecha=m.fecha,
                entregas=m.entregas 
            });
        }

        // GET: api/Movimientos/Nomina/1
        [HttpGet("[action]/{mes}")]
        public async Task<IEnumerable<Nomina>> Nomina([FromRoute] int mes)
        {
            // Initialization.  
            List<Nomina> lst = new List<Nomina>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@mes", mes );

                // Processing.  
                string sqlQuery = "EXEC [dbo].[store_nominaMensual] @mes";

                lst = await _context.Query<Nomina>().FromSql(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        //// GET: api/Movimientos/Nominanomina
        //[HttpGet("[action]/{id}")]
        //public async Task<IEnumerable<Nomina>> Nomina([FromRoute] int id)
        //{
        //    SqlParameter mes = new SqlParameter("@mes", id);
        //    var nomina = await _context.Nominas.FromSql<Nomina>("store_nominaMensual @mes", mes).ToListAsync();
        //    return nomina;

        //    // context.Database.SqlQuery<Nomina>(
        //    //     "store_nominaMensual @mes",
        //    //     new SqlParameter("param1", param1));

        //    //var results = this.Database.SqlQuery<Nomina>("EXEC [dbo].[store_nominaMensual] {0}", param1);


        //}


        // GET: api/Movimientos/vwmovimientos
        [HttpGet("[action]")]
        public async Task<IEnumerable<vw_movimientos>> vwmovimientos()
        {
            var movimiento = await _context.vw_movimientos.ToListAsync();
            return movimiento;
        }

        // GET: api/Movimientos/Mostrar/3
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return Ok(new MovimientosViewModel
            {
                id = movimiento.id,
                idtrabajador = movimiento.idtrabajador,
                idrol = movimiento.idrol,
                fecha = movimiento.fecha,
                entregas = movimiento.entregas
            });
        }

        // PUT: api/Movimientos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <= 0)
            {
                return BadRequest();
            }

            var movimiento = await _context.Movimientos.FirstOrDefaultAsync(m => m.id == model.id);

            if (movimiento == null)
            {
                return NotFound();
            }

            movimiento.idtrabajador = model.idtrabajador;
            movimiento.idrol = model.idrol;
            movimiento.fecha = model.fecha;
            movimiento.entregas = model.entregas;
            
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

        // POST: api/Movimientos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movimiento movimiento = new Movimiento
            {
                idtrabajador = model.idtrabajador,
                idrol = model.idrol,
                fecha = model.fecha,
                entregas = model.entregas
            };

            _context.Movimientos.Add(movimiento);
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
        ///
        // DELETE: api/Movimientos/Eliminar/3
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(movimiento);
        }
        
        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.id == id);
        }
    }
}