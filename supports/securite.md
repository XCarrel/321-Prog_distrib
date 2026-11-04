# Sécurité

> **Fallacy :** 4 (le réseau est sûr).  
> **À retenir :** quatre propriétés à ne pas confondre : **confidentialité** (personne d'autre ne lit), **intégrité** (rien n'a été modifié), **authenticité** (l'auteur est bien celui qu'il prétend), **fraîcheur** (ce n'est pas un vieux message rejoué). Chiffrer ne donne pas les trois autres.

Ce document liste l'ensemble des concepts de sécurité à connaître et à comprendre dans le cadre de ce module.

## Les quatre propriétés

| Propriété | Question | Mécanisme typique |
|---|---|---|
| **Confidentialité** d'une communication | un tiers qui intercepte le message peut-il le comprendre ? | chiffrement |
| **Intégrité** des données | les données reçues sont-elles identiques à celles envoyées ? | hash, signature, MAC |
| **Authenticité** / authentification | qui est l'auteur ? | signature, certificat |
| **Fraîcheur** | le message est-il récent, ou rejoué ? | identifiant unique + horodatage **inclus dans ce qui est signé** |

La **non-répudiation** va plus loin : prouver qui est l'émetteur sans qu'il puisse le nier (signature avec clé privée).

## Primitives

| Terme | Définition |
|---|---|
| **Chiffrement vs encodage** | Le chiffrement rend la donnée incompréhensible à qui n'a pas un secret (la clé). L'encodage (base64, JSON…) n'est qu'une façon d'assembler des données : il ne protège rien. |
| **Clé de chiffrement / déchiffrement** | Donnée secrète utilisée par un algorithme : donnée en clair + clé → donnée chiffrée ; donnée chiffrée + clé → donnée en clair. |
| **Algorithme symétrique** | La **même clé** chiffre et déchiffre. Rapide, mais il faut partager le secret : s'il fuit, tout le monde est compromis. |
| **Algorithme asymétrique** | Une **paire de clés** liées, publique et privée : ce qui est chiffré par l'une n'est lisible que par l'autre. Plus lent, mais il évite de partager un secret. |
| **Clé publique** | Peut être diffusée librement. Sert à **chiffrer pour** son propriétaire et à **vérifier** ses signatures. |
| **Clé privée** | Reste secrète, conservée en sécurité par son détenteur. Sert à **déchiffrer** et à **signer** : c'est elle qui authentifie l'identité. |
| **Hash** | Donnée de taille fixe calculée à partir d'une donnée de taille quelconque, conçue pour qu'il soit extrêmement improbable que deux données différentes donnent le même résultat (collision). Changer une lettre change tout le hash. |
| **MAC / HMAC** | Hash calculé avec un **secret partagé** : prouve que le message vient de quelqu'un qui connaît le secret et qu'il n'a pas été modifié. Ne distingue pas les membres du groupe entre eux. |
| **Signature** | Authentifie la source et valide l'intégrité. Construite en deux temps : (1) hash de la donnée, (2) chiffrement du hash avec la **clé privée**. Le destinataire déchiffre avec la clé publique et compare : s'il y parvient, l'émetteur est bien le détenteur de la clé privée. Jointe au document. |

## Identité et confiance

| Terme | Définition |
|---|---|
| **Certificat** | Document décrivant une entité (personne, organisation) et sa clé publique, le tout **signé par une autorité de certification**. |
| **Autorité de certification (CA)** | Organisation reconnue, qui signe les certificats avec sa clé privée. Tous les utilisateurs ont sa clé publique : une organisation est reconnue comme CA parce qu'un très grand nombre d'entités possèdent exactement la même clé publique. |
| **Révocation** | Rend un certificat invalide, pour quelque raison que ce soit. Les CA tiennent des listes (CRL) à consulter avant d'utiliser un certificat. |

## Pour s'exercer

- Drill : [D09](../activites/drills/D09-securite.md) · Chat : [C7](../activites/chat/C7-confiance.md) · Voir aussi [Confidentialité](confidentialite.md) et [Réseau de confiance](reseau-confiance.md)
