Repositorio del **backend** para una red social de artesanos y productos locales,
construido con **ASP.NET Core Web API**.
El objetivo es conectar artesanos con clientes, permitiendo a los creadores locales mostras sus productos, 
compartir sus historias y gestionar pedidos de manera eficiente.
**Caraterísticas Principales del Proyecto
- **Perfiles de Artesanos**: Cada artesano puede crear un perfil personalizado con su biografía y fotos de su trabajo.
- **Catálogo de Productos**: Módulo para que los artesanos suban sus productos con descripciones y precios.
- **Autenticación y Autorización**: Sistema de seguridad basado en JWT para gestionar usuarios y permisos.
- **API RESTful**: Endpoints claros y documentados para el consumo del frontend.

##Tecnologías Utilizadas
- **Backend**: ASP.NET Core Web API
- **Lenguaje**: C#
- **Base de Datos**: MySQL
- **ORM**: Entity Framework Core
- **Autenticación**: JWT (JSON Web Tokens)
- **Herramientas**: Swagger para documentación de la API

##Configuración y Ejecución
Para ejecutar el proyecto localmente, sigue estos pasos: 
1. **Clonar el repositorio**:
   ```bash
    git clone [https://github.com/tu-usuario/red-artesanias-backend.git](https://github.com/tu-usuario/red-artesanias-backend.git)
    ```

2.  **Configurar la base de datos**:
    -   Asegúrate de tener un servidor MySQL en ejecución.
    -   Actualiza la cadena de conexión en `appsettings.json` con tus credenciales.
    -   Aplica las migraciones de la base de datos:
        ```bash
        dotnet ef database update
        ```

3.  **Ejecutar la aplicación**:
    ```bash
    dotnet run
    ```
    La API estará disponible en `http://localhost:8080`. Puedes probar los endpoints usando la interfaz de Swagger en `http://localhost:8080/swagger`.

## Contribuciones

Las contribuciones son bienvenidas. Siéntete libre de abrir un **pull request** o un **issue** con tus sugerencias.

----
