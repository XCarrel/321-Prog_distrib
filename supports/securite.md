# Sécurité

Ce document liste l'ensemble des concepts de sécurité à connaître et à comprendre dans le cadre de ce module.
## Différence entre chiffrement et encodage
Le chiffrement a pour but de rendre la donnée incompréhensible à toute personne qui ne possède pas un certain secret (clé de chiffrement). L'encodage n'est qu'une manière d'assembler des données selon une structure précise.
## Clé de chiffrement / déchiffrement
Une clé de chiffrement est une donnée secrète qui est utilisée par un algorithme pour chiffrer ou déchiffrer une donnée. L'algorithme de chiffrement reçoit en entrée une donnée en clair et la clé de chiffrement, il produit un résultat que l'on appelle donnée chiffrée. Lorsqu'on fournit la donnée chiffrée et une clé à un algorithme de déchiffrement, ce dernier réalise l'opération inverse et restitue la donnée en clair initiale.
## Algorithme symétrique
Un algorithme de chiffrement symétrique est un algorithme qui utilise la même clé pour chiffrer et pour déchiffrer.
## Algorithme asymétrique
Un algorithme asymétrique est un algorithme qui utilise une paire de clés publique/privée pour les opérations du chiffrement et de déchiffrement. Les deux clés sont intimement liées. Les clés ne sont pas destinées à des opérations fixes ; en d'autres termes, on peut chiffrer avec une clé privée ou publique, et on peut déchiffrer avec une clé privée ou publique aussi. C'est l'usage qui dicte quelle clé est utilisée. Une donnée qui a été chiffrée avec la clé privée ne peut être déchiffrée que par la clé publique (elle ne peut même pas être déchiffrée par la même clé privée qui a été utilisée pour le chiffrement). Inversement, toute donnée qui a été chiffrée avec la clé publique ne peut être déchiffrée que par la clé privée de la même paire de clés.
## Hash
Un hash est une donnée de taille fixe qui est calculée sur la base d'une donnée de taille quelconque. L'algorithme est conçu de telle manière qu'il est extrêmement improbable que les hash de deux données différentes donnent le même résultat (collision).
## Signature
Une signature est un élément cryptographique qui authentifie la source d'une donnée et en valide également l'intégrité. Elle est construite en deux temps :
- calcul du hash de la donnée, lequel permettra de valider l'intégrité de la donnée par le récepteur.
- chiffrement du hash avec la clé privée. De par la nature des clés publiques/privées, le fait d'être capable de déchiffrer le hash avec la clé publique authentifie l'émetteur (le propriétaire de la clé privée).
La signature est généralement jointe au document lui-même, ce qui permet à son destinataire - en réalisant les opérations cryptographiques inverses - de valider l'intégrité du contenu ainsi que l'identité de l'émetteur (non-répudiation)
## Certificat
Un certificat est un document qui contient la description d'une entité (personne ou organisation) et une clé publique dont l'entité détient la clé privée. Le tout est signé par une autorité de certification (CA).
## Clé privée
La clé privée d'une paire de clés est celle qui doit rester secrète, conservée de manière sûre par son détenteur. C'est elle qui va permettre d'authentifier l'identité d'une personne (ou organisation)
## Clé publique
La clé publique d'une paire de clés est celle qui peut être diffusée librement à toute personne ou organisation avec laquelle le détenteur de la clé privée veut effectuer des échanges chiffrés.
## Autorité de certification
Une autorité de certification (CA) est une organisation reconnue d'un très grand nombre de personnes ou organisation. Elle possède une clé privée qui lui permet de signer les certificats. Les utilisateurs ont tous la clé publique de la CA. Un organisme peut être reconnu comme autorité de certification de par le fait qu'un très grand nombre d'entités possèdent exactement la même clé publique.
## Confidentialité d'une communication
Une communication est confidentielle si une personne qui intercepte un message sur le canal de communication ne peut pas comprendre son contenu
## Intégrité des données
L'intégrité des données est un mécanisme qui permet d'être sûr que les données reçues par un utilisateur sont parfaitement identiques à celles que l'expéditeur a envoyé
## Non répudiation
Lorsqu'un message est envoyé par un émetteur, la non-répudiation consiste à prouver qui est l'émetteur de ce message sans que celui-ci ne puisse nier être l'émetteur.
## Authentification
L'authentification est un mécanisme qui permet de certifier l'identité d'une personne
## Révocation
La révocation consiste à rendre un certificat invalide pour quelque raison que ce soit. Les autorités de certification tiennent à jour des CRL (Certificate Revocation Lists) qui peuvent être consultées avant usage d'un certificat