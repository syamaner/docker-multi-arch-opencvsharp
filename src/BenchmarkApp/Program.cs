using BenchmarkDotNet.Running;
using OpenCVSharpBenchmarkApp;

BenchmarkRunner.Run<FeatureMatcherBenchmark>();
BenchmarkRunner.Run<FeatureExtractionBenchmark>();