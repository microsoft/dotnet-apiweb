using System;

namespace Microsoft.Fx.Portability.ObjectModel
{
    public static class BreakingChangeAnalyzerStatusChartColors
    {
        public static string GetColor(BreakingChangeAnalyzerStatus breakingChangeAnalyzerStatus)
        {
            switch (breakingChangeAnalyzerStatus)
            {
                case BreakingChangeAnalyzerStatus.Available:
                    return "#e2efda";
                case BreakingChangeAnalyzerStatus.Investigating:
                    return "#ff6a00";
                case BreakingChangeAnalyzerStatus.NotPlanned:
                    return "#f8cbad";
                case BreakingChangeAnalyzerStatus.Planned:
                    return "#fff2cc";
                case BreakingChangeAnalyzerStatus.Unknown:
                    return "#808080";
                default:
                    throw new ArgumentOutOfRangeException(
                        $"BreakingChangeAnalyzerStatusChartColors_GetColor: {breakingChangeAnalyzerStatus.ToString()} is not defined as a color!");
            }
        }
    }
}