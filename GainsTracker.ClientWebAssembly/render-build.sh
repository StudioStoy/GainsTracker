#!/usr/bin/env bash
# Exit on error
set -o errexit

# Download
wget https://download.visualstudio.microsoft.com/download/pr/ff8c660f-ffa9-4814-ac2d-4089e6ec4eb5/dc806d344844f1d58d8015d105e85c65/dotnet-sdk-7.0.403-linux-x64.tar.gz;

# Extract
mkdir -p $XDG_CACHE_HOME/dotnet && tar zxf dotnet-sdk-7.0.403-linux-x64.tar.gz -C $XDG_CACHE_HOME/dotnet;

# Set vars
export DOTNET_ROOT=$XDG_CACHE_HOME/dotnet;
export PATH=$PATH:$XDG_CACHE_HOME/dotnet; 

# Verify installation
dotnet --version 

# Build/publish static ClientWeb site
dotnet publish -c Release
