# Docker multi-architecture, .NET 6.0 and OpenCVSharp

This repository contains an example Docker build to manage native dependencies to support 32 and 64 bit Arm devices as well as x64 systems utilising Docker's multi architecture support.

This repository is a companion to the [blog post at dev.to](https://dev.to/syamaner/docker-multi-architecture-net-60-and-opencvsharp-1okd).


For more information on the OpenCV build process, please see the [readme.md](./docker/readme.md) file.

An example build using this can be found at: [Example Dockerfile](./src/BenchmarkApp/Dockerfile)

## Change log

- 14/01/2023: Updated the build. [OPENCV_VERSION=4.7.0](https://github.com/opencv/opencv/releases/tag/4.7.0), [OPENCVSHARP_VERSION=4.7.0.20230114](https://github.com/shimat/opencvsharp/releases/tag/4.7.0.20230114)
  - Using this as a base image, the projects should use [OpenCVSharp 4.7.0.20230112 Nuget Packages](https://www.nuget.org/packages/OpenCvSharp4/4.7.0.20230112)
  - An example project using OpenCVSharp runtime libraries is available at [VinylEye repository](https://github.com/syamaner/VinylEye/blob/main/src/backend/VinylEye.Cli/Dockerfile)
  - Published tag: `syamaner/opencvsharp-build:3`