# C6 — Survivre à la coupure du broker

| | |
|---|---|
| Thématique | [Résilience](../../thematiques/07-resilience.md) |
| Drill préalable | [D07](../drills/D07-retry.md) |
| Fallacy | 1 (fiabilité), 2 (latence) |
| Théorie | [Design for failure](../../supports/design4failure.md) : *Timeout*, *Fail Fast*, *Stratégie de retry* |

## But
Le broker disparaît puis revient : votre chat ne plante pas, ne harcèle pas, et reprend tout seul.

## Étapes

**1. Constatez le défaut actuel.** Lancez votre chat, puis `docker compose stop broker`.  
→ Notez ce qui se passe : plantage ? silence ? boucle folle ? (Les trois sont fréquents.)

**2. Détectez la perte.** Votre programme affiche `déconnecté` dès qu'il s'en aperçoit.

**3. Reconnexion avec backoff exponentiel et jitter.** Attentes successives : 1 s, 2 s, 4 s, 8 s… plafonnées à 30 s, chacune **tirée au hasard** entre 1 s et la valeur courante. Affichez chaque tentative et son délai.  
→ Vérifiez avec `docker compose logs broker` après `start` : les connexions de plusieurs chats ne sont **pas** simultanées.

**4. Reprise complète.** À la reconnexion : réabonnez-vous (si votre session n'est pas persistante, vos abonnements sont perdus), republiez votre présence `open`.  
→ Vérifiez : `docker compose start broker` ; en moins de 30 s votre chat reçoit à nouveau, et les autres vous revoient `open`.

**5. Messages à envoyer pendant la panne.** Choisissez et notez dans `NOTES.md` : refuser (« non envoyé ») ou garder en file (bornée à N). Défendez votre choix en une phrase.  
→ *Fail fast* contre *mode dégradé* : voir la théorie.

**6. Panne sale, via le proxy.** Connectez-vous à `localhost:1884` et ajoutez `reset_peer` : `docker compose exec toxiproxy /toxiproxy-cli toxic add -t reset_peer -a timeout=0 mqtt`. Puis retirez-le.  
→ Votre chat doit se comporter comme à l'étape 4.

## Terminé quand

- [ ] arrêt/démarrage du broker sans plantage ni intervention
- [ ] délais de reconnexion croissants, plafonnés, avec hasard (visibles dans la sortie)
- [ ] vous avez décidé et expliqué la politique de l'étape 5
- [ ] vous pouvez expliquer pourquoi sans jitter, 30 chats frappent le broker en même temps

## Si vous utilisez une IA
Prompt : nommer backoff exponentiel, jitter, plafond, l'état à restaurer (abonnements, présence), et le scénario de test (arrêt/démarrage du broker).
