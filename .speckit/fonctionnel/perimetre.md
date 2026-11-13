---
type: Périmètre
title: 'Fonctionnel'
description: 'Ce que contient fonctionnel/ : règles au niveau métier, aucun détail d''implémentation laissé à l''agent.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Fonctionnel


Ce dossier décrit les fonctionnalités de l'application et son interface, telles que l'utilisateur les voit et les utilise. Pour l'existant, il est rétro-documenté au fil des chantiers ([Gouvernance documentaire](../gestion-projet/gouvernance/gouvernance-documentaire.md#qui-décide-du-contenu)).
Il ne doit contenir **aucun détail d'implémentation laissé à la discrétion de l'agent** (stack, architecture, mécanique de calcul, structure de données, bibliothèque, etc.) : ceux-ci relèvent de `contraintes-techniques/` ou `choix-implementation/`. En revanche, un élément d'apparence technique mais **explicitement imposé par l'utilisateur comme règle métier** (ex. un format de nommage des fichiers imposé) reste documenté ici. Les règles sont formulées au niveau métier — le comportement attendu et les cas visibles de l'utilisateur.


---
