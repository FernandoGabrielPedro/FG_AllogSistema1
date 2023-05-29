using Allog2405.Api.Entities;

namespace Allog2405.Api;

public class ClienteData {
    static private ClienteData _data;
    public List<Cliente> listaClientes {get; set;}

    private ClienteData() {
        this.listaClientes = new List<Cliente>{
            new Cliente {
                id = 1,
                primeiroNome = "Pedro",
                ultimoNome = "Segundo",
                cpf = "12345678901"
            },
                new Cliente {
                id = 2,
                primeiroNome = "João",
                ultimoNome = "Sasa",
                cpf = "98765432109"
            }
        };
    }

    static public ClienteData Get() {
        if(_data == null) {
            _data = new ClienteData();
        }

        return _data;
    }
}