#language: nl

@api
Functionaliteit: ZoekMetNaamEnGemeenteVanInschrijving bij geblokkeerde persoonslijst

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) |
    | 0014                 |

  Regel: zoeken vindt ook personen waarvan de persoonslijst is geblokkeerd

    Scenario: Persoonslijst is geblokkeerd
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | geboortedatum (03.10) | geslachtsnaam (02.40) | voornamen (02.10) |
      | 19830526              | Maassen               | Jan Peter         |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0014                              |
      En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
      | geboortedatum (03.10) | geslachtsnaam (02.40) | voornamen (02.10) |
      | 19830526              | Maassen               | Jan Peter         |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | Datum ingang blokkering (66.20) |
      | 20230221                        |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0014                              |
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                               |
      | type                    | ZoekMetNaamEnGemeenteVanInschrijving |
      | geslachtsnaam           | Maassen                              |
      | voornamen               | Jan Peter                            |
      | gemeenteVanInschrijving | 0014                                 |
      | fields                  | burgerservicenummer                  |
      Dan heeft de response 2 personen
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000048 |