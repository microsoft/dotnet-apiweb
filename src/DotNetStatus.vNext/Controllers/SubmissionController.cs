using Microsoft.AspNet.Mvc;
using Microsoft.Fx.Portability;
using Microsoft.Fx.Portability.ObjectModel;
using System;
using System.Threading.Tasks;

namespace DotNetStatus.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IApiPortService _apiPortService;

        public SubmissionController(IApiPortService apiPortService)
        {
            _apiPortService = apiPortService;
        }

        [Route("submission/{id?}")]
        public async Task<ActionResult> Index(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return View((AnalyzeResponse)null);
            }

            try
            {
                var response = await _apiPortService.GetAnalysisAsync(id);

                return View(response.Response);
            }
            catch (NotFoundException)
            {
                var response = new AnalyzeResponse
                {
                    SubmissionId = id,
                    Targets = null
                };

                return View(response);
            }
            catch (PortabilityAnalyzerException e)
            {
                return View(e);
            }
        }

        [Route("Find")]
        public ActionResult Find(string id)
        {
            return RedirectToAction("Index", new { id = id });
        }
    }
}