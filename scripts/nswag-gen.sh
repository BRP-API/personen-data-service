#!/bin/bash

sed -i '' 's/^\([[:space:]]*\)\(properties\)/\1additionalProperties: false\n&/g' ./specificatie/resolved/openapi.yaml
sed -i '' 's/^\([[:space:]]*\)\(properties\)/\1additionalProperties: false\n&/g' ./specificatie/resolved/openapi-v1.yaml

npx nswag run src/GezagMock/server.nswag
npx nswag run src/DataTransferObjects.nswag
npx nswag run src/DataTransferObjectsDeprecated.nswag
npx nswag run src/GezagApiDtos.nswag
npx nswag run src/GezagApiDtosDeprecated.nswag

git checkout -- ./specificatie/resolved/openapi.yaml
git checkout -- ./specificatie/resolved/openapi-v1.yaml