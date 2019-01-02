using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    [Table("UF")]
    public class UF
    {
        [Key]
        public int UfId { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Esse campo deve ser preenchido")]
        public string Descricao { get; set; }
    }
}