---
type: Journal
title: 'Journal des évènements externes'
description: 'Journal des évènements externes au dépôt (publications, incidents chez les utilisateurs, changements des services tiers interrogés, évolutions notables de l''amont) ; tenu sur demande de l''utilisateur seulement.'
tags: [socle]
generated: { by: claude-code/claude-opus-5-5, at: 2026-11-13T12:00:00Z }
---

# Journal des évènements externes

Ce fichier est un **journal**, pas un document de référence : contrairement aux autres fichiers `.speckit/`, il déroge intentionnellement à leurs règles de mise à jour et de format (cf. [Mise à jour du .speckit](gestion-projet/gouvernance/gouvernance-documentaire.md#mise-à-jour-du-speckit)).

- **Mise à jour** : uniquement sur demande explicite de l'utilisateur. Contrairement aux autres fichiers `.speckit/`, l'agent ne doit **jamais** y ajouter d'entrée de sa propre initiative, même s'il a connaissance d'un évènement pertinent (ex. version publiée au cours de la conversation). Il peut le signaler à l'utilisateur, mais l'ajout attend sa demande explicite.
- **Contenu** : évènements **externes au repo** susceptibles d'influencer son développement (publication d'une version, incident constaté chez un utilisateur, changement ou indisponibilité d'une API d'une source de données, évolution notable de l'amont, etc.). Les décisions ou changements internes au code/projet n'y figurent pas — ils relèvent des fichiers de référence appropriés.
- **Format** : chaque évènement est une entrée datée, insérée à sa **position chronologique réelle** (date de l'évènement, pas date de consignation) — un évènement rapporté tardivement est donc inséré parmi les entrées existantes, pas simplement ajouté en fin de journal. Contrairement aux autres fichiers, l'historique est ici la donnée elle-même : une entrée existante n'est **jamais réécrite ni fusionnée** à l'initiative de l'agent — seule l'insertion d'une nouvelle entrée peut déplacer la position des entrées suivantes dans le fichier, sans modifier leur contenu. Chaque entrée précise au minimum la date, la nature de l'évènement et son impact/portée pour le développement.
- **Correction** : cette immutabilité protège contre une réécriture spontanée de l'agent, pas contre une correction demandée par l'utilisateur. Si l'utilisateur signale une erreur dans une entrée existante (date, nature, impact) ou demande le retrait d'une information sensible, l'entrée concernée est corrigée ou retirée sur sa demande explicite, au même titre que tout ajout au journal. Cette correction n'efface que l'état courant du fichier, pas l'historique Git : ce journal ne doit donc jamais contenir de donnée réellement sensible (secret, identifiant) ; si cela arrivait néanmoins, la réponse est la rotation de l'identifiant concerné, traitée comme un incident de sécurité indépendant de cette correction.

## Évènements

_Aucun évènement consigné pour le moment._
