# Carnet de voyage

Du 2 novembre au 23 décembre 2026, la classe CMID3b et moi avons parcouru un chemin d'apprentissage de la programmation de systèmes distribués.
Ce document relate les péripéties de ce voyage, qui part quand même un peu dans l'inconnu...

![](./supports/assets/mistylandscape.jpeg)

Les dernières fois que j'ai fait ce cours, nous avions fourni une base de code assez importante que chaque étudiant devait s'approprier pour pouvoir travailler dessus.
Nous avions choisi de faire cela pour minimiser le temps que nous aurions perdu à la recréer durant le cours.

Ce choix a posé pas mal de difficultés à plusieurs élèves, qui avaient de la peine à s'y retrouver.

Pour ce voyage-ci, nous avons décidé de nous en passer car le temps nécessaire à sa réalisation est aujourd'hui drastiquement réduit grâce aux outils d'intelligence artificielle.

Nous ne partons donc pas d'une base éprouvée et connue d'au moins une personne du groupe (moi) et nous allons tout construire ensemble.

<hr>

## Etape 1

### Lundi 2 novembre

Nous avons survolé rapidement les [objectifs](https://www.modulbaukasten.ch/module/321/1/fr-FR?degree=13d8d40b-6d82-eb11-a812-0022486f6f83&title=Programmer-des-syst%C3%A8mes-distribu%C3%A9s&tab=2) du module sur le site ICT-Berufsbildung, mais dans le but uniquement de repérer quelques mots-clés: transfert de données, protocole, authentification, surveillance, ...

Nous avons passé plus de temps à discuter du système (naturellement distribué) que nous allons construire ensemble.

Nous avons ensuite discuté de la manière dont nous allons travailler ensemble pour atteindre notre objectif. Voici ce qu'il en est ressorti:

- Tout notre code est dans un repo Github (et pas Jujutsu)
- On a une équipe de classe
- On fait du gitflow, que l'on travaille seul ou à plusieurs sur une branche
- Les branches sont mergées par pull request (PR). Nous devons encore préciser qui peut accepter les PR.
- On crée des issues dans le repo, on les suit dans un projet Kanban lié au repo
- Nous pratiquons le daily scrum
- Nous devons nommer un scrum master
- Nos communications passent par discord. Le serveur est [là](https://discord.gg/tBRJZqgZ), c'est Tony qui en est l'admin.

J'ai présenté une première ébauche de la manière dont l'évaluation sera faite dans ce module:

- Il y aura de l'oral
- Il pourrait y avoir de l'écrit old school (papier !)
- Il y aura des checkpoints

Il ne s'agit que d'une ébauche car certaines décisions n'ont pas encore été prises et d'autres dépendront des choix que nous ferons au début du parcours.

J'ai parlé du projet, qui consistera à faire un mediaplayer pour ambiancer les BP.

Nous avons commencé un petit [drill](./drills/OODrills/) OO pour nous assurer qu'on part avec un bagage minimal solide.

Quel que soit le chemin, nous ne pourrons pas nous entendre si nous ne maîtrisons pas tous le concept de sérialisation. Nous avons donc commencé par étudier la [théorie](./supports/Sérialisation.md) de ce concept, puis nous avons commencé un petit [drill](./drills/serialisation/).

On s'est arrêté là, on reprendra et finira les deux drills mercredi

### Mercredi 4 novembre

Nous allons finir les deux drills (OO et sérialisation).

Nous ferons un debrief avec un focus sur la comparaison par valeur (`Equals`) et par référence (`==`)

Chacun commencera la réalisation de son bureau, l'endroit à partir duquel il va gérer sa BP.  
Les conditions suivantes s'appliquent à tous les bureaux:
- Le stack utilisé pour son développement est libre. Le code est personnel, il est maintenu dans un repo personnel.
- Le modèle minimal de BP contient:
  - `ID`, un ID de commerce, une chaine de caractères satisfaisant `^COM-[A-Z]+(?:-[A-Z]+)*$` qui identifie le commerce sans ambiguïté
  - `Name`, le nom public du commerce ("Au bon croissant", "Le Mille-Miettes", ...)
  - `Logo`, une image, prévue pour être affichée en format 16:9
  - `Special`, un produit "phare"
- Votre bureau peut s'appuyer sur un modèle plus étendu, mais ces propriétés sont obligatoires
- Un produit est modélisé avec
  - (TBD)

On échange des BP sérialisées par Discord