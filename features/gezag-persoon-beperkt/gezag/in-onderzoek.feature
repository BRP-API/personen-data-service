#language: nl

Functionaliteit: doorgeven van indicatie in onderzoek

  Regel: wanneer de Gezag API een gezagsrelatie levert waarvan het inOnderzoek veld is gevuld, dan wordt dit veld doorgegeven

    Scenario: gezag wordt gevraagd en de Gezag API levert een gezagsrelatie met inOnderzoek veld
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0599                 | 0599010051001502                         |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En voor de persoon geldt het volgende gezag
      | type                     | minderjarige.burgerservicenummer | ouder.burgerservicenummer | inOnderzoek |
      | EenhoofdigOuderlijkGezag | 000000048                        | 000000012                 | true        |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0599010051001502                        |
      | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
      | type                     | inOnderzoek | ouder.burgerservicenummer | minderjarige.burgerservicenummer |
      | EenhoofdigOuderlijkGezag | true        | 000000012                 | 000000048                        |
