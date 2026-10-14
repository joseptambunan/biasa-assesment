# ASP.NET MVC + Login API (.NET 8)

Satu project ASP.NET Core MVC dengan halaman login dan API login yang berbagi cookie authentication.

## Running

    dotnet run --project AspNetMvcLogin
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.21
    dotnet user-secrets init
    dotnet user-secrets set "Jwt:Key" "cd71e904-9ad3-4384-8f22-6d8029ffaf3d"
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.21
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.21
    dotnet tool install --global dotnet-ef
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.3
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.21
    dotnet tool run dotnet-ef migrations add AddKategoriTable
    dotnet tool run dotnet-ef migrations add AddUserGroupTable

Buka http://localhost:5080/Account/Login.

Kredensial demo:

    Username: admin
    Password: Admin123!
