# Building OpenCv and OpenCVSharp Docker image with multi architecture support

The Dockerfile in ./opencv-sharp directory will perform the following:

- Build OpenCV and OpenCV contrib projects using release 4.5.5 (25 Dec 2021)
- Build OpenCvSharpExtern  (release 4.5.3.20211228) dynamically linking it to OpenCV
- Copy libOpenCvSharpExtern.so and all of the dependencies to the /artifacts directory on the image to be used later.

The docker file can be seen in the [./opencv-sharp/Dockerfile](./opencv-sharp/Dockerfile) file.


A typical consumer will use this as part of a multi stage build and then copy contents of /artifacts into where the libraries should be located in the application image (for instance /usr/lib/)

An example build using this can be found at: [Example Dockerfile](../src/BenchmarkApp/Dockerfile)



## Credits

./opencv-sharp/cpld.sh  script uses ldd to detect the dependencies of a dynamic library and copies them to the desired directory.
   This script is used as is and credits goes to  Hemanth.HM (hemanth[dot]hm[at]gmail[dot]com).
   Url: https://h3manth.com/content/copying-shared-library-dependencies accessed 16/04/2022

   Thanks to this approach, the image size is relatively compact and the target application so far runs on various operating systems without any compatibility issues.
