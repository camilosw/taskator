@TaskListCtrl = ($scope, $http) ->
  $http.get('tasks.json').success (data) =>
    $scope.projects = data.projects
    $scope.clients = data.clients
    $scope.users = data.users
    $scope.tasks = data.tasks    
  $scope.order = 'due_date'

TaskListCtrl.$inject = ['$scope', '$http']

jQuery ->
  $('.tasks-group').jScrollPane()