#!/bin/bash
set -e

POSTGRES="psql --username ${POSTGRES_USER}"

echo "Creating default user: ${DB_USER}"
echo "with password: ${POSTGRES_PASSWORD}"

$POSTGRES <<EOSQL
CREATE USER "${DB_USER}" WITH CREATEDB PASSWORD '${POSTGRES_PASSWORD}';
CREATE USER "${DB_USER}-test" WITH CREATEDB PASSWORD '${DB_USER}-test';
EOSQL