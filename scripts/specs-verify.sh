#!/bin/bash

PARAMS="{ \
    \"apiUrl\": \"http://localhost:8000/haalcentraal/api\", \
    \"logFileToAssert\": \"./test-data/logs/personen-data-service.json\", \
    \"oAuth\": { \
        \"enable\": false \
    } \
}"

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt \
                -f summary \
                features/docs \
                -p UnitTest \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-integratie-summary.txt \
                -f summary \
                features/docs \
                -p Integratie \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-informatie-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-informatie-api-summary.txt \
                -f summary \
                features/docs \
                -p InfoApi \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-data-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-data-api-summary.txt \
                -f summary \
                features/docs \
                -p DataApi \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-gezag-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-summary.txt \
                -f summary \
                features/docs \
                -p GezagApi \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-deprecated.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-deprecated-summary.txt \
                -f summary \
                features/docs \
                -p GezagApiDeprecated \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/personen/test-result.json \
                -f summary:./test-reports/cucumber-js/personen/test-result-summary.txt \
                -f summary \
                features/gezag-persoon-beperkt \
                features/persoon \
                features/persoon-beperkt \
                features/raadpleeg-met-burgerservicenummer \
                features/zoek-met-adresseerbaar-object-identificatie \
                features/zoek-met-geslachtsnaam-en-geboortedatum \
                features/zoek-met-geslachtsnaam-voornamen-en-gemeente-van-inschrijving \
                features/zoek-met-nummeraanduiding-identificatie \
                features/zoek-met-postcode-en-huisnummer \
                features/zoek-met-straatnaam-huisnummer-en-gemeente-van-inschrijving \
                -p DataApi \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/personen/test-result-deprecated.json \
                -f summary:./test-reports/cucumber-js/personen/test-result-deprecated-summary.txt \
                -f summary \
                features/gezag-persoon-beperkt \
                features/persoon \
                features/persoon-beperkt \
                features/raadpleeg-met-burgerservicenummer \
                features/zoek-met-adresseerbaar-object-identificatie \
                features/zoek-met-geslachtsnaam-en-geboortedatum \
                features/zoek-met-geslachtsnaam-voornamen-en-gemeente-van-inschrijving \
                features/zoek-met-nummeraanduiding-identificatie \
                features/zoek-met-postcode-en-huisnummer \
                features/zoek-met-straatnaam-huisnummer-en-gemeente-van-inschrijving \
                -p DataApiDeprecated \
                > /dev/null
