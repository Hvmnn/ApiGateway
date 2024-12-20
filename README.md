# ApiGateway

Este proyecto corresponde al Taller 2 para la asignatura Arquitectura de Sistemas.

## Requerimientos

- **[ASP.NET Core 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)** 
- **[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)** 
- **[Postman](https://www.postman.com/downloads/)** para probar la API

## Clonar el Repositorio

Clona el repositorio con el siguiente comando:

```bash
git clone https://github.com/Hvmnn/ApiGateway
```

## Restaurar el Proyecto

Después de clonar el repositorio, navega a la carpeta del proyecto y restaura los paquetes de NuGet:

```bash
cd .\ApiGatewy\
dotnet restore
```

## Ejecutar la Aplicación

Para ejecutar la aplicación, utiliza el siguiente comando:

```bash
dotnet run
```

Esto iniciará el servidor en `http://localhost:5047`.

Despues de esto estará lista para comunicarse a través de gRPC hacia los microservicios de Careers y Usuarios, como tambien para comunicarse a través de HTTP/S con el microservicio de Access.
