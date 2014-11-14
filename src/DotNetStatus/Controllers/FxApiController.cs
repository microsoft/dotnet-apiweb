using Microsoft.AspNet.Mvc;
using Microsoft.Fx.Portability;
using System;
using System.Threading.Tasks;

namespace DotNetStatus.Controllers
{
    public class FxApiController : Controller
    {
        private readonly IApiPortService _service;

        public FxApiController(IApiPortService service)
        {
            _service = service;
        }

        [Route("/fxapi")]
        public async Task<ActionResult> Index(string docId)
        {
            if (String.IsNullOrEmpty(docId))
            {
                return new HttpNotFoundResult();
            }

            try
            {
                var result = await _service.GetApiInformationAsync(docId);

                return View(result.Response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return new HttpNotFoundResult();
            }
        }

    }
}
