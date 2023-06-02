using Allog2405.Api.Entities;

namespace Allog2405.Api.Models;

public class ClienteForGetClienteDTO {
    public int? id {get; set;}
    public string? nome {get; set;}
    public string? cpf {get; set;}
    public IEnumerable<EnderecoForGetClienteDTO>? listaEnderecos {get; set;} = new List<EnderecoForGetClienteDTO>();

    public ClienteForGetClienteDTO(Cliente cliente, List<Endereco> listaEnderecos) {
        this.id = cliente.id;
        this.nome = cliente.nome;
        this.cpf = cliente.cpf;
        this.listaEnderecos = listaEnderecos.Select(e => new EnderecoForGetClienteDTO(e));
    }

    public ClienteForGetClienteDTO() {}
}