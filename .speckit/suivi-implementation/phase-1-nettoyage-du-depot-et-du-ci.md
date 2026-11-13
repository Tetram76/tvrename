---
type: Plan d'implémentation
title: 'Phase 1 — Nettoyage du dépôt et du CI'
description: 'Phase 1, nettoyage du dépôt et du CI (sans version propre, rejoint v10.0.0) : fichiers et workflows hérités du fork, retirés ou remis aux règles du .speckit ; notes pour son découpage.'
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T18:00:00Z }
---

# Phase 1 — Nettoyage du dépôt et du CI

*Ne change pas l'application : pas de version propre, ses Pull Requests rejoignent le jalon `v10.0.0` ([Versions et releases](../gestion-projet/depot/versions-et-releases.md)).*

Objectif : un dépôt et un CI débarrassés de ce que le fork a hérité sans usage, et conformes au `.speckit/`, avant le nettoyage du code (exigence de l'utilisateur).

Phase pas encore découpée. **Notes pour le découpage** :

- **Dépôt** : fichiers hérités sans usage ou obsolètes pour le fork, chacun examiné avant d'être retiré ou mis à jour, selon [Repository](../gestion-projet/depot/repository.md) (ex. `.bettercodehub.yml`, service arrêté ; `TVRename.sln.DotSettings.user`, réglages personnels d'un IDE ; `CefSharp.BrowserSubprocess.runtimeconfig.json` à la racine ; `PULL_REQUEST_TEMPLATE.md`, que remplace le template du compte ; `README.md`, `CONTRIBUTING.md`, `SECURITY.md`, `privacy-policy.md`, `.github/ISSUE_TEMPLATE/`, `.github/FUNDING.yml`, qui désignent l'amont, ses forums et son site).
- **CI** : workflows hérités remis aux règles du `.speckit/` ([Intégration continue (CI)](../gestion-projet/depot/integration-continue-ci.md)) : `dotnet-desktop.yml` (chemins de l'exécuteur codés en dur, étapes de diagnostic laissées, construction de l'installeur), `TVRename/msbuild.rsp` et le profil de publication. L'épinglage des actions par empreinte relève de la Phase 3 ([Mises à jour des dépendances](../gestion-projet/depot/mises-a-jour-des-dependances.md)).
- Ce qui touche à un document destiné aux utilisateurs de l'application (`README.md`, politique de confidentialité) leur est visible : son contenu est soumis à la validation de l'utilisateur ([Validation fonctionnelle](../gestion-projet/revue-tickets/validation-fonctionnelle.md)).
