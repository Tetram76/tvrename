# Journal des changements du .speckit

## 2026-11-14

* **Licence retirée hors de l'application** : les tests du dépôt vérifient aussi qu'aucune licence ne figure dans les commentaires des sources, dans l'installeur ni dans le `README.md` (`choix-implementation/organisation-du-code.md`) (#30).

## 2026-11-13

* **Usage privé de l'application** (exigence de l'utilisateur) : ajouté à `fonctionnel/contexte.md` ; licence de l'application retirée de `contraintes-techniques/licences.md` et de la reprise depuis l'amont (décision de l'utilisateur) ; les tests du dépôt vérifient qu'aucune source de l'application ne renvoie à l'amont ni à une licence (#26).

* **Contrôle qualité hebdomadaire** : harnais `quality-audit.yml` mis en place (SonarQube Cloud, règles de style .NET, tests de mutation par Stryker.NET sur la classe que reflète chaque classe de tests), ticket de suivi tenu par `scripts/Update-QualityAuditIssue.ps1`, que ses tests exécutent avec des doublures de GitHub et de SonarQube Cloud ; les tests du dépôt exercent les scripts de l'outillage ; une entrée de mutant est son mutateur et son fichier, le rapport joint à chaque exécution détaillant chaque mutant ; outillage du contrôle documenté ; workflow ajouté aux autres workflows du CI ; chantier #6 barré dans le suivi de la Phase 2 (#31).

* **Environnement de validation fonctionnelle** : version de validation compilée sans contournement, le fichier de réponse hérité `TVRename/msbuild.rsp`, qui faisait de toute compilation une publication, étant retiré avec les profils de publication ; chantier #21 barré dans le suivi de la Phase 1 (#29).

* **Périmètre de deux chantiers de la Phase 4** précisé (décisions de l'utilisateur) : le recentrage sur les séries TV (#9) retire les films et leurs collections, et toutes les fonctionnalités, options et données qui ne concernent qu'eux ; le retrait des fonctions RSS (#12) s'étend aux fonctions NZB (intégration de SABnzbd) ; les notes de la Phase 2, le périmètre de l'analyse SonarQube Cloud et les exclusions de couverture du CI suivent (#28).

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
