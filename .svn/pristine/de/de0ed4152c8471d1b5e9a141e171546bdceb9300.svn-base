using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrocaFigurinhas.Models.ModelView
{
    public class LoginModelView
    {
        private const string MensagemDeErroCampoObrigatorio = "O campo {0} é de preenchimento obrigatório";
        private const string MensageDeErroTamanhoMaximo = "O campo {0} deve conter {1} caracteres";

        [DisplayName("Login")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = MensagemDeErroCampoObrigatorio)]
        [MaxLength(20, ErrorMessage = MensageDeErroTamanhoMaximo)]
        public string Login { get; set; }

        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = MensagemDeErroCampoObrigatorio)]
        [MaxLength(20, ErrorMessage = MensageDeErroTamanhoMaximo)]
        public string Senha { get; set; }
    }
}