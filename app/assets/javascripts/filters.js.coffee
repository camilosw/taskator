angular.module('taskFilters', []).
  filter('user', ->
    (input) ->
      input.users[input.user_id].email if input.users[input.user_id]? 
  )