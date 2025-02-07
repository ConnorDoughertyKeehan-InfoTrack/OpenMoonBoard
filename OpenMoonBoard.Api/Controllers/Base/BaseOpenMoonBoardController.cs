using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OpenMoonBoard.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class BaseOpenMoonBoardController : ControllerBase
{
    protected int HttpLoginId => int.Parse(User.FindFirstValue("LoginId") ?? "0");
}
