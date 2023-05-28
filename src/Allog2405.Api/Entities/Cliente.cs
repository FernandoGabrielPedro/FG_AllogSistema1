namespace Allog2405.Api.Entities;

public class Cliente {
    public int id {get; set;}
    public string nome {get; set;} = string.Empty;
    public string cpf {get; set;} = string.Empty;
}


public class ClienteDTO {
    public string? nome {get; set;}
    public string? cpf {get; set;}
}