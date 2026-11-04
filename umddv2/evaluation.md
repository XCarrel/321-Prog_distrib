# Évaluation

L'IA est permise **si on comprend**. L'évaluation mesure la compréhension, pas la production.

## Deux pistes

À chaque checkpoint ou épreuve, la piste est attribuée (ou tirée) par l'enseignant.

### Piste Code
On donne un code qui contient un défaut lié à une fallacy (doublon traité deux fois, retry sans backoff, `ts` utilisé pour l'ordre…).  
L'élève **explique** (nomme le défaut, la fallacy, le scénario de panne) puis **corrige**.

### Piste Feature
On donne une user story. L'élève rédige le **prompt** qu'il donnerait à une IA. Le prompt est noté :

| Critère               | Attendu                                                                 |
|-----------------------|-------------------------------------------------------------------------|
| Concepts              | nomme les notions de théorie utiles (QoS, idempotence, LWT, signature…) |
| Fallacy               | nomme la fallacy menaçante et ce qui l'illustre                         |
| Panne                 | précise le comportement en cas d'échec (timeout, doublon, nœud mort)    |
| Critère d'acceptation | décrit un test vérifiable, conforme au [contrat](contracts/README.md)   |

Puis l'élève exécute le résultat et **explique 3 minutes un passage tiré au hasard** du code généré.

## Barème indicatif
Chaque tâche sur 4 : compréhension (2) · justesse (1) · communication (1). Un code non expliqué vaut 0 sur la compréhension, même s'il fonctionne.

## Rattachement aux compétences
Chaque tâche est étiquetée avec une ou deux bandes de la [matrice](../evaluation/matrice%20des%20compétences.md) (A2, C1, D1/D2, E1, F1) et un niveau B/I/A.

## Format
- **Checkpoints** : Forms de 10 min (théorie + une mini-tâche piste Code), voir [modalités](../evaluation/modalites.md).
- **Épreuve (S8)** : une user story sur la ville, en classe, IA permise. Pas de rendu sans défense orale.

