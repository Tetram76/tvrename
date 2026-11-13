---
type: Contrainte technique
title: 'Application existante'
description: 'Le dépôt est un fork détaché de TV-Rename/tvrename, application Windows Forms maintenue et non réécrite ; reprises ponctuelles depuis l''amont ; à lire pour tout chantier qui touche au code hérité.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Application existante

- **Fork détaché** : le dépôt est un fork de [`TV-Rename/tvrename`](https://github.com/TV-Rename/tvrename) (« l'amont »), détaché au commit `a5c0998`, conservé dans la branche `archive/master`. L'amont n'a plus publié de version stable depuis `5.0.8` (2023), mais seulement des pré-versions (jusqu'à `v5.2.7`). Il ne suit plus l'amont et ne s'y synchronise pas.
- **Reprises ponctuelles** : une modification de l'amont peut être reprise par un chantier dédié, par exemple quand l'amont a adapté l'application à une nouvelle version de l'API d'une source de données (ex. TheTVDB) (mise en œuvre : [Reprise depuis l'amont](../gestion-projet/depot/reprise-depuis-l-amont.md)).
- **Maintenance, pas réécriture** : l'application existante est **conservée** et améliorée par étapes ([Objectifs du projet](../gestion-projet/gouvernance/objectifs-du-projet.md)) : son architecture, sa technologie d'interface ([Stack cible](stack-cible.md)), ses réglages et les fichiers qu'elle produit chez ses utilisateurs restent compatibles d'une version à l'autre, sauf décision contraire de l'utilisateur.
- **Code hérité non spécifié** : l'application n'a ni spécification ni tests ; son comportement actuel fait foi jusqu'à sa rétro-documentation dans `fonctionnel/` et [`modele-metier/`](../modele-metier/index.md), menée au fil des chantiers ([`suivi-implementation/`](../suivi-implementation/index.md)).
- **Sources de données externes** : l'application interroge des services tiers (TheTVDB, TMDB, TVmaze, ainsi que GitHub, dont les releases de l'amont servent à la recherche de mises à jour, jusqu'au retrait de ce canal, premier chantier de la Phase 4), dont elle dépend et dont les API évoluent sans préavis.
