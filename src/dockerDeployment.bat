docker rm -f NotificationService
docker build . -t notification-service && ^
docker run -d --name NotificationService -p 2502:80 ^
--env-file ./../../secrets.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it notification-service
echo finish notification-service
pause
