using Microsoft.AspNet.Mvc;
using Microsoft.Fx.Portability;
using Microsoft.Fx.Portability.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetStatus.Controllers
{
    public class UsageController : Controller
    {
        private readonly IApiPortService _apiPortService;

        public UsageController(IApiPortService apiPortService)
        {
            _apiPortService = apiPortService;
        }

        [Route("usage")]
        public async Task<ActionResult> Index()
        {
            return View(await GetViewModelAsync(0, 25, UsageDataFilter.HideSupported));
        }

        [Route("usage/page")]
        public async Task<PartialViewResult> Page(int? skip, int? top, UsageDataFilter? filter, IEnumerable<string> targets)
        {
            return PartialView("_Usage", await GetViewModelAsync(skip ?? 0, top, filter, targets));
        }

        private async Task<UsageDataCollection> GetViewModelAsync(int skip, int? top = null, UsageDataFilter? filter = null, IEnumerable<string> targets = null)
        {
            try
            {
                var usage = await _apiPortService.GetUsageDataAsync(skip, top, filter, targets);

                return usage.Response;
            }
            catch (PortabilityAnalyzerException)
            {
                return null;
            }
        }
    }
}
