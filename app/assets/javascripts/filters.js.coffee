angular.module('taskFilters', []).
  filter('user', ->
    (input) ->
      input.users[input.user_id].username if input.users[input.user_id]? 
  )