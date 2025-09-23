# language: nl
@api @deprecated
Functionaliteit: gezagsrelaties van een meerderjarige

  Regel: een meerderjarige krijgt voor een minderjarig kind met twee ouders met gezag de gezagsrelatie naar beide ouders geleverd

    Scenario: beide ouders hebben gezag over het minderjarige kind van bevraagde persoon
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000024 |
        |           000000048 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
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
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde                    |
        | type                                                   | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                 000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter                 |
        | minderjarige.naam.voorvoegsel                          | te                        |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                     |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                        |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                  |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                 |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar          |
        | minderjarige.geslacht.code                             | M                         |
        | minderjarige.geslacht.omschrijving                     | man                       |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                                      | waarde              |
        | burgerservicenummer                       |           000000024 |
        | naam.voornamen                            | Alex                |
        | naam.voorvoegsel                          | te                  |
        | naam.geslachtsnaam                        | Hoogh               |
        | naam.adellijkeTitelPredicaat.code         | JH                  |
        | naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | geslacht.code                             | M                   |
        | geslacht.omschrijving                     | man                 |
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

  Regel: een meerderjarige die als enige gezag heeft over een minderjarig kind krijgt de gezagsrelatie naar zichzelf geleverd

    Scenario: alleen de bevraagde persoon heeft gezag over de minderjarige
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000024 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
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
        | ouder.burgerservicenummer                              |                000000024 |
        | ouder.naam.voornamen                                   | Alex                     |
        | ouder.naam.voorvoegsel                                 | te                       |
        | ouder.naam.geslachtsnaam                               | Hoogh                    |
        | ouder.naam.adellijkeTitelPredicaat.code                | JH                       |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkheer                 |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat                |
        | ouder.geslacht.code                                    | M                        |
        | ouder.geslacht.omschrijving                            | man                      |

  Regel: een meerderjarige die samen met diens partner gezag heeft over een minderjarig kind krijgt de gezagsrelatie naar de ouder en de partner geleverd

    Scenario: de ouder en diens partner hebben gezag over het kind
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
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
        | ouder.burgerservicenummer                              |           000000024 |
        | ouder.naam.voornamen                                   | Alex                |
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

  Regel: een meerderjarige die gezag heeft over een minderjarig kind van de partner krijgt de gezagsrelatie naar de ouder en zichzelf geleverd
    # de gezagsmodule levert bij het bevragen van de niet-ouder geen gezag
    # het gezag kan achterhaald worden door het gezag van de kinderen van de partner op te vragen
    # voor elke gezagsrelatie van de minderjarige kinderen wordt bepaald of de niet-ouder gezamenlijk gezag heeft over het kind

    Scenario: persoon heeft van rechtswege gezamenlijk gezag over het minderjarige kind van diens partner
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
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
        | ouder.burgerservicenummer                              |           000000024 |
        | ouder.naam.voornamen                                   | Alex                |
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

    Scenario: persoon heeft van rechtswege gezamenlijk gezag over enkele van de minderjarige kinderen van diens partner
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
        |                   000000024 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon met burgerservicenummer '000000013' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | morgen - 6 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon met burgerservicenummer '000000014' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Arie            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | morgen - 3 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      Gegeven de persoon met burgerservicenummer '000000061' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Karel              |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 50 jaar |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000048 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000061 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000013 |
        | ouder.burgerservicenummer        |        000000048 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000061 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000061 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
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
        | ouder.burgerservicenummer                              |           000000048 |
        | ouder.naam.voornamen                                   | Carolina            |
        | ouder.naam.voorvoegsel                                 | te                  |
        | ouder.naam.geslachtsnaam                               | Hoogh               |
        | ouder.naam.adellijkeTitelPredicaat.code                | JV                  |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw           |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | ouder.geslacht.code                                    | V                   |
        | ouder.geslacht.omschrijving                            | vrouw               |
        | derde.type                                             | BekendeDerde        |
        | derde.burgerservicenummer                              |           000000061 |
        | derde.naam.voornamen                                   | Karel               |
        | derde.naam.voorvoegsel                                 | te                  |
        | derde.naam.geslachtsnaam                               | Hoogh               |
        | derde.naam.adellijkeTitelPredicaat.code                | JH                  |
        | derde.naam.adellijkeTitelPredicaat.omschrijving        | jonkheer            |
        | derde.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | derde.geslacht.code                                    | M                   |
        | derde.geslacht.omschrijving                            | man                 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
        | minderjarige.burgerservicenummer                       |           000000013 |
        | minderjarige.naam.voornamen                            | Alex                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | morgen - 6 jaar     |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
        | ouder.burgerservicenummer                              |           000000048 |
        | ouder.naam.voornamen                                   | Carolina            |
        | ouder.naam.voorvoegsel                                 | te                  |
        | ouder.naam.geslachtsnaam                               | Hoogh               |
        | ouder.naam.adellijkeTitelPredicaat.code                | JV                  |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw           |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | ouder.geslacht.code                                    | V                   |
        | ouder.geslacht.omschrijving                            | vrouw               |
        | derde.type                                             | BekendeDerde        |
        | derde.burgerservicenummer                              |           000000061 |
        | derde.naam.voornamen                                   | Karel               |
        | derde.naam.voorvoegsel                                 | te                  |
        | derde.naam.geslachtsnaam                               | Hoogh               |
        | derde.naam.adellijkeTitelPredicaat.code                | JH                  |
        | derde.naam.adellijkeTitelPredicaat.omschrijving        | jonkheer            |
        | derde.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | derde.geslacht.code                                    | M                   |
        | derde.geslacht.omschrijving                            | man                 |

    Scenario: persoon heeft geen gezag over het minderjarige kind van diens partner
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      Gegeven de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000024 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een ouder '2' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000024 |
        |           000000036 |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

    Scenario: persoon heeft gezag over een eigen kind en heeft van rechtswege gezamenlijk gezag over het minderjarige kind van diens partner
      Gegeven de persoon met burgerservicenummer '000000036' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Bert             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000036 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000036 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
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
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000036 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000024 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000048 |
        |           000000061 |
      Gegeven de persoon met burgerservicenummer '000000061' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Karel              |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 50 jaar |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezamenlijkGezag    |
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
        | ouder.burgerservicenummer                              |           000000036 |
        | ouder.naam.voornamen                                   | Bert                |
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
        | naam                                                   | waarde                    |
        | type                                                   | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                 000000024 |
        | minderjarige.naam.voornamen                            | Alex                      |
        | minderjarige.naam.voorvoegsel                          | te                        |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                     |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                        |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                  |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar          |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                 |
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

    Abstract Scenario: voor het minderjarige kind van de partner <omschrijving>
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      Gegeven de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 12 jaar |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

      Voorbeelden:
        | soort gezag | omschrijving                      | meerderjarige |
        | N           | kan het gezag niet bepaald worden |               |
        | G           | is er tijdelijk geen gezag        |               |
        | OG1         | heeft alleen de ouder gezag       |     000000024 |

  Regel: een meerderjarige krijg voor een meerderjarig kind geen gezagsrelatie geleverd

    Scenario: gezag wordt gevraagd van ouder met meerderjarig kind
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Jan Peter          |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 18 jaar |
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon met burgerservicenummer '000000012' heeft geen gezagsrelaties
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Alex             |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 45 jaar |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige krijgt voor een minderjarig kind waarvoor het gezag niet bepaald kan worden geen gezagsrelatie geleverd

    Scenario: gezag over minderjarige kind kan niet worden bepaald
      Gegeven de persoon met burgerservicenummer '000000024' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam | waarde             |
        | type | GezagNietTeBepalen |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige krijgt voor een minderjarig kind waarover tijdelijk niemand gezag heeft geen gezagsrelatie geleverd

    Scenario: tijdelijk heeft niemand gezag over een minderjarig kind
      Gegeven de persoon met burgerservicenummer '000000024' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft de volgende 'gezagsverhouding' gegevens
        | naam                               | waarde |
        | indicatie curateleregister (33.10) |      1 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam | waarde             |
        | type | TijdelijkGeenGezag |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige die van rechtswege gezag heeft over een minderjarige die geen kind is van de meerderjarige krijgt de gezagsrelatie naar zichzelf geleverd
    # de gezagsmodule levert bij het bevragen van de niet-ouder geen gezag
    # het gezag kan achterhaald worden door het gezag van de kinderen van de partner op te vragen
    # voor elke gezagsrelatie van de minderjarige kinderen wordt bepaald of de niet-ouder voogd is van dit kind

    Scenario: de partner van overleden ouder heeft gezag over de minderjarige kinderen
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon met burgerservicenummer '000000024' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon met burgerservicenummer '000000036' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft de volgende 'overlijden' gegevens
        | naam                     | waarde   |
        | datum overlijden (08.10) | 20231001 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000048' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
        |                   000000036 |                                                           20120428 |
      En de 'partner' is gewijzigd naar de volgende gegevens
        | burgerservicenummer (01.20) | datum ontbinding huwelijk/geregistreerd partnerschap (07.10) | reden ontbinding huwelijk/geregistreerd partnerschap (07.40) |
        |                   000000036 |                                                     20231001 | O                                                            |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer |    000000048 |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer |    000000048 |

    Scenario: de partner van ouder onder curatele heeft gezag over een minderjarig kind, maar niet over alle kinderen
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon met burgerservicenummer '000000012' gelden de volgende gezagsrelaties
        | bsnMinderjarige | soortGezag | bsnMeerderjarige |
        |       000000024 | N          |                  |
      En de persoon met burgerservicenummer '000000024' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000036' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000048' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000061' heeft de volgende 'gezagsverhouding' gegevens
        | naam                               | waarde |
        | indicatie curateleregister (33.10) |      1 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000073 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000073' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000061 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000073 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000073 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer |    000000073 |

    Scenario: de partner van overleden ouder heeft gezag over de minderjarige kinderen en heeft inmiddels andere partner
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000036' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft de volgende 'overlijden' gegevens
        | naam                     | waarde   |
        | datum overlijden (08.10) | 20231001 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000048' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
        |                   000000036 |                                                           20120428 |
      En de 'partner' is gewijzigd naar de volgende gegevens
        | burgerservicenummer (01.20) | datum ontbinding huwelijk/geregistreerd partnerschap (07.10) | reden ontbinding huwelijk/geregistreerd partnerschap (07.40) |
        |                   000000036 |                                                     20191001 | O                                                            |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
        |                   000000061 |                                                           20230614 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      En de persoon met burgerservicenummer '000000061' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
        |                   000000048 |                                                           20230614 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer |    000000048 |

  Regel: een persoon die in RNI staat ingeschreven krijgt gezag niet te bepalen geleverd wanneer de leeftijd lager is dan 18 jaar
    # voor een persoon die staat ingeschreven in RNI (gemeente van inschrijving is gelijk aan 1999) levert de gezagsmodule altijd soort gezag 'N' (niet te bepalen)
    # wanneer zeker is dat de persoon meerderjarig is, moet geen 'gezag niet te bepalen' worden geleverd
    # wanneer de leeftijd exact bepaald kan worden (zie ../leeftijd/overzicht.feature), wordt 'gezag niet te bepalen' alleen geleverd wanneer de leeftijd lager is dan 18 jaar

    Abstract Scenario: gezag van minderjarig persoon in RNI kan niet worden bepaald
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | <geboortedatum> |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | naam                              | waarde |
        | gemeente van inschrijving (09.10) |   1999 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezagNietTeBepalen  |
        | toelichting                                            | test                |
        | minderjarige.burgerservicenummer                       |           000000024 |
        | minderjarige.naam.voornamen                            | Alex                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | <geboortedatum>     |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |

      Voorbeelden:
        | geboortedatum            | berekende leeftijd |
        | gisteren - 5 jaar        |                  5 |
        | vandaag - 17 jaar        |                 17 |
        | morgen - 12 jaar         |                 17 |
        | vorige maand - 10 jaar   |                 10 |
        | vorige maand - 17 jaar   |                 17 |
        | volgende maand - 18 jaar |                 17 |

    Abstract Scenario: voor een meerderjarig persoon in RNI wordt geen gezag geleverd
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | <geboortedatum> |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | naam                              | waarde |
        | gemeente van inschrijving (09.10) |   1999 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

      Voorbeelden:
        | geboortedatum            | berekende leeftijd |
        |                 19590210 |                65+ |
        | vandaag - 18 jaar        |                 18 |
        | vorige maand - 18 jaar   |                 18 |
        | volgende maand - 19 jaar |                 18 |

  Regel: een persoon die in RNI staat ingeschreven met een onvolledige geboortedatum krijgt gezag niet te bepalen geleverd wanneer het geboortejaar is ten minste 19 jaar voor het huidige jaar
    # wanneer de leeftijd niet exact bepaald kan worden en er is wel een geboortejaar bekend, wordt 'gezag niet te bepalen' alleen geleverd wanneer de persoon aan het begin van het jaar al meerderjarig (18 jaar) is

    Abstract Scenario: gezag van minderjarig persoon in RNI kan niet worden bepaald
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | <geboortedatum> |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | naam                              | waarde |
        | gemeente van inschrijving (09.10) |   1999 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezagNietTeBepalen  |
        | toelichting                                            | test                |
        | minderjarige.burgerservicenummer                       |           000000024 |
        | minderjarige.naam.voornamen                            | Alex                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | <geboortedatum>     |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |

      Voorbeelden:
        | geboortedatum        | leeftijd      |
        | deze maand - 5 jaar  |   4 of 5 jaar |
        | dit jaar - 5 jaar    |   4 of 5 jaar |
        | deze maand - 18 jaar | 17 of 18 jaar |
        | dit jaar - 18 jaar   | 17 of 18 jaar |

    Abstract Scenario: voor een meerderjarig persoon in RNI wordt geen gezag geleverd
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde          |
        | geslachtsaanduiding (04.10)          | M               |
        | voornamen (02.10)                    | Alex            |
        | adellijke titel of predicaat (02.20) | JH              |
        | voorvoegsel (02.30)                  | te              |
        | geslachtsnaam (02.40)                | Hoogh           |
        | aanduiding naamgebruik (61.10)       | E               |
        | geboortedatum (03.10)                | <geboortedatum> |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | naam                              | waarde |
        | gemeente van inschrijving (09.10) |   1999 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

      Voorbeelden:
        | geboortedatum        | leeftijd      |
        | deze maand - 35 jaar | 34 of 35 jaar |
        | dit jaar - 35 jaar   | 34 of 35 jaar |
        | deze maand - 19 jaar | 18 of 19 jaar |
        | dit jaar - 19 jaar   | 18 of 19 jaar |

  Regel: een persoon die in RNI staat ingeschreven krijgt gezag niet te bepalen geleverd wanneer de geboortedatum volledig onbekend is

    Scenario: gezag van minderjarig persoon in RNI kan niet worden bepaald
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | naam                                 | waarde   |
        | geslachtsaanduiding (04.10)          | M        |
        | voornamen (02.10)                    | Alex     |
        | adellijke titel of predicaat (02.20) | JH       |
        | voorvoegsel (02.30)                  | te       |
        | geslachtsnaam (02.40)                | Hoogh    |
        | aanduiding naamgebruik (61.10)       | E        |
        | geboortedatum (03.10)                | 00000000 |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | naam                              | waarde |
        | gemeente van inschrijving (09.10) |   1999 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | GezagNietTeBepalen  |
        | toelichting                                            | test                |
        | minderjarige.burgerservicenummer                       |           000000024 |
        | minderjarige.naam.voornamen                            | Alex                |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            |            00000000 |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |

  Regel: een meerderjarige met gezag over meerdere minderjarigen krijgt de gezagsrelaties van al deze minderjarigen

    Scenario: meerderjarige heeft gezag over meerdere kinderen
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | V                |
        | voornamen (02.10)                    | Carolina         |
        | adellijke titel of predicaat (02.20) | JV               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 35 jaar |
      Gegeven de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000013 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000014 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000048 |
        |           000000061 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000013 |
        | ouder.burgerservicenummer        |                000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000014 |
        | ouder.burgerservicenummer        |        000000048 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000073 |
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
      En de persoon met burgerservicenummer '000000061' heeft de volgende gegevens
        | naam                                 | waarde             |
        | geslachtsaanduiding (04.10)          | M                  |
        | voornamen (02.10)                    | Karel              |
        | adellijke titel of predicaat (02.20) | JH                 |
        | voorvoegsel (02.30)                  | te                 |
        | geslachtsnaam (02.40)                | Hoogh              |
        | aanduiding naamgebruik (61.10)       | E                  |
        | geboortedatum (03.10)                | gisteren - 50 jaar |
      En de persoon met burgerservicenummer '000000073' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan              |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 55 jaar |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000048 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde                    |
        | type                                                   | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                 000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter                 |
        | minderjarige.naam.voorvoegsel                          | te                        |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                     |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                        |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                  |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                 |
        | minderjarige.geboorte.datum                            | morgen - 12 jaar          |
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
        | naam                                                   | waarde                   |
        | type                                                   | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer                       |                000000013 |
        | minderjarige.naam.voornamen                            | Alex                     |
        | minderjarige.naam.voorvoegsel                          | te                       |
        | minderjarige.naam.geslachtsnaam                        | Hoogh                    |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                       |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer                 |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat                |
        | minderjarige.geboorte.datum                            | morgen - 6 jaar          |
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
        | ouder.burgerservicenummer                              |           000000048 |
        | ouder.naam.voornamen                                   | Carolina            |
        | ouder.naam.voorvoegsel                                 | te                  |
        | ouder.naam.geslachtsnaam                               | Hoogh               |
        | ouder.naam.adellijkeTitelPredicaat.code                | JV                  |
        | ouder.naam.adellijkeTitelPredicaat.omschrijving        | jonkvrouw           |
        | ouder.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | ouder.geslacht.code                                    | V                   |
        | ouder.geslacht.omschrijving                            | vrouw               |
        | derde.type                                             | BekendeDerde        |
        | derde.burgerservicenummer                              |           000000073 |
        | derde.naam.voornamen                                   | Jan                 |
        | derde.naam.voorvoegsel                                 | te                  |
        | derde.naam.geslachtsnaam                               | Hoogh               |
        | derde.naam.adellijkeTitelPredicaat.code                | JH                  |
        | derde.naam.adellijkeTitelPredicaat.omschrijving        | jonkheer            |
        | derde.naam.adellijkeTitelPredicaat.soort               | predicaat           |
        | derde.geslacht.code                                    | M                   |
        | derde.geslacht.omschrijving                            | man                 |
