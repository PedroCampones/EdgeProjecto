using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationEdge.Models
{
    [Table("Categories")]
    public class Categoria
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public String Name { get; set; }

        [NotMapped]
        public List<Produto> Produtos { get; set; }
    }
}