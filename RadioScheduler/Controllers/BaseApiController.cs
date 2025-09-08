using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
public abstract class BaseApiController : ControllerBase {

}
