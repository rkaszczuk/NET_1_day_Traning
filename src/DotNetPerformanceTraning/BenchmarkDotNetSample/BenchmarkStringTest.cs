using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkDotNetSample
{
    [SimpleJob(RunStrategy.Monitoring, targetCount: 1, invocationCount:3)]
    [MemoryDiagnoser]
    public class BenchmarkStringTest
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IEnumerable<int> StringLengths { get; set; } = Enumerable.Range(1, 10).Select(x => x * 10000);
        public IEnumerable<int> SegmentSizes { get; set; } = Enumerable.Range(1, 10).Select(x=>x*100);
       
        [ParamsSource(nameof(StringLengths))]
        public int StringLength { get; set; }
        [ParamsSource(nameof(SegmentSizes))]
        public int SegmentSize { get; set; }

        [Benchmark]
        public void StringConcat()
        {
            var result = String.Empty;
            while(result.Length <= StringLength)
            {
                result = result + RandomString(SegmentSize);
            }
        }
        [Benchmark]
        public void StringBuilderAppend()
        {
            var result = new StringBuilder();
            while (result.Length <= StringLength)
            {
                result.Append(RandomString(SegmentSize));
            }
            result.ToString();
        }
    }
}
