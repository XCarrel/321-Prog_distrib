---
theme: default
title: Intro
class: text-center
drawings:
  persist: true
transition: slide-left
mdc: true
lineNumbers: true
layout: center
---

---
layout: image
# the image source
image: ./assets/mqtt.webp
---

---

### Intro

- Introduction à MQTT (Message Queuing Telemetry Transport)
- Un protocole de messagerie léger pour l'IoT et au-delà

---



### Points abordés

- Introduction à la messagerie en file d'attente
- Qu'est-ce que MQTT ?
- Composants clés de MQTT
- Fonctionnement de MQTT
- Avantages de MQTT
- Concepts avancés : LWT, Retain Message, Keep Alive
- Gestion des pannes : Utilisation d’un cluster MQTT
- Applications de MQTT
- Conclusion et Q&R

---

### Introduction à la messagerie en file d'attente

- **Définition** : Une forme de communication asynchrone utilisée pour connecter des systèmes distribués.
- **Exemple** : Utilisée dans l'IoT, la messagerie en temps réel, etc.
- **Défis** : Les protocoles traditionnels peuvent être lourds et peu adaptés aux ressources limitées.

---

### Qu'est-ce que MQTT ?

- **Acronyme** : Message Queuing Telemetry Transport
- **Vue d'ensemble** : Protocole de messagerie léger, conçu pour des appareils contraints et des réseaux à faible bande
  passante.
- **Origine** : Développé par IBM et standardisé par OASIS.

---

### Composants clés de MQTT

- **Broker (Courtier)** : Serveur central pour recevoir et distribuer des messages.
- **Client** : Appareil qui peut publier et s'abonner à des messages.
- **Sujet (Topic)** : Chaîne utilisée pour filtrer les messages.
- **Messages** : Les données échangées.

---

### Fonctionnement de MQTT

- **Modèle Pub/Sub** : Un éditeur publie sur un sujet, et les abonnés reçoivent les messages associés.
- **Niveaux de Qualité de Service (QoS)** :
    - **QoS 0** : Au plus une fois.
    - **QoS 1** : Au moins une fois.
    - **QoS 2** : Une fois seulement.

---

### Concepts avancés de MQTT

- **Last Will and Testament (LWT)** : Notification de déconnexion inattendue d’un client.
- **Retain Message** : Message conservé sur le broker jusqu'à être remplacé.
- **Keep Alive** : Intervalle de "ping" pour maintenir la connexion active.

---

### Gestion des pannes : Utilisation d’un cluster MQTT

- **Problème** : En cas de panne de serveur (broker), les messages risquent de ne pas être délivrés, ce qui entraîne une
  interruption de service.
- **Solution : Mise en place d’un cluster** :
    - **Cluster MQTT** : Plusieurs brokers MQTT travaillent ensemble, ce qui permet une redondance et une continuité de
      service.
    - **Répartition de la charge** : Chaque broker dans le cluster partage la charge, permettant de gérer un plus grand
      nombre de connexions simultanées.
    - **Haute disponibilité** : En cas de panne d’un broker, les autres prennent le relais sans perte de données.
    - **Mise en miroir des sessions** : Les informations de session, les abonnements et les messages retenus sont
      partagés entre brokers, permettant aux clients de récupérer leurs données après une déconnexion.
- **Technologies courantes** :
    - **Kubernetes** : Orchestration de conteneurs pour automatiser le déploiement et la gestion d'un cluster MQTT.
    - **Mosquito, RabbitMQ, Redis avec EMQTT ou Apache Kafka** : Utilisés comme stockage pour le clustering MQTT afin de stocker les messages
      temporaires et les sessions.

---

### Avantages de MQTT

- **Efficacité** : Faible bande passante et charge réseau minimale.
- **Simplicité** : Architecture légère.
- **Fiabilité** : QoS, LWT, et clustering pour la résilience.
- **Scalabilité** : Supporte un grand nombre d'appareils connectés.

---

### Applications de MQTT

- **IoT** : Capteurs, domotique, appareils connectés.
- **Véhicules connectés** : Systèmes de navigation.
- **Messagerie en temps réel** : Notifications.
- **Industrie** : Automatisation.

---