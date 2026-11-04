# Drills

Courts (≤ 20 min), **sans IA**, papier ou ligne de commande (`mosquitto_pub/sub`), indépendants du langage.  
Objectif : comprendre le mécanisme *avant* de demander du code. Chaque drill vise une fallacy.

| #   | Seq. | Fallacy                         | Thème                         |
|-----|------|---------------------------------|-------------------------------|
| D1  | S1   | 3 bande passante, 8 homogénéité | Sérialisation                 |
| D2  | S2   | heure                           | Temps                         |
| D3  | S2   | 2 latence, 1 fiabilité          | MQTT : QoS, retain            |
| D4  | S3   | 1 fiabilité, 5 topologie        | Mort d'un nœud (LWT)          |
| D5  | S4   | 1 fiabilité                     | Idempotence                   |
| D6  | S4   | 1, 6                            | Transaction distribuée        |
| D7  | S5   | 2, 1                            | Retry, backoff, jitter        |
| D8  | S5   | 5, 6                            | Cluster : qui répond ?        |
| D9  | S6   | 4 sécurité                      | Signature, chiffrement, rejeu |
| D10 | S7   | 7 coût                          | Métriques et énergie          |

---

### D1 — Sérialisation
Écrire à la main le JSON d'une `business` (voir [contrat](contracts/business.schema.json)), compter les octets. Comparer avec le dump hexadécimal d'une version binaire fournie.  
**Question** : un autre groupe renomme `price` en `cost`. Que se passe-t-il chez vous ? Qu'est-ce qui l'aurait évité ?

### D2 — Temps
Trois machines, horloges à +0 s, +4 s, −3 s. Six messages avec leur `ts` local. Reconstruire l'ordre réel.  
**Question** : que corrige NTP, que ne corrige-t-il pas ? Pourquoi ne peut-on pas trier un chat sur `ts` seul ?

### D3 — QoS et retain
Tableau de scénarios : un abonné est hors ligne au moment du publish, QoS 0/1/2, `retain` oui/non, session propre oui/non.  
**Prédire** qui reçoit quoi, puis vérifier à la CLI.

### D4 — Mort d'un nœud
Dessiner le diagramme de séquence : un client meurt sans `DISCONNECT`. Qui l'annonce, et quand ? Calculer le délai de détection pour un keep-alive de 60 s (le broker attend 1,5 × keep-alive).  
**Question** : quel délai acceptable pour une BP « fermée » dans l'annuaire ?

### D5 — Idempotence
Un registre bancaire de 6 opérations dont 2 doublons (QoS 1 = au moins une fois). Calculer le solde final *sans* puis *avec* déduplication par `id`.  
**Classer** : `solde = 100` et `solde += 100`, laquelle est idempotente ?

### D6 — Transaction
Fair Trade : la coop paie la BP, la banque plante *avant* d'accuser réception. Lister tous les états possibles du couple (stock, argent). Concevoir l'ordre des opérations et la compensation.

### D7 — Retry
Cinq clients, la banque revient en ligne. Tracer la charge reçue avec un retry fixe à 1 s, puis avec backoff exponentiel, puis avec jitter.  
**Question** : pourquoi le retry fixe peut-il re-tuer le service ?

### D8 — Cluster
Quatre annuaires identiques, un seul doit répondre. Écrire sur papier la règle d'élection avec des moyens MQTT (retain, LWT). Que se passe-t-il si deux se croient élus ?

### D9 — Sécurité
Quatre messages interceptés : un modifié, un rejoué, un à l'expéditeur usurpé, un lu par un tiers. Associer à chacun le mécanisme nécessaire (signature, chiffrement, nonce/horodatage).  
**Question** : que garantit le chiffrement seul ? Que garantit la signature seule ?

### D10 — Métriques et énergie
Trois symptômes (messages en retard, doublons, silence). Choisir pour chacun la métrique qui le révèle (latence p95, messages/s, écart de heartbeat).  
Calculer la durée de vie d'une batterie pour un capteur qui publie toutes les secondes puis toutes les minutes (valeurs fournies).
