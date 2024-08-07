#!/bin/bash

PARAMS="{ \
    \"logFileToAssert\": \"./test-data/logs/historie-data-service.json\", \
    \"oAuth\": { \
        \"enable\": false \
    } \
}"

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt \
                -f summary \
                features/docs \
                --tags "not @integratie"

npx cucumber-js -f json:./test-reports/cucumber-js/personen/test-result.json \
                -f summary:./test-reports/cucumber-js/personen/test-result-summary.txt \
                -f summary \
                features/gezag-persoon-beperkt \
                features/persoon \
                features/persoon-bepertk \
                features/raadpleeg-met-burgerservicenummer \
                features/zoek-met-adresseerbaar-object-identificatie \
                features/zoek-met-geslachtsnaam-en-geboortedatum \
                features/zoek-met-geslachtsnaam-voornamen-en-gemeente-van-inschrijving \
                features/zoek-met-nummeraanduiding-identificatie \
                features/zoek-met-postcode-en-huisnummer \
                features/zoek-met-straatnaam-huisnummer-en-gemeente-van-inschrijving \
                --world-parameters "$PARAMS"
