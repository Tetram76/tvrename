---
type: Choix d'implémentation
title: 'Environnement de validation fonctionnelle'
description: 'Version d''une Pull Request compilée depuis ses seules sources et lancée par un script PowerShell, avec ses propres dossiers de données, à côté de l''installation de l''utilisateur ; à lire pour modifier les scripts de validation ou ce qui isole une instance.'
applies_to:
  - 'scripts/Start-Validation.ps1'
  - 'scripts/Stop-Validation.ps1'
  - 'TVRename/App/SingleInstanceService.cs'
  - 'TVRename/NLog.config'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T18:30:00Z }
---

# Environnement de validation fonctionnelle

Ce que l'environnement garantit à l'utilisateur est dans [Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md), ce que l'application isole dans [Dossiers de données](../fonctionnel/dossiers-de-donnees.md). Ce concept dit comment.

- **Deux scripts PowerShell 7** (`scripts/Start-Validation.ps1`, `scripts/Stop-Validation.ps1`) : l'un compile et lance la version d'une Pull Request (son numéro en paramètre), l'autre l'arrête et retire tout ce que le premier a créé.
- **Les sources du seul commit validé**, extraites par `git archive` dans un dossier temporaire (`%TEMP%\tvrename-validation`), puis compilées par le SDK .NET du poste : ni les modifications en cours du clone de l'utilisateur, ni sa branche, ni ses autres fichiers n'entrent dans ce qui est validé, et son clone n'est pas modifié (seul `git fetch` y ajoute le commit de la Pull Request).
  - **Alternative écartée (`git worktree`)** : elle laisse dans le clone de l'utilisateur une entrée de worktree et une branche à nettoyer.
  - **Alternative écartée (version compilée par le CI, téléchargée)** : le CI ne publie aucune version compilée ; il faudrait l'y ajouter, avec un artefact de plusieurs centaines de mégaoctets à chaque push, et une authentification de `gh` sur le poste de l'utilisateur.
- **Dossiers de données repartis de zéro à chaque lancement** : la validation porte sur ce que la Pull Request change, pas sur ce qu'une validation précédente a laissé ; `-KeepData` les conserve d'un lancement à l'autre, pour reprendre un cas déjà préparé.
- **Dossier des journaux transmis à NLog par son contexte global** (`GlobalDiagnosticsContext`), lu par `NLog.config` avec le dossier habituel pour valeur par défaut : le dossier n'est connu qu'au lancement, après le chargement de la configuration, et la documentation de NLog recommande ce contexte, plutôt qu'une variable de configuration, pour une valeur simple fixée par le code.
  - **Alternative écartée (variable de configuration `${var:…}`)** : la documentation de NLog la déconseille pour une valeur fixée à l'exécution (comportement inattendu au rechargement de la configuration).
- **Mono-instance par dossier de données d'exécution** : avec `/appdatapath:`, le nom du mutex et du sémaphore de l'application porte une empreinte du dossier, et l'instance attend les arguments des suivantes sur un port que le système lui attribue, écrit dans ce dossier, où les suivantes le lisent. Sans l'option, noms et port fixe (19191) sont inchangés.
  - **Alternative écartée (port fixe tiré de l'empreinte du dossier)** : un port déjà pris par un autre programme rendrait la transmission impossible, sans recours.
  - **Alternative écartée (canal nommé, *named pipe*, pour toute instance)** : il changerait le canal de l'instance lancée sans l'option, que l'application existante emploie et qui reste inchangé.
