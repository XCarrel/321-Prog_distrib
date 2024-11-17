# Idempotence, en pratique

## HTTP
[La théorie](../../supports/idempotence.md) aborde les méthodes POST et PUT sans parler des autres...

1. Créer un document d’une page maximum qui résume l’état des lieux de toutes les méthodes HTTP en lien avec 
leur idempotence et leur impact, soit :

- GET
- POST
- PUT
- PATCH
- DELETE
- TRACE
- HEAD
- OPTIONS
- CONNECT
- PRI

Il s’agit donc pour chaque verbe, d’indiquer si la spécification indique s’il doit être idempotent ainsi que donner
un exemple.

## Powercher et idempotence
Powercher ne gère pas complètement l’idempotence "out of the box" et admettons que des messages concernant 
de l’argent 💰 s’échangent sur Powercher...

### Version A

1. Ajouter une gestion des messages dupliqués basée sur l’enveloppe au niveau de la réception de message depuis la
   maison.
2. Déporter la logique au niveau de l’agent en ajoutant un `flag` optionnel qui permet de choisir si on veut un agent
   idempotent ou pas (`pas d’invocation de OnMessageReceived si un message dupliqué arrive`).
