---
type: Choix d'implémentation
title: 'Environnement de validation fonctionnelle'
description: 'Version d''une Pull Request compilée depuis ses seules sources en version de validation (constante VALIDATION), dossier de données dans le dossier temporaire et instance à part, lancée et arrêtée par deux scripts PowerShell ; à lire pour modifier les scripts de validation ou la version de validation.'
applies_to:
  - 'scripts/Start-Validation.ps1'
  - 'scripts/Stop-Validation.ps1'
  - 'TVRename/App/Program.cs'
  - 'TVRename/App/SingleInstanceService.cs'
  - 'TVRename/TVRename/PathManager.cs'
  - 'TVRename/NLog.config'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T21:40:00Z }
---

# Environnement de validation fonctionnelle

Ce que l'environnement garantit à l'utilisateur est dans [Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md). Ce concept dit comment.

- **Une version de validation, distincte de l'application livrée** : compilée avec la constante `VALIDATION` (propriété `ValidationBuild` du projet), elle range tous ses fichiers (réglages, caches, journaux, cache et journal du navigateur intégré, audit) dans `%TEMP%\TVRename` au lieu de `%APPDATA%\TVRename`, et forme une instance à part, qui tourne à côté du TV Rename du poste sans jamais lui transmettre ses arguments ni recevoir les siens. L'application livrée, compilée sans la constante, n'en a rien : ni option, ni comportement nouveau.
  - **Alternative écartée (option de la ligne de commande)** : elle ajouterait à l'application livrée une option sans usage pour ses utilisateurs, à documenter et à maintenir comme une fonctionnalité.
  - **Alternative écartée (`/userfilepath:` seule)** : elle ne déplace que les réglages et les caches de la bibliothèque, pas les journaux, le cache du navigateur intégré ni l'audit, et ne sépare pas l'instance de celle du poste ; elle garde son usage ordinaire (décision de l'utilisateur).
- **Seule l'activation dépend de la constante** : le dossier de données déplacé et l'instance à part sont compilés dans toutes les versions et couverts par les tests ; la constante ne fait que les activer au démarrage.
- **Instance à part** : le nom de son mutex et de son sémaphore porte le nom de l'instance (`validation`), et elle attend les arguments d'une instance suivante sur un port que le système lui attribue, écrit dans son dossier de données, où la suivante le lit. L'instance du poste garde ses noms et son port fixe (19191).
  - **Alternative écartée (autre port fixe)** : un port déjà pris par un autre programme rendrait la transmission impossible, sans recours.
  - **Alternative écartée (canal nommé, *named pipe*, pour toute instance)** : il changerait le canal de l'application livrée.
- **Dossier des journaux transmis à NLog par son contexte global** (`GlobalDiagnosticsContext`), lu par `NLog.config` avec le dossier habituel pour valeur par défaut : le dossier n'est connu qu'au démarrage, après le chargement de la configuration, et la documentation de NLog recommande ce contexte pour une valeur simple fixée par le code.
  - **Alternative écartée (variable de configuration `${var:…}`)** : la documentation de NLog la déconseille pour une valeur fixée à l'exécution (comportement inattendu au rechargement de la configuration).
- **Deux scripts PowerShell 7** (`scripts/Start-Validation.ps1`, `scripts/Stop-Validation.ps1`) : l'un compile et lance la version de validation d'une Pull Request (son numéro en paramètre), l'autre l'arrête et retire tout ce que le premier a créé.
- **Les sources du seul commit validé**, extraites par `git archive` dans `%TEMP%\tvrename-validation`, puis compilées par le SDK .NET du poste : ni les modifications en cours du clone de l'utilisateur, ni sa branche, ni ses autres fichiers n'entrent dans ce qui est validé, et son clone n'est pas modifié (seul `git fetch` y ajoute le commit de la Pull Request).
  - **Alternative écartée (`git worktree`)** : elle laisse dans le clone de l'utilisateur une entrée de worktree et une branche à nettoyer.
  - **Alternative écartée (version compilée par le CI, téléchargée)** : le CI ne publie aucune version compilée ; il faudrait l'y ajouter, avec un artefact de plusieurs centaines de mégaoctets à chaque push, et une authentification de `gh` sur le poste de l'utilisateur.
- **Dossier de données reparti de zéro à chaque lancement** : la validation porte sur ce que la Pull Request change, pas sur ce qu'une validation précédente a laissé ; `-KeepData` le conserve d'un lancement à l'autre, pour reprendre un cas déjà préparé.
