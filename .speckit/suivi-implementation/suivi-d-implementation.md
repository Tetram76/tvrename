---
type: Plan d'implémentation
title: 'Suivi d''implémentation'
description: 'Ce que décrit le suivi : un chantier par ligne, état porté par le ticket, version de chaque phase, ordre des phases.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Suivi d'implémentation

Ce dossier décrit le plan d'implémentation du projet, découpé en phases puis en chantiers — pas l'historique des décisions qui ont conduit à ce découpage.

Chaque ligne correspond à un chantier, c'est-à-dire à un ticket GitHub (et à une Pull Request) : elle en indique le **numéro** et le **titre**. Le contenu détaillé du chantier est porté par le ticket. L'**état** d'un chantier est celui de son ticket, signalé par la mise en forme de son numéro, selon la règle unique de [Suivi par tickets](../gestion-projet/revue-tickets/suivi-par-tickets.md) (Statut).

Chaque phase, ou partie de phase, qui change l'application est livrée par une version ([Versions et releases](../gestion-projet/depot/versions-et-releases.md)), indiquée sous son titre.

## Ordre des phases

Les phases se suivent dans l'ordre que fixe l'utilisateur :

1. **Phase 0 — Mise en place** : gouvernance, CI, analyse de la qualité, moyens de test et de validation.
2. **Phase 1 — Nettoyage du code** : couverture de tests et porte de qualité de SonarQube Cloud.
3. **Phase 2 — Sécurité et montées de version** : correctifs de sécurité, puis montées de version.
4. **Phase 3 — Corrections et nouvelles fonctionnalités**.

La **rétro-documentation** de l'existant (`fonctionnel/`, [`modele-metier/`](../modele-metier/index.md)) n'est pas une phase : elle est menée au fur et à mesure, par les chantiers des phases 1 et 2 qui en ont besoin (un chantier de couverture documente d'abord le comportement que ses tests fixent), puis **complétée et finalisée avant la Phase 3**, dont elle est la condition de démarrage.
