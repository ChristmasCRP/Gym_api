Aplikacja Gym Management

Prosta aplikacja webowa do zarządzania członkami siłowni. Składa się z backendu w .NET Core Web API oraz frontendowego pliku HTML z JavaScriptem i Bootstrapem. Obsługuje CRUD, wyszukiwanie, upload zdjęć informacyjnych oraz ich podgląd.

Funkcjonalności
- Dodawanie nowych członków
- Edycja danych członka
- Usuwanie członków
- Wyświetlanie listy wszystkich członków
- Wyszukiwanie członka po ID
- Upload pliku JPG (np. informacyjne zdjęcie)
- Podgląd zdjęcia powiązanego z członkiem

Technologie
- Backend: ASP.NET Core Web API (.NET 8)
- ORM: Entity Framework Core
- Frontend: HTML + Bootstrap 5 + JavaScript (Fetch API)
- Baza danych: SQL Server (LocalDB)

Uruchomienie projektu

1. Klonowanie repozytorium:
git clone https://github.com/ChristmasCRP/Gym_api.git

cd Gym_api

3. Otwórz projekt w Visual Studio:
- Otwórz plik Gym_api.sln

3. Ustaw połączenie z bazą w pliku appsettings.json:

"ConnectionStrings": {
  "GymDb": "Server=(localdb)\\MSSQLLocalDB;Database=GymDB;Trusted_Connection=True;"
}

4. Wykonaj migracje w Package Manager Console:
Update-Database

5. Uruchom aplikację:
- Backend: F5 lub Ctrl+F5
- Frontend: otwórz Frontend/index.html w przeglądarce

Uploadowane pliki:
- Zapisywane są w folderze: wwwroot/uploads/
- Dostępne przez: https://localhost:7153/uploads/nazwa_pliku.jpg

Przykładowe endpointy API:
GET    /api/GymMembers                   - lista wszystkich członków

GET    /api/GymMembers/{id}              - dane konkretnego członka

POST   /api/GymMembers                   - dodanie nowego członka

PUT    /api/GymMembers/{id}              - edycja danych

DELETE /api/GymMembers/{id}              - usunięcie członka

POST   /api/GymMembers/{id}/upload-plan  - upload pliku JPG

Autorzy:
Projekt zaliczeniowy
Jakub Danilkiewicz, Piotr Bach, Adrianna Czyżewska
2025
