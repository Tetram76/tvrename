---
type: Gouvernance
title: 'Versions et releases'
description: 'Numérotation semver à partir de la version 10, jalons, rattachement des tickets et PR, publication d''une version (tag, release, installeur, analyse).'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Versions et releases

Les versions de l'application sont connues du dépôt avant leur publication : chaque version à venir est un **jalon** GitHub (*milestone*), auquel sont rattachés les tickets et les Pull Requests qu'elle livre ; chaque version publiée est un **tag** et une **release** GitHub, avec son installeur. Une version correspond à un état stable et identifiable de l'application, sur `main`.

- **Numérotation** : [Semantic Versioning](https://semver.org/) (`MAJEUR.MINEUR.CORRECTIF`), tag `v<version>` (ex. `v10.0.0`), à partir de la **version majeure 10** (exigence de l'utilisateur) : un numéro que l'amont, publié jusqu'à la pré-version `v5.2.7` (dernière version stable : `5.0.8`, [Application existante](../../contraintes-techniques/application-existante.md)), n'atteindra pas de sitôt, pour qu'aucune version du fork ne porte le même numéro qu'une version de l'amont ; l'installeur d'une version du fork remplace en outre une installation de l'amont, qu'il voit comme plus ancienne.
  - la première version est `v10.0.0` (Phase 2), puis une version **mineure** par phase, ou partie de phase, du plan qui change l'application ; la Phase 0, qui ne change pas l'application, n'en a pas, ses Pull Requests rejoignant le jalon de la première version ;
  - une version **corrective** (ex. `v10.0.1`) livre une correction publiée entre deux de ces versions ;
  - une version **majeure** n'est décidée que pour un changement qui rompt la compatibilité pour les utilisateurs de l'application (réglages, fichiers, prérequis), annoncé à l'utilisateur ([Prise de décision](../gouvernance/prise-de-decision.md)).
  - **Alternative écartée (numérotation dans la suite de l'amont, ex. `v5.3.0`)** : l'amont, toujours actif en pré-versions, publierait tôt ou tard des versions de mêmes numéros que celles du fork.
  - **Alternative écartée (numérotation repartie de `1.0.0` ou de `0.x`)** : l'installeur d'une version du fork passerait pour plus ancien qu'une installation de l'amont, et les deux numérotations se confondraient.

  La version que livre chaque phase, ou partie de phase, est indiquée dans [`suivi-implementation/`](../../suivi-implementation/index.md).
- **Jalon** : chaque version à venir est un jalon de même nom (`v10.0.0`), dont la description désigne la phase ou la partie qu'elle livre. Il est créé au découpage de cette phase ou de cette partie, avec ses tickets, ou avec le ticket d'une version corrective. Il ne porte pas d'échéance : le projet ne planifie pas de dates.
- **Rattachement** : chaque ticket est rattaché au jalon de la version qui le livre ([Suivi par tickets](../revue-tickets/suivi-par-tickets.md)) ; un ticket hors plan l'est dès que sa réalisation est prévue dans une version, et reste sans jalon tant qu'elle ne l'est pas (ex. un ticket de suivi d'un harnais, étiquette `harness`, que ne réalise aucune PR). Chaque Pull Request fusionnée sur `main` est rattachée au jalon de son ticket de référence ; sans ticket, à celui de la version en cours, la première version non publiée. Un ticket fermé sans être réalisé (*not planned*) quitte son jalon.
- **Publication** : une version est publiée quand tous les tickets de son jalon sont fermés :
  - **tag** annoté et signé (clé GPG de [Signature des commits](signature-des-commits.md)), posé sur le commit de `main` qui la complète ;
  - **release** GitHub sur ce tag, dont les notes sont générées par GitHub à partir des Pull Requests fusionnées depuis le tag de la version précédente, et vérifiées avant publication comme tout texte publié ([Budget IA](../gouvernance/budget-ia.md), mention de Codex) ;
  - **installeur**, construit et joint à la release par le workflow `dotnet-desktop.yml`, que déclenche sa publication ;
  - **analyse SonarQube Cloud** de `main`, relancée dès le tag posé : la période du nouveau code commence à la première analyse qui porte la version ([Analyse de la qualité du code](analyse-de-la-qualite-du-code.md)), qui doit être celle du commit publié, et non celle de la fusion suivante, dont les changements en seraient exclus ;
  - **fermeture du jalon**.
- La publication d'une version est un acte de gestion du projet ([Prise de décision](../gouvernance/prise-de-decision.md)) : l'agent l'accomplit dès que la règle ci-dessus la prescrit, puis en informe l'utilisateur.
