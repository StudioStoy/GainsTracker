#!/bin/bash

# Check if make is installed
if ! command -v make &> /dev/null; then
  echo "make is not installed. Installing..."

  # Determine the platform
  UNAME=$(uname)
  
  if [[ "$UNAME" == "Linux" ]]; then
    sudo apt-get update && sudo apt-get install -y make
  elif [[ "$UNAME" == "Darwin" ]]; then
    brew install make
  elif [[ "$UNAME" =~ MINGW64_NT || "$UNAME" =~ MINGW32_NT || "$UNAME" =~ MSYS_NT || "$UNAME" =~ CYGWIN_NT ]]; then
    if command -v choco &> /dev/null; then
      choco install make
    else
      echo "Chocolatey is not installed. Please install make manually or install Chocolatey (https://chocolatey.org/) and try again."
      exit 1
    fi
  else
    echo "Unsupported operating system. Please install make manually."
    exit 1
  fi
fi
