
-- Crear de la Base de datos
CREATE DATABASE EA_GestionProyectosDB;
GO


USE EA_GestionProyectosDB;
GO


-- Crear la tabla de Usuarios
CREATE TABLE dbo.Usuario (

    UsuarioId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    NombreCompleto NVARCHAR(200) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Telefono NVARCHAR(10) NOT NULL UNIQUE,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('ADMIN', 'EDITOR', 'VIEWER')),
    Estado NVARCHAR(10) NOT NULL CHECK (Estado IN ('ACTIVO', 'ELIMINADO')) DEFAULT 'ACTIVO',
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaModificacion DATETIME2

);
GO


-- Crear la tabla de Proyectos
CREATE TABLE dbo.Proyecto (

    ProyectoId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    UsuarioId UNIQUEIDENTIFIER NULL,
    Estado NVARCHAR(10) NOT NULL CHECK (Estado IN ('ACTIVO', 'INACTIVO', 'ELIMINADO')) DEFAULT 'ACTIVO',
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaModificacion DATETIME2

    CONSTRAINT FK_Proyecto_Usuario FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuario (UsuarioId)

);
GO


-- Crear la tabla de Actividades
CREATE TABLE dbo.Actividad (

    ActividadId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NOT NULL,
    Fecha DATE NOT NULL,
    HorasEstimadas INT NOT NULL,
    HorasReales INT NOT NULL DEFAULT 0,
    ProyectoId UNIQUEIDENTIFIER NOT NULL,
    Estado NVARCHAR(10) NOT NULL CHECK (Estado IN ('ACTIVO', 'INACTIVO', 'ELIMINADO')) DEFAULT 'ACTIVO',
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaModificacion DATETIME2

    CONSTRAINT FK_Actividad_Proyecto FOREIGN KEY (ProyectoId) REFERENCES dbo.Proyecto (ProyectoId)

);
GO


-------------------------------------------------------
-- Insertar datos 
-------------------------------------------------------

DELETE FROM dbo.Actividad;
DELETE FROM dbo.Proyecto;
DELETE FROM dbo.Usuario;


INSERT INTO dbo.Usuario (NombreCompleto, Correo, Telefono, Rol)
VALUES 
('Ana Mar�a Torres Palacios', 'ana.torres@krugercorp.com', '0991234567', 'ADMIN'),
('Luis Miguel P�rez Pe�a', 'luis.perez@krugercorp.com', '0987654321', 'EDITOR'),
('Martha Piedad Ruiz Ortiz', 'marta.ruiz@krugercorp.com', '0971122334', 'VIEWER'),
('Carlos Xavier Le�n Mena', 'carlos.leon@krugercorp.com', '0969988776', 'EDITOR'),
('Diana Daniela V�lez Garc�a', 'diana.velez@krugercorp.com', '0955544332', 'VIEWER');



-- (Usuarios a asociar)
DECLARE @Usuario1 UNIQUEIDENTIFIER = (SELECT TOP 1 UsuarioId FROM dbo.Usuario WHERE Correo = 'ana.torres@krugercorp.com');
DECLARE @Usuario2 UNIQUEIDENTIFIER = (SELECT TOP 1 UsuarioId FROM dbo.Usuario WHERE Correo = 'luis.perez@krugercorp.com');
DECLARE @Usuario3 UNIQUEIDENTIFIER = (SELECT TOP 1 UsuarioId FROM dbo.Usuario WHERE Correo = 'marta.ruiz@krugercorp.com');

-- (GUID de los proyectos)
DECLARE @Proyecto1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Proyecto2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Proyecto3 UNIQUEIDENTIFIER = NEWID();

-- Insertar Proyectos
INSERT INTO dbo.Proyecto (ProyectoId, Nombre, Descripcion, FechaInicio, FechaFin, UsuarioId)
VALUES 
(@Proyecto1, 'Sistema de Gesti�n de Inventario', 'Aplicaci�n para controlar inventarios en tiempo real', '2025-07-01', '2025-12-15', @Usuario1),
(@Proyecto2, 'Portal de Clientes', 'Portal web para consulta de facturaci�n y soporte', '2025-06-01', '2025-11-30', @Usuario2),
(@Proyecto3, 'Aplicaci�n M�vil de Tareas', 'App Android para gestionar tareas diarias', '2025-05-15', '2025-10-31', @Usuario3);



-- Insertar Actividades para Proyecto 1
INSERT INTO dbo.Actividad (Titulo, Descripcion, Fecha, HorasEstimadas, ProyectoId)
VALUES 
('Dise�o de Base de Datos', 'Definici�n del modelo relacional', '2025-07-05', 8, @Proyecto1),
('Configuraci�n del servidor', 'Instalaci�n y setup de SQL Server', '2025-07-10', 6, @Proyecto1),
('Revisi�n con cliente', 'Feedback sobre funcionalidades iniciales', '2025-07-15', 4, @Proyecto1);

-- Insertar Actividades para Proyecto 2
INSERT INTO dbo.Actividad (Titulo, Descripcion, Fecha, HorasEstimadas, ProyectoId)
VALUES 
('Dise�o UI', 'Mockups en Figma', '2025-06-05', 5, @Proyecto2),
('Frontend - Login', 'Desarrollo del formulario de login', '2025-06-10', 6, @Proyecto2),
('Backend - Autenticaci�n', 'Creaci�n del API de login', '2025-06-12', 8, @Proyecto2),
('Integraci�n API', 'Conexi�n del frontend con backend', '2025-06-15', 7, @Proyecto2),
('Pruebas unitarias', 'Tests de endpoints principales', '2025-06-20', 4, @Proyecto2);

-- Insertar Actividades para Proyecto 3
INSERT INTO dbo.Actividad (Titulo, Descripcion, Fecha, HorasEstimadas, ProyectoId)
VALUES 
('Dise�o App', 'Wireframes m�viles', '2025-05-20', 4, @Proyecto3),
('Login App', 'Pantalla de autenticaci�n', '2025-05-25', 6, @Proyecto3),
('Notificaciones push', 'Configuraci�n de notificaciones Firebase', '2025-06-01', 5, @Proyecto3),
('Sincronizaci�n offline', 'Guardar tareas sin conexi�n', '2025-06-05', 8, @Proyecto3),
('Pantalla de tareas', 'Lista de tareas con estado', '2025-06-10', 7, @Proyecto3),
('Publicaci�n en Play Store', 'Subida y configuraci�n en la tienda', '2025-06-20', 3, @Proyecto3);
