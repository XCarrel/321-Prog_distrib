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
# Introduction à l'Internet des Objets (IoT)
---
layout: image
# the image source
image: ./assets/iot.jpg
---

---

- **Qu'est-ce que l'IoT ?**
    - Définition et concepts de base
    - Exemples d'applications : maison intelligente, santé, agriculture

---

# L'importance de la synchronisation dans l'IoT

<v-clicks depth="2">

- **Pourquoi la synchronisation est-elle cruciale ?**

    - Horodatage des données
    - Coordination des dispositifs
    - Maintien de la cohérence dans les applications distribuées
  
</v-clicks>

---

# Protocole NTP (Network Time Protocol)

<v-clicks depth="2">

- **Qu'est-ce que NTP ?**
    - Définition : protocole pour synchroniser les horloges des appareils
    - Fonctionnement de base : serveurs NTP et clients
    - Exemples d'utilisation dans les systèmes IoT

- **Avantages du NTP**
    - Fiabilité : réduction des erreurs de synchronisation
    - Précision : synchronisation à la milliseconde pour les applications critiques

</v-clicks>

---

# La sérialisation dans l'IoT

<v-clicks depth="2">

- **Qu'est-ce que la sérialisation ?**
    - Conversion des données en un format transportable
    - Nécessaire pour l'envoi de données entre dispositifs

- **Formats de sérialisation courants**
    - JSON : léger et lisible par l'homme
    - Protobuf : compact et performant
    - XML : extensible, mais plus lourd

- **Exemples de sérialisation dans les applications IoT**
    - Échanges de données capteurs <--> serveurs
    - Communication entre les microservices

</v-clicks>

---

# Les microservices dans l'architecture IoT

<v-clicks depth="2">

- **Pourquoi utiliser les microservices ?**
    - Modularité : chaque service gère une tâche spécifique
    - Scalabilité : possibilité de déployer indépendamment chaque microservice
    - Résilience : services isolés, impact réduit en cas de panne

- **Exemple d'architecture microservices dans l'IoT**
    - Service de gestion des capteurs
    - Service de traitement de données
    - Service d'analyse et de prévisions

</v-clicks>

---

<v-clicks depth="2">

# Exemple d'architecture IoT utilisant NTP, sérialisation et microservices

- **Synchronisation avec NTP**
    - Chaque dispositif utilise un serveur NTP pour des horodatages cohérents
- **Communication via la sérialisation**
    - Données sérialisées (JSON, Protobuf) pour transmission efficace
- **Architecture microservices**
    - Services indépendants pour le traitement et l'analyse des données

</v-clicks>

---

# Défis et bonnes pratiques

<v-clicks depth="2">

- **Défis dans les systèmes IoT**
    - Fiabilité de la connexion
    - Sécurité des données
    - Gestion des mises à jour

- **Bonnes pratiques**
    - Utiliser des protocoles sécurisés pour les transmissions
    - Optimiser la sérialisation pour réduire la charge réseau
    - Concevoir des microservices résilients

</v-clicks>

---

# Conclusion

- **L'IoT et l'impact des technologies comme NTP, sérialisation et microservices**
    - Importance de la synchronisation, de l'échange de données et de la modularité
    - L'avenir de l'IoT repose sur des architectures évolutives et sécurisées
