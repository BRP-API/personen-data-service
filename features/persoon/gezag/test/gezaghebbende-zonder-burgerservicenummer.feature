# language: nl
Functionaliteit: het gezag kan worden bepaald voor een minderjarige waarvan een of beide gezaghebbenden niet staan ingeschreven in de BRP

  Regel: als minderjarige één ouder heeft en staande huwelijk is geboren, dan hebben geboortemoeder en haar partner het gezag

    @deprecated
    Voorbeeld: minderjarige heeft één ouder en is staande huwelijk geboren en partner van moeder staat niet ingeschreven in de BRP
      Gegeven de persoon 'Partner' zonder burgerservicenummer
      * is meerderjarig
      * is een vrouw
      En de minderjarige persoon 'Minderjarige' met één ouder 'Moeder' die gehuwd is met 'Partner'
      Als 'gezag' wordt gevraagd van 'Minderjarige'
      Dan is het gezag over 'Minderjarige' niet te bepalen met de toelichting 'Gezag kan niet worden bepaald omdat relevante gegevens ontbreken. Het gaat om de volgende gegevens: niet-ouder van bevraagde persoon is niet in BRP geregistreerd'

    @nieuw
    Voorbeeld: minderjarige heeft één ouder en is staande huwelijk geboren en partner van moeder staat niet ingeschreven in de BRP
      Gegeven de persoon 'Partner' zonder burgerservicenummer
      * is meerderjarig
      * is een vrouw
      En de minderjarige persoon 'Minderjarige' met één ouder 'Moeder' die gehuwd is met 'Partner'
      Als 'gezag' wordt gevraagd van 'Minderjarige'
      Dan is het gezag over 'Minderjarige' gezamenlijk gezag met ouder 'Moeder' en derde 'Partner'
