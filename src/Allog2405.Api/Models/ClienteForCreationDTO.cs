using System.ComponentModel.DataAnnotations;

namespace Allog2405.Api.Models;

public class ClienteForCreationDTO {
    [Required(ErrorMessage="Você deve preencher o atributo Nome.")]
    [MaxLength(100, ErrorMessage="O nome deve possuir menos de 100 caracteres.")]
    public string nome {get; set;}
    [Required(ErrorMessage="Você deve preencher o atributo CPF.")]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage="O CPF deve consistir exatamente de 11 números")]
    public string cpf {get; set;}
}