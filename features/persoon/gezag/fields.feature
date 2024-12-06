# language: nl
@api
Functionaliteit: gezagsrelaties vragen met fields

  Regel: Het vragen van één of meerdere velden van 'gezag' levert alle velden van 'gezag' die van toepassing zijn op de persoon

    Abstract Scenario: gezag vragen met fields <fields> geeft alle van velden van alle soorten gezag van toepassing op de persoon
      Gegeven de persoon met burgerservicenummer '000000048' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
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
        | derde.burgerservicenummer        |        000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam        | waarde             |
        | type        | GezagNietTeBepalen |
        | toelichting | test               |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000060 |
      En het gezag heeft de volgende derden
        | burgerservicenummer |
        |           000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam | waarde             |
        | type | TijdelijkGeenGezag |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | <fields>                        |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
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
        | derde.burgerservicenummer        |        000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam        | waarde             |
        | type        | GezagNietTeBepalen |
        | toelichting | test               |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000060 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam | waarde             |
        | type | TijdelijkGeenGezag |

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

    Abstract Scenario: gezag vragen met fields <fields> dat niet van toepassing is op de persoon geeft alle velden van gezag die wel van toepassing zijn op de persoon
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | <fields>                        |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000048 |

      Voorbeelden:
        | fields                           |
        | gezag.ouders                     |
        | gezag.derde                      |
        | gezag.derden                     |
        | gezag.ouders.burgerservicenummer |
        | gezag.derde.burgerservicenummer  |
        | gezag.derden.burgerservicenummer |
        | gezag.ouders,gezag.derden        |

    Scenario: er is gezag van toepassing op de persoon, maar dit wordt niet gevraagd
      Gegeven de persoon met burgerservicenummer '000000048' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | burgerservicenummer             |
      Dan heeft de response een persoon met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |

    @fout-case
    Scenario: De fields parameter bevat het pad naar een niet bestaand veld in gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag.bestaatNiet               |
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
      Gegeven de persoon met burgerservicenummer '000000048' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt het volgende gezag
        | naam        | waarde             |
        | type        | GezagNietTeBepalen |
        | toelichting | test               |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam        | waarde             |
        | type        | GezagNietTeBepalen |
        | toelichting | test               |
