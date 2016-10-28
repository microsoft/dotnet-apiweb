// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Index(int? skip, int? top, UsageDataFilter? filter, IEnumerable<string> targets)
        {
            try
            {
                var usage = await _apiPortService.GetUsageDataAsync(skip ?? 0, top ?? 25, filter ?? UsageDataFilter.HideSupported, targets);

                return View(usage.Response);
            }
            catch (PortabilityAnalyzerException)
            {
                return View(null);
            }
        }
    }
}
