FROM debian:bullseye-slim AS build-native-env
ARG TARGETPLATFORM
ENV DEBIAN_FRONTEND=noninteractive
# 4.5.5: 25 Dec 2021
# https://github.com/opencv/opencv/wiki/ChangeLog#version455
ENV OPENCV_VERSION=4.5.5
# 4.5.3.20211228: 28 Dec 2021
#https://github.com/shimat/opencvsharp/releases/tag/4.5.3.20211228
ENV OPENCVSHARP_VERSION=4.5.3.20211228

WORKDIR /
RUN apt-get update && apt-get -y install --no-install-recommends \
      apt-transport-https \
      software-properties-common \
      wget \
      unzip \
      openssl \
      cmake \
      ca-certificates \
      build-essential \
      git \
      ninja-build \
      libtbb-dev \
      libatlas-base-dev \
      libgtk2.0-dev \
      libavcodec-dev \
      libavformat-dev \
      libswscale-dev \
      libxine2-dev \
      libv4l-dev \
      libtheora-dev \
      libvorbis-dev \
      libxvidcore-dev \
      libopencore-amrnb-dev \
      libopencore-amrwb-dev \
      libopenjp2-7-dev \
      libavresample-dev \
      x264 \
      libtesseract-dev \
      libdc1394-22-dev \
      libgdiplus 

# Setup OpenCV and opencv-contrib sources using the specified release.
RUN wget https://github.com/opencv/opencv/archive/${OPENCV_VERSION}.zip && \
    unzip ${OPENCV_VERSION}.zip && \
    rm ${OPENCV_VERSION}.zip && \
    mv opencv-${OPENCV_VERSION} opencv
RUN wget https://github.com/opencv/opencv_contrib/archive/${OPENCV_VERSION}.zip && \
    unzip ${OPENCV_VERSION}.zip && \
    rm ${OPENCV_VERSION}.zip && \
    mv opencv_contrib-${OPENCV_VERSION} opencv_contrib
# cmake and build OpenCV optinally specying architecture related cmake options.
RUN if [ "$TARGETPLATFORM" = "linux/amd64" ]; then \
        ADDITIONAL_FLAGS=""; \
    elif [ "$TARGETPLATFORM" = "linux/arm64" ]; then \
        ADDITIONAL_FLAGS=" -D ENABLE_NEON=ON -D CPU_BASELINE=NEON ";  \
    elif [ "$TARGETPLATFORM" = "linux/arm/v7" ]; then \
        ADDITIONAL_FLAGS=" -D CPU_BASELINE=NEON -D ENABLE_NEON=ON "; \
    fi && cd opencv && mkdir build && cd build && \
    cmake $ADDITIONAL_FLAGS \
    -D OPENCV_EXTRA_MODULES_PATH=/opencv_contrib/modules \
    -D CMAKE_BUILD_TYPE=RELEASE \
    -D BUILD_EXAMPLES=OFF \
    -D WITH_TBB=ON \
    -D BUILD_TBB=ON \
    -D WITH_OPENMP=ON \
    -D WITH_QT=OFF \
    -D WITH_V4L=ON \
    -D WITH_EIGEN=ON \
    -D WITH_LIBV4L=ON \
    -D OPENCV_ENABLE_NONFREE=ON \
    -D OPENCV_GENERATE_PKGCONFIG=ON \
    -D BUILD_DOCS=OFF \
    -D BUILD_PERF_TESTS=OFF \
    -D BUILD_TESTS=OFF \
    -D BUILD_opencv_java=OFF \
    -D BUILD_opencv_python=OFF .. \
    && make -j$(nproc) \
    && make install \
    && ldconfig
    
# Download OpenCvSharp to build OpenCvSharpExtern native library
RUN git clone https://github.com/shimat/opencvsharp.git
RUN cd opencvsharp && git fetch --all --tags --prune && git checkout ${OPENCVSHARP_VERSION}

WORKDIR /opencvsharp/src
RUN mkdir /opencvsharp/make \
    && cd /opencvsharp/make \
    && cmake -D CMAKE_INSTALL_PREFIX=/opencvsharp/make /opencvsharp/src \
    && make -j$(nproc) \
    && make install \
    && cp /opencvsharp/make/OpenCvSharpExtern/libOpenCvSharpExtern.so /usr/lib/ \
    && ldconfig

# Copy the library and dependencies to /artifacts (to be used by images consuming this build)
# cpld.sh will copy the library we specify (./libOpenCvSharpExtern.so) and any dependencies
#    to the /artifacts directory. This is useful for sharing the library with other images
#    consuming this build.
WORKDIR /opencvsharp/make/OpenCvSharpExtern
COPY cpld.sh .
RUN chmod +x cpld.sh && \
    mkdir /artifacts && \
    ./cpld.sh ./libOpenCvSharpExtern.so /artifacts/ 
RUN cp ./libOpenCvSharpExtern.so /artifacts/ 

# Publish the artifacts using a clean image
FROM debian:bullseye-slim AS final

RUN mkdir /artifacts
COPY --from=build-native-env /artifacts/ /artifacts

WORKDIR /artifacts