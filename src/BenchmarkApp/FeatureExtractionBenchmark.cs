using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using OpenCvSharp;
using OpenCvSharp.Features2D;
using OpenCvSharp.XFeatures2D;

namespace OpenCVSharpBenchmarkApp
{
    [InProcess]
    public class FeatureExtractionBenchmark : IDisposable
    {
        private static readonly Mat Gray1 = new();
        private static readonly Mat Gray2 = new();

        static FeatureExtractionBenchmark()
        {
            using var img1 = Cv2.ImRead("image1.jpg");
            using var img2 = Cv2.ImRead("image2.jpg");
            Cv2.CvtColor(img1, Gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(img2, Gray2, ColorConversionCodes.BGR2GRAY);
        }

        [Benchmark]
        public void SiftFeatureExtraction()
        {
            using Mat descriptors1 = new();
            using Mat descriptors2 = new();
            using var sift = SIFT.Create();
            sift.DetectAndCompute(Gray1, null, out var keyPoints1, descriptors1);
            sift.DetectAndCompute(Gray2, null, out var keyPoints2, descriptors2); ;

            DoNothing(keyPoints1, keyPoints2);
        }

        [Benchmark]
        public void SurfFeatureExtraction()
        {
            using Mat descriptors1 = new();
            using Mat descriptors2 = new();
            using var surf = SURF.Create(500);
            surf.DetectAndCompute(Gray1, null, out var keyPoints1, descriptors1);
            surf.DetectAndCompute(Gray2, null, out var keyPoints2, descriptors2);

            DoNothing(keyPoints1, keyPoints2);
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DoNothing(KeyPoint[] keyPoints1, KeyPoint[] keyPoints2)
        {

        }

        public void Dispose()
        {
            Gray1.Dispose();
            Gray2.Dispose();
        }
    }
}
