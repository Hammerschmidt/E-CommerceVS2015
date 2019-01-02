using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(50, ErrorMessage = "No máximo 50 caracteres!")]
        [Display(Name = "Nome do produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição do produto")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem do produto")]
        public string Imagem { get; set; }

        [Display(Name = "Categoria do produto")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(0, 1000, ErrorMessage = "Valores entre 0 e 1000!")]
        [Display(Name = "Preço do produto")]
        public double Preco { get; set; }

        [Display(Name = "Quantidade do produto")]
        public int Quantidade { get; set; }

        [Display(Name = "Tamanho")]
        public double Tamanho { get; set; }
    }
}