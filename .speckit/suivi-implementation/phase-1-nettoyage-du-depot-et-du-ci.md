---
type: Plan d'implémentation
title: 'Phase 1 — Nettoyage du dépôt et du CI'
description: 'Phase 1, nettoyage du dépôt et du CI (sans version propre, rejoint v10.0.0) : fichiers hérités du fork, documents destinés aux utilisateurs, construction de l''installeur.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T19:11:02Z }
---

# Phase 1 — Nettoyage du dépôt et du CI

*Ne change pas l'application : pas de version propre, ses Pull Requests rejoignent le jalon `v10.0.0` ([Versions et releases](../gestion-projet/depot/versions-et-releases.md)).*

Objectif : un dépôt et un CI débarrassés de ce que le fork a hérité sans usage, et conformes au `.speckit/`, avant le nettoyage du code (exigence de l'utilisateur). L'épinglage des actions par empreinte relève de la Phase 3 ([Mises à jour des dépendances](../gestion-projet/depot/mises-a-jour-des-dependances.md)).

| Ticket | Chantier |
| --- | --- |
| #19 | Fichiers hérités du fork sans usage et fichiers de santé communautaire de l'amont |
| #20 | Documents destinés aux utilisateurs : `README.md` et politique de confidentialité |
| #21 | Construction de l'installeur : workflow de publication et configuration de publication hérités |
