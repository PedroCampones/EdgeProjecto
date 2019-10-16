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
    [RoutePrefix("api/Produtos")]
    public class ProdutosController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Produto> _produtos = (from p in _db.Produtos
                                       select p).ToList();
            return Ok(_produtos);
        }


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get([FromUri] int id)
        {
            Produto p = (from x in _db.Produtos
                         where x.Id == id
                         select x).FirstOrDefault();
            if (p == null)
            {
                return BadRequest();
            }
            return Ok(p);
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody]Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return BadRequest();
                }
                bool existeCategoria = (from _c in _db.Categorias
                                        where _c.Id == produto.CategoriaId
                                        select _c).Any();
                if (!existeCategoria)
                {
                    return BadRequest("Não existe a categoria selecionada!");
                }
                base.Validate(produto);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _db.Produtos.Add(produto);
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
        public IHttpActionResult Update([FromUri]int id, [FromBody] Produto produto)
        {
            try
            {
                Produto p = (from x in _db.Produtos
                             where x.Id == id
                             select x).FirstOrDefault();

                if (p == null)
                {
                    return NotFound();
                }
                bool existeCategoria = (from _c in _db.Categorias
                                        where _c.Id == produto.CategoriaId
                                        select _c).Any();
                if (!existeCategoria)
                {
                    return BadRequest("Não existe a categoria selecionada!");
                }
                base.Validate(produto);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                p.Name = produto.Name;
                p.Price = produto.Price;
                p.CategoriaId = produto.CategoriaId;
                _db.Produtos.Attach(p);
                _db.Entry(p).State = EntityState.Modified;
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
                Produto produto = (from x in _db.Produtos
                                   where x.Id == id
                                   select x).FirstOrDefault();

                if (produto == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.Produtos.Remove(produto);

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
