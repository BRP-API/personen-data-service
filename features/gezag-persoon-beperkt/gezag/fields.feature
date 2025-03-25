# language: nl
@api
Functionaliteit: gezagsrelaties vragen met fields bij zoeken op adresseerbaar object aanduiding

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0599 |                         0599010051001502 |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      |                              0599 |
    En voor de persoon geldt het volgende gezag
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer |                000000012 |
      | ouder.burgerservicenummer        |                000000048 |
    En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer |                 000000024 |
    En het gezag heeft de volgende ouders
      | burgerservicenummer |
      |           000000048 |
      |           000000061 |
    En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer |        000000036 |
      | ouder.burgerservicenummer        |        000000061 |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        |        000000048 |
    En de persoon met burgerservicenummer '000000061' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      |                              0599 |
    En voor de persoon geldt het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer |                 000000024 |
    En het gezag heeft de volgende ouders
      | burgerservicenummer |
      |           000000048 |
      |           000000061 |
    En voor de persoon geldt ook het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer |        000000036 |
      | ouder.burgerservicenummer        |        000000061 |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        |        000000048 |
    En de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      |                              0599 |
    En voor de persoon geldt het volgende gezag
      | naam                             | waarde                   |
      | type                             | EenhoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer |                000000012 |
      | ouder.burgerservicenummer        |                000000048 |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      |                              0599 |
    En voor de persoon geldt het volgende gezag
      | naam                             | waarde                    |
      | type                             | TweehoofdigOuderlijkGezag |
      | minderjarige.burgerservicenummer |                 000000024 |
    En het gezag heeft de volgende ouders
      | burgerservicenummer |
      |           000000048 |
      |           000000061 |
    En de persoon met burgerservicenummer '000000036' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      |                              0599 |
    En voor de persoon geldt het volgende gezag
      | naam                             | waarde           |
      | type                             | GezamenlijkGezag |
      | minderjarige.burgerservicenummer |        000000036 |
      | ouder.burgerservicenummer        |        000000061 |
      | derde.type                       | BekendeDerde     |
      | derde.burgerservicenummer        |        000000048 |

  Regel: Het vragen van één of meerdere velden van 'gezag' levert alle velden van 'gezag' die van toepassing zijn op de persoon

    Abstract Scenario: gezag vragen met fields <fields> geeft alle van velden van alle soorten gezag van toepassing op de persoon
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | burgerservicenummer,<fields>            |
      Dan heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000024 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000061 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000036 |
        | ouder.burgerservicenummer        |        000000061 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000061 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000024 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000061 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000036 |
        | ouder.burgerservicenummer        |        000000061 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000048 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000024 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000061 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000036 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000036 |
        | ouder.burgerservicenummer        |        000000061 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |

      Voorbeelden:
        | fields                                     |
        | gezag                                      |
        | gezag.type                                 |
        | gezag.minderjarige                         |
        | gezag.ouders                               |
        | gezag.ouder                                |
        | gezag.derde                                |
        | gezag.derden                               |
        | gezag.minderjarige.burgerservicenummer     |
        | gezag.ouders.burgerservicenummer           |
        | gezag.ouder.burgerservicenummer            |
        | gezag.derde.burgerservicenummer            |
        | gezag.derden.burgerservicenummer           |
        | gezag.ouders,gezag.ouder                   |
        | gezag.type,gezag.minderjarige,gezag.ouders |

    Scenario: er is gezag van toepassing op de persoon, maar dit wordt niet gevraagd
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | burgerservicenummer                     |
      Dan heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000061 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
      En heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000036 |

    @fout-case
    Scenario: De fields parameter bevat het pad naar een niet bestaand veld in gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag.bestaatNiet                       |
      Dan heeft de response de volgende gegevens
        | naam     | waarde                                                      |
        | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
        | title    | Een of meerdere parameters zijn niet correct.               |
        | status   |                                                         400 |
        | detail   | De foutieve parameter(s) zijn: fields[0].                   |
        | code     | paramsValidation                                            |
        | instance | /haalcentraal/api/brp/personen                              |
      En heeft de response invalidParams met de volgende gegevens
        | code   | name      | reason                                       |
        | fields | fields[0] | Parameter bevat een niet bestaande veldnaam. |

  Regel: Het toelichting veld bij een 'niet te bepalen' gezagsrelatie wordt niet geleverd

    Scenario: Gezag kan niet worden bepaald voor de gevraagde persoon en de response bevat een toelichting
      Gegeven adres 'A2' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0518 |                         0518010051001502 |
      Gegeven de persoon met burgerservicenummer '000000480' is ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0518 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0518010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
