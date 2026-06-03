# language: nl
@api
Functionaliteit: RaadpleegMetBurgerservicenummer wanneer er meerdere persoonslijsten zijn met zelfde burgerservicenummer

  @fout-case
  Scenario: zelfde burgerservicenummer is gebruikt op meerdere persoonslijsten en geen daarvan is afgevoerd
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | geslachtsnaam (02.40) |
      | Maassen               |
    En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | geslachtsnaam (02.40) |
      | Rafi                  |
    Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                                 |
      | type                | RaadpleegMetBurgerservicenummer        |
      | burgerservicenummer |                              000000024 |
      | fields              | burgerservicenummer,naam.geslachtsnaam |
    Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   |                                                         400 |
      | detail   | De foutieve parameter(s) zijn: burgerservicenummer.         |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brp/personen                              |
    En heeft de response invalidParams met de volgende gegevens
      | code   | name                | reason                                           |
      | unique | burgerservicenummer | De opgegeven persoonidentificatie is niet uniek. |
