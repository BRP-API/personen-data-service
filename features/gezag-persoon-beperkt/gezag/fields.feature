#language: nl

Functionaliteit: vragen van gezagsrelaties bij zoeken op adresseerbaar object identificatie

  Scenario: geleverde persoon heeft eenhoofdig ouderlijk gezag
    Gegeven adres 'A1'
      | identificatiecode verblijfplaats (11.80) |
      |                         0599010000219679 |
    En de minderjarige 'P1'
    En de meerderjarige 'P2'
    En 'P1' is vandaag 2 jaar geleden ingeschreven op adres 'A1'
    En 'P1' heeft de volgende gezagsrelaties
    * eenhoofdig ouderlijk gezag over 'P1' met ouder 'P2'
    Als 'gezag' wordt gevraagd van personen gezocht met adresseerbaar object identificatie van 'A1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is eenhoofdig ouderlijk gezag met ouder 'P2'

  Scenario: meerdere geleverde personen
    Gegeven adres 'A1'
      | identificatiecode verblijfplaats (11.80) |
      |                         0599010000219679 |
    En de minderjarige 'P1'
    En de meerderjarige 'P2'
    En 'P1' is vandaag 2 jaar geleden ingeschreven op adres 'A1'
    En 'P2' is gisteren 5 jaar geleden ingeschreven op adres 'A1'
    En 'P1' heeft de volgende gezagsrelaties
    * eenhoofdig ouderlijk gezag over 'P1' met ouder 'P2'
    En 'P2' heeft de volgende gezagsrelaties
    * eenhoofdig ouderlijk gezag over 'P1' met ouder 'P2'
    Als 'gezag' wordt gevraagd van personen gezocht met adresseerbaar object identificatie van 'A1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is eenhoofdig ouderlijk gezag met ouder 'P2'
    En heeft 'P2' de volgende gezagsrelaties
    * het gezag over 'P1' is eenhoofdig ouderlijk gezag met ouder 'P2'
