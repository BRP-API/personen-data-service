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
