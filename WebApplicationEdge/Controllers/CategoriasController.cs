using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationEdge.DAL;
using WebApplicationEdge.Models;

namespace WebApplicationEdge.Controllers
{
    [RoutePrefix("api/Categorias")]
    public class CategoriasController : ApiController
    {
        private readonly DBContext _db = new DBContext();
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Categoria> categorias = (from p in _db.Categorias
                                          select p).ToList();
            return Ok(categorias);
        }


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get([FromUri] int id)
        {
            Categoria c = (from x in _db.Categorias
                           where x.Id == id
                           select x).FirstOrDefault();
            if (c == null)
            {
                return BadRequest();
            }
            return Ok(c);
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody]Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    return BadRequest();
                }
                base.Validate(categoria);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _db.Categorias.Add(categoria);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri]int id, [FromBody] Categoria categoria)
        {
            try
            {
                Categoria c = (from x in _db.Categorias
                               where x.Id == id
                               select x).FirstOrDefault();

                if (c == null)
                {
                    return NotFound();
                }
                base.Validate(categoria);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                c.Name = categoria.Name;
                _db.Categorias.Attach(c);
                _db.Entry(c).State = EntityState.Modified;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Categoria categoria = (from x in _db.Categorias
                                       where x.Id == id
                                       select x).FirstOrDefault();

                if (categoria == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _db.Categorias.Remove(categoria);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
