# BlazorTechNotes

![.NET](https://img.shields.io/badge/.NET-10.0-blue)
![C#](https://img.shields.io/badge/C%23-14.0-purple)
![Blazor](https://img.shields.io/badge/Blazor-Server-green)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)
![Docker](https://img.shields.io/badge/Docker-Compose-blue)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-orange)

## Descripción

BlazorTechNotes es una aplicación web para gestionar notas técnicas. Permite a los usuarios registrarse, iniciar sesión y crear, editar y visualizar notas de manera intuitiva. Está diseñada siguiendo principios de Clean Architecture para mantener un código modular y escalable.

## Características Principales

- **Autenticación de Usuarios**: Registro y login de usuarios con manejo seguro de contraseñas.
- **Gestión de Notas**: Crear, actualizar y visualizar notas con detalles como título, contenido, autor y fecha de publicación.
- **Interfaz Responsiva**: Diseño adaptativo utilizando Bootstrap para una experiencia óptima en dispositivos móviles y de escritorio.
- **Arquitectura Limpia**: Separación en capas (Domain, Application, Infrastructure) para facilitar el mantenimiento y las pruebas.
- **Base de Datos**: Integración con SQL Server mediante Entity Framework Core.
- **Contenedorización**: Configuración con Docker Compose para facilitar el despliegue.

## Tecnologías Utilizadas

- **Framework**: .NET 10.0 con Blazor Server para la interfaz de usuario.
- **Lenguaje**: C# 14.0.
- **Base de Datos**: SQL Server 2022 con Entity Framework Core para el ORM.
- **Contenedorización**: Docker y Docker Compose.
- **Estilos**: Bootstrap 5.3 para el diseño responsivo.
- **Otros**: Middleware personalizado para autorización, servicios de aplicación y repositorios.

## Instalación y Ejecución

### Prerrequisitos

- .NET 8.0 SDK
- SQL Server (local o en contenedor)
- Docker y Docker Compose (opcional para contenedorización)

### Pasos

1. Clona el repositorio:
   ```
   git clone https://github.com/tu-usuario/BlazorTechNotes.git
   cd BlazorTechNotes
   ```

2. Restaura las dependencias:
   ```
   dotnet restore
   ```

3. Configura la base de datos en `appsettings.json` (o usa Docker Compose para SQL Server).

4. Ejecuta las migraciones:
   ```
   dotnet ef database update
   ```

5. Ejecuta la aplicación:
   ```
   dotnet run
   ```

### Usando Docker

1. Construye y ejecuta con Docker Compose:
   ```
   docker-compose up --build
   ```

## Contribución

Las contribuciones son bienvenidas. Por favor, abre un issue o envía un pull request.

## Licencia

Este proyecto está bajo la Licencia MIT.