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

Nous avons discuté un peu d'[actualité](https://www.24heures.ch/eca-vaud-perd-12-9-millions-pour-un-logiciel-jamais-livre-597188765739).

J'ai repris rapidement la portée générale du projet (voir plus loin pourquoi).

Vous avez avancé sur les deux drills (OO et sérialisation), sans les finir complètement.

Nous avons fait un debrief avec un focus sur la comparaison par valeur (`Equals`) et par référence (`==`).

Nous avons décidé de garder le thème des boulangerie-pâtisseries pour notre projet.

J'ai créé le repo, l'équipe et les droits d'accès.  

Chacun a commencé la réalisation de son app "backoffice", l'endroit à partir duquel on va gérer sa BP.  
Les conditions suivantes s'appliquent à tous les backoffices:
- Le stack utilisé pour son développement est libre. 
- Il doit pouvoir s'intégrer à notre infrastructure de communication (MQTT)
- Le code est personnel, il est maintenu dans un repo personnel.
- Le modèle minimal de BP contient:
  - `ID`, un ID de commerce, une chaine de caractères satisfaisant `^COM-[0-9]{4}$` qui identifie le commerce sans ambiguïté
  - `Name`, le nom public du commerce ("Chez Léon", "Le Mille-Miettes", ...)
  - `Logo`, une image prévue pour être affichée en format 16:9
  - `Special`, un produit "phare"
- Votre backoffice peut s'appuyer sur un modèle plus étendu, mais les propriétés ci-dessus sont obligatoires
- Votre app backoffice est capable d'afficher une autre BP que la vôtre
- Un produit est modélisé avec
  - `ID`, une chaine de caractères satisfaisant `^PROD-[0-9]{4}$` qui identifie le produit sans ambiguïté
  - `Name`, le nom d'affichage du produit
  - `Picture`, une photo prévue pour être affichée en format carré
  - `Price`, le prix de vente à l'unité, en francs (avec centimes)

TODO: définir ensemble l'emplacement de la référence commune pour les modèles

Objectifs individuels:
- Créer son backoffice "Hello World"
- Sérialiser son business dans un fichier JSON
- Redémarrer son backoffice avec relecture (désérialisation) du fichier
- Echanger des fichiers par Discord, afficher le business d'un autre dans son backoffice

DoD:
- [x] Le repo du projet commun est créé sur Github : https://github.com/ETML-INF/UMDD
- [x] Il est privé, accessible par tous les membres de l'équipe
- [ ] On a défini l'endroit où sont les modèles de référence. (_pas fait_)

### Lundi 9 novembre

On visait à:
- Finaliser les BP 1.0 : Modéliser, Afficher, Sérialiser le modèle (bouton), désérialiser le modèle (démarrage)
- Faire le checkpoint à 9h50 après la pause
- Voir la théorie NTP
- Définir le format du message NTP
- Être capable de relire une heure "UMDD-NTP" et l'afficher 
- Faire des User Stories

DoD:
- [x] Toutes les BP affichent et (dé)sérialisent
- [x] Format "UMDD-NTP": dans le [readme UMDD](https://github.com/ETML-INF/UMDD/tree/develop#Time)
- [ ] Toutes les BP affichent la même heure

### Mercredi 11 novembre

On va définir et mettre en place une infrastructure de communication par dessus le réseau TCP/IP déjà disponible.

Il y a plusieurs alternatives possibles. Nous pourrions envisager de chercher, essayer, tester, évaluer, bref: expérimenter pour ensuite faire notre choix. Le temps que cela prendrait serait toutefois prohibitif. Nous allons donc partir sans analyse sur une des solutions possibles: MQTT. Nous discuterons de la pertinence de ce choix en fin de module, une fois que nous l'aurons expérimenté.

Nous allons voir la [théorie MQTT](./supports/mqtt.md).

Nous avons ensuite des choix à faire ensemble:
- Quel broker MQTT utilisons-nous ? Comment en garantir la disponibilité continue ?
- Quelle est la structure des topics ?
- Comment est-ce que la "Source de temps" va-t-elle s'en servir pour diffuser l'heure ?

Nous reprendrons la pratique, chacun ayant pour but la désérialisation d'un objet porteur du timestamp unix.  

Ceux qui auront atteint cet objectif se mettront à coder une source de temps, seul ou à deux (max). Ils mettront leur code dans le dépôt UMDD, sur une branche ([gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow)).  

Etapes:
1. Être capable d'envoyer un message json sur un topic choisi à un broker MQTT
2. Envoyer son heure courante chaque seconde  
3. Dockeriser  
4. Permettre de définir un décalage temporel  
5. Permettre de définir un facteur d'accélération du temps

Quand nous en aurons trois qui semblent prêtes au moins à être dockerisées, nous en sélectionnerons une qui deviendra officielle (merged). Les autres seront mises de côté (mais pas abandonnées!).

**Attention**: Il s'agit là de code commun cette fois. En fin de module, tout le monde est sensé être en mesure de débugger ou améliorer ce composant!  

Chacun ajoutera à sa BP la réception du message de temps et l'affichage dynamique.

DoD:
- [ ] Le broker MQTT de Dousville est défini et documenté
- [ ] Le broker est disponible
- [ ] Le topic pour la diffusion de l'heure est défini et documenté
- [ ] Une source de temps diffuse l'heure commune
- [ ] Toutes les BP affichent l'heure dynamiquement
