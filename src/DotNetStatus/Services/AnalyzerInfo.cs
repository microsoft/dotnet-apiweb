// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Fx.Portability.ObjectModel
{
    public class DiagnosticAnalyzerInfo
    {
        public string AnalyzerName { get; set; }
        public string Id { get; set; }
        public ICollection<CompatibilityRangeAttribute> Attributes { get; set; }
        public bool IsCompatibilityDiagnostic { get; set; }

        public int IdNumber
        {
            get
            {
                var matches = Regex.Match(Id, @"(\d+)");

                return int.Parse(matches.Captures[0].Value);
            }
        }
    }

    public class CompatibilityRangeAttribute
    {
        public string FrameworkName { get; }
        public Version StartVersion { get; }
        public Version EndVersion { get; }
        public Version LatestVersion { get; }
        public Version DefaultVersion { get; }
        public bool IsRetargeting { get; }
    }
}
