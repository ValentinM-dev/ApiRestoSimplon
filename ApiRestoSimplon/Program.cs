using ApiRestoSimplon.Class;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestoSimplonDB>(opt => opt.UseSqlite("Data Source=RestoSimplonDb.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v0.1", new OpenApiInfo
    {
        Title = "RestoSimplon Api",
        Version = "v0.1",
        Description = "Version 0.1 de l'API pour gerer des Commandes, Articles et Clients",
        Contact = new OpenApiContact
        {
            Name = "Valentin, Bafodé et Lisa",
            Email = "RestoSimplon@exercice.com",
            Url = new Uri("https://RestoSimpon.com"),
        }
    });

    // Annotations Swagger
    c.EnableAnnotations();
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "RestoSimplon API V0.1");
        c.RoutePrefix = "";
    });
}

//Ajouter les données des articles dans la base de données
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestoSimplonDB>();
    DbInitializer.Seed(dbContext);
}

// Mes routes sont localhost/restoSimplon
RouteGroupBuilder RestoSimplon = app.MapGroup("/restoSimplon");

// Mes routes sont localhost/restoSimplon/articles
RouteGroupBuilder articles = RestoSimplon.MapGroup("/articles");
// Mes routes sont localhost/restoSimplon/commands
RouteGroupBuilder commands = RestoSimplon.MapGroup("/commands");
// Mes routes sont localhost/restoSimplon/clients
RouteGroupBuilder clients = RestoSimplon.MapGroup("/clients");
// Mes routes sont localhost/restoSimplon/categories
RouteGroupBuilder categories = RestoSimplon.MapGroup("/categories");

// Routes  articles:
articles.MapGet("/", GetAllArticle).WithTags("Articles");  // Route pour obtenir tous les  articles
articles.MapGet("/{id}", GetArticle).WithTags("Articles"); // Route pour obtenir l'article par son id
articles.MapPost("/", CreateArticle).WithTags("Articles");  // Route pour la création d'un article
articles.MapPut("/{id}", UpdateArticle).WithTags("Articles"); // Route pour la mise à jour d'un article
articles.MapDelete("/{id}", DeleteArticle).WithTags("Articles"); // Route pour supprimer un article


// Routes  commandes:
commands.MapGet("/", GetAllCommands).WithTags("Commandes"); // Route pour obtenir toute les  commandes
commands.MapGet("/{id}", GetCommand).WithTags("Commandes"); // Route pour la commande par son id 
commands.MapGet("/bydate/{date}", GetCommandsByDate).WithTags("Commandes"); //Route pour obtenir les commandes par date
commands.MapPost("/", CreateCommand).WithTags("Commandes"); // Route pour la création d'une commande
commands.MapPut("/{id}", UpdateCommand).WithTags("Commandes"); // Route pour la mise à jour d'une commande
commands.MapDelete("/{id}", DeleteCommand).WithTags("Commandes"); // Route pour supprimer une commande


// Routes clients:
clients.MapGet("/", GetAllClient).WithTags("Clients");
clients.MapGet("/{id}", GetClient).WithTags("Clients");
clients.MapPost("/", CreateClient).WithTags("Clients");
clients.MapPut("/", UpdateClient).WithTags("Clients");
clients.MapDelete("/", DeleteClient).WithTags("Clients");

// Routes Categories:
categories.MapGet("/", GetCategorie).WithTags("Categories");

app.Run();

// Méthodes de gestion des routes articles 

static async Task<IResult> GetAllArticle(RestoSimplonDB db)
{
    var articles = await db.Articles.ToListAsync();  // la variable articles pour effectuer le requete a la bdd et recupérer les articles
    return TypedResults.Ok(articles);
}

static async Task<IResult> GetArticle(int id, RestoSimplonDB db)
{
    var article = await db.Articles.FindAsync(id);
    return article is not null
        ? TypedResults.Ok(article)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateArticle(ArticleDTO articleDTO, RestoSimplonDB db)
{
    var article = new Article
    {
        NameArticle = articleDTO.NameArticle,  // mise a jour des proprétés de l'article 
        CategorieId = articleDTO.CategorieId,
        PrixArticle = articleDTO.PrixArticle,
        Status = articleDTO.Status
    };

    db.Articles.Add(article);  // ajout en bdd
    await db.SaveChangesAsync(); // sauvegarde en bdd

    return TypedResults.Created($"/restoSimplon/articles/{article.Id}", articleDTO);
}

static async Task<IResult> UpdateArticle(int id, ArticleDTO articleDTO, RestoSimplonDB db)
{
    var article = await db.Articles.FindAsync(id);

    if (article is null)
        return TypedResults.NotFound();

    article.NameArticle = articleDTO.NameArticle;
    article.CategorieId = articleDTO.CategorieId;
    article.PrixArticle = articleDTO.PrixArticle;
    article.Status = articleDTO.Status;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteArticle(int id, RestoSimplonDB db)
{
    var article = await db.Articles.FindAsync(id);
    if (article is null)
        return TypedResults.NotFound();

    db.Articles.Remove(article);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}




// Méthodes de gestion des routes pour commande

static async Task<IResult> GetAllCommands(RestoSimplonDB db)
{
    var commands = await db.Commands.ToListAsync();
    return TypedResults.Ok(commands);
}


static async Task<IResult> GetCommand(int id, RestoSimplonDB db)
{
    var command = await db.Commands.FindAsync(id);
    return command is not null
        ? TypedResults.Ok(command)
        : TypedResults.NotFound();
}



static async Task<IResult> CreateCommand(CommandDTO commandDTO, RestoSimplonDB db)
{
    var command = new Command
    {
        Id = commandDTO.Id,
        ClientId = commandDTO.ClientId,
        MontantCommande = commandDTO.MontantCommande,
        DateCommand = commandDTO.DateCommand,
        Articles = commandDTO.Articles,
        NbArticle = commandDTO.NbArticle
    };

    db.Commands.Add(command);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/commands/{command.Id}", new CommandDTO
    {
        Id = command.Id,
        ClientId = command.ClientId,
        MontantCommande = command.MontantCommande,
        DateCommand = command.DateCommand,
        Articles = command.Articles,
        NbArticle = command.NbArticle
    });
}


static async Task<IResult> UpdateCommand(int id, CommandDTO commandDTO, RestoSimplonDB db)
{
    var command = await db.Commands
        .Include(c => c.Articles) // Inclure la liste des articles
        .FirstOrDefaultAsync(c => c.Id == id);

    if (command is null)
        return TypedResults.NotFound();

    command.ClientId = commandDTO.ClientId;
    command.DateCommand = commandDTO.DateCommand;
    command.MontantCommande = commandDTO.MontantCommande;
    command.Articles = commandDTO.Articles; // Mettre à jour la liste des articles
    command.NbArticle = commandDTO.NbArticle;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}



static async Task<IResult> DeleteCommand(int id, RestoSimplonDB db)
{
    var command = await db.Commands.FindAsync(id); // recherche de la commande dans la bdd

    if (command is null)
        return TypedResults.NotFound();

    db.Commands.Remove(command);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> GetCommandsByDate(DateTime date, RestoSimplonDB db)
{
    var commands = await db.Commands
        .Where(c => c.DateCommand.Date == date.Date)  // trie des commandes par date
        .ToListAsync();

    return commands.Any()
        ? TypedResults.Ok(commands)
        : TypedResults.NotFound();
}



// Methode de getion des routes clients

// Route pour récupérer l'ensemble des clients

static async Task<IResult> GetAllClient(RestoSimplonDB db)
{
    var clients = await db.Clients.ToListAsync();
    return TypedResults.Ok(clients);
}
static async Task<IResult> GetClient(int id, RestoSimplonDB db)
{


    // recupère le client par son id
    var client = await db.Clients.FindAsync(id);

    // ensuite retourne le client avec ses informations

    return client is not null
        ? TypedResults.Ok(client)
        : TypedResults.NotFound();
}
static async Task<IResult> GetCategorie(int id, RestoSimplonDB db)
{
    var categorie = await db.Categories.FindAsync(id);
        return categorie is not null
        ? TypedResults.Ok(categorie)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateClient(Client client, RestoSimplonDB db)
{
    db.Clients.Add(client);
    await db.SaveChangesAsync();



    return TypedResults.Created($"/client/{client.Id}");

}



static async Task<IResult> UpdateClient(int id, ClientDTO client, RestoSimplonDB db)
{
    var clientDb = await db.Clients.FindAsync(id);
    if (clientDb is null) return TypedResults.NotFound();

    clientDb.Name = client.Name;
    clientDb.Prenom = client.Prenom;
    clientDb.Adress = client.Adress;
    clientDb.PhoneNumber = client.PhoneNumber;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteClient(int id, RestoSimplonDB db)
{
    if (await db.Clients.FindAsync(id) is Client client)
    {
        db.Clients.Remove(client);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}
