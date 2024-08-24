using ApplicationLayer;
using EnterpriseLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using FrameworksAndDrivers_ExternalService;
using FrameworksDrivers_API.Middlewares;
using FrameworksDrivers_API.Validators;
using InterfaceAdapter_Repository;
using InterfaceAdapters_Adapters;
using InterfaceAdapters_Adapters.Dtos;
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

//Validadores

builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

//dependencias
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
builder.Services.AddScoped<IExternalService<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<GetPostUseCase>();

builder.Services.AddHttpClient<IExternalService<PostServiceDTO>, PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("Beers")
.WithOpenApi();

app.MapPost("/beer", async (BeerRequestDTO beerRequest, AddBeerUseCase<BeerRequestDTO> beerUseCase, IValidator<BeerRequestDTO> validator ) =>
{
    var result = await validator.ValidateAsync(beerRequest);

    if(!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }

    await beerUseCase.ExecuteAsync(beerRequest);
    return Results.Created();
})
.WithName("addBeer")
.WithOpenApi();

app.MapGet("/beerDetail", async (GetBeerUseCase<Beer, BeerDetailViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("beerDetail")
.WithOpenApi();

app.MapGet("/post", async (GetPostUseCase postUseCase) =>
{
    return await postUseCase.ExecuteAsync();
})
.WithName("posts")
.WithOpenApi();

app.Run();
//https://jsonplaceholder.typicode.com/posts