using ApplicationLayer;
using EnterpriseLayer;
using InterfaceAdapter_Repository;
using InterfaceAdapters_Data;
using InterfaceAdapters_Mappers;
using InterfaceAdapters_Mappers.Dtos.Request;
using InterfaceAdapters_Presenters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependencias
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("Beers")
.WithOpenApi();

app.MapPost("/beer", async (BeerRequestDTO beerRequest, AddBeerUseCase<BeerRequestDTO> beerUseCase) =>
{
    await beerUseCase.ExecuteAsync(beerRequest);
    return Results.Created();
})
.WithName("addBeer")
.WithOpenApi();

app.Run();
