# D02 — Temps : trier des messages quand les horloges mentent

| | |
|---|---|
| Thématique | [Temps](../../thematiques/02-temps.md) |
| Fallacy | « tout le monde connaît l'heure » |
| Durée | 20 min |
| Matériel | papier, crayon |
| Théorie | [NTP](../../supports/ntp.md) : *NTP en pratique* et [Alternative logique (Lamport)](../../supports/ntp.md#alternative-logique) |

## But
Comprendre pourquoi trier un chat sur l'horodatage local est faux, et ce que NTP corrige (ou pas).

## Situation
Trois machines. Leurs horloges ne sont pas à l'heure : **A** a raison, **B** avance de 4 s, **C** retarde de 3 s. Six messages sont envoyés, voici l'**heure réelle** (invisible pour le système) et l'**horodatage local** que chaque machine a écrit dans `ts` :

| Msg | Émetteur | Heure réelle (08:00:xx) | `ts` écrit | Remarque |
|---|---|---|---|---|
| m1 | A | 10 | 08:00:10 | |
| m2 | B | 11 | 08:00:15 | question de Bob |
| m3 | C | 12 | 08:00:09 | **réponse** de Carl à m2 |
| m4 | A | 14 | 08:00:14 | |
| m5 | B | 16 | 08:00:20 | question de Bob |
| m6 | C | 18 | 08:00:15 | **réponse** de Carl à m5 |

## Étapes

1. **Triez par `ts`.** Écrivez l'ordre obtenu.  
   → m3, m1, m4, m2/m6 (égalité), m5.

2. **Lisez le chat** dans cet ordre. Quel est le problème avec m3 ?  
   → la réponse de Carl apparaît *avant* la question de Bob.

3. **Calculez le décalage d'une horloge à la NTP.** Carl (client) interroge un serveur de temps : il envoie à `T1 = 10,000` (heure de Carl), le serveur reçoit à `T2 = 14,050` et répond à `T3 = 14,052` (heure du serveur), Carl reçoit à `T4 = 10,104` (heure de Carl). Formules : `décalage = ((T2 − T1) + (T3 − T4)) / 2` et `délai = (T4 − T1) − (T3 − T2)`.  
   → décalage = (4,050 + 3,948) / 2 = **3,999 s** : Carl retarde d'environ 4 s. Délai = 0,104 − 0,002 = **0,102 s**.

4. **Que se passe-t-il si Carl corrige son horloge ?** Les `ts` de C deviennent exacts. Le tri par `ts` est-il maintenant juste ?  
   → *presque* : il reste l'erreur résiduelle (de l'ordre du délai réseau, ici ≈ 0,05 s) et des horloges qui dérivent à nouveau. Deux messages à 10 ms d'écart restent ambigus.

5. **Horloge logique (Lamport).** Règles : chaque machine a un compteur à 0. À l'**envoi**, elle fait `compteur + 1` et estampille. À la **réception**, elle fait `max(compteur local, estampille reçue) + 1`.  
   Rejouez la scène dans l'ordre réel. C reçoit m2 avant d'envoyer m3, et m5 avant d'envoyer m6. Notez les estampilles m1…m6.  
   → m1 = 1 (A), m2 = 1 (B), C reçoit m2 → 2, envoie m3 = **3**, m4 = 2 (A), m5 = 2 (B), C reçoit m5 → max(3,2)+1 = 4, envoie m6 = **5**.

6. **Triez par (estampille, émetteur).**  
   → m1, m2, m4, m5, m3, m6 : chaque réponse vient bien après sa question.

## Débrief

- L'ordre obtenu à l'étape 6 est-il l'ordre réel ? (non : m3 est arrivé avant m4 en réel). Que garantit-il à la place ?
- Quand a-t-on **besoin** de l'heure réelle (facture, horaires d'ouverture) et quand l'**ordre** suffit-il (chat) ?

<details><summary>Solution</summary>

- Il garantit l'**ordre causal** : si un message en a provoqué un autre, le premier est avant. Pour deux messages sans lien, l'ordre est arbitraire mais identique chez tous (c'est ce qui compte).
- Heure réelle : horaires, facturation, journaux à corréler avec l'extérieur → NTP. Ordre : chat, file d'événements → logique.
</details>
