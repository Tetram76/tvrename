---
type: Gouvernance
title: 'Configuration du repository GitHub'
description: 'Réglages du dépôt GitHub, branche main et ancienne branche master archivée, Ruleset de main, sécurité du dépôt, merge fait par l''agent au nom de l''utilisateur, jamais par un collaborateur ; à lire pour toute modification de la configuration GitHub.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T18:00:00Z }
---

# Configuration du repository GitHub

- **Branche principale** : `main`, tirée du dernier commit de `master` ([Application existante](../../contraintes-techniques/application-existante.md)).
- **Ancienne branche `master`** : renommée `archive/master` et verrouillée en lecture seule par un Ruleset dédié (id `27213529` : toute mise à jour, suppression et force-push interdits). Elle garde l'état du code au moment du détachement du fork, et n'est plus jamais modifiée ni fusionnée.
  - **Alternative écartée (suppression de `master`)** : irréversible, alors que son seul coût est une branche de plus dans la liste ; l'état de départ du fork resterait certes dans l'historique de `main`, mais sans nom qui le désigne.
  - **Alternative écartée (`master` conservée sous son nom)** : elle reste la branche que proposent par habitude les outils et les contributeurs, au risque d'y voir arriver des commits ou des Pull Requests.
- **Stratégie de merge** : squash merge uniquement (historique linéaire et lisible sur `main`), titre du commit squashé repris du titre de la PR et corps de sa description (`squash_merge_commit_title = PR_TITLE`, `squash_merge_commit_message = PR_BODY`, [Conventions de commit](conventions-de-commit.md)).
- **Suppression automatique** des branches de feature après merge.
- **Fonctionnalités actives** : Issues, Releases.
- **Visibilité** : dépôt **public**.
- **Fonctionnalités désactivées** : Wiki, Projects, Discussions.
- **Protection de `main`** : un **Ruleset** GitHub (id `27213526`), **actif**, avec les règles suivantes :
  - PR obligatoire avant tout merge
  - Force-push interdit
  - Suppression de `main` interdite
  - Squash merge uniquement
  - **Commits signés obligatoires** (`required_signatures`)
  - **Checks de la CI requis** (`required_status_checks`) : chaque job du workflow CI est un check requis (cf. [Intégration continue (CI)](integration-continue-ci.md)). Le Ruleset n'exige pas que la branche soit à jour avec `main` avant le merge : les PR sont fusionnées en squash une à une, et cette exigence imposerait d'intégrer `main` de nouveau, puis d'attendre CI et revue, après chaque fusion d'une autre PR. L'agent tient néanmoins chaque branche de travail à jour de sa branche de destination ([Stratégie de branches](strategie-de-branches.md)).
  - **Porte de qualité de SonarQube Cloud requise**, une fois la porte rendue bloquante, au plus tard à la fin de la Phase 2 (jusque-là, porte non bloquante et check non requis, exigence de l'utilisateur, sans que les retours de l'analyse sur une Pull Request soient moins traités, [Analyse SonarQube Cloud](../revue-tickets/analyse-sonarqube-cloud.md)) : le check `SonarCloud Code Analysis`, que l'application GitHub de SonarQube Cloud (`integration_id` `12526`) publie sur chaque commit analysé, en échec si la porte de qualité échoue (cf. [Analyse de la qualité du code](analyse-de-la-qualite-du-code.md)).
- **Sécurité du dépôt** (`security_and_analysis`, disponible gratuitement car dépôt public) :
  - **Secret scanning** : activé
  - **Push protection** (blocage des push contenant un secret détecté) : activée
  - **Dependabot alerts** : activées ; elles n'ouvrent aucune Pull Request, et signalent les vulnérabilités des packages que déclare `TVRename/TVRename.csproj` en attendant le contrôle de sécurité quotidien ([Mises à jour des dépendances](mises-a-jour-des-dependances.md), vulnérabilités connues), qui les rendra redondantes : leur maintien sera revu par son chantier (Phase 3).
  - **Dependabot security updates** : désactivées ; leurs PR, prêtes pour la revue dès leur ouverture, déclencheraient une revue Codex avant même la revue de l'agent ([Budget IA](../gouvernance/budget-ia.md)).
- **Workflow hérité du fork** : `dotnet-desktop.yml` (installeur publié avec chaque release, [Versions et releases](versions-et-releases.md)).
- **Merge fait par l'agent, au nom de l'utilisateur** : fusionner une Pull Request dont les conditions de la [Règle de merge](../revue-tickets/regle-de-merge-non-regression-porte-de-qualite-et-revue-codex-obligatoires.md) sont réunies est un acte de gestion du projet ([Prise de décision](../gouvernance/prise-de-decision.md)) : l'agent l'accomplit lui-même, sans attendre ni demander l'accord de l'utilisateur, puis l'en informe. L'utilisateur peut aussi fusionner lui-même.
- **Merge interdit à tout collaborateur** : le dépôt est la propriété d'un compte **personnel** (`Tetram76`), pas d'une organisation — la restriction de push/merge par utilisateur ou équipe n'est **pas disponible** sur ce type de dépôt, elle ne peut donc pas être imposée techniquement. La garantie repose donc sur la gestion des droits d'accès : **aucun collaborateur ne doit recevoir un accès `Write` (ou supérieur)** au dépôt. Toute contribution externe passe par un **fork** + Pull Request ; le merge de cette PR reste effectué par l'utilisateur (ou par l'agent agissant en son nom), jamais par le contributeur externe lui-même.
