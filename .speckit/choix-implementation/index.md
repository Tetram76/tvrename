# Choix d'Implémentation

* [Environnement de validation fonctionnelle](environnement-de-validation-fonctionnelle.md) - Version d'une Pull Request compilée depuis ses seules sources en version de validation (constante VALIDATION), dossier de données dans le dossier temporaire et instance à part, lancée et arrêtée par deux scripts PowerShell ; à lire pour modifier les scripts de validation ou la version de validation.
* [Organisation du code](organisation-du-code.md) - Projets de la solution et leur rôle, projets de tests ; à lire pour ajouter un projet ou placer du code.
* [Outillage de développement](outillage.md) - Outillage .NET : xunit, CI sur exécuteur Windows, tests du dépôt sans l'application, couverture et analyse SonarQube Cloud, outillage du contrôle qualité hebdomadaire (Stryker.NET, règles de style, API de SonarQube Cloud) ; à lire pour tout test, build ou workflow.
* [Choix d'Implémentation](perimetre.md) - Ce que recense choix-implementation/ : choix de l'agent, leur raison et les alternatives écartées, rien de ce que le code montre ; remis en cause s'il le faut.
* [Versions et bibliothèques](versions-et-bibliotheques.md) - .NET 10 LTS (net10.0-windows), montée suivante vers .NET 12 ; bibliothèques héritées du fork.
