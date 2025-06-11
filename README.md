# Kanapkomania

**Symulator układania kanapek** stworzony w Unity, zainspirowany stylem sieci Subway.  
Po wybraniu kuchni gracz dobiera składniki, układa kanapki i porównuje je z wyświetlanymi przepisami. Za poprawne wykonanie otrzymuje punkty jako nagrodę. Celem gry jest zdobycie jak największej ilości punktów w ciągu 5 minut.

---

## Wykorzystanie sztucznej inteligencji

Do generowania dostępnych składników oraz nowych zamówień wykorzystujemy lokalnie uruchomiony model **gemma3:4B** za pośrednictwem Ollama:

1. Na podstawie wybranej kuchni model proponuje zestaw składników dostępnych w stoisku.  
2. W ciągu 30–60 sekund ten sam model tworzy nowe zamówienie: generuje nazwę kanapki oraz listę potrzebnych składników.  
3. Aby uzyskać spójne wyniki, do modelu przekazujemy zarówno nazwę wybranej kuchni, jak i dostępne składniki.  

---

## Opis projektu

Projekt **Kanapkomania** zawiera m.in.:

- Intuicyjny, estetyczny interfejs użytkownika w 2D.
- Dwie gotowe sceny.
- Możliwość wyboru i układania składników (chleb, wędlina, mięso, warzywa, sosy itp.).  
- Automatyczna weryfikacja gotowej kanapki względem zapisanego przepisu.  
- Pliki JSON z definicjami składników i przepisów — łatwa edycja i rozbudowa zestawów.  

---

## Interfejs użytkownika

Scena wyboru kuchni:
![image](https://github.com/user-attachments/assets/a01c2769-8c3d-401e-b694-1845b3a9bade)

Scena składania kanapek:
![image](https://github.com/user-attachments/assets/62e225e7-4784-4831-9488-a7a888d25a34)



