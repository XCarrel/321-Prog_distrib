# Powercher v1

## Gestion du cash
1. On part avec 0
2. On reçoit régulièrement du cash au travers des messages cash (voir ci-après) diffusés notamment par `PowerBank`

### Message `CASH`
À traiter et surtout éviter les problèmes de potences...

## Équipement des maisons
Vous devez équiper votre maison `d’appliances` en respectant le cash à disposition.
Cela implique que vous devez sauvegarder ([Sérialisation](../../supports/Sérialisation.md)) l’état de votre maison avant
de l’arrêter, afin de pouvoir aller ajouter les appareils et la redémarrer ensuite (en mettant à jour le budget, bien sûr).

> ⚠  ATTENTION, PowerWatch vous surveille !!!

## Messages `TOWN_ENVIRONMENT`
- Recevoir et traiter les infos reçues
  - Bilan énergétique
    - Calcul du bilan énergétique en fonction du temps écoulé
    - Affichage de ce bilan
  - Heure
    - Affichage

## Messages `HOUSE_STATUS_REQUEST`
- Répond avec `HOUSE_STATUS`

