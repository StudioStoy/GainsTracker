#!/bin/bash
set -e

POSTGRES="psql --username ${POSTGRES_USER}"

echo "Creating database: ${DB_APP_NAME}"

$POSTGRES <<EOSQL
CREATE DATABASE "${DB_APP_NAME}" OWNER "${DB_APP_USER}";
EOSQL