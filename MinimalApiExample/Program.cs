using MinimalApiExample;

var builder = WebApplication.CreateBuilder(args);

// Adicione o serviço do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use o middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.99m, InStock = true },
    new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 15.99m, InStock = false },
    // Adicione mais produtos para teste
};

// Rota GET para listar todos os produtos
app.MapGet("/products", () => products);

// Rota GET para obter um produto pelo ID
app.MapGet("/products/{id}", (int id) => products.FirstOrDefault(p => p.Id == id));

// Rota POST para adicionar um novo produto
app.MapPost("/products", (Product product) => {
    products.Add(product);
    return Results.Created($"/products/{product.Id}", product);
});

// Rota PUT para atualizar um produto
app.MapPut("/products/{id}", (int id, Product updatedProduct) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null) return Results.NotFound();

    product.Name = updatedProduct.Name;
    product.Description = updatedProduct.Description;
    product.Price = updatedProduct.Price;
    product.InStock = updatedProduct.InStock;

    return Results.NoContent();
});

// Rota DELETE para excluir um produto
app.MapDelete("/products/{id}", (int id) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null) return Results.NotFound();

    products.Remove(product);
    return Results.NoContent();
});

app.Run();


