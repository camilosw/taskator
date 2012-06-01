// Actualiza o crea un proyecto
function actualizarProyecto(proyecto) {
  $.post(path + "Proyecto/AjaxEdit/" + proyecto.Id(),
    { nombre: proyecto.Nombre(), idCliente: proyecto.IdCliente(), 
      codigoPresupuesto: proyecto.CodigoPresupuesto() }, function (id) {
    // Si es un proyecto nuevo, el servidor retorna el Id que se le asignó
    if (id > 0) {
      proyecto.Id(id);
    }
  });
}

// Actualiza el orden de los proyectos
function reordenarProyectos() {
  var listadoProyectos = $.map(viewModel.proyectos(), function (proyecto) {
    return proyecto.Id();
  });
  $.ajax({
    url: path + 'Proyecto/UpdateOrder',
    data: JSON.stringify(listadoProyectos),
    type: 'POST',
    contentType: 'application/json, charset=utf-8',
    dataType: 'json'
  });
}

// Actualiza el orden de las tareas
function reordenarTareas() {
  var listadoProyectos = $.map(viewModel.proyectos(), function (proyecto) {
    var listadoTareas = $.map(proyecto.Tareas(), function (tarea) {
      return tarea.Id();
    });
    return { IdProyecto: proyecto.Id(), IdTareas: listadoTareas };
  });
  $.ajax({
    url: path + 'Tarea/UpdateOrder',
    data: JSON.stringify(listadoProyectos),
    type: 'POST',
    contentType: 'application/json, charset=utf-8',
    dataType: 'json'
  });
}

function corregirTiempo(tiempoEstimado) {
  $.post(path + "Tarea/CorregirTiempo/", { tiempo: tiempoEstimado() }, function (tiempoCorregido) {
    tiempoEstimado(tiempoCorregido);
  });
}