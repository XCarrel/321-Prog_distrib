# D07 — Retry : backoff et jitter aux dés

| | |
|---|---|
| Thématique | [Résilience](../../thematiques/07-resilience.md) |
| Fallacy | 1 (fiabilité), 2 (latence) |
| Durée | 20 min, par groupes de 5 (un élève = un client) |
| Matériel | un dé à 6 faces par groupe, papier quadrillé |
| Théorie | [Design for failure](../../supports/design4failure.md) : *Stratégie de retry* |

## But
Voir pourquoi des clients qui réessayent « poliment » peuvent re-tuer un service qui vient de revenir.

## Situation
Cinq clients échouent à joindre la banque à **t = 0 s**. La banque est **morte jusqu'à t = 5 s**, puis fonctionne. Un client qui réussit s'arrête. Une requête qui tombe pendant la panne est **gaspillée**. Vous comptez les requêtes reçues par la banque seconde par seconde (t = 1 … 15).

## Stratégie A : retry fixe, toutes les 1 s

1. Chaque client réessaye à t = 1, 2, 3, 4, 5…  
2. Remplissez la ligne « A » du tableau (nombre de requêtes à chaque seconde), notez le pic et le nombre de requêtes gaspillées.  
   → 5 requêtes à chaque seconde de t = 1 à 4 (**20 gaspillées**), et à t = 5 les 5 arrivent **ensemble** : pic de 5, tous réussissent.

## Stratégie B : backoff exponentiel (sans jitter)

3. Attente doublée à chaque échec : 1 s, 2 s, 4 s, 8 s… Les tentatives tombent à t = 1, 3, 7, 15…  
4. Remplissez la ligne « B ».  
   → t = 1 et 3 : 5 requêtes gaspillées à chaque fois (**10 gaspillées**) ; la banque est revenue à t = 5 mais personne n'essaye avant **t = 7**, où les 5 arrivent **ensemble** (pic 5).

## Stratégie C : backoff + jitter

5. Même doublement, mais plafonné à 6 s (durées courantes *d* = 1, 2, 4, 6, 6…). À chaque tentative le client attend un temps **tiré au hasard** : lancez le dé et prenez `min(dé, d)` secondes (approximation acceptable du « hasard entre 1 et *d* »).  
6. Chaque élève joue son client et note ses instants d'essai. Remplissez la ligne « C » avec les instants réels du groupe.  
   → les tentatives se répartissent, le pic tombe à 1 ou 2, les réussites sont étalées : le service est rechargé progressivement.

| t (s) | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | Gaspillées | Pic |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| A | | | | | | | | | | | | | | | | | |
| B | | | | | | | | | | | | | | | | | |
| C | | | | | | | | | | | | | | | | | |

## Débrief

- A : peu de temps d'attente, beaucoup de gaspillage. B : peu de gaspillage, temps de reprise rallongé. C : un compromis. Quel paramètre limiterait le pire des cas de B (attente de 8 s, 16 s…) ?
- Avec 1000 clients au lieu de 5, que devient le pic de B ? Et celui de C ?
- Pourquoi une banque qui revient au bout de 5 s peut-elle retomber immédiatement avec la stratégie A ou B ?

<details><summary>Solution</summary>

- Un **plafond** (cap) d'attente, par exemple 30 s, et un **nombre maximal** de tentatives.
- B : pic de 1000 requêtes simultanées à chaque vague. C : réparti sur la durée d'attente (≈ 1000 / d requêtes par seconde).
- Les clients synchronisés frappent en même temps, le *thundering herd* : la file se remplit avant que le service ait fini de redémarrer.
</details>
