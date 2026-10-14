#!/usr/bin/env bash
set -euo pipefail

project_dir="$(cd "$(dirname "$0")/.." && pwd)"
project_path="$project_dir/AspNetMvcLogin.csproj"

cd "$project_dir"
dotnet tool run dotnet-ef database update --project "$project_path"
