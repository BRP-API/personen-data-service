#!/bin/bash

sed -i '' 's/^\([[:space:]]*\)\(properties\)/\1additionalProperties: false\n&/g' ./specificatie/resolved/openapi.yaml

npx nswag run src/GezagMock/server.nswag
npx nswag run src/DataTransferObjects.nswag

git checkout -- ./specificatie/resolved/openapi.yaml