#language: nl

@skip-verify
Functionaliteit: wanneer bij gezagsbepaling gegevens in onderzoek gebruikt zijn wordt dit meegegeven

  Regel: wanneer gegevensin onderzoek staan bij het bevragen van een minderjarige wordt een gezag uitspraak geleverd met een toelichting welke gegevens in onderzoek zijn 
    
    Achtergrond:
      Gegeven de persoon 'Jorine' met burgerservicenummer '000000012'
      * is meerderjarig
      En de persoon 'Bastiaan' met burgerservicenummer '000000024'
      * is meerderjarig
      En de persoon 'Nandy' met burgerservicenummer '000000036'
      * is minderjarig
      * is in Nederland geboren
      * is ingeschreven in de BRP

    Abstract Scenario: gezag wordt bepaald met gegeven van persoon: <omschrijving> in onderzoek er is sprake van TweehoofdigOuderlijkGezag
      Gegeven persoon 'Nandy'
      * zijn de volgende gegevens gewijzigd
      | aanduiding in onderzoek (83.10) |
      | <aanduiding onderzoek>          |
      * heeft 'Jorine' als ouder 1
      * heeft 'Bastiaan' als ouder 2
      En 'Jorine' en 'Bastiaan' zijn met elkaar gehuwd
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                     Nandy |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      |                      true |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Jorine    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Bastiaan  |

      Voorbeelden:
      | aanduiding onderzoek | omschrijving             |
      | 010310               | geboortedatum            |
      | 010330               | geboorteland             |
      | 010000               | hele categorie           |

    Abstract Scenario: gezag wordt bepaald met gegeven van huwelijk: <omschrijving> in onderzoek er is sprake van TweehoofdigOuderlijkGezag
      Gegeven 'Jorine' en 'Bastiaan' zijn met elkaar gehuwd met de volgende gegevens
        | datum huwelijkssluiting/aangaan geregistreerd partnerschap (06.10) | aanduiding in onderzoek (83.10) |
        | gisteren - 2 jaar                                                  | <aanduiding onderzoek>          |
      Gegeven persoon 'Nandy'
        * heeft 'Jorine' als ouder 1
        * heeft 'Bastiaan' als ouder 2
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                     Nandy |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      |                      true |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Jorine    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Bastiaan  |

      Voorbeelden:
      | aanduiding onderzoek | omschrijving                                | 
      | 050120               | burgerservicenummer partner                 | 
      | 050000               | hele categorie partnerschap                 | 
      | 050600               | hele groep aangaan huwelijk of partnerschap | 
      | 050610               | datum aangaan huwelijk of partnerschap      | 
      | 050710               | datum ontbinding huwelijk of partnerschap   | 

    Abstract Scenario: gezag wordt bepaald met gegeven van gerechtelijke uitspraak: <omschrijving> in onderzoek er is sprake van TweehoofdigOuderlijkGezag
      Gegeven voor 'Nandy' is een gerechtelijke uitspraak over het gezag gedaan met de volgende gegevens
        | naam                                 | waarde                  |
        | indicatie gezag minderjarige (32.10) | 12                      |
        | ingangsdatum geldigheid (85.10)      | gisteren - 2 jaar       |
        | aanduiding in onderzoek (83.10)      | <aanduiding onderzoek>  |
      En persoon 'Nandy'
      * heeft 'Jorine' als ouder 1
      * heeft 'Bastiaan' als ouder 2
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                     Nandy |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      |                      true |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Jorine    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Bastiaan  |

        Voorbeelden:
        | aanduiding onderzoek | omschrijving                    | 
        | 113210               | indicatie gezag minderjarig     | 
        | 118510               | ingangsdatum geldigheid gezag   | 
        | 110000               | hele groep                      | 

    Scenario: gezag wordt bepaald met gegeven geslachtsnaam van ouder 1 in onderzoek, er is sprake van EenhoofdigOuderlijkGezag
      Gegeven persoon 'Nandy'
      * heeft 'Jorine' als ouder 1 met de volgende gegevens
        | geslachtsaanduiding (04.10)     | datum ingang familierechtelijke betrekking (62.10)  | aanduiding in onderzoek (83.10)  |
        | V                               | gisteren - 17 jaar                                  | 020410                           |  
      * heeft 'Bastiaan' als ouder 2 met de volgende gegevens
        | geslachtsaanduiding (04.10)     | datum ingang familierechtelijke betrekking (62.10)  |
        | M                               | gisteren - 17 jaar                                  |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    | 
        | type                             | EenhoofdigOuderlijkGezag  |
        | ouder.burgerservicenummer        | 000000012                 |
        | ouder.naam.geslachtsnaam         | Jorine                    |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                     Nandy |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      | true                      |
        
    Scenario: gezag wordt bepaald met gegeven geslachtsnaam van ouder 2 in onderzoek, er is sprake van EenhoofdigOuderlijkGezag
      Gegeven persoon 'Nandy'
      * heeft 'Jorine' als ouder 1 met de volgende gegevens
        | geslachtsaanduiding (04.10)     | datum ingang familierechtelijke betrekking (62.10)  |
        | V                               | gisteren - 17 jaar                                  | 
      * heeft 'Bastiaan' als ouder 2 met de volgende gegevens
        | geslachtsaanduiding (04.10)     | datum ingang familierechtelijke betrekking (62.10)  | aanduiding in onderzoek (83.10)  |
        | M                               | gisteren - 17 jaar                                  | 030410                           |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    | 
        | type                             | EenhoofdigOuderlijkGezag  |
        | ouder.burgerservicenummer        | 000000012                 |
        | ouder.naam.geslachtsnaam         | Jorine                    |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                     Nandy |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      | true                      |

  Regel: wanneer gegevens in onderzoek staan bij bevragen van een meerderjarige wordt een gezag uitspraak geleverd met een toelichting welke gegevens in onderzoek zijn
  
    Achtergrond:
      Gegeven de persoon 'Rianne' met burgerservicenummer '000000012'
      * is meerderjarig
      En de persoon 'Roel' met burgerservicenummer '000000024'
      * is meerderjarig
      En 'Rianne' en 'Roel' zijn met elkaar gehuwd
      En de persoon 'Richard' met burgerservicenummer '000000036'
      * is minderjarig
      * is in Nederland geboren
      * is ingeschreven in de BRP
      * heeft 'Rianne' als ouder 1
      * heeft 'Roel' als ouder 2
      En de persoon 'Randy' met burgerservicenummer '000000048'
      * is minderjarig
      * is in Nederland geboren
      * is ingeschreven in de BRP
      * heeft 'Rianne' als ouder 1
      * heeft 'Roel' als ouder 2
      En de persoon 'Rijk' met burgerservicenummer '000000061'
      * is minderjarig
      * is in Nederland geboren
      * is ingeschreven in de BRP
      * heeft 'Rianne' als ouder 1
      * heeft 'Roel' als ouder 2

    Abstract Scenario: gezag wordt bepaald met gegeven van 1 van de kinderen: <omschrijving> in onderzoek er is sprake van TweehoofdigOuderlijkGezag
      Gegeven persoon 'Richard'
      * zijn de volgende gegevens gewijzigd
      | aanduiding in onderzoek (83.10) |
      | <aanduiding onderzoek>          |
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000024 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000061 |
        | minderjarige.naam.geslachtsnaam  |                      Rijk |
        | minderjarige.geboorte.datum      |                  20080128 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Rianne    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Roel      |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000036 |
        | minderjarige.naam.geslachtsnaam  |                   Richard |
        | minderjarige.geboorte.datum      |                  20080128 |
        | inOnderzoek                      |                      true |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Rianne    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Roel      |
      En heeft de persoon een 'gezag' met de volgende gegevens
        | naam                             | waarde                    |
        | type                             | TweehoofdigOuderlijkGezag |
        | minderjarige.burgerservicenummer |                 000000048 |
        | minderjarige.naam.geslachtsnaam  |                     Randy |
        | minderjarige.geboorte.datum      |                  20080128 |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000012 |
        | naam.geslachtsnaam  | Rianne    |
      En heeft 'gezag' een 'ouder' met de volgende gegevens
        | naam                | waarde    |
        | burgerservicenummer | 000000024 |
        | naam.geslachtsnaam  | Roel      |

      Voorbeelden:
      | aanduiding onderzoek | omschrijving             | 
      | 010310               | geboortedatum            |
      | 010330               | geboorteland             | 
      | 010000               | hele categorie           | 
