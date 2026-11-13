---
type: Choix d'implémentation
title: 'Outillage de développement'
description: 'Outillage .NET : xunit, CI sur exécuteur Windows, tests du dépôt sans l''application, couverture et analyse SonarQube Cloud, outillage du contrôle qualité hebdomadaire (Stryker.NET, règles de style, API de SonarQube Cloud) ; à lire pour tout test, build ou workflow.'
applies_to:
  - '.github/workflows/ci.yml'
  - 'tests/**'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T22:15:00Z }
---

# Outillage de développement

- **xunit** pour tous les tests, outil de test de référence de l'écosystème .NET.
- **CI sur un exécuteur Windows** (`windows-latest`) : seule plateforme où l'application, qui cible `net10.0-windows`, se compile et ses tests s'exécutent. Les tests du dépôt, qui n'en dépendent pas, s'y exécutent aussi : un seul exécuteur, un seul job, une seule exécution des tests par push ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md)).
  - **Alternative écartée (tests du dépôt sur un exécuteur Linux, dans un job à part)** : plus rapide à démarrer, mais un check requis de plus, pour des tests de quelques secondes.
- **Sources modifiées** : le job détermine lui-même, par `git diff` sur la base de la Pull Request, si elle modifie une source ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md), sources) ; sans source modifiée, il n'exécute que les tests du dépôt, sans compiler l'application.
- **Couverture et analyse SonarQube Cloud** : l'analyseur `dotnet-sonarscanner` en outil local (`dotnet-tools.json`) et la couverture des tests collectée par `coverlet` au format OpenCover, dans le même job que les tests, après eux. L'analyseur désactive `TreatWarningsAsErrors` dans la construction qu'il observe (documentation de SonarScanner for .NET) : il observe donc une seconde construction, complète, après la construction stricte et les tests, sans tenir lieu ni de l'une ni des autres. Ses étapes se poursuivent après un échec, pour qu'une analyse qui n'aboutit pas ne fasse jamais échouer la non-régression ([Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md)).
  - **Alternative écartée (analyse observant l'unique construction)** : la construction de la non-régression perdrait `TreatWarningsAsErrors`, et un échec de l'analyse pourrait l'interrompre.
  - **Alternative écartée (analyse dans un job à part)** : un exécuteur Windows de plus à démarrer, un dépôt à extraire et des packages à restaurer de nouveau, et la couverture à transmettre d'un job à l'autre, pour éviter une construction de moins d'une minute.
- **Contrôle qualité hebdomadaire** ([Contrôle qualité hebdomadaire](../gestion-projet/depot/controle-qualite-hebdomadaire.md)), sur un exécuteur Windows, seul à compiler l'application :
  - **Tests de mutation : Stryker.NET** (`dotnet-stryker`, outil local, licence Apache 2.0), outil de référence des tests de mutation en .NET, qui travaille sur les tests xunit tels qu'ils sont et dont les commentaires de désactivation portent le rejet d'un mutant équivalent.
  - **Code couvert : la classe que reflète chaque classe de tests** ([Organisation du code](organisation-du-code.md), projet miroir), hors interface Windows Forms : la portée s'étend d'elle-même à chaque classe de tests ajoutée, et un mutant que rien ne couvre y est un test qui manque à la classe testée.
    - **Alternative écartée (fichiers que touche la couverture mesurée)** : un fichier exercé en passant par le test d'une autre classe y entrerait, avec tous ses mutants non couverts, qui ne désignent aucun test manquant de cette classe.
  - **Règles de style : la construction de la solution**, analyse du style activée (`EnforceCodeStyleInBuild`) et règles listées passées en avertissement dans une `.editorconfig` complétée sur la copie de l'exécuteur, jamais dans le dépôt ; `IDE0005` exige le fichier de documentation, que la construction génère.
  - **SonarQube Cloud : son API Web**, avec le secret `SONAR_TOKEN` du dépôt : problèmes ouverts, points sensibles à revoir et porte de qualité de `main`.
  - **Tenue du ticket : un script PowerShell** (`scripts/Update-QualityAuditIssue.ps1`), qui réunit les relevés et réécrit le ticket, et qu'on exécute aussi sur le poste sans rien écrire (sans `-Update`).
    - **Alternative écartée (script dans le workflow, comme pour la veille de l'amont)** : la répartition par urgence et la relecture du ticket précédent sont une logique qu'il faut pouvoir exécuter et vérifier hors de l'exécuteur.
