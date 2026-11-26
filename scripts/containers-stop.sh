#!/bin/bash

docker compose \
    -f .docker/db.yml \
    -f .docker/gezag-mock.yml \
    -f .docker/personen-data-service.yml \
    down --volumes
