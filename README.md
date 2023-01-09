# Introduction 
Orders Project using .NetCore on backend APIs, React and React Admin on frontend, validation using SonarQube.

## Docker

Install docker on windows.

`$ choco install docker-cli`

Install docker on Ubuntu.

apt get update

`$ sudo apt-get update`

install

`$ sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin`

Start it

`$ sudo systemctl start docker`

Install docker on Mac with Brew

`$ brew install --cask docker`

Install docker on windows.

`$ choco install docker-cli`

## Running the app

There is a `docker-compose.yml` file for starting SQLSERVER, API, and Frontend.

`$ docker compose up`

After running, you can stop the Docker container with

`$ docker compose down`

IMPORTANT: If you change something on the project you need to execute the follow command before "docker compose up".

`$ docker compose build` 


## Url Swagger for Api Documentation

```
http://localhost:8080/api/swagger
```

## Frontend URL

```
http://localhost
```

## Test K8S local

`$ cd infra`
`$ sh ./provisioning-local.sh`

## Test K8S Production

`$ cd infra`
`$ sh ./provisioning.sh`