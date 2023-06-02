namespace Allog2405.Api.Entities;

public class Endereco {
    public int id {get; set;}
    public int idCliente {get; set;}
    public string logradouro {get; set;} = string.Empty;
    public int numero {get; set;}
    public string bairro {get; set;} = string.Empty;
    public string cidade {get; set;} = string.Empty;
    public string estado {get; set;} = string.Empty;
}