// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Fx.Portability;
using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(docId))
            {
                return new NotFoundResult();
            }

            try
            {
                var result = await _service.GetApiInformationAsync(docId);

                return View(result.Response);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return new NotFoundResult();
            }
        }

        [Route("search")]
        public async Task<ActionResult> Search(string term)
        {
            var result = await SearchAsync(term);

            return Json(result.Response);
        }

        [Route("validate")]
        public async Task<ActionResult> Validate(string query)
        {
            var result = await SearchAsync(query);

            var api = result.Response.FirstOrDefault(f => string.Equals(f.FullName, query, StringComparison.Ordinal));

            if (api == null)
            {
                return new NotFoundResult() as ActionResult;
            }
            else
            {
                return Json(api);
            }
        }

        private Task<ServiceResponse<IReadOnlyCollection<ApiDefinition>>> SearchAsync(string query)
        {
            return _service.SearchFxApiAsync($"\"{query}\"");
        }
    }
}
