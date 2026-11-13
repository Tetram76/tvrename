---
type: Gouvernance
title: 'Repository'
description: 'Dépôt GitHub et services rattachés, gérés par l''agent via leurs API ; documentation du dépôt (README.md, README.dev.md).'
tags: [socle]
applies_to:
  - 'README.md'
  - 'README.dev.md'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Repository

- **Hébergement** : GitHub
- **Nom du dépôt** : `Tetram76/tvrename`, fork détaché de `TV-Rename/tvrename` ([Application existante](../../contraintes-techniques/application-existante.md))
- **URL** : <https://github.com/Tetram76/tvrename>
- La **gestion du repository GitHub** (configuration, branches, protections, sécurité, CI/CD, etc.) relève du périmètre de l'agent et doit être maintenue conformément aux contraintes du projet. Il en va de même des **services externes rattachés au dépôt** : paramétrage du projet SonarQube Cloud et marquage de ses problèmes ([Analyse de la qualité du code](analyse-de-la-qualite-du-code.md)).
  - L'agent accomplit lui-même ces actes ([Prise de décision](../gouvernance/prise-de-decision.md), acte de gestion du projet), et ne les laisse jamais à l'utilisateur comme une tâche à faire de son côté. Il passe par l'**API** de chaque service, avec l'accès que l'utilisateur a donné au poste : `gh` pour GitHub ; pour SonarQube Cloud, un jeton personnel de l'utilisateur placé dans la variable d'environnement `SONAR_TOKEN` du poste. L'agent agit ainsi au nom de l'utilisateur, comme sur GitHub. Il n'agit jamais par la session de l'utilisateur dans un navigateur.
  - Seul ce qui exige l'utilisateur en personne lui revient : se connecter à un service, créer un secret ou un jeton et en placer la valeur (secret de dépôt, variable d'environnement du poste), activer la revue Codex sur le dépôt, qu'aucun agent ne lit ni ne manipule.
  - Un acte que l'outillage de l'agent lui refuse (ex. une commande bloquée par le mode automatique de Claude Code) n'est pas contourné : l'agent nomme l'acte refusé, la commande et la raison du refus, et demande à l'utilisateur de l'autoriser.
- **Documentation du dépôt**, en deux fichiers à la racine, selon leur lecteur :
  - `README.md`, en anglais : pour qui découvre l'application, l'installe ou l'utilise — présentation, fonctionnalités, installation, indicateurs de qualité en tête ([Analyse de la qualité du code](analyse-de-la-qualite-du-code.md)) ;
  - `README.dev.md`, en français : pour qui maintient le projet — spécifications et démarche pilotée par elles, organisation du code, développement local (build, tests, lancement depuis les sources), validation fonctionnelle, publication d'une version.

  Chacun renvoie à l'autre ; une information ne figure que dans celui de son lecteur.
