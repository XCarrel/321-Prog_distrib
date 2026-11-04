# C5 — Ignorer les doublons

| | |
|---|---|
| Thématique | [Idempotence](../../thematiques/05-idempotence.md) |
| Drill préalable | [D05](../drills/D05-idempotence.md) |
| Fallacy | 1 (fiabilité) |
| Théorie | [Idempotence](../../supports/idempotence.md) |

## But
Un même message reçu deux fois n'est affiché et traité qu'une fois.

## Étapes

**1. Un `id` frais par message, un `id` stable par réémission.** Si votre programme renvoie un message (échec d'envoi, retry), il réutilise **le même `id`**.  
→ Vérifiez par lecture de votre code : où l'`id` est-il créé ? Un retry le recrée-t-il ?

**2. Provoquez un doublon.** Publiez la même enveloppe deux fois à la main (même `-m`).  
→ Vous devez observer : sans protection, votre chat l'affiche deux fois.

**3. Dédupliquez par `id`.** Gardez les `id` déjà vus ; ignorez ceux qu'on a déjà traités.  
→ Vérifiez : l'étape 2 n'affiche plus qu'une ligne. Journalisez `doublon ignoré : <id>`.

**4. Bornez la mémoire.** Choisissez et écrivez dans `NOTES.md` une règle de purge. Candidates : garder les N derniers `id` ; garder ceux des X dernières minutes (grâce au `ts`).  
→ Vérifiez : envoyez 1500 messages distincts (une petite boucle de votre cru), puis rejouez le tout premier. Avec la règle « 1000 derniers », il repasse ; avec « 10 minutes », non. Lequel défendez-vous ?

**5. Vraie livraison en double.** En QoS 1, le broker peut vous remettre un message que vous aviez déjà reçu si votre accusé s'est perdu. Constatez-le dans le fil d'une capture (`mosquitto_sub -d` affiche les paquets et le drapeau `DUP`) ou expliquez par écrit comment vous le provoqueriez avec `toxic add -t reset_peer`.

## Terminé quand

- [ ] doublon fabriqué puis ignoré
- [ ] règle de purge écrite et justifiée
- [ ] vous pouvez dire pourquoi QoS 1 impose cette étape et QoS 2 ne l'aurait pas imposée (et pourquoi on ne l'utilise pas ici)

## Si vous utilisez une IA
Prompt : nommer l'idempotence, la clé de déduplication (`id`), la règle de purge **choisie**, et le test des 1500 messages.
