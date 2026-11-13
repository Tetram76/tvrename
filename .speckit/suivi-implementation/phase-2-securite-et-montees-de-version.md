---
type: Plan d'implémentation
title: 'Phase 2 — Sécurité et montées de version'
description: 'Phase 2, sécurité et montées de version (v10.1.0) : outillage du relevé et du contrôle de sécurité, correctifs de sécurité, puis montées de version ; notes pour son découpage.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Phase 2 — Sécurité et montées de version

*Version : `v10.1.0`.*

Phase pas encore découpée. **Notes pour le découpage** :

- **Outillage d'abord** ([Mises à jour des dépendances](../gestion-projet/depot/mises-a-jour-des-dependances.md)) : épinglage de toutes les versions (packages centralisés dans `Directory.Packages.props`, `global.json`, `dotnet-tools.json`, actions GitHub par empreinte), relevé des versions (`scripts/Get-DependencyUpdates.ps1`), contrôle de sécurité quotidien (`.github/workflows/security-audit.yml`) et son ticket de suivi.
- **Correctifs de sécurité** ensuite, chacun avec sa revue de l'existant, puis les **montées de version** des autres dépendances, dans l'ordre de leur risque ; une bibliothèque abandonnée est remplacée ([Versions et bibliothèques](../choix-implementation/versions-et-bibliotheques.md)).
- La couverture de tests de la Phase 1 est la protection de ces montées : une montée qui touche un module encore peu couvert commence par sa couverture.
