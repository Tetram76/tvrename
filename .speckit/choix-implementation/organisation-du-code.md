---
type: Choix d'implémentation
title: 'Organisation du code'
description: 'Projets de la solution et leur rôle, projets de tests ; à lire pour ajouter un projet ou placer du code.'
applies_to:
  - 'TVRename.slnx'
  - 'TVRename/TVRename.csproj'
  - 'tests/*/*.csproj'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Organisation du code

| Projet | Rôle |
| --- | --- |
| `TVRename` | L'application héritée du fork, en un seul projet : interface Windows Forms, modèle, sources de données, analyse de la bibliothèque et actions. Son découpage éventuel en projets par responsabilité est une décision à prendre au fil du nettoyage, quand un chantier en montre le besoin, et non un préalable. |
| `tests/TVRename.Repository.Tests` | Tests du dépôt : confrontent ses fichiers à leurs règles (structure du `.speckit/`, table des moments d'`AGENTS.md`), sans référence à l'application. |

Les tests de l'application vivent sous `tests/`, dans un projet miroir de celui qu'ils testent (ex. `tests/TVRename.Tests`), créé par le premier chantier qui en a besoin ([Qualité du code](../gestion-projet/gouvernance/qualite-du-code.md), non-régression).

- **Tests du dépôt séparés des tests de l'application** : ils ciblent `net10.0`, sans Windows Forms, et s'exécutent seuls, sans compiler l'application, pour une Pull Request qui ne modifie que la documentation ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md)).
  - **Alternative écartée (tests du dépôt dans le projet des tests de l'application)** : toute PR de documentation compilerait l'application et ses dépendances pour quelques secondes de vérification.
