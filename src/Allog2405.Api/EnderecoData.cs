using Allog2405.Api.Entities;

namespace Allog2405.Api;

public class EnderecoData {
    static private EnderecoData _data;
    public List<Endereco> listaEnderecos {get; set;}

    private EnderecoData() {
        this.listaEnderecos = new List<Endereco>{
            new Endereco {
                id = 1,
                idCliente = 1,
                logradouro = "Rua Dona Maria",
                numero = 567,
                bairro = "Bairro das Lajotas",
                cidade = "Navegantes",
                estado = "SC"
            },
            new Endereco {
                id = 2,
                idCliente = 2,
                logradouro = "Rua Santos Dumon",
                numero = 498,
                bairro = "Bairro do Seu João",
                cidade = "Curitiba",
                estado = "PR"
            }
        };
    }

    static public EnderecoData Get() {
        if(_data == null) {
            _data = new EnderecoData();
        }

        return _data;
    }
}