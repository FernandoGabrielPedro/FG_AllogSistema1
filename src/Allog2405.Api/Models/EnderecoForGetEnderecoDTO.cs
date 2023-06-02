using Allog2405.Api.Entities;

namespace Allog2405.Api.Models;

public class EnderecoForGetEnderecoDTO {
    public int? id {get; set;}
    public ClienteForGetEnderecoDTO? cliente {get; set;}
    public string? logradouro {get; set;} = string.Empty;
    public int? numero {get; set;}
    public string? bairro {get; set;} = string.Empty;
    public string? cidade {get; set;} = string.Empty;
    public string? estado {get; set;} = string.Empty;

    public EnderecoForGetEnderecoDTO(Endereco endereco, Cliente cliente) {
        this.id = endereco.id;
        this.cliente = new ClienteForGetEnderecoDTO(cliente);
        this.logradouro = endereco.logradouro;
        this.numero = endereco.numero;
        this.bairro = endereco.bairro;
        this.cidade = endereco.cidade;
        this.estado = endereco.estado;
    }

    public EnderecoForGetEnderecoDTO() {}
}