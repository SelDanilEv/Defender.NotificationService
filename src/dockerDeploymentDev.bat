docker rm -f DevNotificationService
docker build . -t dev-notification-service && ^
docker run -d --name DevNotificationService -p 49052:80 ^
--env-file ./../../secrets/secrets.dev.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it dev-notification-service
echo finish dev-notification-service
docker image prune -f
pause
