# From version 15 on additional upgrades must be done, and I can't be bothered! So enjoy version 14 for now. 
FROM postgres:14-alpine 

COPY ./create_users.sh /docker-entrypoint-initdb.d/10-create_users.sh
COPY ./create_dbs.sh /docker-entrypoint-initdb.d/20-create_dbs.sh

# Guarantee Linux file endings
RUN sed -i -e 's/\r$//' /docker-entrypoint-initdb.d/10-create_users.sh && \
    sed -i -e 's/\r$//' /docker-entrypoint-initdb.d/20-create_dbs.sh
