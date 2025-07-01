#language: nl

Functionaliteit: doorgeven van indicatie in onderzoek

  Regel: wanneer de Gezag API een gezagsrelatie levert waarvan het inOnderzoek veld is gevuld, dan wordt dit veld doorgegeven

    Scenario: gezag wordt gevraagd en de Gezag API levert een gezagsrelatie met inOnderzoek veld
      Gegeven adres 'A1'
        | identificatiecode verblijfplaats (11.80) |
        |                         0599010000219679 |
      En de minderjarige 'P1'
      En de meerderjarige 'P2'
      En 'P1' is vandaag 2 jaar geleden ingeschreven op adres 'A1'
      En 'P1' heeft de volgende gezagsrelaties
      * eenhoofdig ouderlijk gezag over 'P1' met ouder 'P2'
      En het gezag is in onderzoek
      Als 'gezag' wordt gevraagd van personen gezocht met adresseerbaar object identificatie van 'A1'
      Dan heeft 'P1' de volgende gezagsrelaties
      * het gezag over 'P1' is eenhoofdig ouderlijk gezag met ouder 'P2'
      En is het gezag in onderzoek