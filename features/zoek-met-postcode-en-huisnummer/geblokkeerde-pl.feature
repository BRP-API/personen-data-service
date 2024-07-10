#language: nl

@api
Functionaliteit: ZoekMetPostcodeEnHuisnummer bij geblokkeerde persoonslijst

  Regel: zoeken vindt ook personen waarvan de persoonslijst is geblokkeerd

    Scenario: Persoonslijst is geblokkeerd
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisletter (11.30) |
      | 0599                 | 2628HJ           | 2                  | A                  |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisletter (11.30) |
      | 0599                 | 2628HJ           | 2                  | B                  |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | Datum ingang blokkering (66.20) |
      | 20230221                        |
      Als personen wordt gezocht met de volgende parameters
      | naam       | waarde                      |
      | type       | ZoekMetPostcodeEnHuisnummer |
      | postcode   | 2628HJ                      |
      | huisnummer | 2                           |
      | fields     | burgerservicenummer         |
      Dan heeft de response 2 personen
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000048 |