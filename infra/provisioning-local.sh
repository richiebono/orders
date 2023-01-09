echo "Provisioning Metrics Server"
sh ./k8s/metrics-server/metrics-server.sh

echo "Provisioning MSSQL"
sh ./k8s/mssql/mssql.sh

echo "Create Namespaces"
kubectl apply -f ./k8s/apps/namespaces/

echo "Create All ConfigMaps"
kubectl apply -f ./k8s/apps/configmaps/

echo "Create All deployments"
kubectl apply -f ./k8s/apps/deployments/

echo "Create All Services"
kubectl apply -f ./k8s/apps/services/

echo "Create All HPA"
kubectl apply -f ./k8s/apps/hpa/
