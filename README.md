# Opis programu

Projekt polega na symulowaniu jazdy samochodu. Po wprowadzeniu danych początkowych takich jak rodzaj samochodu i budżet, pokazuje się okno symulacji. W nowo otwartym oknie można auto zatankować, odpalić i symulować jazdę wraz z podaną informacją o przebytych kilometrach. Usterki po których zmienia się stan samochodu, generowane są losowo. Jeśli stan nie pozwala na dalszą jazdę, to za pomocą pieniędzy naprawie się auto.




# Użyte wzorce projektowe: 

## Obserwator

Wzorzec ten używany jest do aktualizacji informacji w oknie głównym. Okno główne jest obserwatorem, a samochód obserwowanym. Za każdym razem gdy zmieniane są pola takie jak paliwo czy kondycja, to samochód powiadamia okno główne o tym, które następnie aktualizuje UI.

## Stan
Wzorzec stanu zaiplementowany w stanie auta. Kondycję samochodu reprezentuje klasa "stanSamochodu". Samochód jest kontekstem i zawiera metody "Uszkodzenia" i "Napraw". Metoda "Uszkodzenia" tworzy nowy stan dla kondycji, a metoda "Napraw" zmienia kondycję na pierwszy stan.

## Fabryka
Wzorzec fabryki przeznaczony do budowy samochodu, początkowego stanu oraz zużycia paliwa w zależności od rodzaju samochodu. Do stworzenia wymienionych obiektów służy statyczna klasa "FabrykaSamochodow".
