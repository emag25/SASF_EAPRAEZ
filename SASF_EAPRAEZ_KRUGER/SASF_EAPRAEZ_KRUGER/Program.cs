using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions;
using SASF_EAPRAEZ_KRUGER.Exceptions.Filters;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Services.Proyectos;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContextGestionProyectos>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Add<ModelValidationFilter>();
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GESTIÓN DE PROYECTO (SASF EAPRAEZ)", Version = "v1" });
 });


// --
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
