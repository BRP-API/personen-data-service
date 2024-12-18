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
    En de persoon 'Zoe' met burgerservicenummer '000000036'
    * is minderjarig
    * is in Nederland geboren
    * is ingeschreven in de BRP
    * heeft 'Gerda' als ouder 1

  Regel: gezag kan tijdelijk niet worden bepaald als de ouder is opgeschort

    Scenario: De moeder van de minderjarige is opgeschort er is TijdelijkGeenGezag
      Gegeven persoon 'Gerda'
      * is overleden
      Als personen wordt gezocht met de volgende parameters
        | naam                | waarde                          |
        | type                | RaadpleegMetBurgerservicenummer |
        | burgerservicenummer |                       000000036 |
        | fields              | gezag                           |
      Dan heeft de response een persoon met een 'gezag' met de volgende gegevens
        | naam                             | waarde                                                  |
        | type                             | TijdelijkGeenGezag                                      |
        | minderjarige.burgerservicenummer |                                               000000036 |
        | toelichting                      | Tijdelijk geen gezag omdat beide ouders overleden zijn. |
