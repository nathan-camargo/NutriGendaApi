using Microsoft.EntityFrameworkCore;
using NutriGendaApi.src.Data;
using NutriGendaApi.src.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container.
builder.Services.AddControllers(); // Para adicionar suporte a controladores
builder.Services.AddScoped<DietService>();
builder.Services.AddScoped<HealthProfileService>();
builder.Services.AddScoped<NutritionistService>();
builder.Services.AddScoped<UserService>();

// Configurar o DbContext com a string de conexão
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar Swagger para gerar documentação automática da API
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Mostra detalhes do erro em desenvolvimento
    app.UseSwagger(); // Ativar Swagger
    app.UseSwaggerUI(); // Ativar UI do Swagger
}

app.UseHttpsRedirection(); // Redirecionamento HTTP para HTTPS
app.UseAuthorization(); // Middleware de autorização
app.MapControllers(); // Mapear controladores para rotas

app.Run(); // Executar aplicativo
