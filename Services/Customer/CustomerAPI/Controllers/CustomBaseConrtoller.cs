using Microsoft.AspNetCore.Mvc;
using Shared.CommonModel;

namespace CustomerAPI.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseModel<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
