#language: nl

@api
Functionaliteit: ZoekMetAdresseerbaarObjectIdentificatie van persoonslijst met opschorting bijhouding

    Achtergrond:
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800010051001502                         |

  Regel: Een persoonslijst met reden opschorting bijhouding "W" (wissen) wordt niet geleverd

    Scenario: persoonslijst heeft opschorting bijhouding reden "W"
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | W                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 0 personen


  Regel: Een persoonslijst met reden opschorting bijhouding "F" (fout) wordt niet geleverd

    Scenario: persoonslijst heeft opschorting bijhouding reden "F"
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | F                                    |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 0 personen


  Regel: Een persoonslijst met reden opschorting bijhouding gelijk aan "O" (overleden) wordt alleen gevonden bij gebruik van parameter inclusiefOverledenPersonen met waarde true

    Scenario: persoonslijst heeft opschorting bijhouding reden "O" en parameter inclusiefOverledenPersonen wordt niet gebruikt
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | naam                                 | waarde |
      | reden opschorting bijhouding (67.20) | O      |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met alleen de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |

    Scenario: persoonslijst heeft opschorting bijhouding reden "O" en parameter inclusiefOverledenPersonen wordt gebruikt met waarde false
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | naam                                 | waarde |
      | reden opschorting bijhouding (67.20) | O      |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | inclusiefOverledenPersonen       | false                                   |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met alleen de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |

    Scenario: persoonslijst heeft opschorting bijhouding reden "O" en parameter inclusiefOverledenPersonen wordt gebruikt met waarde true
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | naam                                 | waarde |
      | reden opschorting bijhouding (67.20) | O      |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | inclusiefOverledenPersonen       | true                                    |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 2 personen


  Regel: Een persoonslijst met overige reden opschorting bijhouding kan wel worden gevonden en geleverd

    Abstract Scenario: persoonslijst heeft opschorting bijhouding reden "<opschorting>"
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) |
      | 0800                              |
      En de persoon heeft de volgende 'inschrijving' gegevens
      | datum opschorting bijhouding (67.10) | reden opschorting bijhouding (67.20) |
      | 20220829                             | <opschorting>                        |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0800010051001502                        |
      | fields                           | burgerservicenummer                     |
      Dan heeft de response 1 persoon

      Voorbeelden:
      | opschorting | omschrijving              |
      | E           | emigratie                 |
      | M           | ministerieel besluit      |
      | R           | pl is aangelegd in de rni |
      | .           | onbekend                  |