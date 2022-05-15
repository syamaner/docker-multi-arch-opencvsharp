ARG OPENCV_SHARP_BUILD_TAG=2
ARG SDK_VERSION=6.0.202-bullseye-slim-amd64
ARG RUNTIME_VERSION=6.0.4-bullseye-slim

FROM syamaner/opencvsharp-build:$OPENCV_SHARP_BUILD_TAG AS opencv

# Given we are building a .Net application, the build does not have to be in the target arcitecture.
# Reference: https://github.com/dotnet/dotnet-docker/issues/1537#issuecomment-755351628
FROM mcr.microsoft.com/dotnet/sdk:$SDK_VERSION as build

ARG TARGETPLATFORM
WORKDIR /src
COPY . .

RUN if [ "$TARGETPLATFORM" = "linux/amd64" ]; then \
        RID=linux-x64 ; \
    elif [ "$TARGETPLATFORM" = "linux/arm64" ]; then \
        RID=linux-arm64 ; \
    elif [ "$TARGETPLATFORM" = "linux/arm/v7" ]; then \
        RID=linux-arm ; \
    fi && \
    dotnet publish -c release -o /app -r $RID --self-contained false

FROM mcr.microsoft.com/dotnet/runtime:$RUNTIME_VERSION as final

WORKDIR /app

# Copy opencv sharp native binding and runtime dependencies.
COPY --from=opencv /artifacts/ /usr/lib/ 
RUN ldconfig

COPY --from=build /app/ /app/  

ENTRYPOINT [ "dotnet", "/app/OpenCVSharpBenchmarkApp.dll" ]