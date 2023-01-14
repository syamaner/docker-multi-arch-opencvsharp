export build_number="3"
#linux/amd64,linux/arm64/v8,linux/arm/v7
docker buildx build --push --platform linux/amd64,linux/arm64/v8,linux/arm/v7 --tag syamaner/opencvsharp-build:${build_number} ./opencv-sharp