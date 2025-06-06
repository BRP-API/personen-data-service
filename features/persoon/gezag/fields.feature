#language: nl

Functionaliteit: vragen van gezagsrelaties bij raadplegen met burgerservicenummer

  Scenario: geleverde persoon heeft eenhoofdig ouderlijk gezag
    Gegeven de minderjarige 'P1'
    En de meerderjarige 'P2'
    En 'P1' heeft de volgende gezagsrelaties
    * eenhoofdig ouderlijk gezag over 'P1' met ouder 'P2'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is eenhoofdig ouderlijk gezag met ouder 'P2'

  @deprecated # tag kan weg als GezagMock kan switchen tussen deprecated en niet-deprecated gezagsrelaties
  Scenario: geleverde persoon heeft gezamenlijk ouderlijk gezag
    Gegeven de minderjarige 'P1'
    En de meerderjarige 'P2'
    En de meerderjarige 'P3'
    En 'P1' heeft de volgende gezagsrelaties
    * gezamenlijk ouderlijk gezag over 'P1' met ouder 'P2' en ouder 'P3'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is gezamenlijk ouderlijk gezag met ouder 'P2' en ouder 'P3'

  Scenario: geleverde persoon heeft gezamenlijk gezag
    Gegeven de minderjarige 'P1'
    En de meerderjarige 'P2'
    En de meerderjarige 'P3'
    En 'P1' heeft de volgende gezagsrelaties
    * gezamenlijk gezag over 'P1' met ouder 'P2' en derde 'P3'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is gezamenlijk gezag met ouder 'P2' en derde 'P3'

  Scenario: geleverde persoon heeft gezamenlijk gezag met onbekende derde
    Gegeven de minderjarige 'P1'
    En de meerderjarige 'P2'
    En 'P1' heeft de volgende gezagsrelaties
    * gezamenlijk gezag over 'P1' met ouder 'P2' en een onbekende derde
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is gezamenlijk gezag met ouder 'P2' en een onbekende derde

  Scenario: geleverde persoon heeft voogdij
    Gegeven de minderjarige 'P1'
    En 'P1' heeft de volgende gezagsrelaties
    * voogdij over 'P1'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is voogdij

  Scenario: geleverde persoon heeft voogdij met derde
    Gegeven de minderjarige 'P1'
    En de meerderjarige 'P2'
    En 'P1' heeft de volgende gezagsrelaties
    * voogdij over 'P1' met derde 'P2'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * het gezag over 'P1' is voogdij met derde 'P2'

  Scenario: geleverde persoon heeft tijdelijk geen gezag
    Gegeven de minderjarige 'P1'
    En 'P1' heeft de volgende gezagsrelaties
    * tijdelijk geen gezag over 'P1' met de toelichting 'dit is de reden dat er tijdelijk geen gezag is'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * is het gezag over 'P1' tijdelijk geen gezag met de toelichting 'dit is de reden dat er tijdelijk geen gezag is'

  Scenario: gezag van geleverde persoon is niet te bepalen
    Gegeven de minderjarige 'P1'
    En 'P1' heeft de volgende gezagsrelaties
    * gezag over 'P1' is niet te bepalen met de toelichting 'dit is de reden dat het gezag niet te bepalen is'
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' de volgende gezagsrelaties
    * is het gezag over 'P1' niet te bepalen met de toelichting 'dit is de reden dat het gezag niet te bepalen is'

  Scenario: geleverde persoon heeft geen gezagsrelaties
    Gegeven de minderjarige 'P1'
    En 'P1' heeft geen gezaghouder
    Als 'gezag' wordt gevraagd van personen gezocht met burgerservicenummer van 'P1'
    Dan heeft 'P1' geen gezaghouder