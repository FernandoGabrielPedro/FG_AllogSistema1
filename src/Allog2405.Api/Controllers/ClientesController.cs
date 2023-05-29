using System.Text.RegularExpressions;
using Allog2405.Api;
using Allog2405.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Allog2405.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase {

    //Retorna o status de validação do cpf.
    //Valores de retorno:
    //0 = Sucesso
    //1 = CPF nulo
    //2 = CPF inválido
    //3 = CPF já existente
    private int ValidarCpf(string cpf) {
        string cpfRegexExp = @"^[0-9]{11}$";

        ClienteData _data = ClienteData.Get();

        Regex cpfRegex = new Regex(cpfRegexExp);

        if(cpf == null)
            return 1;
        if(!cpfRegex.Match(cpf).Success)
            return 2;
        foreach(Cliente c in _data.listaClientes)
            if (cpf == c.cpf)
                return 3;
        
        return 0;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClienteDTO>> GetClientes() {
        List<Cliente> listaClientes = ClienteData.Get().listaClientes;
        List<ClienteDTO> listaClientesResult = new List<ClienteDTO>();
        foreach(Cliente c in listaClientes) {
            listaClientesResult.Add(new ClienteDTO(c));
        }
        return Ok(listaClientesResult);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetClientePorId")]
    public ActionResult<ClienteDTO> GetClientePorId(int id) {
        Cliente? cliente = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == id, null);
        if (cliente == null) return NotFound();

        ClienteDTO clienteResult = new ClienteDTO(cliente);
        return Ok(clienteResult);
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<ClienteDTO> GetClientePorCpf(string cpf) {
        Cliente? cliente = ClienteData.Get().listaClientes.FirstOrDefault(c => c.cpf == cpf, null);
        if(cliente == null) return NotFound();

        ClienteDTO clienteResult = new ClienteDTO(cliente);
        return Ok(clienteResult);
    }

    [HttpPost]
    public ActionResult CreateCliente(ClienteDTO clienteBody) {
        ClienteData _data = ClienteData.Get();

        int cpfValidacao = ValidarCpf(clienteBody.cpf);
        switch(cpfValidacao) {
            case 1: return UnprocessableEntity("UNPROCESSABLEENTITY: CPF é nulo.");
            case 2: return UnprocessableEntity("UNPROCESSABLEENTITY: CPF é inválido.");
            case 3: return BadRequest("BADREQUEST: CPF já utilizado.");
        }
        clienteBody.nome ??= String.Empty;

        Cliente newCliente = new Cliente {
            id = _data.listaClientes.Max(c => c.id) + 1,
            nome = clienteBody.nome,
            cpf = clienteBody.cpf
        };

        _data.listaClientes.Add(newCliente);

        return CreatedAtRoute(
            "GetClientePorId",
            new {id = newCliente.id},
            newCliente
        );
    }

    [HttpPut("{id}")]
    public ActionResult EditCliente(int id, ClienteDTO clienteBody) {
        ClienteData _data = ClienteData.Get();

        Cliente? cliente = _data.listaClientes.FirstOrDefault(n => n.id == id, null);
        if(cliente == null) return NotFound();

        if(clienteBody.nome != null) cliente.nome = clienteBody.nome;
        if(clienteBody.cpf != null) {
            int cpfValidacao = ValidarCpf(clienteBody.cpf);
            switch(cpfValidacao) {
                case 2: return UnprocessableEntity("UNPROCESSABLEENTITY: CPF é inválido.");
                case 3: return BadRequest("BADREQUEST: CPF já utilizado.");
            }
            cliente.cpf = clienteBody.cpf;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteClientePorId(int id) {
        Cliente cliente = ClienteData.Get().listaClientes.FirstOrDefault(n => n.id == id);

        if (cliente == null) return NotFound();

        ClienteData.Get().listaClientes.Remove(cliente);
        return NoContent();
    }
}