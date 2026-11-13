# Journal des changements du .speckit

## 2026-11-13

* **Documents destinés aux utilisateurs** : `README.md` et politique de confidentialité réécrits pour le fork ; chantier #20 barré dans le suivi de la Phase 1 (#27).

* **Journal de l'application** : concept ajouté à `fonctionnel/` ; le journal reste local, aucun journal ni aucune donnée de diagnostic n'est envoyé à un service distant (exigence de l'utilisateur), l'envoi des erreurs à Papertrail et sa case des Préférences étant retirés (#25).

* **Fichiers hérités du fork** retirés du dépôt (configuration de Better Code Hub, réglages personnels d'IDE, configuration d'exécution de CefSharp pour `netcoreapp3.1`, template de PR de l'amont, fichiers de santé communautaire et modèles de tickets qui désignent l'amont) ; chantier #19 barré dans le suivi de la Phase 1 (#23).

* **Phase 1 découpée** en trois chantiers indépendants, rattachés au jalon `v10.0.0` : fichiers hérités du fork sans usage et fichiers de santé communautaire de l'amont (#19), documents destinés aux utilisateurs, soumis à leur validation (#20), construction de l'installeur (#21) ; notes de découpage remplacées par la liste des tickets (#22).

* **Environnement de validation fonctionnelle** : version d'une Pull Request compilée depuis ses seules sources en version de validation (constante `VALIDATION`), qui range tous ses fichiers dans `%TEMP%\TVRename` et tourne à côté d'un TV Rename lancé, lancée par `scripts/Start-Validation.ps1`, arrêtée par `scripts/Stop-Validation.ps1` ; `/userfilepath:` garde son usage ordinaire (décision de l'utilisateur) ; l'application livrée reste inchangée ; chantier #5 barré dans le suivi de la Phase 0 (#18).

* **Tickets de suivi des harnais** : un état, pas un historique (exigences de l'utilisateur) : description en trois parties, objectif, dates (`Dernier contrôle`, `Dernière prise en compte`) et points à corriger, sans décompte ni compte rendu ; traitement dû quand la ligne `Dernière prise en compte` manque ; point traité aussitôt retiré ; relevé des versions, traitement du contrôle qualité et veille de l'amont alignés (#17).

* **Veille de l'amont** : harnais `upstream-watch.yml` mis en place ; ses points sont les évolutions de l'amont que l'agent n'a pas encore traitées, chaque exécution n'ajoutant que celles reçues depuis la précédente (la première fois, depuis `archive/master`) ; ticket fermé quand il ne liste plus rien ; workflow ajouté aux autres workflows du CI ; chantier #4 barré dans le suivi de la Phase 0 (#17).

* **Plateformes** : l'application ne vise que Windows et Linux, en natif à terme, le travail actuel portant sur Windows seul (exigence de l'utilisateur, Stack cible) ; retrait du code propre aux autres plateformes ajouté aux notes de découpage de la Phase 2, support natif de Linux à celles de la Phase 4 (#15).

* **Organisation du code** : projet des tests de l'application, `tests/TVRename.Tests`, miroir de `TVRename`, dont la couverture alimente l'analyse SonarQube Cloud ; chantier #3 barré dans le suivi de la Phase 0 (#14).

* **Analyse de la qualité du code** : analyse SonarQube Cloud mise en place dans le job du CI, après la construction stricte et les tests, dont elle reprend la couverture ; porte de qualité non bloquante tant que l'état du code ne permet pas de la rendre bloquante, ce qui se fait dès qu'il le permet et au plus tard à la fin de la Phase 2 (exigences de l'utilisateur) ; règle de merge, traitement de l'analyse, Ruleset et notes de la Phase 2 alignés ; retours de l'analyse traités même porte non bloquante ; CodeQL retiré, SonarQube Cloud restant la seule analyse statique (décision de l'utilisateur) (#13).

* **Gouvernance** : création du `.speckit/`, sur le modèle de gouvernance de BDthèque : développement piloté par la spécification, format OKF, revue Codex bloquante, analyse SonarQube Cloud, tickets et harnais, chantiers en parallèle ; projet défini comme la maintenance d'un fork détaché de TV Rename (Windows Forms, pas de réécriture), rétro-documentation de l'existant au fil des chantiers et validée par l'utilisateur, reprises ponctuelles de fonctionnalités de l'amont et veille mensuelle de l'amont (harnais `upstream-watch`) (exigences de l'utilisateur) ; branche `main` tirée de `master`, archivée en `archive/master` ; numérotation des versions à partir de `v10.0.0`, pour ne jamais rejoindre celle de l'amont ; premiers chantiers de la Phase 4, dans l'ordre : retrait du canal de mise à jour automatique, recentrage sur les séries TV par le retrait des films, retrait des fonctions RSS, distinction des séries TV et des animés, moteur de recherche web par défaut de chaque catégorie (décisions de l'utilisateur) ; ce qu'il est prévu de retirer n'est ni testé ni rétro-documenté, et est exclu de la couverture (exigence de l'utilisateur) ; plan en cinq phases, numérotées de 0 à 4 : mise en place, nettoyage du dépôt et du CI, nettoyage du code, sécurité et montées de version, corrections et nouvelles fonctionnalités (#8).
