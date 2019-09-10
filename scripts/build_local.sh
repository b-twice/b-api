#!/bin/bash
set -x #echo on
./scripts/sync_org.sh
TERM=xterm dotnet build