#!/bin/bash
set -e
cd "$(dirname "$0")"
dotnet test RandomDeck.POCO.csproj --verbosity normal
