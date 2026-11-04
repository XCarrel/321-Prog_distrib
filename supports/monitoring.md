# Monitoring d'un système informatique complexe

> **Fallacies :** 1 (fiabilité), 7 (coût).  
> **À retenir :** surveiller coûte (requêtes, messages, énergie). Interroger donne un état net mais charge le système ; écouter charge peu mais un **silence est ambigu** (mort ou rien à dire ?). Dans les deux cas il faut décider **ce qu'on mesure**, **à quelle fréquence** et **ce qu'on fait de l'alerte**.

## 1. Importance du monitoring

Le **monitoring** est un pilier de l'exploitation d'un système distribué. Il permet de :

- **vérifier la disponibilité** des services (détection rapide des pannes) ;
- **maintenir les performances** (latence, débit, saturation des ressources) ;
- **anticiper les incidents** (tendances, capacité, dégradation progressive) ;
- **faciliter le diagnostic** (corrélation d'événements et de métriques) ;
- **respecter les engagements de service** :
  - **SLI** (*Service Level Indicator*) : une valeur chiffrée qui mesure l'état d'un élément (nœud, connexion, sous-système…) ;
  - **SLO** (*Service Level Objective*) : la valeur totale que l'on vise à atteindre pour un élément ;
  - **SLA** (*Service Level Agreement*) : la valeur minimale garantie aux utilisateurs.

Sans monitoring fiable, l'exploitation est réactive au mauvais sens : on découvre les problèmes par les utilisateurs plutôt que par des signaux techniques.

## 2. Deux approches

| | **Actif (polling)** | **Réactif (événementiel)** |
|---|---|---|
| Qui prend l'initiative ? | le système de monitoring interroge | le système surveillé signale |
| Exemples | ping, requête HTTP, lecture de métriques, *health check* | logs envoyés en continu, événements (crash, redémarrage), métriques poussées, alertes, **heartbeat** |
| Avantages | contrôle centralisé, mesures régulières et comparables, détecte vite une indisponibilité franche | charge minimale, pas de données redondantes |
| Limites | charge sur les systèmes surveillés, granularité limitée par la fréquence, événements brefs manqués, passage à l'échelle difficile | exige de gérer la détection des crashs, état à un instant T plus difficile à consolider |

## 3. Monitoring actif (polling)

Interrogation **périodique** des composants : le système de monitoring envoie des requêtes à intervalles réguliers pour vérifier l'état ou mesurer des indicateurs (ping ICMP, requête HTTP sur une API, lecture CPU/mémoire/disque par un agent, scripts de santé).

## 4. Monitoring réactif (événementiel)

Remontée d'événements ou de données **à l'initiative du système surveillé** : logs, événements système, métriques poussées vers un collecteur, alertes. Pour détecter un crash, on a besoin d'un **heartbeat** (signal périodique) dont **l'absence** est elle-même l'alerte, ou d'un LWT ([MQTT](mqtt.md#last-will-and-testament-lwt)).

Exemple : un broker MQTT publie ses propres métriques sur le topic `$SYS/#` (nombre de clients connectés, messages reçus…). Voir [D10](../activites/drills/D10-monitoring-energie.md).

## 5. Logging (journalisation)

Le monitoring donne une image **instantanée**. Il s'accompagne d'une stratégie de logging pour conserver l'information **au fil du temps**. Une erreur peut être :

- **permanente** : seule une intervention humaine élimine la cause et rétablit le fonctionnement normal (une alimentation qui « crame ») ;
- **transitoire** : la cause peut disparaître, par une intervention humaine ou automatiquement (une application s'arrête faute d'espace disque, un job archive des données, elle redémarre une fois la place récupérée).

Si personne ne regarde le monitoring pendant une erreur transitoire, on peut la rater. Le log la conserve : on enregistre son **apparition** et sa **disparition** (les transitions, pas chaque occurrence).

## 6. Que mesurer ?

| Symptôme | Métrique qui le révèle |
|---|---|
| messages en retard | latence (p95), écart entre l'horodatage d'émission et de réception |
| effets doublés | doublons détectés (même `id`) |
| silence d'un service | écart entre deux battements (heartbeat) |
| ralentissement général | messages par seconde, saturation |
| service mort ou connecté | nombre de clients connectés |

Chaque mesure a un coût : la période d'un heartbeat se calcule ([Puissance et énergie](Puissance%20et%20énergie.md), [D10](../activites/drills/D10-monitoring-energie.md)).
