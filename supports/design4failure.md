# Design for failure

> **Fallacies :** 1 (fiable), 2 (latence nulle), 5 (topologie constante).  
> **À retenir :** les pannes ne sont pas une exception, ce sont des données du problème. On conçoit en trois temps : **détecter**, **réagir**, **reprendre**. Une mauvaise reprise (retry naïf) peut faire pire que la panne.

Une architecture distribuée a de nombreux avantages, mais aussi des défauts :

- plus il y a de nœuds, plus il y a de risques de panne, matérielle ou logicielle ;
- l'échange par le réseau accroît encore ce risque ;
- la diversité matérielle et logicielle ajoute des différences inattendues.

Il est donc déraisonnable de croire que tout va bien se passer. Le **design for failure** consiste à **embrasser la loi de Murphy** (*tout ce qui est susceptible de mal tourner tournera mal*) : se préparer davantage à traiter des erreurs qu'à fonctionner dans un monde idéal.

> **Résilience** : capacité d'un système d'information à se récupérer rapidement et efficacement après une perturbation.

![resilience2.png](assets/resilience2.png)

L'image est irréaliste : en pratique chaque maison est l'œuvre d'un architecte différent, et un habitant ne peut pas supposer que tout se passe à l'identique chez ses voisins.

Trois phases : la **détection** (traitée par le [monitoring et le logging](monitoring.md)), la **réaction**, la **reprise**.

---

# Réaction

Il faut agir en fonction de la gravité des conséquences. D'abord **qualifier** l'erreur :

- **transitoire** : elle peut disparaître spontanément (pic de charge, saturation momentanée, perte de réseau brève, service momentanément indisponible) ;
- **permanente** : elle exige une intervention humaine (bug, mauvaise configuration, machine arrêtée, donnée invalide).

Exemple : en montagne, vous cherchez l'horaire du train avec un mauvais réseau : erreur transitoire, elle disparaîtra quand vous vous serez déplacé. Si vous vous trompez d'URL (`www.cffff.ch`), l'erreur est permanente et réessayer plus tard ne servira à rien.

### Timeout

> Un *timeout* est le délai maximal accordé à une opération distante pour répondre avant de la considérer comme échouée.

En communication asynchrone, une bonne gestion des timeouts est indispensable : sans elle, on risque de bloquer un service, voire tout le système. Un timeout ne doit pas déclencher automatiquement un retry, il doit déclencher une **décision stratégique** (réessayer ? abandonner ? dégrader ?).

### Fail Fast

Plus la réaction est rapide, mieux c'est. La durée des timeouts joue un rôle, mais il faut aussi placer détection et traitement des erreurs le plus **en amont** possible, pour ne pas les propager.

Exemple : un formulaire de profil dont le champ « année de naissance » garde par erreur la date du jour. Le système l'enregistre, et une action ultérieure qui calcule l'âge échoue. Avec *fail fast*, on contrôle dès la soumission. Peut-on aller plus loin ?

<details>
<summary>Comme ça !</summary>
On modifie l'interface pour qu'elle ne permette même pas de soumettre le formulaire tant que la date saisie n'est pas acceptable.
</details>

### Mode dégradé

Un système peut changer de mode de fonctionnement face à une panne et ne fournir qu'une partie de ses services : un système de réservation qui permet de consulter mais plus de créer. Autre stratégie : réduire intentionnellement la performance pour alléger la charge (*throttling*).

Technique classique de throttling, le **Token Bucket** : chaque client reçoit un seau de jetons ; chaque requête en consomme un ; seau vide, la requête est rejetée ; le système remplit les seaux au rythme qui lui convient (sans dépasser la capacité). Diminuer le nombre de jetons distribués réduit la capacité apparente.

### Graceful Shutdown

Arrêter proprement un service (panne ou maintenance) : les opérations interrompues brutalement obligent les clients à des traitements particuliers et augmentent la charge globale. Un arrêt propre permet aussi de **transférer le service** à un autre système de façon transparente.

### Redondance

Un composant **jumeau passif**, prêt à prendre le relais (matériel ou logiciel), comme le remplaçant d'une équipe sportive. À la panne : **basculement** (*failover*).

### Cluster

Plusieurs composants similaires, tous **actifs**, qui collaborent pour offrir un service vu comme unique de l'extérieur. En cas de panne, le cluster se réorganise. Voir [Cluster](cluster.md).

---

# Reprise

Le système revient à un fonctionnement nominal.

### Stratégie de retry

La manière de reprendre joue un rôle essentiel. Une mauvaise stratégie provoque un effet **domino** (ou boule de neige) :

1. un serveur très sollicité répond plus lentement ;
2. des clients déclenchent un timeout et **renvoient** leur dernière requête ;
3. le serveur traite alors le travail en double, répond encore plus lentement ;
4. de plus en plus de clients réessayent… jusqu'au crash.

**Backoff exponentiel** : espacer les tentatives de façon exponentielle (1 s, 2 s, 4 s, 8 s…), avec un **plafond** (*backoff cap*) pour éviter un délai infini, et si possible un retry manuel. Avantage : on laisse le système récupérer. Inconvénient : si le service revient juste après un essai raté, l'attente peut être longue.

**Jitter.** Si tous les clients appliquent exactement la même stratégie (ouverture de la billetterie de Paléo : 1, 2, 4, 8, 16 secondes), ils frappent **en même temps** à chaque vague. Avec des composants automatisés, on **ajoute du hasard** au délai. Voir [D07](../activites/drills/D07-retry.md).

> Un retry n'est sûr que si l'opération est **idempotente** ([Idempotence](idempotence.md)) : sinon on risque l'effet doublé.

### Circuit breaker

Un *circuit breaker* (fusible) **protège les services critiques** de l'effet domino en interrompant temporairement les appels vers un service défaillant. Trois états :

1. **Closed (fermé)** : tout va bien, les requêtes passent.
2. **Open (ouvert)** : situation problématique, les requêtes **échouent immédiatement** sans solliciter le service cible.
3. **Half-open (semi-ouvert)** : après un délai (suivant le backoff), quelques requêtes test sont envoyées. Si elles réussissent, retour à *closed* ; sinon, *open* pour un nouveau délai.

---

# Synthèse

Un bon système distribué :

- a été conçu avec la conviction que des pannes surviendront (**design for failure**) ;
- réagit aussi tôt que possible à la moindre anomalie (**fail fast**) ;
- compte un maximum de nœuds capables de reprendre seuls leur activité (**résilience**).

Pour concevoir un système résilient, il faut anticiper les échecs, les accepter, et prévoir comment les traiter.

## Pour s'exercer

- Drill : [D07](../activites/drills/D07-retry.md), [D08](../activites/drills/D08-cluster.md) · Chat : [C6](../activites/chat/C6-reconnexion.md)
