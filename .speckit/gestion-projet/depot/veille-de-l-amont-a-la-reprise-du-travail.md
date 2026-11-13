---
type: Gouvernance
title: 'Veille de l''amont à la reprise du travail'
description: 'À chaque reprise du travail, après le traitement du contrôle qualité : quand l''analyse de la veille de l''amont est due et où sa trace se lit ; si elle l''est, lire Reprise depuis l''amont.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Veille de l'amont à la reprise du travail

Le ticket de suivi de la veille de l'amont (étiquettes `upstream-watch` et `harness`, [Issues](../revue-tickets/issues.md#tickets-de-suivi-des-harnais)) est lu à chaque reprise du travail, après le traitement du contrôle qualité ([Traitement du contrôle qualité à la reprise du travail](traitement-du-controle-qualite-a-la-reprise-du-travail.md)) et avant le chantier. Tant que la veille et son ticket n'existent pas (Phase 0, [`suivi-implementation/`](../../suivi-implementation/index.md)), l'analyse n'est jamais due.

- **Due** quand la description du ticket liste au moins une évolution de l'amont et ne porte pas de section « Analyse du `<date>` » : la veille réécrit la description en entier à chaque exécution, si bien que la section disparaît avec elle, et une analyse au plus a lieu par exécution. L'analyse est alors menée selon [Reprise depuis l'amont](reprise-depuis-l-amont.md), lu en entier, et sa trace ajoutée. Sinon, la reprise passe au chantier.
- **Dans un lot de chantiers en parallèle**, seul l'orchestrateur la vérifie et la mène, une fois pour le lot ([Chantiers en parallèle](../gouvernance/chantiers-en-parallele.md)) : chaque session de chantier la trouverait due le même jour.
  - **Alternative écartée (commentaire du ticket à chaque analyse, ou trace dans un fichier du dépôt)** : écartée pour les mêmes raisons que pour le relevé des versions ([Relevé des versions à la reprise du travail](releve-des-versions-a-la-reprise-du-travail.md)).
