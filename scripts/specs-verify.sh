#!/bin/bash

EXIT_CODE=0

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt \
                -f summary \
                features/docs \
                -p UnitTest \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-integratie-summary.txt \
                -f summary \
                features/docs \
                -p Integratie \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-informatie-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-informatie-api-summary.txt \
                -f summary \
                features/docs \
                -p InfoApi \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-data-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-data-api-summary.txt \
                -f summary \
                features/docs \
                -p DataApi \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-gezag-api.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-summary.txt \
                -f summary \
                features/docs \
                -p GezagApi \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-deprecated.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-gezag-api-deprecated-summary.txt \
                -f summary \
                features/docs \
                -p GezagApiDeprecated \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/personen/test-result.json \
                -f summary:./test-reports/cucumber-js/personen/test-result-summary.txt \
                -f summary \
                -p DataApi \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/personen/test-result-deprecated.json \
                -f summary:./test-reports/cucumber-js/personen/test-result-deprecated-summary.txt \
                -f summary \
                -p DataApiDeprecated \
                > /dev/null
if [ $? -ne 0 ]; then EXIT_CODE=1; fi

# Exit with error code if any command failed
exit $EXIT_CODE
