# TV Rename — développement

Ce document s'adresse à qui maintient le projet ; pour découvrir, installer ou utiliser l'application, voir [README.md](README.md).

## Un fork détaché, piloté par sa spécification

Ce dépôt est un fork détaché de [TV-Rename/tvrename](https://github.com/TV-Rename/tvrename), maintenu et amélioré par étapes, sans réécriture. Le projet est mené en **développement piloté par la spécification** : le dossier [`.speckit/`](.speckit/index.md) est la source de vérité du projet (gouvernance, contraintes, choix techniques, fonctionnalités, plan), et [`AGENTS.md`](AGENTS.md) son point d'entrée pour les agents IA.

Le plan d'implémentation est dans [`.speckit/suivi-implementation/`](.speckit/suivi-implementation/index.md) : mise en place, nettoyage du dépôt et du CI, nettoyage du code, sécurité et montées de version, puis corrections et nouvelles fonctionnalités.

## Organisation du code

| Dossier | Contenu |
| --- | --- |
| `TVRename/` | L'application (Windows Forms, `net10.0-windows`). |
| `tests/TVRename.Tests/` | Tests de l'application (xunit), dont la couverture est mesurée par l'analyse SonarQube Cloud. |
| `tests/TVRename.Repository.Tests/` | Tests du dépôt : structure du `.speckit/`, table des moments d'`AGENTS.md`. |
| `scripts/` | Outillage du dépôt (ex. `Get-SpeckitConcepts.ps1`, concepts du `.speckit/` qui régissent les fichiers modifiés d'une branche). |
| `.github/workflows/` | CI de non-régression et analyse SonarQube Cloud (`ci.yml`), construction de l'installeur à la publication d'une release, veille mensuelle de l'amont (`upstream-watch.yml`), qui liste dans un ticket de suivi ce que l'amont a apporté depuis le dernier commit analysé. |
| `dotnet-tools.json` | Outils .NET locaux (`dotnet tool restore`), dont l'analyseur de SonarQube Cloud. |

## Développement local

Prérequis : Windows, SDK .NET 10.

```powershell
dotnet build TVRename.slnx --configuration Release
dotnet test TVRename.slnx --configuration Release
```

Pour lancer l'application depuis les sources sans toucher aux réglages et aux caches d'une installation existante, lui donner un dossier de données à part :

```powershell
dotnet run --project TVRename -- /userfilepath:$env:TEMP\tvrename-dev
```

## Contribuer

Toute modification passe par une Pull Request vers `main` (squash merge, commits signés, [Conventional Commits](https://www.conventionalcommits.org/)) ; les règles complètes sont dans [`.speckit/gestion-projet/`](.speckit/gestion-projet/index.md).
