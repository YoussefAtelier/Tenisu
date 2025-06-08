# 🎾 Tenisu - API Statistiques Joueurs de Tennis

Ce projet est une API développée en .NET pour L’Atelier, dans le cadre d’un test technique backend. Elle permet de consulter les joueurs de tennis, leurs statistiques, et diverses informations pertinentes sur leurs performances.

---

## 🚀 Liens importants

- 🌐 **API en ligne (Azure)** : [Swagger UI](https://tenisu-h0argchucgazd2ck.germanywestcentral-01.azurewebsites.net/swagger/index.html)
- 💻 **Code source GitHub** : [https://github.com/YoussefAtelier/Tenisu]

---

## 📌 Fonctionnalités

- 🔹 `GET /Players`  
  Renvoie la liste des joueurs triée du meilleur au moins bon.

- 🔹 `GET /Players/{id}`  
  Renvoie les détails d’un joueur à partir de son ID.

- 🔹 `GET /statistics`  
  Renvoie :
  - Le pays avec le **meilleur ratio de victoires**
  - L’**IMC moyen** de tous les joueurs
  - La **médiane** de la taille des joueurs

---

## 🛠️ Stack technique

- **Langage** : C# (.NET 8)
- **Base de données** : PostgreSQL (hébergée sur [Supabase])
- **ORM / Accès aux données** : [Dapper]
- **Hébergement** : [Azure App Service]
- **Documentation API** : Swagger 
- **Tests unitaires** : [NUnit]

---

## ⚙️ Installation locale

### 1. Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (sur supaBase)
- Visual Studio

### 2. Cloner le projet

```bash
git clone https://github.com/YoussefAtelier/Tenisu.git
cd Tenisu
