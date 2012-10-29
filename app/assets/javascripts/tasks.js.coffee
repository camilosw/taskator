@TaskListCtrl = ($scope, Tasks) ->
  data = Tasks.index ->
    $scope.projects = data.projects
    $scope.clients = data.clients
    $scope.users = data.users
    $scope.tasks = data.tasks

  $scope.order = 'due_date'

TaskListCtrl.$inject = ['$scope', 'Tasks']

@TaskDetailCtrl = ($scope, $routeParams, Task) ->  
  $scope.task = Task.show({id: $routeParams.id})

TaskDetailCtrl.$inject = ['$scope', '$routeParams', 'Task']

jQuery ->
  $('.tasks-group').jScrollPane()