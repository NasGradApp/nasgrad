if [ $# -eq 0 ]
  then
    echo "Error: you need to provide address of the server as a parameter"
    exit 1
fi
cd ../../
HOST_CURRENT_ADDRESS="$1" docker-compose up