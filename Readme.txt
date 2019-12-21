* Use http and do https behind reverse proxy. Problematic otherwise to get certificate onto docker image.
* docker-compose build to rebuild docker-compose up to start up

Previous run commands
docker stop todoInstance 
docker build -t todo .
docker run --rm -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS="http://*:80" --name todoInstance todo