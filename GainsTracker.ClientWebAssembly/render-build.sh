#!/usr/bin/env bash
# Exit on error
set -o errexit

# Download
wget https://download.visualstudio.microsoft.com/download/pr/1ebffeb0-f090-4001-9f13-69f112936a70/5dbc249b375cca13ec4d97d48ea93b28/dotnet-sdk-8.0.402-linux-x64.tar.gz

# Extract
mkdir -p $XDG_CACHE_HOME/dotnet && tar zxf dotnet-sdk-7.0.403-linux-x64.tar.gz -C $XDG_CACHE_HOME/dotnet;

# Set vars
export DOTNET_ROOT=$XDG_CACHE_HOME/dotnet;
export PATH=$PATH:$XDG_CACHE_HOME/dotnet; 

# Verify installation
dotnet --version 

# Build/publish static ClientWeb site
dotnet publish -c Release
