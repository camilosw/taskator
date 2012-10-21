@ClientCtrl = ($scope, $http) ->
  $scope.clients = [
    {
      "nombre": "Cliente 1"
      "activo": true
    },{
      "nombre": "Cliente 2"
      "activo": false
    }
  ] 
  $http.get('clientes.json').success (data) =>
    $scope.clients = data
  $scope.query = {"activo": true }

  ClientCtrl.$inject = ['$scope', '$http']