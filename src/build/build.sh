export build_number="1"

cd ../BenchmarkApp

DOCKER_CLI_EXPERIMENTAL=true docker buildx build \
     --push \
     --platform linux/amd64,linux/arm64/v8,linux/arm/v7 \
     --tag syamaner/opencvsharp-bench:${build_number} \
     .
