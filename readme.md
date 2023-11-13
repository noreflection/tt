docker build -t aspnetapp .


docker run -d -p 7991:80 --name aspnetapp-api aspnetapp