# language: nl
Functionaliteit: het gezag kan worden bepaald voor een minderjarige waarvan een of beide gezaghebbenden niet staan ingeschreven in de BRP
  Achtergrondf:
    Gegeven adres 'A1'
    | identificatiecode verblijfplaats (11.80) |
    | 0599010051001502                         |

  Regel: als minderjarige één ouder heeft en staande huwelijk is geboren, dan hebben geboortemoeder en haar partner het gezag

    @deprecated
    Voorbeeld: minderjarige heeft één ouder en is staande huwelijk geboren en partner van moeder staat niet ingeschreven in de BRP
      Gegeven de persoon 'Partner' zonder burgerservicenummer
      * is meerderjarig
      * is een vrouw
      En de minderjarige persoon 'Minderjarige' met één ouder 'Moeder' die gehuwd is met 'Partner'
      * 'Minderjarige' is ingeschreven op adres 'A1' op '1 januari 2024'
      Als personen wordt gezocht met de volgende parameters
        | naam                             | waarde                                  |
        | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
        | adresseerbaarObjectIdentificatie |                        0599010051001502 |
        | fields                           | gezag                                   |
      Dan is het gezag over 'Minderjarige' niet te bepalen met de toelichting 'Gezag kan niet worden bepaald omdat relevante gegevens ontbreken. Het gaat om de volgende gegevens: niet-ouder van bevraagde persoon is niet in BRP geregistreerd'

    @nieuw
    Voorbeeld: minderjarige heeft één ouder en is staande huwelijk geboren en partner van moeder staat niet ingeschreven in de BRP
      Gegeven de persoon 'Partner' zonder burgerservicenummer
      * is meerderjarig
      * is een vrouw
      En de minderjarige persoon 'Minderjarige' met één ouder 'Moeder' die gehuwd is met 'Partner'
      * 'Minderjarige' is ingeschreven op adres 'A1' op '1 januari 2024'
      Als 'gezag' wordt gevraagd van 'Minderjarige'
      Dan is het gezag over 'Minderjarige' gezamenlijk gezag met ouder 'Moeder' en derde 'Partner'
