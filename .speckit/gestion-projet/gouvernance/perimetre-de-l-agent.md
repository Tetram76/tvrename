---
type: Gouvernance
title: 'Périmètre de l''agent'
description: 'Livrables que produit l''agent : code, tests, outillage, documentation du dépôt et rétro-documentation du .speckit.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Périmètre de l'agent

L'agent produit l'intégralité des livrables du projet, y compris :

- le code de l'application et ses tests ;
- l'outillage du dépôt (CI, scripts, harnais) ;
- la documentation du dépôt (`README.md`, `README.dev.md`) ;
- la **rétro-documentation** du `.speckit/` : la description, tirée du code existant, des fonctionnalités (`fonctionnel/`) et du modèle métier (`modele-metier/`), que l'agent propose et que l'utilisateur valide ([Qui décide du contenu](gouvernance-documentaire.md#qui-décide-du-contenu)).
