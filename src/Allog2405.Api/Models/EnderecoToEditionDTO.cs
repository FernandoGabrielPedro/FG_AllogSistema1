namespace Allog2405.Api.Models;

public class EnderecoForEditionDTO {
    public int idCliente {get; set;}
    public int idEndereco {get; set;}
    public string? logradouro {get; set;}
    public int? numero {get; set;}
    public string? bairro {get; set;}
    public string? cidade {get; set;}
    public string? estado {get; set;}
}