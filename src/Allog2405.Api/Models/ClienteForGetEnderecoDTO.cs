using Allog2405.Api.Entities;

namespace Allog2405.Api.Models;

public class ClienteForGetEnderecoDTO {
    public int? id {get; set;}
    public string? nome {get; set;}
    public string? cpf {get; set;}

    public ClienteForGetEnderecoDTO(Cliente cliente) {
        this.id = cliente.id;
        this.nome = cliente.nome;
        this.cpf = cliente.cpf;
    }

    public ClienteForGetEnderecoDTO() {}
}