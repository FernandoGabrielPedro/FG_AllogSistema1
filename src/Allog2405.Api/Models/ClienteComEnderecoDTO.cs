using Allog2405.Api.Entities;

namespace Allog2405.Api.Models;

public class ClienteComEnderecoDTO {
    public int? id {get; set;}
    public string? nome {get; set;}
    public string? cpf {get; set;}
    public ICollection<EnderecoDTO>? listaEnderecos {get; set;} = new List<EnderecoDTO>();

    public ClienteComEnderecoDTO(Cliente cliente, List<Endereco> listaEnderecos) {
        this.id = cliente.id;
        this.nome = cliente.nome;
        this.cpf = cliente.cpf;
        this.listaEnderecos = listaEnderecos.Select(e => new EnderecoDTO(e)).ToList();
    }

    public ClienteComEnderecoDTO() {}
}