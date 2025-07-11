    using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductDetailDetailServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
using MultiShop.Catalog.Middleware;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Explicitly include the AutoMapper namespace

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
   opt =>
   {
       opt.Authority = builder.Configuration["IdentityServerUrl"];
       opt.Audience = "ResourceCatalog";
       opt.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
   });

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogReadPermission", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("scope", "CatalogReadPermission"));
    
    options.AddPolicy("CatalogFullPermission", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("scope", "CatalogFullPermission"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Specify the assembly explicitly to resolve ambiguity
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MultiShop Catalog API", Version = "v1" });
    
    // Add OAuth2 configuration
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
        {
            ClientCredentials = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{builder.Configuration["IdentityServerUrl"]}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "CatalogReadPermission", "Read Permission for Catalog API" },
                    { "CatalogFullPermission", "Full Permission for Catalog API" }
                }
            }
        }
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "CatalogReadPermission", "CatalogFullPermission" }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthenticationErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
