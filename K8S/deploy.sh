#ingress controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml

#rabbitmq
kubectl apply -f rabbitmq-depl.yaml

#database
cd database || exit
kubectl apply -f persistant-volume-claim.yaml
kubectl apply -f database-depl.yaml
cd ../

#user-service
cd user-service/ || exit
kubectl apply -f user-depl.yaml
kubectl apply -f user-np-service.yaml
cd ../

#coffee-service
cd coffee-service/ || exit
kubectl apply -f coffee-depl.yaml
kubectl apply -f coffee-np-service.yaml
cd ../

#favorite-service
cd favorite-service/ || exit
kubectl apply -f favorite-depl.yaml
kubectl apply -f favorite-np-service.yaml
cd ../


#order-service
cd order-service/ || exit
kubectl apply -f order-depl.yaml
kubectl apply -f order-np-service.yaml
cd ../

#API gateway
kubectl apply -f ingress-service.yaml
