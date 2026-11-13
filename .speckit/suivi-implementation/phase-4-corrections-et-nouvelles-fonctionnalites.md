---
type: Plan d'implémentation
title: 'Phase 4 — Corrections et nouvelles fonctionnalités'
description: 'Phase 4, corrections et nouvelles fonctionnalités, après la rétro-documentation complète de l''existant ; premiers chantiers, dans l''ordre : retrait du canal de mise à jour automatique, recentrage sur les séries TV (retrait des films), retrait des fonctions RSS, distinction des séries TV et des animés, moteur de recherche web par catégorie ; notes pour son découpage.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T17:26:31Z }
---

# Phase 4 — Corrections et nouvelles fonctionnalités

Phase pas encore découpée. Elle ne commence qu'une fois la rétro-documentation de l'existant complétée et finalisée ([Suivi d'implémentation](suivi-d-implementation.md), ordre des phases).

Ses premiers chantiers sont décidés par l'utilisateur, et menés **dans cet ordre**, chacun après le précédent :

| Ticket | Chantier |
| --- | --- |
| #7 | Retrait du canal de mise à jour automatique : l'application ne recherche plus de nouvelle version (elle interroge aujourd'hui les releases de l'amont, `TV-Rename/tvrename`) |
| #9 | Recentrage sur les séries TV : retrait de tout ce qui touche aux films |
| #12 | Retrait des fonctions RSS |
| #10 | Distinction des séries TV et des animés, source de données de chaque catégorie comprise, à spécifier avec l'utilisateur avant sa réalisation |
| #11 | Moteur de recherche web par défaut de chaque catégorie (séries TV, animés) |

**Notes pour le découpage** :

- Les défauts de l'existant relevés pendant les phases 2 et 3, sans être corrigés pour ne pas changer le comportement, y sont repris (tickets `Bug`).
- **Support natif de Linux, à terme** ([Stack cible](../contraintes-techniques/stack-cible.md), plateformes) : chantier ultérieur, hors des premiers chantiers ci-dessus ; il suppose de revoir la technologie d'interface, Windows Forms n'existant que sous Windows, ce que l'utilisateur décide avant sa réalisation.
- Les reprises de fonctionnalités de l'amont ([Reprise depuis l'amont](../gestion-projet/depot/reprise-depuis-l-amont.md)) peuvent intervenir à toute phase quand l'application en dépend (ex. une source de données qui change d'API) ; les autres y sont décidées par l'utilisateur.
