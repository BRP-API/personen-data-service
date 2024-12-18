# language: nl
Functionaliteit: 1.1 - Staat het kind (minderjarige) als ingezetene in de BRP?
  De persoon (minderjarige) moet voorkomen in de BRP aangezien dit de bron is voor
  het achterhalen van het gezag. Een niet ingezeten kind staat wel de RNI (ook
  onderdeel van de BRP), maar hiervan zijn de gegevens (mogelijk) niet actueel. Een
  gezagsmutatie wordt bijvoorbeeld niet bijgewerkt in de RNI.

  Een persoon is "ingezetene" en "ingeschreven in de BRP" wanneer deze staat ingeschreven
  in een Nederlandse gemeente. Een persoon is "niet ingezetene" wanneer deze staat ingeschreven 
  in het RNI (Register Niet Ingezetenen). In dat geval is het gezag niet te bepalen.

  Gebruikte velden:
    - Gemeente van inschrijving -> 08.09.10

  Achtergrond:
    Gegeven de persoon 'Gerda' met burgerservicenummer '000000012'
    * is meerderjarig
    En de persoon 'Bert' met burgerservicenummer '000000024'
    * is meerderjarig
    En 'Gerda' en 'Bert' zijn met elkaar gehuwd
    En de persoon 'Zoe' met burgerservicenummer '000000036'
    * is minderjarig
    * is in Nederland geboren
    * heeft 'Gerda' als ouder 1
    * heeft 'Bert' als ouder 2

  Regel: gezag kan worden bepaald voor ingezeten kinderen

    Scenario: Van een niet-ingezeten kind kan geen gezag worden bepaald er is sprake van GezagNietTeBepalen
      Gegeven persoon 'Zoe'
      * is niet ingeschreven in de BRP
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
     Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                                                               |
        | type                             | GezagNietTeBepalen                                                   |
        | toelichting                      | gezag is niet te bepalen omdat minderjarige niet in Nederland woont. |
        | minderjarige.burgerservicenummer |                                                            000000036 |

    Scenario: Van een kind met onbekende verblijfplaats kan geen gezag worden bepaald er is sprake van GezagNietTeBepalen
      Gegeven persoon 'Zoe'
      * heeft een onbekende verblijfplaats
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                                                                                                        |
        | type                             | GezagNietTeBepalen                                                                                            |
        | toelichting                      | gezag is niet te bepalen omdat de volgende relevante gegevens ontbreken: verblijfplaats van bevraagde persoon |
        | minderjarige.burgerservicenummer |                                                                                                     000000036 |

    Scenario: Van een kind met een onbekende gemeente van inschrijving kan geen gezag worden bepaald er is sprake van GezagNietTeBepalen
      Gegeven persoon 'Zoe'
      * heeft een onbekende gemeente van inschrijving
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                                                                                                                   |
        | type                             | GezagNietTeBepalen                                                                                                       |
        | toelichting                      | gezag is niet te bepalen omdat de volgende relevante gegevens ontbreken: gemeente van inschrijving van bevraagde persoon |
        | minderjarige.burgerservicenummer |                                                                                                                000000036 |

    Scenario: Wanneer gezag wordt opgevraagd van een kind waarvan leeftijd onbekend is, wordt geen gezag teruggeven.
      Gegeven persoon 'Zoe'
      * heeft de volgende gegevens
        | geboortedatum (03.10) |
        |              00000000 |
      En is ingeschreven in de BRP
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon zonder gezag
