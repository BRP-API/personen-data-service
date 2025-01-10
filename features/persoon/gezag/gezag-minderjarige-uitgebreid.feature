# language: nl
@api
Functionaliteit: gezagsrelaties van een meerderjarige

  Regel: wanneer gezag wordt gevraagd wordt indien aanwezig naam, geslacht en geboorte geleverd

    Scenario: gezag voor een persoon met Voogdij wordt gevraagd
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                                 | waarde           |
        | geslachtsaanduiding (04.10)          | M                |
        | voornamen (02.10)                    | Jan Peter        |
        | adellijke titel of predicaat (02.20) | JH               |
        | voorvoegsel (02.30)                  | te               |
        | geslachtsnaam (02.40)                | Hoogh            |
        | aanduiding naamgebruik (61.10)       | E                |
        | geboortedatum (03.10)                | morgen - 18 jaar |
      Gegeven de persoon heeft de volgende 'gezagsverhouding' gegevens
        | naam                                 | waarde |
        | indicatie gezag minderjarige (32.10) |     1D |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En het gezag heeft geen derden
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                                                   | waarde              |
        | type                                                   | Voogdij             |
        | minderjarige.burgerservicenummer                       |           000000012 |
        | minderjarige.naam.voornamen                            | Jan Peter           |
        | minderjarige.naam.voorvoegsel                          | te                  |
        | minderjarige.naam.geslachtsnaam                        | Hoogh               |
        | minderjarige.naam.aanduidingNaamgebruik.code           | E                   |
        | minderjarige.naam.aanduidingNaamgebruik.omschrijving   | eigen geslachtsnaam |
        | minderjarige.naam.adellijkeTitelPredicaat.code         | JH                  |
        | minderjarige.naam.adellijkeTitelPredicaat.omschrijving | jonkheer            |
        | minderjarige.naam.adellijkeTitelPredicaat.soort        | predicaat           |
        | minderjarige.geboorte.datum                            | morgen - 18 jaar    |
        | minderjarige.geslacht.code                             | M                   |
        | minderjarige.geslacht.omschrijving                     | man                 |
      En heeft 'gezag' geen derden
