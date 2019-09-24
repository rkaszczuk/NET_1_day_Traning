using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkDotNetSample
{
    //[MinColumn, MaxColumn]
    //[WarmupCount(0)]
    //[IterationCount(50)]
    //[HtmlExporter]
    //[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    //[Config(typeof(PercentilesConfig))]
    public class BenchmarkSleepTest
    {
        //[Benchmark]
        //[BenchmarkCategory("StaticValue")]
        //public void Sleep2Sec() => System.Threading.Thread.Sleep(2 * 1000);

        //[Benchmark(Baseline = true)]
        //[BenchmarkCategory("StaticValue")]
        //public void Sleep1Sec() => System.Threading.Thread.Sleep(1 * 1000);

        //[Params(100, 200, 300, 400)]
        //[ParamArray()]
        //public int SleepTimeMs { get; set; }
        //[BenchmarkCategory("DynamicValue")]
        //[Benchmark]
        //public void SleepFromRangeSec() => System.Threading.Thread.Sleep(SleepTimeMs);

        //public int[] SleepTimeMsRange { get; set; } = new int[] { 100, 200, 300, 400 };

        //[ArgumentsSource(nameof(SleepTimeMsRange))]
        //[BenchmarkCategory("DynamicValue")]
        //[Benchmark]
        //public void SleepFromRangeSec(int sleepTime) => System.Threading.Thread.Sleep(sleepTime);

        //[GlobalSetup]
        //public void Setup()
        //{

        //}

        //[GlobalCleanup]
        //public void Cleanup()
        //{

        //}

    }

}
