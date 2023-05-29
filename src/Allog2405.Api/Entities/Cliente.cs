namespace Allog2405.Api.Entities;

public class Cliente {
    public int id {get; set;}
    public string nome {get; set;} = string.Empty;
    public string cpf {get; set;} = string.Empty;
}


public class ClienteDTO {
    public int? id {get; set;}
    public string? nome {get; set;}
    public string? cpf {get; set;}

    public ClienteDTO(Cliente cliente) {
        this.id = cliente.id;
        this.nome = cliente.nome;
        this.cpf = cliente.cpf;
    }
}