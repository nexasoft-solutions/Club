using Microsoft.AspNetCore.Mvc;

namespace NexaSoft.Club.Api.Extensions;

public static class ActionResultExtensions
{
   public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        if (result.IsFailure)
        {
            return controller.BadRequest(result);
        }
        return controller.Ok(result);
    }
}
