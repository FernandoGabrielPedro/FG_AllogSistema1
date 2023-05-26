using Allog2405.Api;
using Allog2405.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Allog2405.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase {

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> GetClientes() {
        List<Cliente> result = ClienteData.Get().listaClientes;
        return Ok(result);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetClientePorId")]
    public ActionResult<Cliente> GetClientePorId([FromRoute] int id) {
        Cliente? cliente = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == id, null);
        return cliente != null ? Ok(cliente) : NotFound();
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<Cliente> GetClientePorCpf([FromRoute] string cpf) {
        Cliente? cliente = ClienteData.Get().listaClientes.FirstOrDefault(c => c.cpf == cpf, null);
        return cliente != null ? Ok(cliente) : NotFound();
    }

    [HttpPost]
    public ActionResult CreateCliente([FromBody] Cliente clienteBody) {
        ClienteData data = ClienteData.Get();

        Cliente newCliente = new Cliente {
            id = data.listaClientes.Max(c => c.id) + 1,
            nome = clienteBody.nome,
            cpf = clienteBody.cpf
        };

        foreach(Cliente c in data.listaClientes)
            if (newCliente.cpf == c.cpf)
                return Unauthorized("--CPF EXISTENTE");

        data.listaClientes.Add(newCliente);
        return CreatedAtRoute(
            "GetClientePorId",
            new {id = newCliente.id},
            newCliente
        );
    }

    [HttpPut("{id}")]
    public ActionResult EditCliente([FromRoute] int id, [FromBody] Cliente clienteBody) {
        ClienteData data = ClienteData.Get();

        Cliente? cliente = data.listaClientes.FirstOrDefault(n => n.id == id, null);
        if(cliente == null) return NotFound();

        Cliente newCliente = new Cliente {
            id = data.listaClientes.Max(c => c.id) + 1,
            nome = clienteBody.nome,
            cpf = clienteBody.cpf
        };

        foreach(Cliente c in data.listaClientes)
            if (newCliente.cpf == c.cpf)
                return Unauthorized("--CPF EXISTENTE");

        if(newCliente.nome != null && newCliente.nome != "") data.listaClientes.First(n => n.id == id).nome = newCliente.nome;
        if(newCliente.cpf != null && newCliente.cpf != "") data.listaClientes.First(n => n.id == id).cpf = newCliente.cpf;
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<Cliente> DeleteClientePorId([FromRoute] int id) {
        ClienteData data = ClienteData.Get();

        Cliente? cliente = data.listaClientes.FirstOrDefault(n => n.id == id, null);
        if (cliente == null) return NotFound();

        data.listaClientes.Remove(cliente);
        return Ok();
    }
}