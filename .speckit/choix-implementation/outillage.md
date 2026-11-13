---
type: Choix d'implémentation
title: 'Outillage de développement'
description: 'Outillage .NET : xunit, CI sur exécuteur Windows, tests du dépôt sans l''application, couverture et analyse SonarQube Cloud ; à lire pour tout test, build ou workflow.'
applies_to:
  - '.github/workflows/ci.yml'
  - 'tests/**'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Outillage de développement

- **xunit** pour tous les tests, outil de test de référence de l'écosystème .NET.
- **CI sur un exécuteur Windows** (`windows-latest`) : seule plateforme où l'application, qui cible `net10.0-windows`, se compile et ses tests s'exécutent. Les tests du dépôt, qui n'en dépendent pas, s'y exécutent aussi : un seul exécuteur, un seul job, une seule exécution des tests par push ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md)).
  - **Alternative écartée (tests du dépôt sur un exécuteur Linux, dans un job à part)** : plus rapide à démarrer, mais un check requis de plus, pour des tests de quelques secondes.
- **Sources modifiées** : le job détermine lui-même, par `git diff` sur la base de la Pull Request, si elle modifie une source ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md), sources) ; sans source modifiée, il n'exécute que les tests du dépôt, sans compiler l'application.
- **Couverture et analyse SonarQube Cloud** : mises en place par leur chantier de la Phase 0 ([`suivi-implementation/`](../suivi-implementation/index.md)), avec l'analyseur `dotnet-sonarscanner` en outil local et la couverture des tests collectée par `coverlet` au format OpenCover, dans le même job que les tests.
- **Contrôle qualité hebdomadaire** : son outillage (tests de mutation, règles de style relevées, API de SonarQube Cloud) est choisi par son chantier de la Phase 1, sur le modèle décrit par [Contrôle qualité hebdomadaire](../gestion-projet/depot/controle-qualite-hebdomadaire.md).
