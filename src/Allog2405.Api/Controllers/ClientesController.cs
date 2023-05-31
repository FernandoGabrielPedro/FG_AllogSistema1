using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Allog2405.Api;
using Allog2405.Api.Entities;
using Allog2405.Api.Models;

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
        //case 1: return UnprocessableEntity("UNPROCESSABLEENTITY: CPF é nulo.");
        //case 2: return UnprocessableEntity("UNPROCESSABLEENTITY: CPF é inválido.");
        //case 3: return BadRequest("BADREQUEST: CPF já utilizado.");
    private int ValidarCpf(string cpf) {
        string cpfRegexExpressao = @"^[0-9]{11}$";

        ClienteData _data = ClienteData.Get();

        Regex cpfRegex = new Regex(cpfRegexExpressao);

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
        List<Cliente> listaClientesFromData = ClienteData.Get().listaClientes;
        IEnumerable<ClienteDTO> listaClientesToReturn = ClienteData.Get().listaClientes.Select(c => new ClienteDTO(c));
        return Ok(listaClientesToReturn);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetClientePorId")]
    public ActionResult<ClienteDTO> GetClientePorId(int id) {
        Cliente? clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == id, null);
        if (clienteEntity == null) return NotFound();

        ClienteDTO clienteToReturn = new ClienteDTO(clienteEntity);
        return Ok(clienteToReturn);
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<ClienteDTO> GetClientePorCpf(string cpf) {
        Cliente? clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(c => c.cpf == cpf, null);
        if(clienteEntity == null) return NotFound();

        ClienteDTO clienteToReturn = new ClienteDTO(clienteEntity);
        return Ok(clienteToReturn);
    }

    [HttpPost]
    public ActionResult<ClienteDTO> CreateCliente(ClienteForCreationDTO clienteForCreationDTO) {

        int cpfValidacao = ValidarCpf(clienteForCreationDTO.cpf);
        switch(cpfValidacao) {
            case 1: return UnprocessableEntity();
            case 2: return UnprocessableEntity();
            case 3: return BadRequest();
        }
        clienteForCreationDTO.nome ??= String.Empty;

        Cliente clienteEntity = new Cliente {
            id = ClienteData.Get().listaClientes.Max(c => c.id) + 1,
            nome = clienteForCreationDTO.nome,
            cpf = clienteForCreationDTO.cpf
        };

        ClienteData.Get().listaClientes.Add(clienteEntity);

        ClienteDTO clienteToReturn = new ClienteDTO(clienteEntity);

        return CreatedAtRoute(
            "GetClientePorId",
            new {id = clienteToReturn.id},
            clienteToReturn
        );
    }

    [HttpPut("{id}")]
    public ActionResult<ClienteDTO> EditCliente(int id, ClienteForEditionDTO clienteForEditionDTO) {

        if(clienteForEditionDTO.id != id) return BadRequest();

        Cliente? clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(n => n.id == id, null);
        if(clienteEntity == null) return NotFound();
        
        if(clienteForEditionDTO.nome != null) clienteEntity.nome = clienteForEditionDTO.nome;
        if(clienteForEditionDTO.cpf != null) {
            int cpfValidacao = ValidarCpf(clienteForEditionDTO.cpf);
            switch(cpfValidacao) {
                case 2: return UnprocessableEntity();
                case 3: return BadRequest();
            }
            clienteEntity.cpf = clienteForEditionDTO.cpf;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<ClienteDTO> DeleteClientePorId(int id) {

        Cliente clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(n => n.id == id);
        if (clienteEntity == null) return NotFound();

        ClienteData.Get().listaClientes.Remove(clienteEntity);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult<ClienteDTO> PatchCliente([FromRoute] int id, [FromBody] JsonPatchDocument<ClienteForPatchDTO> patchDocument) {
        Cliente clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(n => n.id == id);
        if (clienteEntity == null) return NotFound();

        ClienteForPatchDTO clienteToPatch = new ClienteForPatchDTO{
            nome = clienteEntity.nome,
            cpf = clienteEntity.cpf
        };

        patchDocument.ApplyTo(clienteToPatch);

        clienteEntity.nome = clienteToPatch.nome;
        clienteEntity.cpf = clienteToPatch.cpf;

        return NoContent();
    }
}