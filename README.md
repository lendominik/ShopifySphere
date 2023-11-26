# ShopMVC
> Aplikacja sklepu internetowego oparta na platformie ASP.NET Core MVC umożliwia użytkownikom przeglądanie dostępnej listy przedmiotów, wykonywanie wyszukiwania, filtrowanie wyników, sortowanie ich według różnych kryteriów, wybieranie produktów oraz dokonywanie zakupów online.
> Aplikacja umożliwia administratorom zarządzanie kategoriami, produktami oraz monitorowanie statusu zamówień składanych przez klientów. Dzięki tej aplikacji, administratorzy mogą dodawać, edytować i usuwać kategorie produktów, zarządzać dostępnymi przedmiotami w sklepie, a także aktualizować status zamówień składanych przez klientów.
> Aplikacja podczas dodawania przez użytkownika produktów do koszyka przechowuje je w sesji użytkownika, dopiero po złożeniu zamówienia produkty z koszyka trafiają do bazy danych.
> 
## Spis treści
* [Główne informacje](#główne-informacje)
* [Użyte technologie](#użyte-technologie)
* [Wyróżniające elementy](#wyróżniające-elementy)
* [Zdjęcia](#zdjęcia)
* [Kontakt](#kontakt)


## Główne informacje


## Użyte technologie
- C#
- ASP.NET Core MVC
- Entity Framework Core
- HTML
- CSS
- JavaScript
- SQL Server
- Bootstrap
- Visual Studio
- Git
- Clean Architecture
- Stripe
- xUnit 

## Wyróżniające elementy
- Kosz z zakupami użytkownika przechowywany jest w sesji, a do bazy trafia dopiero po złożeniu zamówienia.
- Prosty w obsłudze dla klientów system sklepu intenetowego.
- Prosty w obsłudze dla administratorów system zarządządzania zamówieniami, przedmiotami i kategoriami.
- Paginacja, która umożliwia filtrowanie elementów, sortowanie oraz wyszukiwanie.
- Projekt został zaprojektowany z wykorzystaniem architektury Clean Architecture, co sprawia, że aplikacja jest elastyczna i łatwo rozbudowywalna.
- W projekcie jest zastosowana autoryzacja za pomocą paczki Identity.UI, która zapewnia dostęp i widoczność określonych elementów tylko dla administratorów.
- Możliwość logowania się za pomocą Facebook'a.
- Dodawanie zdjęć przedmiotów z komputera.
- System obsługi płatności


## Zdjęcia
### Sekcja kategorie
![image](https://github.com/lendominik/ShopMVC/assets/138286618/0c2055dc-1f59-4149-ad04-dcb049d00c85)

### Sekcja Kategorie -> Edycja kategorii
![image](https://github.com/lendominik/ShopMVC/assets/138286618/6c15ff24-ba74-41fc-8331-799f589a8d97)

### Sekcja Kategorie -> Edycja kategorii -> Dodaj nowy przedmiot
![image](https://github.com/lendominik/ShopMVC/assets/138286618/580d0caf-d268-4655-87bf-3bf5909d32fc)

### Notyfikacja po dodaniu przedmiotu
![image](https://github.com/lendominik/ShopMVC/assets/138286618/153474db-fe3e-4d4a-b09d-e8967bbee2ca)

### Lista przedmiotów
![image](https://github.com/lendominik/ShopMVC/assets/138286618/cf022e0f-0eb3-4f87-b142-1c02e851babb)

### Koszyk po dodaniu przedmiotu do koszyka
![image](https://github.com/lendominik/ShopMVC/assets/138286618/95e38f3d-9cfe-47a0-951f-07f5937cc81f)

### Zmiana ilości w koszyku
![image](https://github.com/lendominik/ShopMVC/assets/138286618/ff245260-3a2f-49c4-b71d-101801514df5)

### Panel składania zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/53df5d29-9756-49f3-b163-f004a413a95f)

### Widok ze szczegółami po złożeniu zamówienia - automatyczne przekierowanie
![image](https://github.com/lendominik/ShopMVC/assets/138286618/362673b9-410a-4aa7-b2e5-592564f4d639)

### Widok po kliknięciu opcji "OPŁAĆ" - automatycznie obsługiwane przez Stripe
![image](https://github.com/lendominik/ShopMVC/assets/138286618/9d3e4cd0-546c-435f-ab47-e94126539da2)

### Widok po opłaceniu zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/57c12df2-98fc-4e4f-a70b-fe20523b6e66)

### Widok w szczegółach zamówienia po jego opłaceniu
![image](https://github.com/lendominik/ShopMVC/assets/138286618/e9976362-eba8-434d-9e32-72de2c5ce59b)

### Widok listy zamówień - gdy zamówienie opłacimy nie ma możliwości jego anulownia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/9147e0ef-e036-41e2-84ab-4d30149b56f4)

### Widok po anulowaniu zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/961415e0-ece1-460a-9132-3332a33a503e)

### Panel zarządzania zamówieniami - widoczny tylko dla użytkowników mających rolę "Owner". Administrator ma możliwość ustawieniu statusu zamówienia, widać to poniżej na zdjęciach
![image](https://github.com/lendominik/ShopMVC/assets/138286618/b0143241-5d62-47c6-93c9-66b3d01af77d)

![image](https://github.com/lendominik/ShopMVC/assets/138286618/de6f07dd-9857-4e58-8915-a53fe9ee2e73)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/840b041d-e6ec-4ed3-8030-4e3b6d385ec3)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/f0a3efed-3cbc-4f30-86f3-fdb69d8ce219)


### Widok dla zwykłego użytkownika, bez roli "Owner"

![image](https://github.com/lendominik/ShopMVC/assets/138286618/246a9266-1996-4466-a7ce-bf046ef075a4)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/9875f3ab-3106-4926-8a2b-2838c08f1e91)

## Kontakt
Email: len.dominik13@gmail.com
