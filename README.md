# **Documentation du projet : Email Assistant**


## **Introduction**
Ce projet a été réalisé avec **C#** et **.NET** dans le cadre de ma candidature.  
Email Assistant est un projet **Backend**. Afin de facilité l'essai, j'ai créé un front-end très simple à utiliser sur **ordinateur** pour une expérience optimale.  

Mon objectif était de créer une application backend intégrant une API Gmail et OpenAI, permettant d'automatiser la gestion des emails professionnels en mettant en avant la génération de réponses d'email à l'aide de l'IA.  

Une fois lancée, l'application récupère les emails, les affiche. Le contenu de l'email est envoyé à l'IA, un contexte est ajouté avec les informations de l'entreprise afin que l'IA ne soit pas hors sujet et une réponse est générée.  

---
## **Utilisation**
### 1. **Lancer l'application**
- Déployée sur Azure, elle est accessible via l'URL https://emailassistantproject-e8f3fnereyangyam.canadacentral-01.azurewebsites.net

### 2. **Envoyer un email**
- Envoyez un email sur : test.receiver.project@gmail.com ou essayez avec un email déjà reçu.
- Si vous envoyez un nouvel email, rafraîchissez la page pour voir l'email dans la liste.

### 3. **Générer une réponse**
- Sélectionnez un email.
- (Optionnel) Ajoutez un **prompt personnalisé** pour ajuster la réponse si nécessaire, puis cliquez sur Générer email.
- Cliquez sur "Générer email" pour une réponse automatique.  
La fonctionnalité "Envoyer" n'a pas encore été implémentée, mais elle pourrait être ajoutée ultérieurement.
---
## **Ce que j'ai appris**
Ce projet m'a permis de :
- Découvrir le langage **C#** et le framework **.NET**.
- Implémenter des **API** telles que Gmail et OpenAI.
- Utiliser **Azure App Services** pour héberger et déployer une application.
- Gérer les **variables d'environnement** pour protéger les données sensibles.
- Manipuler des fichiers externes comme `companysummary.txt` pour stocker des informations supplémentaires.

---

## **Fonctionnalités implémentées**
### 1. **Connexion à la boîte mail test**
- J'ai créé une boîte mail de test dédiée : `test.receiver.project@gmail.com`.
- L'application se connecte à cette boîte via l'API Gmail, en utilisant un token stocké pour éviter de demander une reconnexion à chaque utilisation.  
  Il est possible de connecter n'importe quel compte gmail, cependant pour le test j'ai mis ce token pour une connexion rapide à une boite mail test.

### 2. **Sélection et génération de réponses**
- Les emails sont affichés dans une interface simple.
- En sélectionnant un email, il est possible de :
  - **Générer une réponse** automatique à l'aide d'OpenAI.
  - **Écrire un prompt personnalisé** pour guider la génération de la réponse.

### 3. **Sécurité des données sensibles**
- Les **clés API** et autres données sensibles (comme les credentials Gmail) sont stockées via des **variables d'environnement** sur Azure pour éviter leur inclusion dans le code source.

### 4. **Contexte de l'entreprise**
- J'ai intégré un fichier `companysummary.txt` contenant des informations récupérées sur le site web de l'entreprise (par exemple, sa mission et ses services). Ce contexte est utilisé par l'IA pour générer des réponses adaptées et professionnelles.  
  Le fichier final se trouve dans la branche `code-production`

### 5. **Affichage des emails**
- L'application affiche les emails récupérés via l'API Gmail.
- Il est possible d'envoyer un email sur la boîte de test, et l'email sera visible après un rafraîchissement manuel de la page.

---

## **Limitations actuelles**
### 1. **Pas de base de données**
- L'application est limitée à récupérer a afficher les emails lors de son exécution. Les réponses générées ne sont pas enregistrées.

### 2. **Rafraîchissement manuel**
- Les nouveaux emails n'apparaissent qu'après un **rafraîchissement manuel**. Cependant, le projet pourrait être amélioré en implémentant :
  - **Google Pub/Sub** pour détecter les nouveaux emails.
  - **SignalR** pour mettre à jour l'interface en temps réel.

### 3. **Front-end minimaliste**
- Le projet se concentre principalement sur la partie backend. Le front-end est minimal et pourrait être amélioré pour une meilleure expérience utilisateur.

---

## **Perspectives d'amélioration**
- **Ajout d'une base de données** pour stocker et organiser les emails.
- **Récupération des emails en temps réel** à l'aide de Pub/Sub et SignalR.
- **Envoi d'emails** directement depuis l'application.
- **Bouton modifier** pour permettre de modifier directement l'email généré.
- **Connexion à plusieurs boîtes mail**, avec la possibilité de configurer des comptes Gmail ou autres fournisseurs.
- **Refonte du front-end** pour une interface plus professionnelle et responsive.

---

## **Technologies utilisées**
- **Langage** : C#
- **Framework** : .NET
- **Hébergement** : Azure App Services
- **API** :
  - Gmail (pour récupérer les emails)
  - OpenAI (pour générer les réponses)
- **Gestion des données sensibles** :
  - Variables d'environnement sur Azure
  - Token d'accès Gmail
- **Front-end** :
  - HTML, CSS, JavaScript

---

## Ressources utiles et documentation

Durant le développement de ce projet, j'ai consulté plusieurs ressources pour approfondir mes connaissances et résoudre des problèmes spécifiques. Voici une liste des liens que j'ai trouvés particulièrement utiles :

### **C# et .NET**
- [Introduction à C# (YouTube)](https://www.youtube.com/watch?v=6rDGCwBdQs0&list=WL&index=89&t=3344s)
- [Gestion des exceptions en C# (Microsoft)](https://learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/exceptions/)
- [Exception handling statements (Microsoft)](https://learn.microsoft.com/fr-fr/dotnet/csharp/language-reference/statements/exception-handling-statements)
- [System.ArgumentNullException (Microsoft API)](https://learn.microsoft.com/fr-fr/dotnet/api/system.argumentnullexception.-ctor?view=net-9.0#system-argumentnullexception-ctor)
- [ArgumentNullException (Rollbar)](https://rollbar.com/blog/csharp-argumentnullexception/)
- [Quality rules CA1510 (Microsoft)](https://learn.microsoft.com/fr-fr/dotnet/fundamentals/code-analysis/quality-rules/ca1510)
- [System.IO.FileNotFoundException (Microsoft)](https://learn.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception?view=net-9.0)
- [Vérifier si un fichier existe (Rollbar)](https://rollbar.com/blog/csharp-filenotfoundexception/)

### **API et création d'applications .NET**
- [Minimal vs Controllers (Microsoft)](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio)
- [Création d'API avec .NET (Microsoft)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-9.0)
- [Guide étape par étape pour construire une API RESTful](https://medium.com/@shashankshashu200/building-a-web-api-with-net-a-step-by-step-guide-2b1004c2273e)
- [RESTful APIs avec ASP.NET Core 8 (Dev.to)](https://dev.to/wirefuture/how-to-build-restful-apis-with-aspnet-core-8-j5)

---

### **API Gmail**
- [Quickstart (Google)](https://developers.google.com/gmail/api/quickstart/js)
- [Package NuGet Google.Apis.Gmail.v1](https://www.nuget.org/packages/Google.Apis.Gmail.v1)
- [Documentation des API clients Google pour .NET](https://developers.google.com/api-client-library/dotnet/get_started)
- [Lire le contenu des messages Gmail](https://developers.google.com/gmail/api/reference/rest/v1/users.messages.attachments#MessagePartBody)
- [Comment décoder une base64 en C#](https://medium.com/c-sharp-programming/mastering-base64-encoding-and-decoding-in-c-803805c388d0#:~:text=Decoding%20Base64%20to%20Binary%20Data,FromBase64String(base64EncodedData)%3B)
- [System.Convert.FromBase64String (Microsoft)](https://learn.microsoft.com/en-us/dotnet/api/system.convert.frombase64string?view=net-9.0)

---

### **OpenAI**
- [Package OpenAI .NET](https://platform.openai.com/docs/libraries)
- [Github OpenAI .NET](https://github.com/openai/openai-dotnet)

---

### **Push Notifications avec Pub/Sub et signalR (non implémenté)**
- [Tutoriel SignalR pour .NET 8](https://dev.to/leandroveiga/how-to-implement-real-time-communication-in-net-8-minimal-apis-using-signalr-a-step-by-step-guide-2faj)
- [Guide Gmail Pub/Sub](https://developers.google.com/gmail/api/guides/push?hl=fr)
