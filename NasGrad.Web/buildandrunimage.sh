#!/usr/bin/env bash
sudo docker build -t nasgradweb .
sudo docker run -d -p 8081:80 --name myappweb nasgradweb