docker build -t nasgradapi .
docker run -d -p 8080:80 --name myapp nasgradapi