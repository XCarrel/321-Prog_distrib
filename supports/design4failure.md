# Design for failure

Une architecture distribuée, présente de nombreux avantages que nous avons discuté dans d'autres documents. Mais elle a également ses défauts :

- Plus il y a de nœuds dans le système, plus il y a de risques de panne, matérielle ou logicielle
- Le risque de panne est encore accru par le fait que l'échange de données fait à l'aide d'un réseau
- La diversité matérielle et/ou logicielle augmente elle aussi les risques en raison de différences inattendues entre les éléments du système

On voit donc qu'il est déraisonnable de s'imaginer que tout va bien se passer!

Le concept de **design for failure** équivaut à **embrasser la loi de Murphy** : 

> Tout ce qui est susceptible de mal tourner tournera mal.  

Appliquer le design for failure dans un système distribué, c'est se préparer davantage à traiter des erreurs qu'à fonctionner dans un monde idéal.  
Cela permet d'en améliorer la résilience.

> **Définition**  
>   La résilience d'un système d'information est sa capacité à se récupérer rapidement et efficacement après une perturbation

![resilience2.png](assets/resilience2.png)

Cette image n'est pas très réaliste, entre autres par le fait que toutes les maisons sont identiques. Dans la réalité, il est fort probable que chaque maison soit l'œuvre d'un architecte différent et donc que l'habitant d'une maison ne peut pas faire l'hypothèse que tout se passe à l'identique chez ses voisins.

Le thème est très vaste et ne peut pas être décrit de manière unique et précise.

Nous allons donc nous contenter ici d'aborder certains concepts qui, selon les circonstances, sont judicieux d'implémenter dans son système.

On sépare ces concepts en trois phases distinctes :
- La détection et l'identification
- La réaction
- La reprise

La détection est traitée par le monitoring et logging que nous avons vu précédemment.

<hr>

# Réaction

Une fois qu'une panne ou une erreur a été détectée, il faut agir en fonction de la gravité des conséquences de cette erreur.  

La première chose à faire est de **qualifier la nature** de l'erreur. Il s'agit de déterminer si l'erreur (ou la panne) est
- **Transitoire**, c'est-à-dire qu'elle est susceptible de disparaître spontanément. 
Exemples : pic de charge, saturation momentanée (espace disque insuffisant), perte de réseau brève, indisponibilité momentanée d'un service, ...
- **Permanente**, c'est-à-dire qu'elle nécessite une intervention humaine. 
Exemples : bug applicatif, mauvaise configuration, machine arrêtée, donnée invalide, syntaxe de commande incorrecte, ...

La nature de l'erreur va ensuite guider le processus de réaction.  

Illustration : vous êtes en montagne et vous voulez chercher en ligne l'heure du prochain train pour rentrer chez vous. Le réseau est de mauvaise qualité, l'erreur est transitoire, elle disparaîtra (et vous obtiendrez l'information) lorsque vous vous serez déplacés. Mais si vous vous êtes trompé dans l'URL (par exemple www.cffff.ch), l'erreur est permanente et il ne servira à rien de vouloir réessayer plus tard.

### Timeout

Définition:
> Un timeout est un délai maximal accordé à une opération distante pour répondre avant de considérer qu’elle a échoué.

Dans un système qui s'appuie sur des communications asynchrones, il est indispensable de mettre en place une bonne gestion des timeouts.  
Sans cela, on court le risque de bloquer indéfiniment un service, voire le système tout entier.

Un timeout ne doit pas automatiquement déclencher un retry.  
Il doit déclencher une décision stratégique.

### Fail Fast

C'est une évidence : plus la réaction est rapide, mieux c'est !  

La durée des timeouts joue naturellement un rôle important dans la le temps de réaction.

Mais il est également très important de bien structurer le déroulement des opérations en plaçant les mécanismes de détection, traitement et récupération d'erreurs le plus en amont (= le plus tôt) possible d'un processus de travail.  
Le principe du **fail fast** nous pousse à réfléchir aux moyens d'intercepter les erreurs le plus tôt possible pour éviter de les propager inutilement à l'intérieur de notre système.  

Un des exemples les plus parlants est celui de la saisie de données par un utilisateur:
- Le système demande son année de naissance à un utilisateur dans un formulaire d'édition de profil
- L'utilisateur laisse par erreur la date du jour actuel dans le champ qui lui est proposé
- Il envoie le formulaire
- Le système enregistre la date
- Une action ultérieure dans laquelle l'âge de la personne intervient échoue en raison de l'erreur sur l'âge calculé

Cet exemple est un petit peu extrême. Il n'y a pas besoin de beaucoup de réflexion pour se rendre compte que le système ne devrait pas enregistrer la date du jour comme date de naissance.  
En pensant "fail fast", on va modifier le système pour qu'il effectue le calcul de l'âge dès la soumission du formulaire et qu'il réagisse en cas de résultat manifestement faux.

Mais on peut aller plus loin dans l'attitude "fail fast"!  
Sauriez-vous dire comment ?
<details>
<summary>Comme ça!</summary>
On modifie l'interface pour qu'elle ne permette même pas de soumettre le formulaire tant que la date saisie n'est pas une date acceptable
</details>

### Mode dégradé

Certains systèmes ont la capacité à modifier leur mode de fonctionnement lorsqu'ils détectent une panne qui les empêche de fonctionner de manière normale.  
Ces systèmes ne fournissent alors plus qu'une partie des services pour lesquels ils sont conçus.  
On dit alors qu'ils sont en mode dégradé.  
Ils peuvent dégrader leurs fonctionnalités. Par exemple: un système de réservation qui permet de consulter les réservations existantes, mais qui ne permet plus d'en créer de nouvelles.  
Une autre stratégie consiste à diminuer intentionnellement la performance pour alléger la charge de travail. Le même système de réservation pourra par exemple accepter moins de créations de réservations par minute, le temps d'effectuer des opérations de maintenance.  
On parle alors **throttling**.

Exemple d'une technique classique permettant de gérer le throttling: le **Token Bucket**.  
- Un client se connecte, on lui donne un seau rempli de jetons
- À chacune de ses requêtes
- Si le seau est vide, on rejette la requête.  
- Sinon, on prélève un jeton dans le seau et on traite la requête
- Parallèlement, le système rajoute des jetons dans les seaux de tous ses clients au rythme qui lui convient. On veille à ne pas dépasser la capacité du seau.
 
De cette manière, le système contrôle la charge totale des requêtes qui lui sont soumises.  
Il lui suffit de diminuer le nombre de jetons distribués pour faire baisser sa capacité de traitement apparente.

### Graceful Shutdown.

Si un système nécessite d'être arrêté, que ce soit en raison d'un problème ou d'une activité planifiée, il est important de gérer proprement l'arrêt du service rendu aux applications externes.  

En effet, toute opération interrompue brutalement devra être gérée d'une manière particulière par le client du service. Cela créera une surcharge de travail globale qui est souhaitable d'éviter.

D'autre part, le graceful shutdown permet généralement d'organiser le transfert du service vers un autre système d'une manière qui reste transparente pour les clients du service.

### Redondance

La redondance pour un composant consiste à prévoir un second composant qui est un jumeau du premier et qui est prêt à prendre le relais en cas de défaillance.

Cela peut s'appliquer autant à du matériel qu'à du logiciel.

La caractéristique principale de la redondance, c'est que le composant jumeau est passif. Il ne participe pas aux opérations. Il ne fait que se préparer à remplacer.  
On peut tout à fait faire le parallèle avec un remplaçant dans une équipe sportive: quand un joueur se blesse et n'est plus capable de tenir son poste, on le remplace par un autre joueur qui a - normalement - les mêmes qualités.

Lorsque la panne survient, la réaction consiste à effectuer un **basculement** vers le composant redondant.

### Cluster

Le principe de clustering consiste à mettre ensemble plusieurs composants similaires qui unissent leurs forces pour assurer un service, qui sera vu comme unique de l'extérieur.

Il diffère de la redondance par le fait que chacun des composants est actif. Les composants d'un cluster doivent donc collaborer et se coordonner afin de se partager le travail.

En cas de panne, la réaction consiste à réorganiser le cluster.

Plus de détails [ici](./cluster.md)

<hr>

# Reprise

La troisième phase dans le cycle de vie d'un problème système est celle durant laquelle le système revient dans un état d'opérations nominales


### Timeout

### Stratégie de retry

La manière dont les composants d'un système essayent de reprendre le cours de leurs activités joue un rôle essentiel dans la résilience du système.

Une mauvaise stratégie de reprise peut mener à des effets pires que le problème initial.  
On parle ici d'effet **domino** ou **boule de neige**.

Exemple:
- Un serveur accepte un nombre de connexions de clients relativement élevé
- Ayant une grosse charge de travail, son temps de réponse augmente
- Un (ou plusieurs) de ses clients s'impatiente (si c'est un humain) ou déclenche un timeout (si c'est une machine) 
Il renvoie à nouveau sa dernière requête au serveur
- Le serveur fait donc face à une charge de travail encore plus grande, puisqu'il va devoir traiter cette commande en double
- La charge ayant augmenté, le temps de réponse s'allonge encore plus!
- De plus en plus de clients déclenchent le renvoi de leur requête
- La charge sur le serveur ne fait qu'augmenter jusqu'au moment où ... crash

Une technique permettant d'éviter un tel scénario est l'**Exponential Timeout** (ou backoff exponentiel).  
Elle consiste à espacer progressivement les tentatives de reconnexion après une défaillance. Cela réduit la pression sur le système et évite des surcharges inutiles.

Principe:

- Après un échec, le délai avant de réessayer augmente de manière exponentielle, par exemple :
    - Première tentative : 1 seconde.
    - Deuxième tentative : 2 secondes.
    - Troisième tentative : 4 secondes.
    - Quatrième tentative : 8 secondes, etc.
- La durée maximale (appelée **backoff cap**) est définie pour éviter un délai infini.
- Dans la mesure du possible, on offre la possibilité à l'utilisateur de déclencher une tentative de manière manuelle. 

Avantages

- Réduit le risque de surcharge du système (surtout si plusieurs clients réessaient en même temps).
- Donne au système défaillant le temps de récupérer.

Inconvénient

- Le temps de reprise peut être assez long si le service est rétabli - par exemple - juste après la 10e tentative infructueuse et donc au début d'une longue période d'attente.

Un autre scénario problématique est celui où l'erreur est causée par l'accès simultané d'un grand nombre de clients à un service.  
Exemple : 10h du matin le 25 mars : ouverture de la billetterie de Paleo.

Si le serveur se met en erreur et rejette les requêtes, et que tous les clients appliquent exactement la même stratégie de retry, l'erreur va certainement se reproduire après 1, 2, 4, 8, 16 secondes!

Le problème est moindre lorsqu'il s'agit d'utilisateur humain car il y a peu de chance que les requêtes restent synchronisées.  
Mais lorsqu'il s'agit de composants automatisés, les bonnes pratiques veulent que l'on **ajoute un jitter**, c'est-à-dire de faire aussi varier le délai de manière aléatoire.

### Circuit breaker
Le **circuit breaker** (fusible) est un design pattern utilisé dans les systèmes distribués pour **protéger les services critiques** et éviter un effet domino en cas de défaillance.  
Inspiré des disjoncteurs électriques, il agit comme une protection qui **interrompt temporairement les appels** à un service défaillant.

Un circuit breaker passe par trois états principaux :

1. **Closed (Fermé)** :
    - Le service fonctionne normalement.
    - Les requêtes sont transmises au service cible.

2. **Open (Ouvert)** :
    - En réaction à une situation problématique, le circuit passe en mode **ouvert**.
    - Les requêtes ne sont plus envoyées au service cible mais échouent immédiatement, évitant une surcharge inutile.

3. **Half-Open (Semi-ouvert)** :
    - Après un délai défini, le circuit tente d’envoyer quelques requêtes (appelées *test probes*) en suivant l’exponential backoff dont nous avons parlé plus haut.
    - Si ces requêtes réussissent, le circuit repasse à **closed**.
    - Sinon, il reste en mode **open** pour un nouveau délai.

<hr>

# Synthèse

Tout bon système distribué:
- A été conçu avec la conviction que des pannes et des erreurs surviendront (**design for failure**)
- Réagit aussi tôt que possible à la moindre anomalie (**fail fast**)
- Contient un maximum de nœuds ayant la capacité de reprendre leur activité normale automatiquement sans intervention humaine (**résilience**)

Pour concevoir un système résilient, il est indispensable d'anticiper les échecs, de les accepter, et de prévoir comment les traiter.