#!/usr/bin/env bash
set -euo pipefail

project_dir="$(cd "$(dirname "$0")/.." && pwd)"
project_path="AspNetMvcLogin.csproj"

cd "$project_dir"
dotnet tool run dotnet-ef migrations add AddUserTable
dotnet tool run dotnet-ef migrations add AddGroupTable
dotnet tool run dotnet-ef migrations add AddKategoriTable
dotnet tool run dotnet-ef migrations add AddUserGroupTable
dotnet tool run dotnet-ef migrations add AddProductTable
dotnet tool run dotnet-ef migrations add AddProductStockTable
dotnet tool run dotnet-ef database update --project "$project_path" --startup-project "$project_path"
