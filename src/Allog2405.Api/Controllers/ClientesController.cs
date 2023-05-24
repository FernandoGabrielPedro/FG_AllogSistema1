using Allog2405.Api;
using Allog2405.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Allog2405.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase {

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> GetClientes() {
        var result = ClienteData.Get().listaClientes;
        return Ok(result);
    }
}