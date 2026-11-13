---
type: Plan d'implémentation
title: 'Phase 0 — Mise en place'
description: 'Phase 0, mise en place (sans version propre, rejoint v10.0.0) : gouvernance, analyse SonarQube Cloud, tests de l''application, veille de l''amont, environnement de validation.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T20:00:00Z }
---

# Phase 0 — Mise en place

*Ne change pas l'application : pas de version propre, ses Pull Requests rejoignent le jalon `v10.0.0` ([Versions et releases](../gestion-projet/depot/versions-et-releases.md)).*

| Ticket | Chantier |
| --- | --- |
| ~~#1~~ | Gouvernance pilotée par la spécification : `.speckit/`, `AGENTS.md`, configuration de Claude Code, tests du dépôt, CI de non-régression, paramétrage du dépôt GitHub |
| ~~#2~~ | Analyse SonarQube Cloud de chaque Pull Request et de `main`, porte de qualité, indicateurs du `README.md` |
| ~~#3~~ | Projet de tests de l'application (`tests/TVRename.Tests`) et mesure de sa couverture dans le CI |
| #4 | Veille mensuelle de l'amont (`.github/workflows/upstream-watch.yml`) et son ticket de suivi |
| #5 | Environnement de validation fonctionnelle : version d'une Pull Request compilée et lancée sur le poste de l'utilisateur, avec un dossier de données à part |
