# language: nl
@api
Functionaliteit: gezagsrelaties van een meerderjarige

  Regel: een meerderjarige krijgt voor een minderjarig kind met twee ouders met gezag de gezagsrelatie naar beide ouders geleverd

    Scenario: beide ouders hebben gezag over het minderjarige kind van bevraagde persoon
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000024 |
        |           000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000048 |

  Regel: een meerderjarige die als enige gezag heeft over een minderjarig kind krijgt de gezagsrelatie naar zichzelf geleverd

    Scenario: alleen de bevraagde persoon heeft gezag over de minderjarige
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000024 |

  Regel: een meerderjarige die samen met diens partner gezag heeft over een minderjarig kind krijgt de gezagsrelatie naar de ouder en de partner geleverd

    Scenario: de ouder en diens partner hebben gezag over het kind
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |

  Regel: een meerderjarige die gezag heeft over een minderjarig kind van de partner krijgt de gezagsrelatie naar de ouder en zichzelf geleverd
    # de gezagsmodule levert bij het bevragen van de niet-ouder geen gezag
    # het gezag kan achterhaald worden door het gezag van de kinderen van de partner op te vragen
    # voor elke gezagsrelatie van de minderjarige kinderen wordt bepaald of de niet-ouder gezamenlijk gezag heeft over het kind

    Scenario: persoon heeft van rechtswege gezamenlijk gezag over het minderjarige kind van diens partner
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |

  Regel: een meerderjarige krijg voor een meerderjarig kind geen gezagsrelatie geleverd

    Scenario: gezag wordt gevraagd van ouder met meerderjarig kind
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                  | waarde   |
        | geboortedatum (03.10) | 20040730 |
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige krijgt voor een minderjarig kind waarvoor het gezag niet bepaald kan worden geen gezagsrelatie geleverd

    Scenario: gezag over minderjarige kind kan niet worden bepaald
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam | waarde             |
        | type | GezagNietTeBepalen |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige krijgt voor een minderjarig kind waarover tijdelijk niemand gezag heeft geen gezagsrelatie geleverd

    Scenario: tijdelijk heeft niemand gezag over een minderjarig kind
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft de volgende 'gezagsverhouding' gegevens
        | naam                               | waarde |
        | indicatie curateleregister (33.10) |      1 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam | waarde             |
        | type | TijdelijkGeenGezag |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon zonder gezag

  Regel: een meerderjarige die van rechtswege gezag heeft over een minderjarige die geen kind is van de meerderjarige krijgt de gezagsrelatie naar zichzelf geleverd
    # de gezagsmodule levert bij het bevragen van de niet-ouder geen gezag
    # het gezag kan achterhaald worden door het gezag van de kinderen van de partner op te vragen
    # voor elke gezagsrelatie van de minderjarige kinderen wordt bepaald of de niet-ouder voogd is van dit kind

    Scenario: de partner van overleden ouder heeft gezag over de minderjarige kinderen
      Gegeven adres 'A1' heeft de volgende gegevens
        | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
        |                 0599 |                         0599010051001502 |
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon met burgerservicenummer '000000024' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000036 |
      En de persoon met burgerservicenummer '000000036' heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000012 |
      En de persoon heeft een 'kind' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft de volgende 'overlijden' gegevens
        | naam                     | waarde   |
        | datum overlijden (08.10) | 20231001 |
      En voor de persoon geldt geen gezag
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) |
        |                              0599 |
      En de persoon heeft een 'partner' met de volgende gegevens
        | burgerservicenummer (01.20) | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) |
        |                   000000036 |                                                           20120428 |
      En de 'partner' is gewijzigd naar de volgende gegevens
        | burgerservicenummer (01.20) | datum ontbinding huwelijk/geregistreerd partnerschap (07.10) | reden ontbinding huwelijk/geregistreerd partnerschap (07.40) |
        |                   000000036 |                                                     20231001 | O                                                            |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      En voor de persoon geldt ook het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En het gezag heeft de volgende derden
        | type         | burgerservicenummer |
        | BekendeDerde |           000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer | 000000048    |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000024 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer | 000000048    |
