# 🧩 Proyecto Web API .NET 8 con EF Core

Este proyecto es una API RESTful construida con ASP.NET Core 8 y Entity Framework Core. Utiliza una base de datos SQL Server con tablas y datos precargados desde un script `.sql`.

---

## 📦 Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express)
- Un entorno para ejecutar scripts SQL (SQL Server Management Studio, Azure Data Studio, o similar)

---

## ⚙️ Pasos para ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/emag25/SASF_EAPRAEZ.git
cd SASF_EAPRAEZ/SASF_EAPRAEZ_KRUGER
```

---

### 3. Crear la base de datos

En SQL Server Management Studio, ejecuta el scrpit ubicado en la raiz del repositorio: BaseDatos.sql

Este script:

- Crea la base de datos
- Crea las tablas necesarias
- Inserta datos iniciales

---

### 2. Restaurar dependencias

```bash
dotnet restore
```

---

### 4. Configurar la cadena de conexión

Edita el archivo `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EA_GestionProyectosDB;User Id=usereapraez;Password=12345;TrustServerCertificate=True;"
}
```

---

### 5. Ejecutar la API

```bash
dotnet run --project SASF_EAPRAEZ_KRUGER
```

La API estará disponible en:

```
https://localhost:7235
http://localhost:5103
```

---

## 📬 Endpoints disponibles

Puedes probar los endpoints en **Swagger**, disponible en:

```
https://localhost:7235/swagger
```
 
O, puedes importar en **Postman** la colección del Api ubicada en:

```
/EA_GestionProyectos.postman_collection.json
```

---

## 🧪 Pruebas

Para ejecutar las pruebas unitarias o de integración:

```bash
dotnet test
```

