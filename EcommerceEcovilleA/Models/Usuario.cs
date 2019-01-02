using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "Nome do Usuário")]
        public string Nome { get; set; }

        [Display(Name = "Endereço do Usuário")]
        public string Endereco { get; set; }

        [Display(Name = "Telefone do Usuário")]
        public string Telefone { get; set; }

        [Display(Name = "E-mail do Usuário")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        [Display(Name = "Senha do Usuário")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [NotMapped]
        public string ConfirmacaoSenha { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Uf { get; set; }

    }
}