# D05 — Idempotence : le solde après les doublons

| | |
|---|---|
| Thématique | [Idempotence](../../thematiques/05-idempotence.md) |
| Fallacy | 1 (fiabilité) |
| Durée | 20 min |
| Matériel | papier (ou tableur) |
| Théorie | [Idempotence](../../supports/idempotence.md) : *Définition* et *Importance dans un système avec MQTT* |

## But
Voir qu'une livraison « au moins une fois » (QoS 1) fabrique des doublons, que certaines opérations les supportent et d'autres non, et ce que coûte la déduplication.

## Situation
Le compte d'Alice est à **100**. La banque reçoit ces messages dans cet ordre (un message peut arriver deux fois, le réseau n'ayant pas reçu l'accusé de réception) :

| # | `id` | Opération |
|---|---|---|
| 1 | a1 | `+50` (crédit) |
| 2 | a2 | `−20` (débit) |
| 3 | a2 | `−20` (débit) ← doublon |
| 4 | a3 | `solde = 200` (fixer) |
| 5 | a3 | `solde = 200` (fixer) ← doublon |
| 6 | a4 | `+10` (crédit) |
| 7 | a1 | `+50` (crédit) ← doublon tardif |

## Étapes

1. **Appliquez tout sans réfléchir**, ligne à ligne. Solde final ?  
   → 100 → 150 → 130 → 110 → 200 → 200 → 210 → **260**.

2. **Appliquez avec déduplication** : avant d'appliquer, regardez si l'`id` a déjà été vu. Solde final ?  
   → 100 → 150 (a1) → 130 (a2) → 200 (a3) → 210 (a4). Les lignes 3, 5, 7 sont ignorées. **210**.

3. **Classez** chaque opération : idempotente (rejouée deux fois = une fois) ou non ?  
   → `+50`, `−20` : **non**. `solde = 200` : **oui** (doublon 5 sans effet, même sans déduplication).

4. **Changez l'ordre** : le message 4 (`solde = 200`) arrive *avant* le message 2 (`−20`). Solde final avec déduplication ?  
   → 100 → 150 (a1) → 200 (a3) → 180 (a2) → 190 (a4) = **190**. Autre résultat qu'à l'étape 2, alors que tout est dédupliqué.

5. **Mémoire** : vous gardez la liste des `id` vus. Après un million de messages, qu'en faites-vous ? Proposez une règle de purge **sûre** (qui ne risque pas de laisser repasser un vieux doublon).  
   → par exemple : on ne purge que les `id` plus vieux que le délai maximal de redélivrance (ex. 24 h), d'où l'intérêt de `ts` dans l'enveloppe.

## Débrief

- Idempotent ≠ commutatif : l'étape 4 montre un cas où dédupliquer ne suffit pas. Que faut-il en plus ? (numéro de séquence ? opérations qui ne dépendent pas de l'ordre ?)
- Dans l'[enveloppe](../contrats/README.md), quel champ rend l'étape 2 possible ?
- Que se passe-t-il si c'est **l'expéditeur** qui génère l'`id` à chaque retry au lieu de le réutiliser ?

<details><summary>Solution</summary>

- Il faut ordonner (numéro de séquence par émetteur) ou rendre les opérations indépendantes de l'ordre (fixer un état plutôt qu'ajouter une différence).
- `id`. L'expéditeur doit **réutiliser le même `id` à chaque retransmission** d'un même message, sinon c'est un nouveau message pour la banque.
</details>
