# Foot MQTT Chat

Mettre en place dans la classe un chat MQTT permettant d'échanger des informations sur l'actualité du football

Nous commençons par convenir qu'une information footballistique se décompose ainsi :

`football / pays / ligue / ville / joueur`

Les valeurs acceptables pour les deux premiers niveaux sont :
1. Sujet principal, toujours `football`
2. pays
   - `suisse`
   - `france`
   - `allemagne`
   - `italie`
   - `espagne`
3. ligue
   - `1`
   - `2`
   - `3`

## Mission

1. Installer [MQTTX](https://mqttx.app/)
1. Vous connecter au broker du poste de votre prof
2. Vous abonner à un joueur et à une équipe (ville)
3. Vous faire envoyer des informations par un camarade concernant votre joueur et votre équipe
4. Varier les types d'abonnement (par niveau, tous niveaux) et valider le fonctionnement lors d'envoi de messages
5. Exercer les mécanismes de gestion de connexions (LWT,retained messages)

## Bilan
En commun

