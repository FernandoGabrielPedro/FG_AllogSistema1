namespace Allog2405.Api.Models;

public class ClienteDTO {
    public string primeiroNome {private get; set;} = String.Empty;
    public string ultimoNome {private get; set;} = String.Empty;
    public string nomeCompleto {
        get {
            return primeiroNome + " " + ultimoNome;
        }
    }

    public string cpf {get; set;} = String.Empty;
}