# Prérequis
* Installer Visual Studio Code    
* Installer Dotnet Core SDK & Runtime : https://www.microsoft.com/net/download/core    

# Préparatifs
`mkdir Isen.DotNet`     
`cd Isen.DotNet`     
`git init`     
`touch .gitignore`     
`touch readme.md`     
`code .`     

# Création de l'espace de travail
## Création d'un projet console
Depuis le dossier Isen.DotNet :  
`mkdir Isen.DotNet.ConsoleApp`  
`cd Isen.DotNet.ConsoleApp`  
`dotnet new console`  
`dotnet run`  

## Commit Git
Depuis la racine du projet :  
Etat des fichiers (non trackés) : `git status`  
Tracker les fichiers : `git add .`  
Ils sont maintenant suivis : `git status`  
Commit : `git commit -m "Initial commit"`  
Prendre en compte les modifs (**mais pas les ajouts de fichiers**) au moment du commit (donc faire un add et un commit en même temps :  
`git commit -a -m "updated readme"`  

## Création d'un projet librairie
Depuis le dossier Isen.DotNet :  
`mkdir Isen.DotNet.Library`  
`cd Isen.DotNet.Library`  
`dotnet new classlib`  

## Ajout de la librairie en référence du projet console
Depuis le dossier du projet Console :  
`dotnet add reference ..\Isen.DotNet.Library\Isen.DotNet.Library.csproj`  
Coder la classe Hello.  
Dans le Program.cs, ajouter le using, et appeler la classe Hello.  

## Création d'une solution et ajout des projets
Depuis le dossier racine  
Créer le fichier solution : `dotnet new sln`  
Ajouter le projet librairie :
`dotnet sln add Isen.DotNet.Library\Isen.DotNet.Library.csproj`  
Ajouter le projet console :  
`dotnet sln add Isen.DotNet.ConsoleApp\Isen.DotNet.ConsoleApp.csproj`  
Commit git :   
`git add .`  
`git commit -m "Console, lib, solution"`  

## Ajout d'un projet de test
*TDD = Test Driven Development*  
Depuis le dossier racine :  
`mkdir Isen.DotNet.Tests && cd Isen.DotNet.Tests`  
Créer le projet de tests
`dotnet new xunit`  
Ajouter ce projet à la solution (depuis le dossier racine)
`dotnet sln add Isen.DotNet.Tests\Isen.DotNet.Tests.csproj`  
Depuis le dossier du projet de tests :  
 `dotnet add reference ..\Isen.DotNet.Library\Isen.DotNet.Library.csproj`  
 Commit git (penser à se remettre dans le dossier racine)  
`git add .`  
`git commit -m "Test project"`  

## Push du projet sur un repo remote
Créer un projet sur le serveur Git de votre choix (GitHub, GitLab, interne ISEN...)  
L'url de mon repo est https://gitlab.com/moi/mon-projet.git  
`git remote add origin https://gitlab.com/moi/mon-projet.git`  
Push, en indiquant que master correspond à origin/master  
`git push -u origin master`  

## Ajout d'un tag Git
Créer le tag dans le repo local  
`git tag v0.1`  
Pusher le tag dans le remote repo  
`git push origin v0.1`  

# Ajout d'un modèle
## Types Person et City
Dans le projet Library :  
* Créer un dossier Models/Implementation
* Créer un classe `Person` :
  * `Id` (int)
  * `Name` (string)
  * `FirstName` (string)
  * `LastName` (string)
  * `BirthDate` (DateTime)
* Créer une classe `City` : 
  * `Id` (int)
  * `Name` (string)

## Refactoring : extraction d'un BaseModel
Les classes Person et City ont une partie de leur logique commune.  
Extraire ce qui est commun dans une classe abtraite `BaseModel`.  
La classe de base sera dans le dossier Models/Base.  

# Ajout de Repositories
## CityRepository
A la racine du projet Library, créer un dossier `Repositories/InMemory`  
Ajouter une classe `InMemoryCityRepository`  
Extraire l'interface de ce repo dans `Repositories/Interfaces/IInMemoryCityRepository.cs`  

## Refactoring interface : extraire IBaseRepository
Déplacer les 3 méthodes de l'interface vers IBaseRepository  

## Refactoring Repository
* Créer la classe abstraite Repositories/Base/_BaseRepository.cs  
* Déplacer la logique de CityRepository vers cette classe  

## Appliquer au modele Person
* Dupliquer ICityRepository vers IPersonRepository et adapter ce qui doit l'être.  
* Dupliquer InMemoryCityRepository afin de créer InMemoryPersonRepository.  

## Ajouter une méthode de requête (Find)
Dans `IBaseRepository<T>` et `BaseRepository<T>`, ajouter une méthode Find, qui prendra une fonction de filtrage en paramètre.  

# CRUD
* C = Create
* R = Read
* U = Update
* D = Delete

## Ajout du Delete
Dans `IBaseRepository<T>` et `BaseRepository<T>`, ajouter 2 méthodes :
* Delete(int id)  
* Delete(T model)  
Ajouter les méthodes `DeleteRange` suivantes :  
* DeleteRange(IEnumerable<T> models)  
  * Usage : `repo.DeleteRange(new List<T> {p1, p2, p3});`  
* DeleteRange(params T[] models)  
  * Usage : `repo.DeleteRange(p1, p2, p3);`  

## Ajout de l'Update
Au niveau du `BaseModel`, ajouter une propriété `IsNew`  
Dans `BaseInMemoryRepository`, ajouter une méthode `NewId()`, qui renverra le prochain id disponible.
Dans l'interface `IBaseRepository`,  ajouter la méthode `void Update(T model)`    
Dans `BaseRepository`, ajouter la méthode `void Update(T model)` (abstraite)  
Dans `BaseInMemoryRepository`, overrider et implémenter la méthode Update  
Dans `IBaseRepository` et `BaseRepository`, ajouter les méthodes `UpdateRange` suivantes :  
* UpdateRange(IEnumerable<T> models)  
  * Usage : `repo.UpdateRange(new List<T> {p1, p2, p3});`  
* UpdateRange(params T[] models)  
  * Usage : `repo.UpdateRange(p1, p2, p3);` 

# Projet Asp.Net Core MVC

## Création du projet
Créer du dossier du projet. Depuis le dossier racine :  
`mkdir Isen.DotNet.Web && cd Isen.DotNet.Web`  
Ajouter un projet web de type MVC :   
`dotnet new mvc`  
Référencer la librairie :  
`dotnet add reference ..\Isen.DotNet.Library\Isen.DotNet.Library.csproj`  
Ajouter le projet web à la solution (depuis la racine de la solution) :  
`dotnet sln add Isen.DotNet.Web\Isen.DotNet.Web.csproj`  
Compiler / exécuter (dans le dossier du projet web) :  
`dotnet run`  
Ouvrir le navigateur : `http://localhost:5000`   

## Découvrir et nettoyer les vues
Localiser les fichiers qui correspondent aux actions Index, About et Contact de la vue home.  
Vider ces vues.   
Ecrire le nom et l'url de la vue dans chacune des 3 actions.   

## Configurer la minification js et css
A la racine du projet web, il y a un fichier bundleconfig.json  
Pour que la minification se fasse au build/run (depuis le dossier web) :  
`dotnet add package BuildBundlerMinifier`  

## Eléments de syntaxe Razor (moteur de templating)
Dans Home/Index.cshtml, créer une liste C# des URL.   
Itérer pour afficher ces URL dans la grid Bootstrap.   
Pour afficher les erreurs cshtml détaillées :  
`set ASPNETCORE_ENVIRONMENT=Development` puis `dotnet run`  
Pour revenir au mode Production :  
`set ASPNETCORE_ENVIRONMENT=Production` puis `dotnet run`  

## Vues imbriquées / injections de vues
`Views/Shared/_Layout.cshtml` est le template de toutes les pages.
Les actions sont injectées au niveau de `@RenderBody`  

### Extraire le footer
Créer dans le dossier `Views/Shared` un fichier `_Footer.cshtml`  
Localiser le contenu du footer dans `Layout` et le couper/coller vers `Footer`.  
Remplacer par un appel à ce footer avec `@Html.Partial("_Footer")`.  

### Extraire le menu
Créer dans `Views/Shared` un fichier `_Nav.cshtml`  
Identifier le html correspondant au menu bootstrap  
Déplacer vers `_Nav`  
Injecter `_Nav` dans `_Layout`  
Personnaliser le "nom" de l'appli qui fait office de Logo.  

## Vues particulières et conventions
### _Layout & ViewStart
`_Layout` n'est **pas** appelée par convention. Elle est appelée par `ViewStart.cshtml`  
`ViewStart` est appelée par convention de nommage, avant chaque chargement de vue.  
Ajouter du contenu dans `ViewStart` et voir où il tombe.  
### ViewImports
ViewImports permet de regrouper des `@using` utilisés par le C# embarqué dans les vues.  
### Layout alternatif.
Créer un nouveau fichier de Layout (dupliquer _Layout et l'appeler _LayoutEmpty.cshtml).  
Conserver la structure HTML, les imports CSS et JS, mais retirer toute la mise en forme et le menu.  
Modifier la vue About afin qu'elle utilise _LayoutEmpty.  

## Manipulation du menu Bootstrap
Dans le fichier `_Nav.cshtml`, ajouter les éléments suivants (en dropdown) :  
* Villes
  * Toutes...
  * Ajouter...
Ne pas mettre de lien pour l'instant, mettre # à la place des liens.  
Adapter un menu issu du site Bootstrap 3.3 (https://getbootstrap.com/docs/3.3/).  

# Ajout d'une vue et d'un controleur City
## Mettre à jour le menu
Dans `_Nav.cshtml`, adapter les liens à partir de ce qui est fait pour les autres.  

## Ajouter une vue cshtml pour la liste
Dans Views, créer un dossier `City`.  
Dans le dossier City, créer `Index.cshtml` (ou dupliquer le Index de Home).  

## Ajouter un contrôleur
Dans Controllers, créer une classe `CityController` et implémenter une méthode `Index` (basée sur ce que l'on a dans Home/Index).  

## Maquetter un tableau
Dans `Views/City/Index.cshtml`, créer un tableau html avec le design bootstrap.  

## Injecter les données
Dans le controller, ajouter un champ référence vers `ICityController`.  
Dans l'action Index, envoyer la liste des villes.  
Dans la vue, indiquer que le `Model` est du type `IEnumerable<City>`.  
Faire boucler la ligne du tableau sur le Model.  
Dans `Startup.cs`, méthode `ConfigureServices`, indiquer le binding entre `ICityRepository` et `InMemoryCityRepository` dans le container IoC (injection de dépendances).  

## Vue détail
Dans le controller, ajouter une méthode `Detail`.  
Créer la vue `Views/City/Detail.cshtml`.  
Coder une maquette html de formulaire d'édition.  
Dans le controller, méthode Detail(id), utiliser le repository pour récupérer la ville demandée et l'envoyer vers la vue.  
Dans la vue Detail.cshtml, préciser le type du modèle (City), et injecter les données dans le form.  
Dans le controleur, ajouter une surcharge de Detail, avec City en paramètre, passé en Http POST.  

## Logging
Dans le projet Web, classe Startup, méthode Configure :  
Injecter un provider de logging.

En cas de namespace non trouvé, depuis le projet concerné, exécuter :  
`dotnet add package Microsoft.Extensions.Logging`  

Dans le projet Library, BaseRepository :
* Ajouter un constructeur avec `ILogger<BaseRepository<T>>`
* Le stocker dans un membre protected    

Corriger les erreurs dues à l'ajout de ce constructeur : 
* Constructeur dans `BaseInMemoryRepository`
* Aussi dans `InMemoryCityRepository` 
* Aussi dans `InMemoryPersonRepository` 

Injecter aussi le logger dans CityController (Web) :  
* Ajouter le ILogger dans la signature du constructeur
* Stocker dans un membre
* L'utiliser dans Detail (POST)


# Utilisation d'une base de données

## Avant-propos
### Framework ORM (Object Relationship Mapping)
Mapping object (classes) avec enregistrements (relationnelles)
ou documents (no-sql).  
Entity Framework (EF).
Versions 1 à 6, puis EF Core 1.0, 2.0.  
EF fournit un provider SQL Server, mais aussi Oracle, MySQL, SQLite, Mongo, Cassandra...  

### Mécanisme Code-First
Code les classes (les modèles) et leurs relations.  
CodeFirst s'occupe de créer (ou mettre à jour) le schéma SQL.  

Avec EF + Code first, on n'écrit pas une seule ligne de SQL.  

### SQLite
Sera utilisé pour cette démo. Ne pas utiliser en prod (perfs, accès concurrentiels). 

## Ajout des packages NuGet nécessaires.  
Au niveau du projet Library, on va ajouter 2 packages :  
* Entity Framework
* Provider SQLite pour Entity Framework

Exécuter ces commandes :

    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Design

## Configuration
Dans `appsettings.json` (racine projet web), ajouter :  

    "ConnectionStrings": {
      "DefaultConnection": "DataSource=.\\IsenWebApp.db"
    }

## Ajouter une classe de contexte
Dans le projet Library, ajouter un fichier `data/ApplicationDbContext.cs`.  

## Injecter le service EF / SQLite
Dans `Startup` (web), injecter le service EF / SQLite
dans la méthode `ConfigureServices()`.  

## Mettre à jour et implémenter les DbRepositories

### `IBaseRepository`
* Ajouter une méthode `Save()`  
* Corriger les erreurs générées en implémentant 
  une méthode vide.   

### `BaseDbContextRepository`
Dans Library/Repositories :
* Créer un fichier `DbContext/_BaseDbContextRepository.cs`
* Ajouter un constructeur et un champ pour injecter le contexte
* Override du `ModelCollection` 
* Implémentation de `Delete`, `Update`, `Save`  

### Concrétiser les `DbContextXXXRepository`  
Hériter de `BaseDbContextRepository` pour créer :
* `DbContextCityRepository`  
* Le dupliquer pour faire `DbContextPersonRepository`  

### Modifier l'injection de dépendances
Dans Startup, changer les InMemory par des DbContext
et remettre l'injection en mode Scoped.  

## Initialiser la base de données

### Créer une classe de gestion de la base
Dans Library/data, ajouter une classe `SeedData`.  
Injecter des dépendantes dans le constructeur.  
Dans Startup, ajouter la classe SeedData elle-même au container IoC (configuration de l'injection).  

### Permettre la suppression / recréation
* Coder DropDatabase
* Coder CreateDatabase

### Injecter des données de test (d'initialisation)
* Coder AddCities
* Coder AppPersons

### Appeler les méthodes de SeedData
Dans Program, main :  
* récupérer un scope d'injection de dépendances
* récupérer une instance de SeedData
* Appeler les méthodes codées

Tester l'exécution.  
Télécharger `Sqlitebrowser` et ouvrir le fichier .db créé à la racine du projet web.  
Penser à la refermer systématiquement sinon pas de suppression ou écriture possible.  

## Controllers
### CityController
Ajouter un appel à repo.Save() dans la méthode Detail en HTTP POST.  
Implémenter l'action Delete pour la route `/City/Delete/{id}`  

### Généralisation des controllers
A côté du city controller, créer un fichier _BaseController.  
Faire une classe abstraite `BaseController<T>`.  
Faire hériter `CityController` de cette classe.  
Déplacer les membres et méthodes vers la classe de base et
adapter ce qui doit l'être.  

### PersonController + view
Dupliquer toute la logique associée au vue/controller City :
* Dupliquer l'item de menu
* Créer `PersonController`, qui hérite de `BaseController` 
* Dupliquer le dossier `Views/City` 
* Adapter la vue Index en modifiant les colonnes
* Adapter la vue Detail en complétant le formulaire

## Relations
### Inclusion de la ville d'une personne
Dans `BaseRepository`, ajouter une méthode 
virtuelle `IQueryable<T> Includes(IQueryable<T> queryable)`.  
Inclure son appel dans Single, Find, GetAll.  
Dans PersonRepository, override Includes afin d'inclure la ville.  

### Relation réciproque 
Dans la classe `City` ajouter un champ PersonCollection.  
Ajouter un getter pour PersonCount.  
Inclure cette relation en overridant Includes dans CityRepository.  
Dans la vue des villes (liste), ajouter la colonne pour afficher le nombre de personnes d'une ville.  

### Gestion des conflits d'intégrité référentielle lors d'un delete
Si on efface une ville référencée par des personnes, 3 stratégies possibles :
* Effacer les personnes associées (CASCADE DELETE)
* Ne rien faire et empêcher l'effacement
* Mettre la relation à null (Set Null)  

Dans `ApplicationDbContext` et préciser, pour les 2 entités :
* les relations
* la stratégie au delete d'une ville  

Ajouter le champ Person.CityId qui sert de clé étrangère
à la relation Person.City.  

### Pouvoir affecter la ville d'une personne
Dans le formulaire de détail d'une personne,
ajouter une liste déroulante, contenant toutes les villes,
et permettant donc d'affecter la ville à la personne.

# API RESTful (ou genre)
## Objectif
Mettre en place un schémas de routes (la forme des URL, systématique) et un ensemble de méthodes, qui permettront de réaliser les opérations CRUD.
La combinaison des routes, et des verbes HTTP (GET, POST, PUT, DELETE) permettant de réaliser toutes les opérations CRUD sans aucune adhérence entre le "serveur" et le "client".

Pour les Villes, nous allons créer l'API suivante :  

* GET `/api/city` Renvoie toutes les villes
* GET `/api/city/{id}` Renvoie la ville ayant l'id `{id}`  
* POST `/api/city` Crée la ville passée dans le body de la                          requête HTTP POST
* PUT `/api/city/{id}` Met à jour la ville ayant l'id `{id}`
                       avec les données passées dans le body
                       de la requête HTTP PUT
* DELETE `/api/city/{id}` Efface la ville d'id `{id}`

Et idem pour les personnes : 

Pour les Villes, nous allons créer l'API suivante :
* GET `/api/person` Renvoie toutes les personnes
* GET `/api/person/{id}` Renvoie la person ayant l'id `{id}`  
* POST `/api/person` Crée la person passée dans le body de la                          requête HTTP POST
* PUT `/api/person/{id}` Met à jour la person ayant l'id `{id}`
                       avec les données passées dans le body
                       de la requête HTTP PUT
* DELETE `/api/person/{id}` Efface la person d'id `{id}`

## Implémentation
### Controleur API de Base
Ajouter le qualifier `partial` à la classe BaseController.  
Créer une classe partielle `BaseApiController` en dupliquant puis vidant BaseController.  

### GetAll

Dans BaseApiController, ajouter une méthode GetAll, qui renvoie tout, mais en JSON

Pour éviter le problème de références cycliques dans la sérialisation JSON, configurer les options du sérialiseur dans Startup.cs.

Naviguer vers http://localhost:5000/api/city (sur Chrome, utiliser l'extension JSON Formatter).  

### Contrôle de la sérialisation
#### /api/hello
Cette api renvoie "hello" et l'heure sur le serveur.    
Utiliser le type `dynamic` et la classe `ExpandoObject`.  

#### Optimiser le schéma json
Au niveau du `BaseModel` :  
* Ajouter une méthode (virtuelle) `ToDynamic`, qui renvoie un dynamic avec uniquement `id` et `name`.  
* Override de `ToDynamic` dans `Person` et `City` pour ajouter les champs manquants.  

### GetById
Coder dans `BaseApiController` la méthode GetById.  
Configurer son schéma de route : `api/[controller]/{id}`.  

### Create
Correspond à cette méthode : POST `/api/person`  
Ajouter une méthode Create pour créer un nouvel objet.
Cet objet est passé dans le corps de la requête HTTP, en POST.
Renvoyer l'objet créé.  
Télécharger et utiliser Postman pour tester des requêtes POST.  

### Update
Sur le même principe, implémenter l'update :
* id de l'entité en GET
* entité dans le corprs, en PUT
* Renvoyer l'entité
* Ne rien faire si l'id en GET ne correspond pas à l'id dans le corps

### Delete
Implémenter en utilisant le verbe HttpDelete.  
Utiliser le nom de méthode Remove (car Delete est déjà pris par l'API Web).  
Renvoyer HTTP 204 si c'est ok.  
