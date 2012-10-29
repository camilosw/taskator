angular.module('task', ['taskFilters', 'taskServices']).
  config(['$routeProvider', ($routeProvider) ->
    $routeProvider.
      when('/tasks', {templateUrl: 'assets/tasks/index.html', controller: TaskListCtrl}).
      when('/tasks/user', {templateUrl: 'assets/tasks/index-users.html', controller: TaskListCtrl}).
      when('/tasks/tag', {templateUrl: 'assets/tasks/index.html', controller: TaskListCtrl}).
      when('/tasks/:id', {templateUrl: 'assets/tasks/show.html', controller: TaskDetailCtrl}).
      otherwise({redirectTo: '/tasks'})
  ])