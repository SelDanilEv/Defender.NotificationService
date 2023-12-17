docker rm -f LocalNotificationService
docker build . -t local-notification-service && ^
docker run -d --name LocalNotificationService -p 47052:80 ^
--env-file ./../../secrets/secrets.local.list ^
-e ASPNETCORE_ENVIRONMENT=DockerLocal ^
-it local-notification-service
echo finish local-notification-service
docker image prune -f
pause
