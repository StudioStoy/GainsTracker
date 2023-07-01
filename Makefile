# Define targets
.PHONY: production staging docker local

production:
	docker compose -f docker-compose.production.yml up

staging:
	docker compose -f docker-compose.staging.yml up

docker:
	docker compose -f docker-compose.docker.yml up

local:
	docker compose up
