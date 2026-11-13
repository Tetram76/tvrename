---
type: Choix d'implémentation
title: 'Versions et bibliothèques'
description: '.NET 10 LTS (net10.0-windows), montée suivante vers .NET 12 ; bibliothèques héritées du fork.'
applies_to:
  - 'TVRename/TVRename.csproj'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Versions et bibliothèques

`contraintes-techniques/` n'impose aucune version : les versions retenues sont des choix.

- **.NET 10** (`net10.0-windows`), version de l'existant, et **uniquement des versions LTS** pour la plateforme .NET : une version majeure paraît chaque novembre, les paires LTS (trois ans de support), les impaires STS (deux ans). La montée suivante est **.NET 12**, à sa parution ([Mises à jour des dépendances](../gestion-projet/depot/mises-a-jour-des-dependances.md)) ; les versions mineures et correctives sont suivies au fil de leur parution.
  - **Alternative écartée (versions STS)** : une STS ne prolonge pas le support (la 11 le perd en même temps que la 10), et impose une montée de plus.
- **Bibliothèques** : celles de l'existant (`TVRename/TVRename.csproj`) sont reprises telles quelles ; chacune est revue lors de sa montée de version (Phase 2, [`suivi-implementation/`](../suivi-implementation/index.md)), et remplacée si elle ne satisfait plus [Licences](../contraintes-techniques/licences.md) (projet abandonné ou confidentiel).
