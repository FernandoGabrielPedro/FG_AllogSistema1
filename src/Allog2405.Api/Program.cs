using Allog2405.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
    options.ListenLocalhost(5000);
});

// Add services to the container.
//Foi instalado o pacote JsonPatch, que serve para modificar um atributo de um arquivo Json, já que ele não é instalado por padrão.
//Para usá-lo é necessário do pacote Newtonsoft, que não é instalado por padrão, já que o pacote JsonPatch o usa no lugar da biblioteca padrão, System.Text.Json.
builder.Services.AddControllers(options =>
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter())
).ConfigureApiBehaviorOptions(options =>
    options.SuppressModelStateInvalidFilter = true
);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
