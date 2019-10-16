using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationEdge.Models
{
    [Table("Products")]
    public class Produto
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }


        [Column("Name")]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [Column("Price")]
        public Decimal Price { get; set; }

        [Required]
        [Column("Category_Id")]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}