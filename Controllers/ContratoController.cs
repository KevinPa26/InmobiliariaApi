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
    public class ContratoController : Controller
    {
        private readonly DataContext contexto;

        public ContratoController(DataContext contexto)
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
                return Ok(contexto.Contrato.Include(e => e.inmueble).Include(e => e.inquilino).Where(e => e.inmueble.duenio.email == usuario));
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
                return Ok(contexto.Contrato.Where(e => e.inmueble.duenio.email == usuario).Single(e => e.id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("porInmueble/{id}")]
        public async Task<IActionResult> getContratoPorInmueble(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Contrato.Include(e => e.inmueble).Include(e => e.inquilino).Where(e => e.inmueble.duenio.email == usuario && e.inmuebleid == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Contrato entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = User.Identity.Name;
                    var inmuebleDuenioEmail = contexto.Inmueble.Include(e => e.duenio).Where(e => e.duenio.email == usuario).Single(e => e.id == entidad.inmuebleid).duenio.email;
                    if(inmuebleDuenioEmail == usuario){
                        await contexto.Contrato.AddAsync(entidad);
                        contexto.SaveChanges();
                        return CreatedAtAction(nameof(Get), new { id = entidad.id }, entidad);
                    }
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
        public async Task<IActionResult> Put(int id, Contrato entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Contrato.AsNoTracking().FirstOrDefault(e => e.id == id && e.inmueble.duenio.email == User.Identity.Name) != null)
                {
                    entidad.id = id;
                    contexto.Contrato.Update(entidad);
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
                var entidad = contexto.Contrato.FirstOrDefault(e => e.id == id && e.inmueble.duenio.email == User.Identity.Name);
                if (entidad != null)
                {
                    contexto.Contrato.Remove(entidad);
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
