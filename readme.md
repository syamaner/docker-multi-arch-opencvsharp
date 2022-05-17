# Docker multi-architecture, .NET 6.0 and OpenCVSharp

This repository contains an example Docker build to manage native dependencies to support 32 and 64 bit Arm devices as well as x64 systems utilising Docker's multi architecture support.

This repository is a companion to the [blog post at dev.to](https://dev.to/syamaner/exploring-net-60-opencvsharp-and-docker-multi-architecture-support-jp6-temp-slug-7732988?preview=82d1d60be996565be898e369c3d062c5be05934fa3fc39a7b2a1a8a3201251e64eb4dca9d64c3ab7335565e13d5077a03fee4bcd940077dbe2661cac).


For more information on the OpenCV build process, please see the [readme.md](./docker/readme.md) file.

An example build using this can be found at: [Example Dockerfile](./src/BenchmarkApp/Dockerfile)
