using Allog2405.Api.Entities;

namespace Allog2405.Api.Models;

public class ClienteDTO {
    public int? id {get; set;}
    public string? nome {get; set;}
    public string? cpf {get; set;}
    public IEnumerable<EnderecoDTO>? listaEnderecos {get; set;}

    public ClienteDTO(Cliente cliente) {
        this.id = cliente.id;
        this.nome = cliente.nome;
        this.cpf = cliente.cpf;
        this.listaEnderecos = cliente.listaEnderecos.Select(e => new EnderecoDTO(e));
    }

    public ClienteDTO() {}
}