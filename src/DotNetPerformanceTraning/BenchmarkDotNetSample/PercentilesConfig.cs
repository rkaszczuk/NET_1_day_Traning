using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkDotNetSample
{
    class PercentilesConfig : ManualConfig
    {
        public PercentilesConfig()
        {
            Add(StatisticColumn.P50, StatisticColumn.P95);
                
        }
    }
}
