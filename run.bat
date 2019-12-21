docker stop todoInstance 
docker build -t todo .
docker run --rm -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS="http://*:80" --name todoInstance todo