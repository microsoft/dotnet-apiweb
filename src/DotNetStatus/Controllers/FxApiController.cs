using Microsoft.AspNet.Mvc;
using Microsoft.Fx.Portability;
using System;
using System.Diagnostics;
using System.Linq;
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
                Trace.WriteLine(e);
                return new HttpNotFoundResult();
            }
        }

        [Route("search")]
        public async Task<ActionResult> Search(string term)
        {
            var result = await _service.SearchFxApiAsync(term);

            return Json(result.Response);
        }

        [Route("validate")]
        public async Task<ActionResult> Validate(string query)
        {
            var result = await _service.SearchFxApiAsync(query);

            var api = result.Response.FirstOrDefault(f => String.Equals(f.FullName, query, StringComparison.Ordinal));

            if (api == null)
            {
                return new HttpNotFoundResult() as ActionResult;
            }
            else
            {
                return Json(api);
            }
        }
    }
}
