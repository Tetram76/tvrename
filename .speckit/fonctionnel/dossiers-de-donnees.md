---
type: Règle métier
title: 'Dossiers de données'
description: 'Où l''application range ce qu''elle écrit pour l''utilisateur (réglages, caches, journaux, cache du navigateur intégré, audit), options /userfilepath: et /appdatapath: qui les déplacent, mono-instance par dossier ; à lire pour tout chantier qui touche à ces fichiers ou à la ligne de commande.'
applies_to:
  - 'TVRename/App/CommandLineArgs.cs'
  - 'TVRename/App/Program.cs'
  - 'TVRename/App/SingleInstanceService.cs'
  - 'TVRename/TVRename/PathManager.cs'
  - 'TVRename/NLog.config'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T18:30:00Z }
---

# Dossiers de données

L'application écrit, pour l'utilisateur, deux ensembles de fichiers, chacun dans son dossier :

| Ensemble | Contenu | Dossier par défaut | Option qui le déplace |
| --- | --- | --- | --- |
| **Réglages et caches de la bibliothèque** | réglages, caches des sources de données (TheTVDB, TMDB, TVmaze), état, disposition des fenêtres, statistiques, et leurs copies de sauvegarde | `%APPDATA%\TVRename\TVRename\2.1` | `/userfilepath:<dossier>` |
| **Données d'exécution** | journaux de l'application, cache et journal du navigateur intégré, journal d'audit des mises à jour de TheTVDB | `%APPDATA%\TVRename` | `/appdatapath:<dossier>` |

- **`/userfilepath:<dossier>`** garde son usage habituel : ranger ailleurs les réglages et les caches de la bibliothèque. Il ne déplace rien d'autre (décision de l'utilisateur).
- **`/appdatapath:<dossier>`** déplace les données d'exécution (exigence de l'utilisateur). Un dossier relatif l'est au dossier courant au lancement ; il est créé s'il n'existe pas.
- Les deux options sont indépendantes : chacune ne déplace que son ensemble. Ensemble, elles font une instance qui ne lit ni n'écrit rien des dossiers de l'installation de l'utilisateur ; c'est ainsi que l'environnement de validation lance l'application ([Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md)).

## Une seule instance par dossier de données d'exécution

L'application est **mono-instance** : lancée alors qu'une instance tourne déjà, elle transmet à celle-ci ses arguments (à défaut, la demande de passer au premier plan) et s'arrête.

- **Sans `/appdatapath:`**, une seule instance tourne sur le poste, comme dans l'application existante.
- **Avec `/appdatapath:`**, la règle ne vaut que pour les instances lancées avec le même dossier (exigence de l'utilisateur) : une telle instance tourne à côté d'une instance lancée sans l'option, ou avec un autre dossier, et ne leur transmet jamais ses arguments ; elle ne reçoit que ceux d'une instance lancée avec le même dossier.
