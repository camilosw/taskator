angular.module('taskServices', ['ngResource']).
  factory('Tasks', ($resource) ->
    $resource('/tasks.json', {}, {
      index: {method: 'GET', isArray: false }      
    })
  ).
  factory('Task', ($resource) ->
    $resource('/tasks/:id.json', {}, {
      show: {method: 'GET' }
    })
  )

#angular.module('phonecatServices', ['ngResource']).
#    factory('Phone', function($resource){
#  return $resource('phones/:phoneId.json', {}, {
#    query: {method:'GET', params:{phoneId:'phones'}, isArray:true}
#  });
#});