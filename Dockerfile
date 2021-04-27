FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

MAINTAINER ekuaibao

LABEL name="sample"

# # install System.Drawing native dependencies
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        apt-utils \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        fontconfig \
        xfonts-utils \
     && rm -rf /var/lib/apt/lists/*

RUN ln -s /lib64/libdl.so.2 /lib64/libdl.so

COPY fonts/SourceHanSansCN-Regular.ttf /usr/share/fonts/ttf-dejavu/SourceHanSansCN-Regular.ttf

COPY fonts/simsun.ttc /usr/share/fonts/ttf-dejavu/simsun.ttc