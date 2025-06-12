# language: nl
@api @deprecated
Functionaliteit: gezagsrelaties van een minderjarige(n)

  Regel: voor een minderjarige met twee ouders met gezag wordt de gezagsrelatie naar beide ouders geleverd

    Scenario: beide ouders hebben gezag over de minderjarige
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000012 |
      En het gezag heeft de volgende ouders
        | burgerservicenummer |
        |           000000024 |
        |           000000048 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
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

  Regel: voor een minderjarige met één ouder met gezag wordt de gezagsrelatie naar deze ouder geleverd

    Scenario: één ouder heeft gezag over de minderjarige
      Gegeven de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                   |
        | type                             | EenhoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                000000012 |
        | ouder.burgerservicenummer        |                000000024 |

  Regel: voor een minderjarige met gezamenlijk gezag wordt de gezagsrelatie naar de ouder en de andere gezaghebbende geleverd

    Scenario: de ouder en diens partner hebben gezag over het kind
      Gegeven de persoon met burgerservicenummer '000000024' heeft een 'kind' met de volgende gegevens
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
      En de persoon met burgerservicenummer '000000012' heeft een ouder '1' met de volgende gegevens
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
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde           |
        | type                             | GezamenlijkGezag |
        | minderjarige.burgerservicenummer |        000000012 |
        | ouder.burgerservicenummer        |        000000024 |
        | derde.type                       | BekendeDerde     |
        | derde.burgerservicenummer        |        000000048 |

  Regel: voor een meerderjarige wordt er geen gezagsrelatie naar de ouders geleverd

    Scenario: gezag wordt gevraagd van een meerderjarige
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                  | waarde   |
        | geboortedatum (03.10) | 20040730 |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een ouder '2' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt geen gezag
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag

  Regel: wanneer het gezag voor een minderjarige niet bepaald kan worden, wordt dit in gezag aangegeven

    Scenario: gezag over minderjarige kan niet worden bepaald
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                  | waarde   |
        | geboortedatum (03.10) | 20190516 |
      En de persoon heeft de volgende 'verblijfplaats' gegevens
        | gemeente van inschrijving (09.10) |
        |                              0518 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde             |
        | type                             | GezagNietTeBepalen |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
        | minderjarige.geboorte.datum      |           20190516 |

  Regel: wanneer over een minderjarige tijdelijk niemand gezag heeft, wordt dat in gezag aangegeven

    Scenario: tijdelijk heeft niemand gezag over de minderjarige
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                  | waarde   |
        | geboortedatum (03.10) | 20190516 |
      En de persoon heeft een ouder '1' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000024 |
      En de persoon heeft een ouder '2' met de volgende gegevens
        | burgerservicenummer (01.20) |
        |                   000000048 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde             |
        | type                             | TijdelijkGeenGezag |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde             |
        | type                             | TijdelijkGeenGezag |
        | toelichting                      | test               |
        | minderjarige.burgerservicenummer |          000000012 |
        | minderjarige.geboorte.datum      |           20190516 |

  Regel: wanneer een niet-ouder van rechtswege gezag heeft over de minderjarige wordt deze voogdij in gezag aangegeven

    Scenario: de partner van overleden ouder heeft gezag over de minderjarige
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | naam                  | waarde   |
        | geboortedatum (03.10) | 20190516 |
      En voor de persoon geldt het volgende gezag
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En het gezag heeft de volgende derden
        | burgerservicenummer |
        |           000000024 |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000012 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
        | minderjarige.geboorte.datum      |  20190516 |
      En heeft 'gezag' een 'derde' met de volgende gegevens
        | naam                | waarde       |
        | type                | BekendeDerde |
        | burgerservicenummer |    000000024 |

  Regel: wanneer met een gerechtelijke uitspraak gezag is toegewezen aan een voogd dan is de voogd niet bekend en wordt gezag geleverd zonder de voogd(en)

    Scenario: er is een gerechtelijke uitspraak tot gezag voor ouder 1 en een derde
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende 'gezagsverhouding' gegevens
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
        | naam                             | waarde    |
        | type                             | Voogdij   |
        | minderjarige.burgerservicenummer | 000000012 |
      En heeft 'gezag' geen derden
