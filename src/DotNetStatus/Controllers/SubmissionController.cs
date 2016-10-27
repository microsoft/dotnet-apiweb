// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Fx.Portability;
using Microsoft.Fx.Portability.ObjectModel;
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

        [Route("Find")]
        public ActionResult Find(string id)
        {
            return RedirectToAction("Index", new { id = id });
        }
    }
}