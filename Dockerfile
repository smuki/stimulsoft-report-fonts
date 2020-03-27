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
     && rm -rf /var/lib/apt/lists/*

RUN ln -s /lib64/libdl.so.2 /lib64/libdl.so

