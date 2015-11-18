// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Fx.Portability.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Fx.Portability
{
    public class PortabilityToolsStatusService
    {
        private readonly HttpClient _client;

        public PortabilityToolsStatusService(HttpClient client, ProductInformation productInfo)
        {
            _client = client;
        }

        /// <summary>
        /// Returns Json from the WebAPI calls made to the APIPortService
        /// </summary>
        /// <param name="query">the webapi action to run</param>
        public async Task<T> GetJsonDataAsync<T>(string query)
        {
            using (var response = await _client.GetAsync(CreateFullUrl(query)))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(content, DataExtensions.JsonSettings);
                return result;
            }
        }

        /// <summary>
        /// Creates the full URL for the query to be processed by putting the ApiPort Url together with the query
        /// </summary>
        /// <param name="query">the apiportservice webapi action to be combined with the apiport URL</param>
        private Uri CreateFullUrl(string query)
        {
            var result = new Uri(_client.BaseAddress, query);
            return result;
        }

        public async Task<IEnumerable<DiagnosticAnalyzerInfo>> GetAnalyzersAsync()
        {
            string query = "/Api/data/Analyzers";
            var data = await GetJsonDataAsync<IEnumerable<DiagnosticAnalyzerInfo>>(query);

            return data.OrderBy(o => o.IdNumber);
        }

        public async Task<IEnumerable<BreakingChange>> GetBreakingChangesAsync()
        {
            string query = "/Api/data/BreakingChanges";
            var data = await GetJsonDataAsync<IEnumerable<BreakingChange>>(query);

            return SortBreakingChanges(data);
        }

        public async Task<IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>> GetApiPortSummaryAsync()
        {
            string query = "/Api/toolsData/ApiPort/Summary";
            var data = await GetJsonDataAsync<IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>>(query);

            return data.OrderByDescending(o => o.Value);
        }

        public async Task<IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>> GetAnalyzersSummaryAsync()
        {
            string query = "/Api/toolsData/Analyzers/Summary";
            var data = await GetJsonDataAsync<IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>>(query);

            return data.OrderByDescending(o => o.Value);
        }

        public async Task<IEnumerable<BreakingChange>> GetNotCoveredByAnyAsync()
        {
            string query = "/Api/toolsData/Coverage/None";
            return SortBreakingChanges(await GetJsonDataAsync<IEnumerable<BreakingChange>>(query));
        }

        public async Task<IEnumerable<BreakingChange>> GetCoveredByAllAsync()
        {
            string query = "/Api/toolsData/Coverage/CoveredByAll";
            return SortBreakingChanges(await GetJsonDataAsync<IEnumerable<BreakingChange>>(query));
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>>>> GetRollUpAsync()
        {
            var result = new List<KeyValuePair<string, IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>>>
            {
                 BuildRollUpEnumerable("ApiPort Status", await GetApiPortSummaryAsync()),
                 BuildRollUpEnumerable("Analyzers Status", await GetAnalyzersSummaryAsync()),
                 BuildRollUpEnumerable("Covered By All", BuildRollUpKeyValuePair(BreakingChangeAnalyzerStatus.Available, (await GetCoveredByAllAsync()).Count())),
                 BuildRollUpEnumerable("Not Covered By Any", BuildRollUpKeyValuePair(BreakingChangeAnalyzerStatus.NotPlanned, (await GetNotCoveredByAnyAsync()).Count()))
            };

            return result;
        }

        private KeyValuePair<string, IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>> BuildRollUpEnumerable(string key, IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>> value)
        {
            return new KeyValuePair<string, IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>>>(key, value);
        }

        private IEnumerable<KeyValuePair<BreakingChangeAnalyzerStatus, int>> BuildRollUpKeyValuePair(BreakingChangeAnalyzerStatus breakingChangeAnalyzerStatus, int value)
        {
            return new List<KeyValuePair<BreakingChangeAnalyzerStatus, int>>
            {
                new KeyValuePair<BreakingChangeAnalyzerStatus, int>(breakingChangeAnalyzerStatus, value)
            };
        }

        private IEnumerable<BreakingChange> SortBreakingChanges(IEnumerable<BreakingChange> enumToSort)
        {
            int id;
            return (enumToSort.OrderBy(o => int.TryParse(o.Id, out id) ? id : int.MaxValue));
        }
    }
}
