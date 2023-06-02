using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Allog2405.Api;
using Allog2405.Api.Entities;
using Allog2405.Api.Models;

namespace Allog2405.Api.Controllers;

[ApiController]
[Route("api/enderecos")]
public class EnderecosController : ControllerBase {

    [HttpGet]
    public ActionResult<IEnumerable<EnderecoForGetEnderecoDTO>> GetEnderecos() {
        IEnumerable<EnderecoForGetEnderecoDTO> listaEnderecosToReturn = EnderecoData.Get().listaEnderecos.Select(e => 
            new EnderecoForGetEnderecoDTO(e,
                ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == e.idCliente))
        );
        return Ok(listaEnderecosToReturn);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetEnderecoPorId")]
    public ActionResult<EnderecoForGetEnderecoDTO> GetEnderecoPorId(int id) {
        Endereco enderecoEntity = EnderecoData.Get().listaEnderecos.FirstOrDefault(e => e.id == id);
        if (enderecoEntity == null) return NotFound();

        Cliente clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == enderecoEntity.idCliente);
        EnderecoForGetEnderecoDTO enderecoToReturn = new EnderecoForGetEnderecoDTO(enderecoEntity, clienteEntity);
        return Ok(enderecoToReturn);
    }

    [HttpPost]
    public ActionResult<EnderecoForGetEnderecoDTO> CreateEndereco(EnderecoForCreationDTO enderecoForCreationDTO) {

        Cliente clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == enderecoForCreationDTO.idCliente);
        if (clienteEntity == null) return NotFound();

        int newId = (EnderecoData.Get().listaEnderecos.Any()) ?
            EnderecoData.Get().listaEnderecos.Max(e => e.id) + 1 : 1;
        Endereco enderecoEntity = new Endereco {
            id = newId,
            idCliente = enderecoForCreationDTO.idCliente,
            logradouro = enderecoForCreationDTO.logradouro,
            numero = enderecoForCreationDTO.numero,
            bairro = enderecoForCreationDTO.bairro,
            cidade = enderecoForCreationDTO.cidade,
            estado = enderecoForCreationDTO.estado
        };

        EnderecoData.Get().listaEnderecos.Add(enderecoEntity);

        EnderecoForGetEnderecoDTO enderecoToReturn = new EnderecoForGetEnderecoDTO(enderecoEntity, clienteEntity);

        return CreatedAtRoute(
            "GetEnderecoPorId",
            new {id = enderecoToReturn.id},
            enderecoToReturn
        );
    }

    [HttpPut("{id}")]
    public ActionResult<EnderecoForGetEnderecoDTO> EditEndereco(int id, EnderecoForEditionDTO enderecoForEditionDTO) {

        if(enderecoForEditionDTO.id != id) return BadRequest();

        Endereco? enderecoEntity = EnderecoData.Get().listaEnderecos.FirstOrDefault(e => e.id == id);
        if(enderecoEntity == null) return NotFound();
        
        if(enderecoForEditionDTO.idCliente != null) {
            Cliente clienteEntity = ClienteData.Get().listaClientes.FirstOrDefault(c => c.id == enderecoForEditionDTO.idCliente);
            if(clienteEntity == null) return BadRequest();
            enderecoEntity.idCliente = (int)enderecoForEditionDTO.idCliente;
        }
        if(enderecoForEditionDTO.logradouro != null) enderecoEntity.logradouro = enderecoForEditionDTO.logradouro;
        if(enderecoForEditionDTO.numero != null) enderecoEntity.numero = (int)enderecoForEditionDTO.numero;
        if(enderecoForEditionDTO.bairro != null) enderecoEntity.bairro = enderecoForEditionDTO.bairro;
        if(enderecoForEditionDTO.cidade != null) enderecoEntity.cidade = enderecoForEditionDTO.cidade;
        if(enderecoForEditionDTO.estado != null) enderecoEntity.estado = enderecoForEditionDTO.estado;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<EnderecoForGetEnderecoDTO> DeleteEnderecoPorId(int id) {

        Endereco enderecoEntity = EnderecoData.Get().listaEnderecos.FirstOrDefault(e => e.id == id);
        if (enderecoEntity == null) return NotFound();

        EnderecoData.Get().listaEnderecos.Remove(enderecoEntity);
        return NoContent();
    }
}