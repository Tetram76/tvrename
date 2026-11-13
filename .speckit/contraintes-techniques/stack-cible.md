---
type: Contrainte technique
title: 'Stack cible'
description: 'Stack imposée par l''existant : application de bureau Windows, C# / .NET, Windows Forms ; plateformes visées : Windows, Linux natif à terme, aucune autre.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T17:26:31Z }
---

# Stack cible

La stack est celle de l'application existante ([Application existante](application-existante.md)), conservée :

| Élément | Valeur |
| --- | --- |
| Type d'application | Application de bureau Windows, distribuée par un installeur |
| Langage | C# / .NET (version : cf. [Versions et bibliothèques](../choix-implementation/versions-et-bibliotheques.md)) |
| Interface | Windows Forms |
| Plateformes | Windows ; Linux, en natif, à terme ; aucune autre |

**Plateformes** (exigence de l'utilisateur) : l'application ne vise que Windows et Linux. Windows est la seule plateforme sur laquelle porte le travail actuel. Le support natif de Linux est une cible à terme, réalisée par un chantier dédié ([Phase 4 — Corrections et nouvelles fonctionnalités](../suivi-implementation/phase-4-corrections-et-nouvelles-fonctionnalites.md)) : Windows Forms n'existant que sous Windows, ce chantier revoit la technologie d'interface, sur décision de l'utilisateur ([Application existante](application-existante.md), maintenance, pas réécriture). Le code propre à toute autre plateforme (macOS, environnement d'exécution Mono) est retiré ([Phase 2 — Nettoyage du code](../suivi-implementation/phase-2-nettoyage-du-code.md)).
