#language: nl

@api
Functionaliteit: maximaal aantal zoekresultaten ZoekMetPostcodeEnHuisnummer

  Achtergrond:
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisletter (11.30) |
      | 0599                 | 2628HJ           | 2                  | A                  |
      En er zijn 30 personen ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon met burgerservicenummer '000000031' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | naam                                 | waarde |
      | reden opschorting bijhouding (67.20) | O      |
      En de persoon met burgerservicenummer '000000032' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | naam                                 | waarde |
      | reden opschorting bijhouding (67.20) | F      |
      En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisletter (11.30) |
      | 0599                 | 2628HJ           | 2                  | B                  |
      En de persoon met burgerservicenummer '000000033' is ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
    

  Regel: Wanneer er meer dan 30 personen gevonden worden bij zoeken wordt een foutmelding gegeven

    @fout-case
    Scenario: Meer dan 30 personen gevonden
      Als personen wordt gezocht met de volgende parameters
      | naam       | waarde                      |
      | type       | ZoekMetPostcodeEnHuisnummer |
      | postcode   | 2628HJ                      |
      | huisnummer | 2                           |
      | fields     | burgerservicenummer         |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                                    |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1               |
      | title    | Teveel zoekresultaten.                                                    |
      | status   | 400                                                                       |
      | detail   | Meer dan maximum van 30 zoekresultaten gevonden. Verfijn de zoekopdracht. |
      | code     | tooManyResults                                                            |
      | instance | /haalcentraal/api/brp/personen                                            |

    Scenario: Verfijnde zoekopdracht vindt exact 30 personen omdat overleden personen en afgevoerde persoonslijsten niet worden geleverd
      Als personen wordt gezocht met de volgende parameters
      | naam                       | waarde                      |
      | type                       | ZoekMetPostcodeEnHuisnummer |
      | postcode                   | 2628HJ                      |
      | huisnummer                 | 2                           |
      | huisletter                 | A                           |
      | fields                     | burgerservicenummer         |
      Dan heeft de response 30 personen

    @fout-case
    Scenario: Meer dan 30 personen omdat ook overleden personen gezocht is
      Als personen wordt gezocht met de volgende parameters
      | naam                       | waarde                      |
      | type                       | ZoekMetPostcodeEnHuisnummer |
      | postcode                   | 2628HJ                      |
      | huisnummer                 | 2                           |
      | huisletter                 | A                           |
      | inclusiefOverledenPersonen | true                        |
      | fields                     | burgerservicenummer         |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                                    |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1               |
      | title    | Teveel zoekresultaten.                                                    |
      | status   | 400                                                                       |
      | detail   | Meer dan maximum van 30 zoekresultaten gevonden. Verfijn de zoekopdracht. |
      | code     | tooManyResults                                                            |
      | instance | /haalcentraal/api/brp/personen                                            |
