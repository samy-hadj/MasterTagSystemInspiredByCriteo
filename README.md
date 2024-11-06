# MasterTagSystem

## Description du Projet

MasterTagSystem est un système de gestion de données JSON permettant la création, la modification et l’analyse de structures JSON complexes en temps réel. Ce projet comprend une architecture complète, avec un backend en C# pour le stockage et le traitement des données JSON, un frontend en Angular pour l'affichage et la manipulation de ces données sous forme d’arborescence, et Kafka pour gérer la transmission des données en temps réel. L'application utilise également Mesos pour l'orchestration des services afin de garantir l’évolutivité du système dans un contexte de traitement à grande échelle.

## Objectifs du Projet

Le projet vise à répondre aux objectifs suivants :

1. **Backend** : Développer un système robuste pour consommer, stocker et traiter des messages JSON dans MongoDB, capable de gérer une haute fréquence de traitement (jusqu’à des milliers de messages par seconde).
2. **Frontend** : Créer une interface Angular permettant de manipuler des messages JSON sous forme d’arborescence, avec des fonctionnalités d’édition et de structuration des données.
3. **Kafka** : Assurer la transmission des messages JSON en temps réel vers le backend pour une insertion rapide dans MongoDB.
4. **Mesos** : Utiliser Mesos pour orchestrer les services backend, Kafka et MongoDB, facilitant ainsi la gestion des ressources et l’extensibilité du système.

## Technologies Utilisées

- **Frontend** :
  - **Angular** pour construire l'interface utilisateur permettant de visualiser et de manipuler des données JSON sous forme d’arborescence.
  
- **Backend** :
  - **C# (ASP.NET Core)** pour la gestion des requêtes API et l'interaction avec MongoDB.
  
- **Base de données** :
  - **MongoDB** pour le stockage des données JSON.
  
- **Messagerie en temps réel** :
  - **Kafka** pour la transmission en temps réel des messages JSON entre le frontend et le backend.
  
- **Orchestration** :
  - **Mesos** pour gérer les ressources des services backend, Kafka et MongoDB à grande échelle.

## Fonctionnalités

- **Backend** :
  - Validation et insertion des messages JSON dans MongoDB.
  - API pour recevoir les messages JSON et les traiter.
  
- **Frontend** :
  - Interface d’affichage des messages JSON sous forme d’arborescence.
  - Fonctionnalités pour ajouter, modifier et supprimer des nœuds JSON.
  
- **Kafka** :
  - Transmission des messages JSON en temps réel vers le backend pour traitement immédiat.

- **Mesos** :
  - Orchestration des services backend, Kafka et MongoDB pour garantir la scalabilité.

## Instructions pour l'Exécution

### Frontend

Pour démarrer l'application frontend, assurez-vous d'avoir installé les dépendances nécessaires :

```bash
npm install
```

Ensuite, vous pouvez lancer l'application avec :

```bash
npm start
```

L'application frontend sera accessible à l'adresse suivante : `http://localhost:4200`.

### Backend

Pour démarrer le backend, suivez ces étapes :

```bash
dotnet clean
dotnet run
```

L'API backend sera accessible à l'adresse suivante : `http://localhost:5000/api/tag/validate`.

### Kafka

Pour simuler l'envoi de messages JSON, utilisez le script Python suivant :

```bash
python producerKafka.py
```

### Mesos

L'orchestration via Mesos est configurée pour gérer les ressources des services backend, Kafka et MongoDB. Assurez-vous que Mesos est installé et configuré avant de procéder aux tests de charge.

## Ce que vous avez réalisé

1. **Mise en place de MongoDB** :
   - Installation et configuration locale de MongoDB.
   - Création d'une base de données `CriteoProject` et connexion avec le backend pour le stockage des messages JSON.

2. **Développement du backend en C# (ASP.NET Core)** :
   - Création du service `TagService` pour l'interaction avec MongoDB, incluant des fonctions de validation et d'insertion des JSON.
   - Développement du contrôleur `TagController` pour gérer les requêtes API et insérer les messages JSON dans MongoDB.
   - Tests pour s’assurer que le backend reçoit et stocke correctement les messages JSON.

3. **Simulation de données avec Kafka** :
   - Création d'un script `producerKafka.py` pour simuler l'envoi continu de messages JSON au backend.
   - Tests basiques d'envoi de messages toutes les 2 secondes pour valider la résilience du système.

## Ce qu'il reste à faire

### 1. Finaliser l'interface Angular

- **Objectif** : Développer une interface utilisateur sous forme d’arborescence pour afficher et manipuler les messages JSON.
- **Actions** :
  - Afficher les données JSON sous forme d'arborescence.
  - Ajouter la possibilité d’éditer les nœuds JSON (ajout, modification, suppression).
  - Mettre en place une interface utilisateur simple et intuitive pour valider l’intégrité des modifications avant envoi au backend.

### 2. Intégrer Kafka pour la transmission des données en temps réel

- **Objectif** : Intégrer Kafka pour envoyer les messages JSON en temps réel vers le backend.
- **Actions** :
  - Configurer Kafka pour transmettre les messages JSON.
  - Modifier le backend pour écouter et consommer les messages en temps réel via Kafka.
  - Effectuer des tests pour vérifier la rapidité et l'exactitude du traitement des messages.

### 3. Orchestration avec Mesos

- **Objectif** : Orchestrer les services via Mesos pour une gestion efficace des ressources.
- **Actions** :
  - Configurer Mesos pour orchestrer le backend, MongoDB et Kafka.
  - Tester la scalabilité du système sous charge importante de messages JSON.

### 4. Améliorer la qualité des données dans MongoDB

- **Objectif** : Assurer la qualité et l'intégrité des données stockées.
- **Actions** :
  - Mettre en place des validations plus complexes pour le format et la structure des JSON.
  - Ajouter un processus de vérification pour détecter les données indésirables (frauduleuses ou inutiles).

### 5. Tests finaux et documentation

- **Objectif** : Vérifier l'ensemble des fonctionnalités et préparer l'application pour un déploiement en production.
- **Actions** :
  - Exécuter des tests de bout en bout.
  - Documenter le code et les configurations.
  - Préparer le déploiement en production, incluant les scripts d'installation et la gestion des configurations.
