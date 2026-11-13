---
type: Règle métier
title: 'Journal de l''application'
description: 'Le journal de l''application reste sur le poste de l''utilisateur : aucun journal ni aucune donnée de diagnostic n''est envoyé à un service distant ; à lire pour tout chantier qui touche au journal ou aux échanges de l''application avec l''extérieur.'
applies_to:
  - 'TVRename/App/ApplicationBase.cs'
  - 'TVRename/NLog.config'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T20:30:00Z }
---

# Journal de l'application

L'application consigne son activité et ses erreurs dans un **journal local**, sur le poste de l'utilisateur : un fichier `TVRename.log`, dans le dossier `log` de son dossier de données, archivé chaque jour et à chaque lancement (les 20 dernières archives sont conservées).

**Aucun journal, ni aucune donnée de diagnostic** (erreur, plantage, statistique d'usage), **n'est envoyé à un service distant**, qu'une option le permette ou non (exigence de l'utilisateur) : les messages du journal peuvent contenir les chemins de la bibliothèque de l'utilisateur et les noms de ses séries et de ses films. L'utilisateur qui veut signaler un problème transmet lui-même son journal.
