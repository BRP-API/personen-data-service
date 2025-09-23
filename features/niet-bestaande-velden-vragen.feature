# language: nl
Functionaliteit: niet bestaande velden vragen met fields

  Regel: alle velden van een datum wordt ook geleverd als niet-bestaande sub-velden wordt gevraagd

    Scenario: de geboortedatum van een persoon wordt gevraagd
      Gegeven de persoon met burgerservicenummer '000000152' heeft de volgende gegevens
        | geboortedatum (03.10) |
        |              20250801 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000152 |
        | fields              | geboorte.datum.nietBestaand     |
      Dan heeft de response een persoon met alleen de volgende gegevens
        | naam           | waarde   |
        | geboorte.datum | 20250801 |

    Scenario: de geboortedatum van gezochte personen wordt gevraagd
      Gegeven de persoon met burgerservicenummer '000000152' heeft de volgende gegevens
        | naam                  | waarde   |
        | geslachtsnaam (02.40) | Maassen  |
        | geboortedatum (03.10) | 19500304 |
      Als personen wordt gezocht met de volgende parameters
        | naam          | waarde                              |
        | type          | ZoekMetGeslachtsnaamEnGeboortedatum |
        | geslachtsnaam | Maassen                             |
        | geboortedatum |                          1950-03-04 |
        | fields        | geboorte.datum.nietBestaand         |
      Dan heeft de response een persoon met alleen de volgende gegevens
        | naam           | waarde   |
        | geboorte.datum | 19500304 |

  Regel: alle velden van een tabelwaarde wordt ook geleverd als niet-bestaande sub-velden wordt gevraagd

    Scenario: de 'adellijke titel of predicaat (02.20)' wordt gevraagd met een niet-bestaand sub-veld
      Gegeven de persoon met burgerservicenummer '000000152' heeft de volgende gegevens
        | naam                                 | waarde |
        | adellijke titel of predicaat (02.20) | JH     |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                                    |
        | type                | RaadpleegMetBurgerservicenummer           |
        | burgerservicenummer |                                 000000152 |
        | fields              | naam.adellijkeTitelPredicaat.nietBestaand |
      Dan heeft de response een persoon met de volgende 'naam' gegevens
        | naam                                 | waarde    |
        | adellijkeTitelPredicaat.code         | JH        |
        | adellijkeTitelPredicaat.omschrijving | jonkheer  |
        | adellijkeTitelPredicaat.soort        | predicaat |
