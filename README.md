# P7CreateRestApi

## Présentation
P7CreateRestApi est une API REST développée en .NET (ciblant .NET 6 et .NET 8) permettant la gestion de plusieurs entités métier telles que User, BidList, Curve, Rating, RuleName et Trade. Ce projet est conçu pour servir de backend à des applications nécessitant des opérations CRUD sur ces entités.

## Fonctionnalités principales
- Gestion des utilisateurs (User)
- Gestion des listes d'enchères (BidList)
- Gestion des courbes (Curve)
- Gestion des notations (Rating)
- Gestion des règles (RuleName)
- Gestion des transactions (Trade)

## Prérequis
- .NET 6 ou .NET 8 SDK installé
- Visual Studio 2022 recommandé

## Installation
1. Cloner le dépôt : git clone https://github.com/AlexLeball/Back-end.NET_API_REST.git
2. Ouvrir la solution dans Visual Studio 2022.
3. Restaurer les packages NuGet si nécessaire.

## Exécution
1. Sélectionner le projet `P7CreateRestApi` comme projet de démarrage.
2. Lancer l'application avec F5 ou via la ligne de commande : dotnet run --project P7CreateRestApi
3. L'API sera accessible par défaut sur `https://localhost:5001` ou `http://localhost:5000`.

## Structure des contrôleurs
- `UserController` : Gère les opérations CRUD sur les utilisateurs.
- `BidListController` : Gère les listes d'enchères.
- `CurveController` : Gère les courbes de données.
- `HomeController` : Point d'entrée par défaut de l'API.
- `RatingController` : Gère les notations.
- `RuleNameController` : Gère les règles métier.
- `TradeController` : Gère les transactions.

## Tests
Des tests unitaires sont disponibles dans le projet `TestProject7`.

## Configuration des secrets
Les informations sensibles peuvent être stockées dans le fichier `secrets.json` (ne pas versionner ce fichier).
