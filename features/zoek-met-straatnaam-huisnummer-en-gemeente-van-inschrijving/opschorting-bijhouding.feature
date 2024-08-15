#language: nl

@api
Functionaliteit: ZoekMetStraatHuisnummerEnGemeenteVanInschrijving van persoonslijst met opschorting bijhouding

  Regel: Een persoonslijst met reden opschorting bijhouding "W" (wissen) wordt niet geleverd

    Scenario: persoonslijst heeft opschorting bijhouding reden "W"
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | straatnaam (11.10) | huisnummer (11.20) |
      | 0599                 | Boterdiep          | 32                 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | W                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                                           |
      | type                    | ZoekMetStraatHuisnummerEnGemeenteVanInschrijving |
      | straat                  | Boterdiep                                        |
      | huisnummer              | 32                                               |
      | gemeenteVanInschrijving | 0599                                             |
      | fields                  | burgerservicenummer                              |
      Dan heeft de response 0 personen


  Regel: Een persoonslijst met reden opschorting bijhouding "F" (fout) wordt niet geleverd

    Scenario: persoonslijst heeft opschorting bijhouding reden "F"
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | straatnaam (11.10) | huisnummer (11.20) |
      | 0599                 | Boterdiep          | 32                 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | F                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                                           |
      | type                    | ZoekMetStraatHuisnummerEnGemeenteVanInschrijving |
      | straat                  | Boterdiep                                        |
      | huisnummer              | 32                                               |
      | gemeenteVanInschrijving | 0599                                             |
      | fields                  | burgerservicenummer                              |
      Dan heeft de response 0 personen


  Regel: Een persoonslijst met reden opschorting bijhouding ongelijk aan "O" (overleden) wordt alleen gevonden bij gebruik van parameter inclusiefOverledenPersonen met waarde true

    Scenario: persoonslijst heeft opschorting bijhouding reden "O" en inclusiefOverledenPersonen wordt niet gebruikt
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | straatnaam (11.10) | huisnummer (11.20) |
      | 0599                 | Boterdiep          | 32                 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | O                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                                           |
      | type                    | ZoekMetStraatHuisnummerEnGemeenteVanInschrijving |
      | straat                  | Boterdiep                                        |
      | huisnummer              | 32                                               |
      | gemeenteVanInschrijving | 0599                                             |
      | fields                  | burgerservicenummer                              |
      Dan heeft de response 0 personen

    Abstract Scenario: persoonslijst heeft opschorting bijhouding reden "O" en inclusiefOverledenPersonen heeft waarde <inclusiefOverledenPersonen>
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | straatnaam (11.10) | huisnummer (11.20) |
      | 0599                 | Boterdiep          | 32                 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | O                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                       | waarde                                           |
      | type                       | ZoekMetStraatHuisnummerEnGemeenteVanInschrijving |
      | straat                     | Boterdiep                                        |
      | huisnummer                 | 32                                               |
      | gemeenteVanInschrijving    | 0599                                             |
      | fields                     | burgerservicenummer                              |
      | inclusiefOverledenPersonen | <inclusiefOverledenPersonen>                     |
      Dan heeft de response <aantal gevonden personen> persoon

      Voorbeelden:
      | inclusiefOverledenPersonen | aantal gevonden personen |
      | false                      | 0                        |
      | true                       | 1                        |


  Regel: Een persoonslijst met overige reden opschorting bijhouding kan wel worden gevonden en geleverd

    Abstract Scenario: persoonslijst heeft opschorting bijhouding reden "<opschorting>"
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | straatnaam (11.10) | huisnummer (11.20) |
      | 0599                 | Boterdiep          | 32                 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0599                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | <opschorting>                        |
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                                           |
      | type                    | ZoekMetStraatHuisnummerEnGemeenteVanInschrijving |
      | straat                  | Boterdiep                                        |
      | huisnummer              | 32                                               |
      | gemeenteVanInschrijving | 0599                                             |
      | fields                  | burgerservicenummer                              |
      Dan heeft de response 1 persoon

      Voorbeelden:
      | opschorting | omschrijving              |
      | E           | emigratie                 |
      | M           | ministerieel besluit      |
      | R           | pl is aangelegd in de rni |
      | .           | onbekend                  |
