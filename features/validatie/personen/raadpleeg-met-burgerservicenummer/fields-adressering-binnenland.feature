#language: nl

@input-validatie
Functionaliteit: adressering binnenland velden vragen met fields bij raadplegen met burgerservicenummer

  @geen-protocollering
  Abstract Scenario: de fields parameter bevat adressering binnenland veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                              |
    | adresseringBinnenland                               |
    | adresseringBinnenland.adresregel1                   |
    | adresseringBinnenland.adresregel2                   |
    | adresseringBinnenland.aanhef                        |
    | adresseringBinnenland.aanschrijfwijze               |
    | adresseringBinnenland.aanschrijfwijze.aanspreekvorm |
    | adresseringBinnenland.aanschrijfwijze.naam          |
    | adresseringBinnenland.gebruikInLopendeTekst         |

  @fout-case
  Scenario: De fields parameter bevat een niet-bestaand adressering binnenland veld
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                            |
    | type                | RaadpleegMetBurgerservicenummer   |
    | burgerservicenummer | 000000048                         |
    | fields              | adresseringBinnenland.bestaatNiet |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                      |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
    | title    | Een of meerdere parameters zijn niet correct.               |
    | status   | 400                                                         |
    | detail   | De foutieve parameter(s) zijn: fields[0].                   |
    | code     | paramsValidation                                            |
    | instance | /haalcentraal/api/brp/personen                              |
    En heeft de response invalidParams met de volgende gegevens
    | code   | name      | reason                                       |
    | fields | fields[0] | Parameter bevat een niet bestaande veldnaam. |
