# C2 — Ordre des messages

| | |
|---|---|
| Thématique | [Temps](../../thematiques/02-temps.md) |
| Drill préalable | [D02](../drills/D02-temps.md) |
| Fallacy | « tout le monde connaît l'heure » |
| Théorie | [NTP](../../supports/ntp.md) · [Lamport](../../supports/ntp.md#alternative-logique) |

## But
Votre chat affiche les messages dans un ordre défendable, même si les horloges se contredisent.

## Étapes

**1. Ajoutez une option `--clock-skew <secondes>`** à votre programme. Elle décale de `<secondes>` (positives ou négatives) le `ts` de **tous vos envois**, et rien d'autre.  
→ Vérifiez : `--clock-skew 30` produit un `ts` 30 s dans le futur (comparez à l'heure UTC réelle).

**2. Provoquez le désordre.** Avec un camarade (ou deux instances de votre chat), A tourne avec `--clock-skew 30`, B avec `--clock-skew -30`. A pose une question, B répond.  
→ Vous devez observer : si vous triez par `ts`, la réponse s'affiche avant la question. C'est D02, étape 2, en vrai.

**3. Choisissez une politique et écrivez-la dans `NOTES.md`.** Trois candidates :

| Politique | Avantage | Défaut |
|---|---|---|
| ordre d'arrivée | jamais incohérent localement | différent chez chacun |
| tri par `ts` | commun à tous | fausse avec des horloges décalées |
| tri par `ts` + marque `⚠` si le `ts` s'écarte de plus de 5 s de votre heure à la réception | commun et honnête | ne répare pas l'ordre |

**4. Implémentez la politique retenue.**  
→ Vérifiez avec le scénario de l'étape 2 : le comportement observé correspond à ce que vous avez écrit en 3.

**5. (Défi) Horloge logique.** Pour garantir qu'une réponse vient après sa question, il faut un compteur dans le message. L'[enveloppe](../contrats/envelope.schema.json) n'en a pas : proposez dans une pull request sur `activites/contrats` l'ajout d'un champ **optionnel** `lc` (entier). Que devient un récepteur v1 strict qui le reçoit ?  
→ Indice : le contrat dit « tolérant en réception » pour le payload ; l'enveloppe, elle, est stricte (`additionalProperties: false`). Voilà la fallacy 6 : plusieurs équipes, plusieurs versions.

## Terminé quand

- [ ] `--clock-skew` fonctionne
- [ ] vous avez reproduit une réponse avant sa question
- [ ] votre politique d'ordre est écrite et testée
- [ ] vous pouvez expliquer pourquoi NTP seul ne suffit pas

## Si vous utilisez une IA
Votre prompt doit nommer la fallacy (l'heure), préciser le comportement quand deux `ts` sont égaux ou décalés, et donner le scénario de test de l'étape 2.
