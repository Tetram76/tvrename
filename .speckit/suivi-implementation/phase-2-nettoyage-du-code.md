---
type: Plan d'implémentation
title: 'Phase 2 — Nettoyage du code'
description: 'Phase 2, nettoyage du code (v10.0.0) : couverture de tests et porte de qualité de SonarQube Cloud, rétro-documentation au fil de l''eau, contrôle qualité hebdomadaire ; découpage après la première analyse.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T19:00:00Z }
---

# Phase 2 — Nettoyage du code

*Version : `v10.0.0`.*

Objectif : un code hérité couvert par des tests qui fixent son comportement, et dont l'ensemble réussit la porte de qualité de SonarQube Cloud ([Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md)). Le dépôt et le CI ont été nettoyés avant elle ([Phase 1 — Nettoyage du dépôt et du CI](phase-1-nettoyage-du-depot-et-du-ci.md)).

| Ticket | Chantier |
| --- | --- |
| #6 | Contrôle qualité hebdomadaire (`.github/workflows/quality-audit.yml`) et son ticket de suivi |

**Notes pour le découpage** (la phase est découpée une fois la première analyse SonarQube Cloud de `main` disponible, chantier de la Phase 0) :

- Le découpage part des relevés de l'analyse (problèmes par gravité, couverture, duplication) et les répartit par **module** de l'application (ex. renommage, sources de données, analyse de la bibliothèque, actions, interface), pour que chaque chantier soit indépendant des autres ([Chantiers en parallèle](../gestion-projet/gouvernance/chantiers-en-parallele.md), indépendance).
- Un chantier de couverture d'un module commence par la **rétro-documentation** du comportement qu'il va fixer (`fonctionnel/`, [`modele-metier/`](../modele-metier/index.md)), soumise à la validation de l'utilisateur ([Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md)), puis écrit les tests, puis nettoie le code sous leur protection.
- Le nettoyage ne change pas le comportement de l'application : un défaut relevé en chemin est décrit tel qu'il est, signalé à l'utilisateur et fait l'objet d'un ticket de la Phase 4, sauf décision contraire de l'utilisateur ([Gouvernance documentaire](../gestion-projet/gouvernance/gouvernance-documentaire.md#qui-décide-du-contenu)).
- Le code de l'interface Windows Forms se prête mal aux tests unitaires : le découpage établit comment la règle de non-régression s'y applique ([Suivi par tickets](../gestion-projet/revue-tickets/suivi-par-tickets.md), règles transverses au découpage d'une phase), par exemple en sortant la logique des formulaires vers du code testable.
- **Ce qu'il est prévu de retirer est ignoré** (exigence de l'utilisateur) : le code propre au canal de mise à jour automatique, aux films et à leurs collections, et aux fonctions RSS et NZB, retiré en Phase 4 (#7, #9, #12), n'est ni couvert par des tests, ni rétro-documenté, ni nettoyé ; il est exclu de la couverture que mesure SonarQube Cloud ([Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md), périmètre), pour que la porte de qualité n'en dépende pas. Seul ce qu'exige la sûreté du code conservé (ex. un défaut de sécurité, un code partagé avec les séries) y est traité.
- **Porte de qualité rendue bloquante dès que l'état du code le permet** (exigences de l'utilisateur, [Analyse de la qualité du code](../gestion-projet/depot/analyse-de-la-qualite-du-code.md)) : le découpage en fait un élément du chantier dont la fusion amène l'ensemble du code à réussir la porte, quel que soit son rang dans la phase, et la phase n'est pas terminée tant qu'elle n'est pas bloquante : check `SonarCloud Code Analysis` requis par le Ruleset de `main` ([Configuration du repository GitHub](../gestion-projet/depot/configuration-du-repository-github.md)), et règles qui en dépendent remises à leur forme définitive.
- **Retrait du support des plateformes autres que Windows et Linux** ([Stack cible](../contraintes-techniques/stack-cible.md), plateformes) : un chantier de nettoyage retire le code propre aux autres plateformes (ex. l'ouverture d'une URL ou d'un fichier par `open` sous macOS, la détection de l'environnement d'exécution Mono). Ce qui sert Linux est conservé, pour son support natif à terme.
- Les avertissements de compilation de l'existant (packages restaurés pour .NET Framework, références de packages en double, champs inutilisés) en font partie.
