---
name: validation-fonctionnelle
description: >-
  Drives the functional validation of a chantier the user decides on (visible
  behaviour of the application, retro-documentation of `fonctionnel/` or
  `modele-metier/`, ticket marked « Validation fonctionnelle : requise ») on
  this repository. Use as soon as the content of such a chantier is written and
  its local checks pass — before any other step (PR description polish, marking
  the PR ready) — and whenever the user gives feedback on a version under
  validation.
---

# Validation fonctionnelle

The `.speckit/` is the source of truth:
`.speckit/gestion-projet/revue-tickets/validation-fonctionnelle.md`. **Read it
again, in full, before step 1**: this skill only orders its steps, it adds no
rule.

The one way the user validates a visible change: the version of the draft pull
request, built and started on their Windows machine by the validation
environment (a chantier of Phase 0, `.speckit/suivi-implementation/`), with a
data folder of its own (`/userfilepath:`), never their own settings and caches.
Never anything else in its place — no screenshots sent in the conversation, no
application started by the agent. A retro-documentation alone is validated on
the text of the pull request.

## Steps, in this order

1. **Content complete, local checks green** → commit and push the branch, and
   open the pull request **as a draft** (template of `Tetram76/.github`), right
   away.
2. **Ask for the validation**, in one message, as the concept prescribes:
   - for a visible change: window by window, what is new or changed, the
     state or case to observe; what does not change;
   - for a retro-documentation: each concept proposed, and each behaviour of
     the existing application the agent finds questionable (described as it
     is, not fixed);
   - for a visible change, **every command**, complete and ready to copy,
     each in its own block, in the order the user runs them: update of their
     clone, start of the version under validation (number of the pull request
     included), stop and cleanup. Never refer to an earlier message nor to the
     documentation.
   Then wait: nothing else on the chantier until the user answers.
3. **Each feedback** → fixed in the draft PR (code, tests, concepts), pushed
   at once, then step 2 again with the new head commit.
4. **Explicit validation of the head commit** → only then the SonarQube Cloud
   analysis is handled and the PR is marked « ready for review »
   (`codex-review-loop`).

A question of the user about this procedure is answered by quoting the
`.speckit/` concept above, never by inventing another way.
