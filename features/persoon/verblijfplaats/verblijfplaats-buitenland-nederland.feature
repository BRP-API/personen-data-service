# language: nl

@api
Functionaliteit: verblijfplaats categorie 13 met adres in Nederland

    Scenario: verblijfplaats in Nederland voor persoon ingeschreven in RNI
      Gegeven de persoon met burgerservicenummer '000000012' is ingeschreven op een buitenlands adres met de volgende gegevens
      | naam                                   | waarde          |
      | datum aanvang adres buitenland (13.20) | 20110101        |
      | land adres buitenland (13.10)          | 6030            |
      | regel 1 adres buitenland (13.30)       | Straatnaam 1    |
      | regel 2 adres buitenland (13.40)       | 1111AB Ons Dorp |
      En de persoon is vervolgens ingeschreven op een buitenlands adres met de volgende gegevens
      | naam                                   | waarde          |
      | datum aanvang adres buitenland (13.20) | 20220202        |
      | land adres buitenland (13.10)          | 6030            |
      | regel 1 adres buitenland (13.30)       | Laantje 2       |
      | regel 2 adres buitenland (13.40)       | 2222CD Ons Dorp |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000012                       |
      | fields              | verblijfplaats                  |
      Dan heeft de response een persoon met de volgende 'verblijfplaats' gegevens
      | naam                        | waarde          |
      | datumAanvangAdresBuitenland | 20220202        |
      | land.code                   | 6030            |
      | land.omschrijving           | Nederland       |
      | regel1                      | Laantje 2       |
      | regel2                      | 2222CD Ons Dorp |
