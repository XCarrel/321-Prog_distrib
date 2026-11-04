# Contrat Dousville

Seule source de vérité. **Aucun code fourni**, quel que soit le langage.  
Le contrat grossit à chaque séquence : on ne publie que ce dont la séquence a besoin.

## Transport

| Élément           | Valeur                                  |
|-------------------|-----------------------------------------|
| Broker MQTT       | `mqtt.blue.section-inf.ch` (port 1883)  |
| Encodage          | UTF-8, JSON, un message = une enveloppe |
| Racine des topics | `dousville/`                            |

## Topics

| Topic                              | Service          | Séquence | QoS | Retain  |
|------------------------------------|------------------|----------|-----|---------|
| `dousville/chat/all`               | chat global      | S1       | 0   | non     |
| `dousville/chat/dm/<destinataire>` | chat privé       | S1       | 1   | non     |
| `dousville/vitrine/<bp>`           | vitrine d'une BP | S1       | 1   | **oui** |

À venir (publiés au fil des séquences) : `time` (S2), `log/<bp>` et `directory` (S3), `bank`, `coop` (S4).

## Enveloppe

Tout message est une enveloppe : [envelope.schema.json](envelope.schema.json).

| Champ     | Rôle                       | Théorie qui l'exploite   |
|-----------|----------------------------|--------------------------|
| `v`       | version du contrat         | sérialisation, évolution |
| `id`      | UUID unique du message     | idempotence (S4)         |
| `from`    | nom de la BP émettrice     | confiance (S6)           |
| `to`      | destinataire ou `*`        | adressage                |
| `ts`      | UTC, ISO 8601              | temps (S2)               |
| `type`    | nom du schéma du `payload` | validation               |
| `payload` | contenu, selon `type`      | —                        |

Un message qui ne valide pas contre son schéma est **ignoré et loggé**, jamais planté.

## Schémas

- [envelope.schema.json](envelope.schema.json)
- [chat.schema.json](chat.schema.json) (`type: "chat"`)
- [business.schema.json](business.schema.json) (`type: "business"`)

## Conformité

Le prof fournit un bot de conformité (à venir) : il publie sur les topics du contrat et vérifie vos réponses. Un service est « livré » quand le bot passe, quel que soit le langage.
