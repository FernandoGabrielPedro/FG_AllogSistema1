namespace Allog2405.Api.Entities;

public class Cliente {
    public int id {get; set;}
    public string nome {get; set;} = string.Empty;
    public string cpf {get; set;} = string.Empty;
    public List<Endereco> listaEnderecos {get; set;} = new List<Endereco>();
}