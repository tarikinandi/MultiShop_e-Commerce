using Microsoft.AspNetCore.Authentication.JwtBearer;
using Multishop.Discount.Context;
using Multishop.Discount.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ResourceDiscount";
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
});

// Add authorization policies
builder.Services.AddAuthorization(policyOptions =>
{
    policyOptions.AddPolicy("DiscountFullPermission", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("scope", "DiscountFullPermission"));
});

// Add services to the container.
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService, DiscountService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
