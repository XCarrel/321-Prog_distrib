# Monitoring d’un système informatique complexe

## 1. Importance du monitoring

Le **monitoring** est un pilier fondamental de l’exploitation d'un système distribué.  
Il permet de :

- **Vérifier la disponibilité** des services (détection rapide des pannes)
- **Maintenir les performances** (latence, débit, saturation des ressources)
- **Anticiper les incidents** (tendances, capacité, dégradation progressive)
- **Faciliter le diagnostic** lors d’incidents (corrélation d’événements et de métriques)
- **Respecter les engagements de service**
  - Service Level Indicator (SLI) : une valeur chiffrée qui mesure l'état d'un élément (noeud, connexion, sous-système,...)
  - Service Level Objective (SLO) : une valeur totale que l'on vise à atteindre pour un élément
  - Service Level Agreement (SLA) : la valeur minimale que l'on garantit aux utilisateurs

Sans monitoring fiable, l’exploitation devient réactive au sens négatif : on découvre les problèmes via les utilisateurs plutôt que via des signaux techniques.

## 3. Monitoring actif (polling)

### Principe

Le monitoring actif repose sur une **interrogation périodique** des composants surveillés.  
Le système de monitoring envoie des requêtes à intervalles réguliers pour vérifier l’état ou mesurer des indicateurs.

### Exemples

- Ping ICMP pour vérifier la disponibilité d’un hôte
- Requêtes HTTP pour tester une API
- Lecture de métriques CPU, mémoire ou disque via un agent
- Exécution de scripts de santé (health checks)

### Avantages

- Contrôle centralisé et prévisible
- Mesures régulières et comparables dans le temps
- Détection rapide des indisponibilités franches

### Limites

- Charge supplémentaire sur les systèmes surveillés
- Granularité limitée par la fréquence de polling
- Difficulté à détecter des événements très brefs
- Scalabilité plus complexe à grande échelle

---

## 4. Monitoring réactif (événementiel)

### Principe

Le monitoring réactif repose sur la **remontée d’événements ou de données à l’initiative du système surveillé**.  
Les composants émettent des informations lorsqu’un événement se produit.

### Exemples

- Logs applicatifs envoyés en continu
- Événements système (crash, redémarrage, exception)
- Métriques poussées (push) vers le collecteur
- Alert

### Avantages

- Charge minimale (communication et CPU)
- Pas de données redondantes

### Limites

- Nécessite une gestion de détection des crash
- Consolidation de l'état à un instant T plus complexe

## 5. Logging (journalisation)

 le monitoring donne une image instantanée d'un système. Il est généralement accompagné d'une stratégie de Logging à fin de conserver des informations au fil du temps.  
Une erreur peut être
- Permanente: seule une intervention humaine peut éliminer la cause de l'erreur et rétablir le fonctionnement normal. Exemple : une alimentation qui "crame".
- Transitoire: la cause de l'erreur peut disparaître, soit suite à une intervention humaine, soit de manière automatique. Exemple : une application s'arrête, parce qu'elle n'a plus d'espace à disposition sur le disque, un job programmé effectue l'archivage de données, l'application peut redémarrer parce qu'elle a récupéré de la place.

 Si personne ne regarde le système de monitoring pendant une erreur transitoire, il est tout à fait possible de la rater.

 Le logging va donc permettre de gérer ce risque en enregistrant l'apparition de l'erreur et sa disparition dans un log.