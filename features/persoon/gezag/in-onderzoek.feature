#language: nl

Functionaliteit: doorgeven van indicatie in onderzoek

  Regel: wanneer de Gezag API een gezagsrelatie levert waarvan het inOnderzoek veld is gevuld, dan wordt dit veld doorgegeven

    Scenario: gezag wordt gevraagd en de Gezag API levert een gezagsrelatie met inOnderzoek veld
      Gegeven de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
      | geslachtsnaam (02.40) |
      | Hoogh                 |
      En voor de persoon geldt het volgende gezag
      | type                     | minderjarige.burgerservicenummer | ouder.burgerservicenummer | inOnderzoek |
      | EenhoofdigOuderlijkGezag | 000000048                        | 000000012                 | true        |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000048                       |
      | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
      | type                     | inOnderzoek | ouder.burgerservicenummer | minderjarige.burgerservicenummer | minderjarige.naam.geslachtsnaam |
      | EenhoofdigOuderlijkGezag | true        | 000000012                 | 000000048                        | Hoogh                           |
