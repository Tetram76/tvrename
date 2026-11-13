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
| `scripts/` | Outillage du dépôt : `Get-SpeckitConcepts.ps1` (concepts du `.speckit/` qui régissent les fichiers modifiés d'une branche), lancement et arrêt de la validation fonctionnelle (`Start-Validation.ps1`, `Stop-Validation.ps1`). |
| `.github/workflows/` | CI de non-régression et analyse SonarQube Cloud (`ci.yml`), construction de l'installeur à la publication d'une release, veille mensuelle de l'amont (`upstream-watch.yml`), qui tient dans un ticket de suivi les évolutions de l'amont que l'agent n'a pas encore traitées. |
| `dotnet-tools.json` | Outils .NET locaux (`dotnet tool restore`), dont l'analyseur de SonarQube Cloud. |

## Développement local

Prérequis : Windows, SDK .NET 10.

```powershell
dotnet build TVRename.slnx --configuration Release
dotnet test TVRename.slnx --configuration Release
```

Pour lancer l'application depuis les sources sans toucher aux réglages, aux caches ni aux journaux d'une installation existante, et à côté d'elle si elle tourne, lui donner ses propres dossiers de données ([Dossiers de données](.speckit/fonctionnel/dossiers-de-donnees.md)) : `/userfilepath:` pour les réglages et les caches de la bibliothèque, `/appdatapath:` pour les journaux, le cache du navigateur intégré et l'audit.

```powershell
dotnet run --project TVRename -- /userfilepath:$env:TEMP\tvrename-dev\userfiles /appdatapath:$env:TEMP\tvrename-dev\appdata
```

## Validation fonctionnelle

Ce qu'une Pull Request change de visible est validé sur le poste Windows de l'utilisateur, avant sa revue, avec la version de la Pull Request ([Validation fonctionnelle](.speckit/gestion-projet/revue-tickets/validation-fonctionnelle.md)).

Prérequis : [PowerShell 7.3](https://learn.microsoft.com/powershell/scripting/install/installing-powershell) ou plus récent (`pwsh`), git avec ce dépôt cloné, le SDK .NET 10. Les scripts sont ceux de la branche du clone : le mettre à jour (`git pull` sur `main`) avant une validation.

Depuis la racine du dépôt, avec le numéro de la Pull Request :

```powershell
pwsh ./scripts/Start-Validation.ps1 -PullRequest 18
```

Le script récupère le commit de tête de la Pull Request, en compile les seules sources dans `%TEMP%\tvrename-validation`, sans rien changer au clone hormis ce `git fetch`, puis lance l'application avec ses propres dossiers de données, vides à chaque lancement (`-KeepData` garde ceux de la validation précédente). Elle tourne à côté d'un TV Rename déjà lancé, dont elle ne lit ni n'écrit aucun fichier. Relancer le script arrête d'abord la version précédente.

Pour l'arrêter et retirer tout ce qu'il a créé (sources, compilation, dossiers de données) :

```powershell
pwsh ./scripts/Stop-Validation.ps1
```

## Contribuer

Toute modification passe par une Pull Request vers `main` (squash merge, commits signés, [Conventional Commits](https://www.conventionalcommits.org/)) ; les règles complètes sont dans [`.speckit/gestion-projet/`](.speckit/gestion-projet/index.md).
