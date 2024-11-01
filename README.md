# MasterTagSystem

## Description du Projet

MasterTagSystem est un simulateur de gestion de tags publicitaires qui permet de créer, éditer, valider et parser des tags JSON pour des annonces en temps réel. Ce projet a été conçu pour démontrer des compétences en développement web, tant côté frontend que backend, et vise à répondre aux exigences d'un stage chez Criteo en tant qu'ingénieur logiciel.

## Objectif du Projet

L'objectif principal de ce projet est de simuler la gestion de tags publicitaires (MasterTag) et d'améliorer les compétences techniques nécessaires pour obtenir un stage chez Criteo, où l'accent est mis sur le développement d'applications à grande échelle et en temps réel.

### Technologies Utilisées

- **Frontend**: 
  - TypeScript/JavaScript pour construire une interface utilisateur interactive de gestion des tags.
  
- **Backend**:
  - C# (.NET Core) pour le parsing et la validation des tags JSON.

## Fonctionnalités

- Création et édition d'un tag JSON complexe.
- Validation en temps réel des tags, avec retour d'erreurs si le format est incorrect.
- Traitement de chaque tag dans une file d'attente pour simuler une haute fréquence de requêtes publicitaires.
- **Bonus**: Intégration d'Apache Mesos pour gérer la répartition des requêtes entre plusieurs serveurs.

## Instructions pour l'Exécution

### Frontend

Pour démarrer l'application frontend, assurez-vous d'abord d'avoir installé les dépendances nécessaires :

```bash
npm install
```

Ensuite, vous pouvez lancer l'application avec :

```bash
npm start
```

L'application frontend sera accessible à l'adresse suivante : `http://localhost:4200`.

### Backend

Pour le backend, vous pouvez effectuer un nettoyage et démarrer l'application avec les commandes suivantes :

```bash
dotnet clean
dotnet run
```

L'API backend sera accessible à l'adresse suivante : `http://localhost:5000/api/tag/validate`.

## Conclusion

Ce projet est non seulement un exercice technique, mais également une étape vers une opportunité de carrière chez Criteo. Il illustre la capacité à développer des systèmes complexes en utilisant des technologies modernes et à résoudre des problèmes pratiques dans le domaine de la publicité numérique.

## A Propos de Criteo

Criteo est une entreprise leader dans le domaine des médias commerciaux, permettant aux annonceurs de délivrer des expériences consommateurs enrichies grâce à sa plateforme innovante. En tant qu'aspirant stagiaire, je suis motivé à contribuer à cette mission tout en développant mes compétences en ingénierie logicielle.
