#!/bin/bash

# Detect OS and set sed parameters accordingly
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    SED_INPLACE="-i ''"
elif [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "cygwin" ]] || [[ "$OSTYPE" == "win32" ]]; then
    # Windows (Git Bash, MSYS2, Cygwin)
    SED_INPLACE="-i"
else
    # Linux and others
    SED_INPLACE="-i"
fi

# Apply sed with appropriate flags
sed $SED_INPLACE 's/^\([[:space:]]*\)\(properties\)/\1additionalProperties: false\n&/g' ./specificatie/resolved/openapi.yaml
sed $SED_INPLACE 's/^\([[:space:]]*\)\(properties\)/\1additionalProperties: false\n&/g' ./specificatie/resolved/openapi-v1.yaml

npx nswag run src/GezagMock/server.nswag
npx nswag run src/GezagMock/serverDeprecated.nswag
npx nswag run src/DataTransferObjects.nswag
npx nswag run src/DataTransferObjectsDeprecated.nswag
npx nswag run src/DataTransferObjectsCommon.nswag
npx nswag run src/GezagApiDtos.nswag
npx nswag run src/GezagApiDtosDeprecated.nswag

git checkout -- ./specificatie/resolved/openapi.yaml
git checkout -- ./specificatie/resolved/openapi-v1.yaml