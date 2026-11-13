---
type: Plan d'implémentation
title: 'Phase 1 — Nettoyage du code'
description: 'Phase 1, nettoyage du code (v10.0.0) : couverture de tests et porte de qualité de SonarQube Cloud, rétro-documentation au fil de l''eau, contrôle qualité hebdomadaire ; découpage après la première analyse.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Phase 1 — Nettoyage du code

*Version : `v10.0.0`.*

Objectif : un code hérité couvert par des tests qui fixent son comportement, et dont l'ensemble réussit la porte de qualité de SonarQube Cloud ([Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md)).

| Ticket | Chantier |
| --- | --- |
| #6 | Contrôle qualité hebdomadaire (`.github/workflows/quality-audit.yml`) et son ticket de suivi |

**Notes pour le découpage** (la phase est découpée une fois la première analyse SonarQube Cloud de `main` disponible, chantier de la Phase 0) :

- Le découpage part des relevés de l'analyse (problèmes par gravité, couverture, duplication) et les répartit par **module** de l'application (ex. renommage, sources de données, analyse de la bibliothèque, actions, interface), pour que chaque chantier soit indépendant des autres ([Chantiers en parallèle](../gestion-projet/gouvernance/chantiers-en-parallele.md), indépendance).
- Un chantier de couverture d'un module commence par la **rétro-documentation** du comportement qu'il va fixer (`fonctionnel/`, [`modele-metier/`](../modele-metier/index.md)), soumise à la validation de l'utilisateur ([Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md)), puis écrit les tests, puis nettoie le code sous leur protection.
- Le nettoyage ne change pas le comportement de l'application : un défaut relevé en chemin est décrit tel qu'il est, signalé à l'utilisateur et fait l'objet d'un ticket de la Phase 3, sauf décision contraire de l'utilisateur ([Gouvernance documentaire](../gestion-projet/gouvernance/gouvernance-documentaire.md#qui-décide-du-contenu)).
- Le code de l'interface Windows Forms se prête mal aux tests unitaires : le découpage établit comment la règle de non-régression s'y applique ([Suivi par tickets](../gestion-projet/revue-tickets/suivi-par-tickets.md), règles transverses au découpage d'une phase), par exemple en sortant la logique des formulaires vers du code testable.
