using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using OpenCvSharp;
using OpenCvSharp.Features2D;

namespace OpenCVSharpBenchmarkApp
{
    [InProcess]
    public class FeatureMatcherBenchmark : IDisposable
    {
        private static readonly Mat Descriptors1 = new();
        private static readonly Mat Descriptors2 = new();

        static FeatureMatcherBenchmark()
        {
            // Precompute necessary information as the benchmarks are only measuring the matchers. 
            using Mat gray1 = new();
            using Mat gray2 = new();
            using var img1 = Cv2.ImRead("image1.jpg");
            using var img2 = Cv2.ImRead("image2.jpg");
            Cv2.CvtColor(img1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(img2, gray2, ColorConversionCodes.BGR2GRAY);
            
            using var sift = SIFT.Create();
            sift.DetectAndCompute(gray1, null, out var KeyPoints1, Descriptors1);
            sift.DetectAndCompute(gray2, null, out var KeyPoints2, Descriptors2);
        }

        [Benchmark]
        public void FlannBasedMatcher()
        {
            using var matcher = new FlannBasedMatcher();
            var matches = matcher.Match(Descriptors1, Descriptors2);
            DoNothing(matches);
        }

        [Benchmark]
        public void BfBasedMatcher()
        {
            using var matcher = new BFMatcher();
            var matches = matcher.Match(Descriptors1, Descriptors2);
            DoNothing(matches);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DoNothing(DMatch[] prameters)
        {

        }

        public void Dispose()
        {
            Descriptors1.Dispose();
            Descriptors2.Dispose();
        }
    }
}
