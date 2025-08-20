# language: nl
@api
Functionaliteit: geef geleverde persoonslijsten door aan protocollerings-API

    Achtergrond:
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | pl_id | geboortedatum (03.10) | geslachtsnaam (02.40) | voornamen (02.10) |
      | 2001  | 19830526              | Maassen               | Pieter            |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
      | pl_id | geboortedatum (03.10) | geslachtsnaam (02.40) | voornamen (02.10) | voorvoegsel (02.30) |
      | 2002  | 19830526              | Maassen               | Jan Peter         | van                 |


  Regel: in de response wordt een komma gescheiden lijst van geleverde persoonslijsten opgenomen in de response header "x-geleverde-pls"

    Scenario: raadplegen levert 1 persoon
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000024                       |
      | fields              | naam.voornamen                  |
      Dan heeft de response een persoon met de volgende gegevens
      | naam           | waarde |
      | naam.voornamen | Pieter |
      En de response headers is gelijk aan
      | naam            | waarde |
      | x-geleverde-pls | 2001   |

    Scenario: zoeken levert 2 personen
      Als personen wordt gezocht met de volgende parameters
      | naam          | waarde                              |
      | type          | ZoekMetGeslachtsnaamEnGeboortedatum |
      | geslachtsnaam | Maassen                             |
      | geboortedatum | 1983-05-26                          |
      | fields        | naam.voornamen                      |
      Dan heeft de response 2 personen
      En heeft de response een persoon met de volgende gegevens
      | naam           | waarde |
      | naam.voornamen | Pieter |
      En heeft de response een persoon met de volgende gegevens
      | naam           | waarde    |
      | naam.voornamen | Jan Peter |
      En heeft de response de volgende headers
      | naam            | waarde    |
      | x-geleverde-pls | 2001,2002 |

    Scenario: zoeken levert geen persoon
      Als personen wordt gezocht met de volgende parameters
      | naam          | waarde                              |
      | type          | ZoekMetGeslachtsnaamEnGeboortedatum |
      | geslachtsnaam | Maassen                             |
      | geboortedatum | 1985-05-01                          |
      | fields        | naam.voornamen                      |
      Dan heeft de response 0 personen
      En heeft de response geen 'x-geleverde-pls' header

    @fout-case
    Scenario: raadplegen levert een foutmelding
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | bestaatNiet                     |
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
      En heeft de response geen 'x-geleverde-pls' header
