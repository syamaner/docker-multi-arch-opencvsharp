# Docker multi-architecture, .NET 6.0 and OpenCVSharp

This repository contains an example Docker build to manage native dependencies to support 32 and 64 bit Arm devices as well as x64 systems utilising Docker's multi architecture support.

This repository is a companion to the [blog post at dev.to](https://dev.to/syamaner/docker-multi-architecture-net-60-and-opencvsharp-1okd).


For more information on the OpenCV build process, please see the [readme.md](./docker/readme.md) file.

An example build using this can be found at: [Example Dockerfile](./src/BenchmarkApp/Dockerfile)
