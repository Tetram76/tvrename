---
type: Choix d'implémentation
title: 'Outillage de développement'
description: 'Outillage .NET : xunit, CI sur exécuteur Windows, tests du dépôt sans l''application, couverture et analyse SonarQube Cloud ; à lire pour tout test, build ou workflow.'
applies_to:
  - '.github/workflows/ci.yml'
  - 'tests/**'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T18:00:00Z }
---

# Outillage de développement

- **xunit** pour tous les tests, outil de test de référence de l'écosystème .NET.
- **CI sur un exécuteur Windows** (`windows-latest`) : seule plateforme où l'application, qui cible `net10.0-windows`, se compile et ses tests s'exécutent. Les tests du dépôt, qui n'en dépendent pas, s'y exécutent aussi : un seul exécuteur, un seul job, une seule exécution des tests par push ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md)).
  - **Alternative écartée (tests du dépôt sur un exécuteur Linux, dans un job à part)** : plus rapide à démarrer, mais un check requis de plus, pour des tests de quelques secondes.
- **Sources modifiées** : le job détermine lui-même, par `git diff` sur la base de la Pull Request, si elle modifie une source ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md), sources) ; sans source modifiée, il n'exécute que les tests du dépôt, sans compiler l'application.
- **Couverture et analyse SonarQube Cloud** : l'analyseur `dotnet-sonarscanner` en outil local (`dotnet-tools.json`) et la couverture des tests collectée par `coverlet` au format OpenCover, dans le même job que les tests, après eux. L'analyseur désactive `TreatWarningsAsErrors` dans la construction qu'il observe (documentation de SonarScanner for .NET) : il observe donc une seconde construction, complète, après la construction stricte et les tests, sans tenir lieu ni de l'une ni des autres. Ses étapes se poursuivent après un échec, pour qu'une analyse qui n'aboutit pas ne fasse jamais échouer la non-régression ([Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md)).
  - **Alternative écartée (analyse observant l'unique construction)** : la construction de la non-régression perdrait `TreatWarningsAsErrors`, et un échec de l'analyse pourrait l'interrompre.
  - **Alternative écartée (analyse dans un job à part)** : un exécuteur Windows de plus à démarrer, un dépôt à extraire et des packages à restaurer de nouveau, et la couverture à transmettre d'un job à l'autre, pour éviter une construction de moins d'une minute.
- **Contrôle qualité hebdomadaire** : son outillage (tests de mutation, règles de style relevées, API de SonarQube Cloud) est choisi par son chantier de la Phase 2, sur le modèle décrit par [Contrôle qualité hebdomadaire](../gestion-projet/depot/controle-qualite-hebdomadaire.md).
