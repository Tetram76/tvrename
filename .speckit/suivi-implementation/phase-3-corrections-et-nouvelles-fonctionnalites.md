---
type: Plan d'implémentation
title: 'Phase 3 — Corrections et nouvelles fonctionnalités'
description: 'Phase 3, corrections et nouvelles fonctionnalités, après la rétro-documentation complète de l''existant ; premier chantier : retrait du canal de mise à jour automatique ; notes pour son découpage.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Phase 3 — Corrections et nouvelles fonctionnalités

Phase pas encore découpée. Elle ne commence qu'une fois la rétro-documentation de l'existant complétée et finalisée ([Suivi d'implémentation](suivi-d-implementation.md), ordre des phases).

| Ticket | Chantier |
| --- | --- |
| #7 | Retrait du canal de mise à jour automatique : l'application ne recherche plus de nouvelle version (elle interroge aujourd'hui les releases de l'amont, `TV-Rename/tvrename`) — **premier chantier de la phase** (décision de l'utilisateur) |

**Notes pour le découpage** :

- Les défauts de l'existant relevés pendant les phases 1 et 2, sans être corrigés pour ne pas changer le comportement, y sont repris (tickets `Bug`).
- Les reprises de fonctionnalités de l'amont ([Reprise depuis l'amont](../gestion-projet/depot/reprise-depuis-l-amont.md)) peuvent intervenir à toute phase quand l'application en dépend (ex. une source de données qui change d'API) ; les autres y sont décidées par l'utilisateur.
