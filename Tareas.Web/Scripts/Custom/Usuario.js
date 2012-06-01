//#region View Model
//----------------------------------------------------------------
var Usuario = function (id, nombre, tareas) {
  this.Id = ko.observable(id);
  this.Nombre = ko.observable(nombre);
  this.Tareas = ko.observableArray(tareas);
}

var viewModel = {
  usuarios: ko.observableArray([])
}

//#endregion

//#region Comunicación con el servidor
//-------------------------------------------------------------------
// Carga el listado de estados de las tareas
$.post(path + "Estado/List", function (estados) {
  Estado.valores = estados;
});

// Carga el listado de usuarios
$.post(path + "Usuario/List", function (usuarios) {
  //Usuario.valores = usuarios;
  $.each(usuarios, function () {
    if (this.Autenticado == true) {
      UsuarioRegistrado.id = this.Id;
      UsuarioRegistrado.nombre = this.Nombre;
      return false;
    }
  });
  cargarUsuariosTareas();
});

// Carga el listado inicial de proyectos y tareas. Se carga después de que estén 
// cargados los estados
function cargarUsuariosTareas() {
  $.post(path + "Usuario/ListTareas", function (usuarios) {
    $.map(usuarios, function (usuario) {
      var tareas = null;
      var tareas = $.map(usuario.Tareas, function (tarea) {
        var fechaEntrega = tarea.FechaEntrega == null ? null : new Date(tarea.FechaEntrega);
        var fechaCreada = new Date(tarea.FechaCreada);
        var tareaNueva = new Tarea(tarea.Id, tarea.IdProyecto, tarea.IdEstado,
                          tarea.IdAsignado, tarea.UsuarioCrea, tarea.Descripcion, fechaEntrega, fechaCreada,
                          tarea.TiempoEstimado);
        tareaNueva.setNombreProyecto(tarea.NombreProyecto);
        tareaNueva.TiempoEjecutado(tarea.TiempoEjecutado);
        return tareaNueva;
      });
      viewModel.usuarios.push(new Usuario(usuario.Id, usuario.Nombre, tareas));
    });
  });
}

// Actualiza o crea una tarea
function actualizarTarea(tarea) {
  var dato = {
    IdProyecto: tarea.IdProyecto(),
    IdEstado: parseInt(tarea.IdEstado()),
    IdAsignado: parseInt(tarea.IdAsignado()),
    Descripcion: tarea.Descripcion(),
    FechaEntrega: tarea.FechaEntrega() == null ? null : dateFormat(tarea.FechaEntrega(), 'isoUtcDateTime'),
    TiempoEstimadoString: tarea.TiempoEstimado()
  };
  $.post(path + "Tarea/AjaxEdit/" + tarea.Id(), dato, function (id) {
    if (id > 0) {
      tarea.Id(id);
    }
  });
  // Si la tarea cambia al estado en proceso, verifica que no queden otras tareas en ese estado
  if (tarea.IdEstado() == Estado.IdEnProceso) {
    TareaEnProceso.id = tarea.Id;
    $.each(viewModel.usuarios(), function () {
      $.each(this.Tareas(), function () {
        if (this.Id() != tarea.Id() &&
            this.IdEstado() == Estado.IdEnProceso &&
            this.IdAsignado() == tarea.IdAsignado()) {
          this.IdEstado(Estado.IdPendiente);
        }
      });
    });
  }
}

// Envía el dato de la tarea en curso
// TODO: mirar si esto se puede mejorar con dos for
timeRunner.add(function () {
  $.each(viewModel.usuarios(), function () {
    var finded = false;
    $.each(this.Tareas(), function () {
      var self = this;
      if (self.IdAsignado() == UsuarioRegistrado.id &&
          self.IdEstado() == Estado.IdEnProceso) {
        $.post(path + 'Tarea/LogTrabajoTarea/' + self.Id(), function (tiempoEjecutado) {
          self.TiempoEjecutado(tiempoEjecutado);
        });
        finded = true;
        return false;
      }
    });
    if (finded == true) return false;
  });
});

//#endregion

//#region Binding Handlers
//-------------------------------------------------------------------

//#endregion

ko.applyBindings(viewModel);
