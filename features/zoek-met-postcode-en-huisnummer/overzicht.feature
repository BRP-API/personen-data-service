#language: nl

@api
Functionaliteit: Zoek personen met postcode en huisnummer

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) |
    | 0599                 | 2628HJ           | 2                  |
    En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900101              |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0599                              |

    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisletter (11.30) |
    | 0599                 | 2628HJ           | 2                  | A                  |
    En de persoon met burgerservicenummer '000000025' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900102              |
    En de persoon is ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0599                              |

    En adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | huisnummertoevoeging (11.40) |
    | 0599                 | 2628HJ           | 2                  | III                          |
    En de persoon met burgerservicenummer '000000026' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900103              |
    En de persoon is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0599                              |

    En adres 'A4' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) | aanduiding bij huisnummer (11.50) |
    | 0599                 | 2628HJ           | 2                  | to                                |
    En de persoon met burgerservicenummer '000000027' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900104              |
    En de persoon is ingeschreven op adres 'A4' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0599                              |

    En adres 'A5' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) |
    | 0599                 | 2629HJ           | 2                  |
    En de persoon met burgerservicenummer '000000028' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900105              |
    En de persoon is ingeschreven op adres 'A5' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0599                              |
    En de persoon heeft de volgende 'inschrijving' gegevens
    | naam                                 | waarde |
    | reden opschorting bijhouding (67.20) | O      |

    En adres 'A6' heeft de volgende gegevens
    | gemeentecode (92.10) | postcode (11.60) | huisnummer (11.20) |
    | 0600                 | 2630HJ           | 2                  |
    En de persoon met burgerservicenummer '000000029' heeft de volgende gegevens
    | geboortedatum (03.10) |
    | 19900106              |
    En de persoon is ingeschreven op adres 'A6' met de volgende gegevens
    | gemeente van inschrijving (09.10) |
    | 0600                              |

  Regel: Postcode (niet hoofdlettergevoelig) en huisnummer zijn verplichte parameters. Postcode mag zowel met als zonder spatie tussen de cijfer- en letterdeel worden verstrekt.

    Abstract Scenario: Zoek een persoon met de postcode (<sub-titel>) en huisnummer van het adres van zijn verblijfplaats
      Als personen wordt gezocht met de volgende parameters
      | naam       | waarde                      |
      | type       | ZoekMetPostcodeEnHuisnummer |
      | postcode   | <postcode>                  |
      | huisnummer | 2                           |
      | fields     | burgerservicenummer         |
      Dan heeft de response 4 personen
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000025 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000026 |
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000027 |

      Voorbeelden:
      | postcode | sub-titel                                                             |
      | 2628HJ   | letters zijn hoofdletters                                             |
      | 2628hj   | letters zijn kleine letters                                           |
      | 2628 HJ  | spatie tussen de cijfers en hoofdletters                              |
      | 2628 hj  | spatie tussen de cijfers en kleine letters                            |
      | 2628 Hj  | spatie tussen de cijfers en letters (zowel hoofd- als kleine letters) |

  Regel: Optionele 'adres' parameters (niet hooflettergevoelig) kunnen worden toegevoegd om de zoek criteria aan te scherpen.

    Abstract Scenario: Zoek een persoon met de postcode, huisnummer en huisletter van het adres van zijn verblijfplaats
      Als personen wordt gezocht met de volgende parameters
      | naam       | waarde                      |
      | type       | ZoekMetPostcodeEnHuisnummer |
      | postcode   | 2628HJ                      |
      | huisnummer | 2                           |
      | huisletter | <huisletter>                |
      | fields     | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000025 |

      Voorbeelden:
      | huisletter |
      | A          |
      | a          |

    Abstract Scenario: Zoek een persoon met de postcode, huisnummer en huisnummertoevoeging van het adres van zijn verblijfplaats
      Als personen wordt gezocht met de volgende parameters
      | naam                 | waarde                      |
      | type                 | ZoekMetPostcodeEnHuisnummer |
      | postcode             | 2628HJ                      |
      | huisnummer           | 2                           |
      | huisnummertoevoeging | <huisnummertoevoeging>      |
      | fields               | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000026 |

      Voorbeelden:
      | huisnummertoevoeging |
      | III                  |
      | iii                  |
      | Iii                  |

    Scenario: Zoek een persoon met de postcode, huisnummer en gemeenteVanInschrijving code van het adres van zijn verblijfplaats
      Als personen wordt gezocht met de volgende parameters
      | naam                    | waarde                      |
      | type                    | ZoekMetPostcodeEnHuisnummer |
      | postcode                | 2630HJ                      |
      | huisnummer              | 2                           |
      | gemeenteVanInschrijving | 0600                        |
      | fields                  | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000029 |

  Regel: De optionele 'inclusiefOverledenPersonen' parameter moet worden opgegeven om een overleden persoon te kunnen vinden

    Scenario: Zoek op een adres waar alleen een overleden persoon staat ingeschreven
      Als personen wordt gezocht met de volgende parameters
      | naam       | waarde                      |
      | type       | ZoekMetPostcodeEnHuisnummer |
      | postcode   | 2629HJ                      |
      | huisnummer | 2                           |
      | fields     | burgerservicenummer         |
      Dan heeft de response 0 persoon

    Scenario: Zoek een overleden persoon
      Als personen wordt gezocht met de volgende parameters
      | naam                       | waarde                      |
      | type                       | ZoekMetPostcodeEnHuisnummer |
      | postcode                   | 2629HJ                      |
      | huisnummer                 | 2                           |
      | inclusiefOverledenPersonen | true                        |
      | fields                     | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000028 |
      En heeft de persoon de volgende 'opschortingBijhouding' gegevens
      | naam               | waarde     |
      | reden.code         | O          |
      | reden.omschrijving | overlijden |

  Regel: Optionele 'geboortedatum' parameter kan worden toegevoegd om de zoek criteria aan te scherpen.

    Scenario: Zoek personen met postcode, huisnummer en geboortedatum
      Als personen wordt gezocht met de volgende parameters
      | naam          | waarde                      |
      | type          | ZoekMetPostcodeEnHuisnummer |
      | postcode      | 2628HJ                      |
      | huisnummer    | 2                           |
      | geboortedatum | 1990-01-01                  |
      | fields        | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000024 |

    Scenario: Zoek personen met postcode, huisnummer, huisletter en geboortedatum
      Als personen wordt gezocht met de volgende parameters
      | naam          | waarde                      |
      | type          | ZoekMetPostcodeEnHuisnummer |
      | postcode      | 2628HJ                      |
      | huisnummer    | 2                           |
      | huisletter    | A                           |
      | geboortedatum | 1990-01-02                  |
      | fields        | burgerservicenummer         |
      Dan heeft de response 1 persoon
      En heeft de response een persoon met de volgende gegevens
      | naam                | waarde    |
      | burgerservicenummer | 000000025 |
