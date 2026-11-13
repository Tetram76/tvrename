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
| `tests/TVRename.Repository.Tests/` | Tests du dépôt : structure du `.speckit/`, table des moments d'`AGENTS.md`, scripts de l'outillage. |
| `scripts/` | Outillage du dépôt : `Get-SpeckitConcepts.ps1` (concepts du `.speckit/` qui régissent les fichiers modifiés d'une branche), lancement et arrêt de la validation fonctionnelle (`Start-Validation.ps1`, `Stop-Validation.ps1`), tenue du ticket de suivi du contrôle qualité hebdomadaire (`Update-QualityAuditIssue.ps1`). |
| `.github/workflows/` | CI de non-régression et analyse SonarQube Cloud (`ci.yml`), construction de l'installeur à la publication d'une release (`dotnet-desktop.yml`), veille mensuelle de l'amont (`upstream-watch.yml`), qui tient dans un ticket de suivi les évolutions de l'amont que l'agent n'a pas encore traitées, contrôle qualité hebdomadaire de `main` (`quality-audit.yml`), qui tient dans un ticket de suivi la dette qualité du code (SonarQube Cloud, règles de style, tests de mutation). |
| `dotnet-tools.json` | Outils .NET locaux (`dotnet tool restore`) : l'analyseur de SonarQube Cloud et Stryker.NET, des tests de mutation. |

## Développement local

Prérequis : Windows, SDK .NET 10, [PowerShell 7.3](https://learn.microsoft.com/powershell/scripting/install/installing-powershell) ou plus récent (`pwsh`), qu'exécutent les tests des scripts de l'outillage.

```powershell
dotnet build TVRename.slnx --configuration Release
dotnet test TVRename.slnx --configuration Release
```

Pour lancer l'application depuis les sources sans toucher aux réglages, aux caches ni aux journaux d'une installation existante, et à côté d'elle si elle tourne, la compiler en version de validation : tous ses fichiers vont alors dans `%TEMP%\TVRename` ([Environnement de validation fonctionnelle](.speckit/choix-implementation/environnement-de-validation-fonctionnelle.md)).

```powershell
dotnet build TVRename/TVRename.csproj -p:ValidationBuild=true --configuration Release
./TVRename/bin/Release/net10.0-windows/TVRename.exe
```

## Validation fonctionnelle

Ce qu'une Pull Request change de visible est validé sur le poste Windows de l'utilisateur, avant sa revue, avec la version de la Pull Request ([Validation fonctionnelle](.speckit/gestion-projet/revue-tickets/validation-fonctionnelle.md)).

Prérequis : [PowerShell 7.3](https://learn.microsoft.com/powershell/scripting/install/installing-powershell) ou plus récent (`pwsh`), git avec ce dépôt cloné, le SDK .NET 10. Les scripts sont ceux de la branche du clone : le mettre à jour (`git pull` sur `main`) avant une validation.

Depuis la racine du dépôt, avec le numéro de la Pull Request :

```powershell
pwsh ./scripts/Start-Validation.ps1 -PullRequest 18
```

Le script récupère le commit de tête de la Pull Request, en compile les seules sources en version de validation dans `%TEMP%\tvrename-validation`, sans rien changer au clone hormis ce `git fetch`, puis la lance. Elle range tous ses fichiers dans `%TEMP%\TVRename`, vidé à chaque lancement (`-KeepData` garde celui de la validation précédente), et tourne à côté d'un TV Rename déjà lancé, dont elle ne lit ni n'écrit aucun fichier. Relancer le script arrête d'abord la version précédente.

Pour l'arrêter et retirer tout ce qu'il a créé (sources, compilation, dossier de données) :

```powershell
pwsh ./scripts/Stop-Validation.ps1
```

## Publication d'une version

Une version est publiée quand tous les tickets de son jalon sont fermés : tag `v<version>` annoté et signé sur `main`, puis release GitHub sur ce tag ([Versions et releases](.speckit/gestion-projet/depot/versions-et-releases.md)). La publication de la release déclenche `dotnet-desktop.yml`, qui construit l'installeur, `TVRename-v<version>-Installer.exe`, et le joint à la release.

Pour construire l'installeur sur le poste, avec [NSIS](https://nsis.sourceforge.io) 3 (`makensis`) :

```powershell
dotnet publish TVRename/TVRename.csproj --configuration Release -p:Version=10.0.0
makensis /DVERSION=10.0.0 /DTAG=v10.0.0 Installer.nsi
```

## Contribuer

Toute modification passe par une Pull Request vers `main` (squash merge, commits signés, [Conventional Commits](https://www.conventionalcommits.org/)) ; les règles complètes sont dans [`.speckit/gestion-projet/`](.speckit/gestion-projet/index.md).
