namespace Allog2405.Api.Entities;

public class Cliente {
    public int id {get; set;}
    public string primeiroNome {get; set;} = string.Empty;
    public string ultimoNome {get; set;} = string.Empty;
    public string cpf {get; set;} = string.Empty;
}