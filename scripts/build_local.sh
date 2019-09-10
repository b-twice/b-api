#!/bin/bash
set -x #echo on
./sync_org.sh
TERM=xterm dotnet build