# ShopMVC
> Aplikacja sklepu internetowego oparta na platformie ASP.NET Core MVC umożliwia użytkownikom przeglądanie dostępnej listy przedmiotów, wykonywanie wyszukiwania, filtrowanie wyników, sortowanie ich według różnych kryteriów, wybieranie produktów oraz dokonywanie zakupów online.
> plikacja umożliwia administratorom zarządzanie kategoriami, produktami oraz monitorowanie statusu zamówień składanych przez klientów. Dzięki tej aplikacji, administratorzy mogą dodawać, edytować i usuwać kategorie produktów, zarządzać dostępnymi przedmiotami w sklepie, a także aktualizować status zamówień składanych przez klientów.
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
![image](https://github.com/lendominik/ShopMVC/assets/138286618/0f0fad50-e9c1-4163-96bd-7cea566dfbfa)

### Widok ze szczegółami po złożeniu zamówienia - automatyczne przekierowanie
![image](https://github.com/lendominik/ShopMVC/assets/138286618/362673b9-410a-4aa7-b2e5-592564f4d639)

### Widok po kliknięciu opcji "OPŁAĆ" - automatycznie obsługiwane przez Stripe
![image](https://github.com/lendominik/ShopMVC/assets/138286618/9d3e4cd0-546c-435f-ab47-e94126539da2)

### Widok po opłaceniu zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/57c12df2-98fc-4e4f-a70b-fe20523b6e66)

### Widok w szczegółach zamówienia po jego opłaceniu
![image](https://github.com/lendominik/ShopMVC/assets/138286618/e9976362-eba8-434d-9e32-72de2c5ce59b)


### Widok w sekcji Zamówienia, użytkownik ma możliwość anulowanie swojego zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/e5520bf7-aece-489c-af50-8da7b7d0b457)
### Widok w sekcji Szczegóły, użytkownik dostaje szczegóły dotyczące wysyłki i listę zamówionych przedmiotów
![image](https://github.com/lendominik/ShopMVC/assets/138286618/5a274aa7-9542-4440-8ad8-70c04bd65264)
### Anulowanie zamówienia
![image](https://github.com/lendominik/ShopMVC/assets/138286618/23c7d40b-fae6-4840-9046-b81c64eb2094)
### Panel zarządzania zamówieniami - widoczny tylko dla użytkowników mających rolę "Owner". Administrator ma możliwość ustawieniu statusu zamówienia, widać to poniżej na zdjęciach
![image](https://github.com/lendominik/ShopMVC/assets/138286618/4a32e229-7a9b-4a07-8813-0f906d1f097f)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/a7ca57a0-9af5-429f-bcf6-d5e8d534116b)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/471a42e5-cf37-475e-99e4-edcefc0f96d3)
### Widok dla zwykłego użytkownika, bez roli "Owner"
![image](https://github.com/lendominik/ShopMVC/assets/138286618/25e30a89-a01c-4494-9dce-0e6da8fb1e32)
### Przykładowy widok historii zamówień
![image](https://github.com/lendominik/ShopMVC/assets/138286618/0d10a74d-df5a-43e9-9ae6-054c1ea269b9)
### System płatności
![image](https://github.com/lendominik/ShopMVC/assets/138286618/37ac7233-7ddd-4be2-8aa0-351a41d4291f)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/4c1de18a-8d50-4c8e-9edf-5e6ccfc9cb9c)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/564bde60-d79b-4d28-9417-39c083457b63)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/929dc831-0cfb-4dc2-ae3b-c6b971c7411b)
![image](https://github.com/lendominik/ShopMVC/assets/138286618/ac331545-e00c-4622-965b-2186589ae2f0)

## Kontakt
Email: len.dominik13@gmail.com
