#language: nl

@api @deprecated
Functionaliteit: gezagsrelaties van de bewoners van een adresseerbaar object


  Regel: voor alle meerderjarige en minderjarige personen die staan ingeschreven op gevraagde adresseerbaar object wordt gezag geleverd

    Scenario: 
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0599                 | 0599010051001502                         |
      En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0518                 | 0518010067845216                         |
      En de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En voor de persoon geldt het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000012                 |
      En het gezag heeft de volgende ouders
      | burgerservicenummer |
      | 000000048           |
      | 000000061           |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En voor de persoon geldt het volgende gezag
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000024                |
      | ouder.burgerservicenummer        | 000000048                |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft een 'partner' met de volgende gegevens
      | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
      | 000000061                   | 20120428                                                           |
      En voor de persoon geldt het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000012                 |
      En het gezag heeft de volgende ouders
      | burgerservicenummer |
      | 000000048           |
      | 000000061           |
      En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000024                |
      | ouder.burgerservicenummer        | 000000048                |
      En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer | 000000036        |
      | ouder.burgerservicenummer        | 000000061        |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        | 000000048        |
      En de persoon met burgerservicenummer '000000036' is ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft een ouder '1' met de volgende gegevens
      | burgerservicenummer (01.20) |
      | 000000061                   |
      En voor de persoon geldt het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer | 000000036        |
      | ouder.burgerservicenummer        | 000000061        |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        | 000000048        |
      En de persoon met burgerservicenummer '000000061' is ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft een 'partner' met de volgende gegevens
      | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
      | 000000048                   | 20120428                                                           |
      En de persoon heeft een 'kind' met de volgende gegevens
      | burgerservicenummer (01.20) |
      | 000000036                   |
      En voor de persoon geldt het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000012                 |
      En het gezag heeft de volgende ouders
      | burgerservicenummer |
      | 000000061           |
      En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer | 000000036        |
      | ouder.burgerservicenummer        | 000000061        |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0599010051001502                        |
      | fields                           | burgerservicenummer,gezag               |
      Dan heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000012 |
      En heeft de persoon een 'gezag' met de volgende gegevens
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000012                 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000048 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000061 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |
      En heeft de persoon een 'gezag' met de volgende gegevens
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000024                |
      | ouder.burgerservicenummer        | 000000048                |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000012                 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000048 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000061 |
      En heeft de persoon een 'gezag' met de volgende gegevens
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer | 000000024                |
      | ouder.burgerservicenummer        | 000000048                |
      En heeft de persoon een 'gezag' met de volgende gegevens
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer | 000000036        |
      | ouder.burgerservicenummer        | 000000061        |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        | 000000048        |
