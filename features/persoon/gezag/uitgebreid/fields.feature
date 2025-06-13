# language: nl
@api @deprecated
Functionaliteit: gezagsrelaties vragen met fields

  Regel: Het vragen van één of meerdere velden van 'gezag' levert alle velden van 'gezag' die van toepassing zijn op de persoon

    Abstract Scenario: gezag vragen met fields <fields> geeft alle van velden van alle soorten gezag van toepassing op de persoon
      Gegeven de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000013 |
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
        | minderjarige.burgerservicenummer |                 000000013 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000048 |
        |           000000061 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000014 |
        | ouder.burgerservicenummer        |        000000061 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000075 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000060 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde             |
        | type                             | TijdelijkGeenGezag |
        | minderjarige.burgerservicenummer |          000000080 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon met burgerservicenummer '000000013' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | morgen - 6 jaar |
      En de persoon met burgerservicenummer '000000014' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Arie            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | morgen - 3 jaar |
      En de persoon met burgerservicenummer '000000060' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Hendrik Jan      |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 18 jaar |
      En de persoon met burgerservicenummer '000000061' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Karel              |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 50 jaar |
      En de persoon met burgerservicenummer '000000075' heeft de volgende gegevens
        | naam                                 | waarde            |
        | geslachtsaanduiding (04.10)          | M                 |
        | voornamen (02.10)                    | Piet              |
        | adellijke titel of predicaat (02.20) | JH                |
        | voorvoegsel (02.30)                  | te                |
        | geslachtsnaam (02.40)                | Hoogh             |
        | aanduiding naamgebruik (61.10)       | E                 |
        | geboortedatum (03.10)                | gisteren - 5 jaar |
      En de persoon met burgerservicenummer '000000080' heeft de volgende gegevens
        | naam                                 | waarde            |
        | geslachtsaanduiding (04.10)          | M                 |
        | voornamen (02.10)                    | Bas               |
        | adellijke titel of predicaat (02.20) | JH                |
        | voorvoegsel (02.30)                  | te                |
        | geslachtsnaam (02.40)                | Hoogh             |
        | aanduiding naamgebruik (61.10)       | E                 |
        | geboortedatum (03.10)                | gisteren - 2 jaar |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | <fields>                        |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde                   |
        | type                                                   | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter                |
        | minderjarige.naam.voorvoegsel                          | te                       |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                    |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                       |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                 |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar         |
        | minderjarige.geslacht.code                             | M                        |
        | minderjarige.geslacht.omschrijving                     | man                      |
        | ouder.burgerservicenummer                              |                000000048 |
        | ouder.naam.voornamen                                   | Carolina                 |
        | ouder.naam.voorvoegsel                                 | te                       |
        | ouder.naam.geslachtsnaam                               | Hoogh                    |
        | ouder.naam.adellijkeTitelPredicaat.code                | JV                       |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw                |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat                |
        | ouder.geslacht.code                                    | V                        |
        | ouder.geslacht.omschrijving                            | vrouw                    |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde                    |
        | type                                                   | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                 000000013 |
        | minderjarige.naam.voornamen                            | Alex                      |
        | minderjarige.naam.voorvoegsel                          | te                        |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                     |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                        |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                  |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                 |
        | minderjarige.geboorte.datum                            | morgen - 6 jaar           |
        | minderjarige.geslacht.code                             | M                         |
        | minderjarige.geslacht.omschrijving                     | man                       |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                                      | waarde              |
        | burgerservicenummer                       |           000000048 |
        | naam.voornamen                            | Carolina            |
        | naam.voorvoegsel                          | te                  |
        | naam.geslachtsnaam                        | Hoogh               |
        | naam.adellijkeTitelPredicaat.code         | JV                  |
        | naam.adellijkeTitelPredicaat.omschrijving | jonkvrouw           |
        | naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | geslacht.code                             | V                   |
        | geslacht.omschrijving                     | vrouw               |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                                      | waarde              |
        | burgerservicenummer                       |           000000061 |
        | naam.voornamen                            | Karel               |
        | naam.voorvoegsel                          | te                  |
        | naam.geslachtsnaam                        | Hoogh               |
        | naam.adellijkeTitelPredicaat.code         | JH                  |
        | naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | geslacht.code                             | M                   |
        | geslacht.omschrijving                     | man                 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
        | minderjarige.burgerservicenummer                       |           000000014 |
        | minderjarige.naam.voornamen                            | Arie                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | morgen - 3 jaar     |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
        | ouder.burgerservicenummer                              |           000000061 |
        | ouder.naam.voornamen                                   | Karel               |
        | ouder.naam.voorvoegsel                                 | te                  |
        | ouder.naam.geslachtsnaam                               | Hoogh               |
        | ouder.naam.adellijkeTitelPredicaat.code                | JH                  |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkheer            |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | ouder.geslacht.code                                    | M                   |
        | ouder.geslacht.omschrijving                            | man                 |
        | derde.type                                             | BekendeDerde        |
        | derde.burgerservicenummer                              |           000000048 |
        | derde.naam.voornamen                                   | Carolina            |
        | derde.naam.voorvoegsel                                 | te                  |
        | derde.naam.geslachtsnaam                               | Hoogh               |
        | derde.naam.adellijkeTitelPredicaat.code                | JV                  |
        | derde.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw           |
        | derde.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | derde.geslacht.code                                    | V                   |
        | derde.geslacht.omschrijving                            | vrouw               |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezagNietTeBepalen  |
        | toelichting                                            | test                |
        | minderjarige.burgerservicenummer                       |           000000075 |
        | minderjarige.naam.voornamen                            | Piet                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | gisteren - 5 jaar   |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | Voogdij             |
        | minderjarige.burgerservicenummer                       |           000000060 |
        | minderjarige.naam.voornamen                            | Hendrik Jan         |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | morgen - 18 jaar    |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                                      | waarde              |
        | type                                      | BekendeDerde        |
        | burgerservicenummer                       |           000000048 |
        | naam.voornamen                            | Carolina            |
        | naam.voorvoegsel                          | te                  |
        | naam.geslachtsnaam                        | Hoogh               |
        | naam.adellijkeTitelPredicaat.code         | JV                  |
        | naam.adellijkeTitelPredicaat.omschrijving | jonkvrouw           |
        | naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | geslacht.code                             | V                   |
        | geslacht.omschrijving                     | vrouw               |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | TijdelijkGeenGezag  |
        | minderjarige.burgerservicenummer                       |           000000080 |
        | minderjarige.naam.voornamen                            | Bas                 |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | gisteren - 2 jaar   |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |

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
      Gegeven de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      Gegeven de persoon heeft een ouder '1' met de volgende gegevens
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
        | naam                                                   | waarde                   |
        | type                                                   | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter                |
        | minderjarige.naam.voorvoegsel                          | te                       |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                    |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                       |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                 |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar         |
        | minderjarige.geslacht.code                             | M                        |
        | minderjarige.geslacht.omschrijving                     | man                      |
        | ouder.burgerservicenummer                              |                000000048 |
        | ouder.naam.voornamen                                   | Carolina                 |
        | ouder.naam.voorvoegsel                                 | te                       |
        | ouder.naam.geslachtsnaam                               | Hoogh                    |
        | ouder.naam.adellijkeTitelPredicaat.code                | JV                       |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw                |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat                |
        | ouder.geslacht.code                                    | V                        |
        | ouder.geslacht.omschrijving                            | vrouw                    |

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
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon met burgerservicenummer '000000013' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | morgen - 6 jaar |
      En de persoon met burgerservicenummer '000000061' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Karel              |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 50 jaar |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000013 |
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
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezagNietTeBepalen  |
        | toelichting                                            | test                |
        | minderjarige.burgerservicenummer                       |           000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter           |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar    |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
