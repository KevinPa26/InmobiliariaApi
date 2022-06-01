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
    public class InquilinoController : Controller
    {
        private readonly DataContext contexto;

        public InquilinoController(DataContext contexto)
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
                //return Ok(contexto.Inquilino.Join(contexto.Contrato, inq => inq.id, cont => cont.inquilinoid, (inq, cont) => new { inq, cont}).Where(x => x.cont.id == 5).Select(e => e.inq));
                return Ok(contexto.Inquilino.Join(contexto.Contrato.Include(e => e.inmueble).Where(e => e.inmueble.duenio.email == usuario), inq => inq.id, cont => cont.inquilinoid, (inq, cont) => new { inq, cont}).Select(e => e.inq));
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
                //return Ok(contexto.Contrato.Where(e => e.inmueble.duenio.email == usuario).Single(e => e.id == id));
                return Ok(contexto.Inquilino.Join(contexto.Contrato, inq => inq.id, cont => cont.inquilinoid, (inq, cont) => new { inq, cont}).Where(x => x.cont.id == id).Select(e => e.inq));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("porInmueble/{id}")]
        public async Task<IActionResult> getInquilinoPorInmueble(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inquilino.Join(contexto.Contrato.Include(e => e.inmueble).Where(e => e.inmueble.duenio.email == usuario && e.inmueble.id == id), inq => inq.id, cont => cont.inquilinoid, (inq, cont) => new { inq, cont}).Select(e => e.inq));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
	}
}
