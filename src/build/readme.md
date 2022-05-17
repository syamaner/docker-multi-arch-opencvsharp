Just like the OpenCvSharp dependencies image, the target application can also be built using Docker multi architecture so that same image can be puled using a matching build based on the target architecture on the devices where containers will be created  on.

In this case, we are supporting linux/amd64,linux/arm64/v8,linux/arm/v7 which makes the image compatible with various arm devices as well as intel / amd x64 systems.

