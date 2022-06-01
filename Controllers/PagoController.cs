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
    public class PagoController : Controller
    {
        private readonly DataContext contexto;

        public PagoController(DataContext contexto)
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
                return Ok(contexto.Pago.Include(e => e.contrato).Where(e => e.contrato.inmueble.duenio.email == usuario));
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
                return Ok(contexto.Pago.Include(e => e.contrato).Where(e => e.contrato.inmueble.duenio.email == usuario && e.id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("porContrato/{id}")]
        public async Task<IActionResult> getPagosPorContrato(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Pago.Include(e => e.contrato).Where(e => e.contrato.inmueble.duenio.email == usuario && e.contratoid == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
	}
}
