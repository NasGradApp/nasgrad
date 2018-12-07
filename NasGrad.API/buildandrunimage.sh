sudo docker build -t nasgradapi .
sudo docker run -d -p 8080:80 --name myapp nasgradapi
