# 👨‍🍳 RestoSimplon 👩‍🍳

### Notre client, un restaurant, souhaite digitaliser la gestion de ses commandes.
### Pour répondre à son besoin, nous avons développé une API permettant de simplifier et automatiser la gestion des articles du menu, des clients et des commandes.

####  <span style="color: #26B260">Pour accéder au programme vous aurez besoin de :</span> </br></br> • Visual Studio, ou équivalent <br> • créer un projet ASP.NET Core vide </br> • installer les NuGet nécessaires :</br> <span style="color: #ea7e27">Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.EntityFrameworkCore.Tools</span></br> • copier/coller notre code dans votre fichier 

Dans notre code vous trouverez nos classes, méthodes et routes.

## Technologies Utilisées
- .NET 9
- C# 13.0
- Entity Framework Core
- SQLite
- Swagger pour la documentation de l'API

## Installation
1. Clonez le dépôt :
[https://github.com/votre-repo/ApiRestoSimplon.git](https://github.com/votre-repo/ApiRestoSimplon.git)

2. Accédez au répertoire du projet :
    `cd ApiRestoSimplon`

3. Restaurez les packages NuGet :
    `dotnet restore`

4. Appliquez les migrations et initialisez la base de données :
    `dotnet ef database update`


## Démarrage
Pour démarrer l'application, exécutez la commande suivante :
    `dotnet run`

L'API sera accessible à l'adresse `http://localhost:5000`.


## Routes de l'API
### Articles
- `GET /restoSimplon/articles` : Récupérer tous les articles
- `GET /restoSimplon/articles/{id}` : Récupérer un article par son ID
- `POST /restoSimplon/articles` : Créer un nouvel article
- `PUT /restoSimplon/articles/{id}` : Mettre à jour un article existant
- `DELETE /restoSimplon/articles/{id}` : Supprimer un article

### Commandes
- `GET /restoSimplon/commands` : Récupérer toutes les commandes
- `GET /restoSimplon/commands/{id}` : Récupérer une commande par son ID
- `GET /restoSimplon/commands/bydate/{date}` : Récupérer les commandes par date
- `POST /restoSimplon/commands` : Créer une nouvelle commande
- `PUT /restoSimplon/commands/{id}` : Mettre à jour une commande existante
- `DELETE /restoSimplon/commands/{id}` : Supprimer une commande

### Clients
- `GET /restoSimplon/clients` : Récupérer tous les clients
- `GET /restoSimplon/clients/{id}` : Récupérer un client par son ID
- `POST /restoSimplon/clients` : Créer un nouveau client
- `PUT /restoSimplon/clients` : Mettre à jour un client existant
- `DELETE /restoSimplon/clients` : Supprimer un client

### Catégories
- `GET /restoSimplon/categories` : Récupérer toutes les catégories

## Configuration de la Base de Données
La base de données utilisée dans l'API RestoSimplon est SQLite. Elle est configurée dans le fichier `Program.cs` en utilisant Entity Framework Core.

### Configuration dans `Program.cs`
![ConfigProgram](/Images/Configuration_Program.PNG)

## Initialisation de la Base de Données
L'initialisation de la base de données est effectuée dans le fichier `DbInitializer.cs`. Ce fichier contient la logique pour charger et insérer des données de test à partir de fichiers JSON.

## Utilisation de la Base de Données
Les routes de l'API permettent de gérer les entités de la base de données (Articles, Commandes, Clients, Catégories) via des opérations CRUD (Create, Read, Update, Delete).

### Exemple de Routes pour les Articles
![ArticleExemple](/Images/Exemple_Routes.PNG)



## Documentation Swagger
La documentation Swagger est disponible à l'adresse `http://localhost:5000/swagger`.

## Auteurs
- Valentin
- Bafodé
- Lisa

Pour toute question, veuillez contacter [RestoSimplon@exercice.com](mailto:RestoSimplon@exercice.com).

## Licence
Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
