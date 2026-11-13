---
type: Plan d'implémentation
title: 'Phase 4 — Corrections et nouvelles fonctionnalités'
description: 'Phase 4, corrections et nouvelles fonctionnalités, après la rétro-documentation complète de l''existant ; trois premiers chantiers : retrait du canal de mise à jour automatique, recentrage sur les séries TV (retrait des films), distinction des séries TV et des animés ; notes pour son découpage.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Phase 4 — Corrections et nouvelles fonctionnalités

Phase pas encore découpée. Elle ne commence qu'une fois la rétro-documentation de l'existant complétée et finalisée ([Suivi d'implémentation](suivi-d-implementation.md), ordre des phases).

| Ticket | Chantier |
| --- | --- |
| #7 | Retrait du canal de mise à jour automatique : l'application ne recherche plus de nouvelle version (elle interroge aujourd'hui les releases de l'amont, `TV-Rename/tvrename`) — **premier chantier de la phase** (décision de l'utilisateur) |
| #9 | Recentrage sur les séries TV : retrait de tout ce qui touche aux films — **deuxième chantier de la phase** (décision de l'utilisateur) |
| #10 | Distinction des séries TV et des animés, à spécifier avec l'utilisateur avant sa réalisation — **troisième chantier de la phase** (décision de l'utilisateur) |

**Notes pour le découpage** :

- Les défauts de l'existant relevés pendant les phases 2 et 3, sans être corrigés pour ne pas changer le comportement, y sont repris (tickets `Bug`).
- Les reprises de fonctionnalités de l'amont ([Reprise depuis l'amont](../gestion-projet/depot/reprise-depuis-l-amont.md)) peuvent intervenir à toute phase quand l'application en dépend (ex. une source de données qui change d'API) ; les autres y sont décidées par l'utilisateur.
