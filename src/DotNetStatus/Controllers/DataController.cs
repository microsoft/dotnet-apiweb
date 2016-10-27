// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Fx.Portability;
using Microsoft.Fx.Portability.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetStatus.Controllers
{
    [Route("Data")]
    public class DataController : Controller
    {
        private readonly PortabilityToolsStatusService _portabilityToolsStatusService;
        private readonly IEnumerable<SelectListItem> ListOfDataChoices = new List<SelectListItem>
        {
                new SelectListItem() { Text = "Analyzers", Value="Analyzers" },
                new SelectListItem() { Text = "Analyzers summary", Value="AnalyzersSummary" },
                new SelectListItem() { Text = "ApiPort summary", Value="ApiPortSummary" },
                new SelectListItem() { Text = "Breaking changes", Value="BreakingChanges" },
                new SelectListItem() { Text = "Covered by all", Value = "CoveredByAll" },
                new SelectListItem() { Text = "Not covered by any", Value="NotCoveredByAny" },
                new SelectListItem() { Text = "Roll-Up", Value="RollUp", Selected = true }
        };

        public DataController(PortabilityToolsStatusService portabilityToolsStatusService)
        {
            _portabilityToolsStatusService = portabilityToolsStatusService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View("Index", ListOfDataChoices);
        }

        [Route("Analyzers")]
        public async Task<IActionResult> GetAnalyzersAsync()
        {
            string query = "/Api/data/Analyzers";
            var data = await _portabilityToolsStatusService.GetJsonDataAsync<IEnumerable<DiagnosticAnalyzerInfo>>(query);

            return PartialView("Analyzers", await _portabilityToolsStatusService.GetAnalyzersAsync());
        }

        [Route("BreakingChanges")]
        public async Task<IActionResult> GetBreakingChangesAsync()
        {
            string query = "/Api/data/BreakingChanges";
            var data = await _portabilityToolsStatusService.GetJsonDataAsync<IEnumerable<BreakingChange>>(query);

            return PartialView("BreakingChanges", await _portabilityToolsStatusService.GetBreakingChangesAsync());
        }

        [Route("ApiPortSummary")]
        public async Task<IActionResult> GetApiPortSummaryAsync()
        {
            return PartialView("KeyValuePair", await _portabilityToolsStatusService.GetApiPortSummaryAsync());
        }

        [Route("AnalyzersSummary")]
        public async Task<IActionResult> GetAnalyzersSummaryAsync()
        {
            return PartialView("KeyValuePair", await _portabilityToolsStatusService.GetAnalyzersSummaryAsync());
        }

        [Route("NotCoveredByAny")]
        public async Task<IActionResult> GetNotCoveredByAnyAsync()
        {
            return PartialView("BreakingChanges", await _portabilityToolsStatusService.GetNotCoveredByAnyAsync());
        }

        [Route("CoveredByAll")]
        public async Task<IActionResult> GetCoveredByAllAsync()
        {
            return PartialView("BreakingChanges", await _portabilityToolsStatusService.GetCoveredByAllAsync());
        }

        [Route("RollUp")]
        public async Task<IActionResult> GetRollUpAsync()
        {
            return PartialView("RollUp", await _portabilityToolsStatusService.GetRollUpAsync());
        }
    }
}
