using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_.Net_Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InmobiliariaApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InmobiliariaApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InmuebleController : Controller
    {
        private readonly DataContext contexto;

        public InmuebleController(DataContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmueble.Include(e => e.duenio).Where(e => e.duenio.email == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmueble.Include(e => e.duenio).Where(e => e.duenio.email == usuario).Single(e => e.id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.propietarioid = contexto.Propietario.Single(e => e.email == User.Identity.Name).id;
                    await contexto.Inmueble.AddAsync(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.id }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Inmueble.AsNoTracking().Include(e=>e.duenio).FirstOrDefault(e => e.id == id && e.duenio.email == User.Identity.Name) != null)
                {
                    entidad.id = id;
                    contexto.Inmueble.Update(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = contexto.Inmueble.Include(e => e.duenio).FirstOrDefault(e => e.id == id && e.duenio.email == User.Identity.Name);
                if (entidad != null)
                {
                    contexto.Inmueble.Remove(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
		}

		// DELETE api/<controller>/5
		[HttpDelete("BajaLogica/{id}")]
		public async Task<IActionResult> BajaLogica(int id)
		{
			try
			{
				var entidad = contexto.Inmueble.Include(e => e.duenio).FirstOrDefault(e => e.id == id && e.duenio.email == User.Identity.Name);
				if (entidad != null)
				{
					entidad.estado = 0;//cambiar por estado = 0
					contexto.Inmueble.Update(entidad);
					contexto.SaveChanges();
					return Ok();
				}
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
