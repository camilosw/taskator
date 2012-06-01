//$(window).scroll(function () {
//    $('.column-header').css('top', ($(window).scrollTop()) + "px");
//});

//#region View Model
//----------------------------------------------------------------
var Usuario = {
  valores: [],
  loaded: false
}

var Proyecto = function (id, nombre, idCliente, codigoPresupuesto, tareas) {
  var self = this;
  self.Id = ko.observable(id);
  self.Nombre = ko.observable(nombre);
  idCliente = idCliente == null ? undefined : idCliente;
  self.IdCliente = ko.observable(idCliente);
  self.CodigoPresupuesto = ko.observable(codigoPresupuesto);
  self.Tareas = ko.observableArray(tareas);

  self.NombreCliente = ko.computed(function () {
    return Cliente.GetNombreById(self.IdCliente());
  });

  self.agregarTarea = function (data, event) {
    var tarea = new Tarea(null, self.Id(), Estado.valores[0].Id, null, UsuarioRegistrado.nombre,
            "", null, Date.today(), null);
    tarea.editando(true);
    this.Tareas.push(tarea);

    // Hace scroll si la nueva tarea queda oculta
    var windowHeight = $(window).height();
    var projectElement = $(event.target).parent().parent().parent()
    var projectHeight = projectElement.height();
    if (projectHeight > windowHeight) {
      $('html, body').animate({ scrollTop: projectHeight - 200 }, 600);
    }
  }
}

var viewModel = {
  proyectos: ko.observableArray([]),
  proyectoSeleccionado: ko.observable(),
  seleccionarProyecto: function (proyecto) {
    this.proyectoSeleccionado(proyecto);
  },
  terminarEdicionProyecto: function (proyecto) {
    this.seleccionarProyecto();
    actualizarProyecto(proyecto);
  },
  cancelarEdicionProyecto: function (proyecto) {
    if (proyecto.Id() == -1) {
      this.proyectos.remove(proyecto);
    }
    this.seleccionarProyecto();
  },
  agregarProyecto: function () {
    var proyecto = new Proyecto(-1, '', null, '', []);
    this.proyectoSeleccionado(proyecto);
    this.proyectos.unshift(proyecto);
  }
};
//#endregion

//#region Comunicación con el servidor
//-------------------------------------------------------------------
// Carga el listado de estados de las tareas
$.post(path + "Estado/List", function (estados) {
  Estado.valores = estados;
  Estado.loaded = true;
  cargarProyectosTareas();
});

// Carga el listado de clientes
$.post(path + "Cliente/AjaxList", function (clientes) {
  Cliente.valores = clientes;
  Cliente.loaded = true;
  cargarProyectosTareas();
});

// Carga el listado de usuarios
$.post(path + "Usuario/List", function (usuarios) {
  Usuario.valores = usuarios;
  $.each(usuarios, function () {
    if (this.Autenticado == true) {
      UsuarioRegistrado.id = this.Id;
      UsuarioRegistrado.nombre = this.Nombre;
      return false;
    }
  });
  Usuario.loaded = true;
  cargarProyectosTareas();
});


//setInterval("cargarProyectosTareas()", 5000);

// Carga el listado inicial de proyectos y tareas. Se carga después de que estén 
// cargados los estados
function cargarProyectosTareas() {
  if (!Estado.loaded || !Cliente.loaded || !Usuario.loaded) return;
  $.post(path + "Proyecto/List", function (proyectos) {
    viewModel.proyectos.removeAll();
    $.each(proyectos, function (index, proyecto) {
      var tareas = $.map(proyecto.Tareas, function (tarea) {
        var fechaEntrega = tarea.FechaEntrega == null ? null : new Date(tarea.FechaEntrega);
        var fechaCreada = new Date(tarea.FechaCreada);
        if (tarea.IdEstado == Estado.IdEnProceso) {
          TareaEnProceso.id = tarea.Id;
        }
        var tareaNueva = new Tarea(tarea.Id, tarea.IdProyecto, tarea.IdEstado, tarea.IdAsignado,
                          tarea.UsuarioCrea, tarea.Descripcion, fechaEntrega, fechaCreada,
                          tarea.TiempoEstimado);
        tareaNueva.TiempoEjecutado(tarea.TiempoEjecutado);
        return tareaNueva;
      });
      viewModel.proyectos.push(new Proyecto(proyecto.Id, proyecto.Nombre, proyecto.IdCliente,
                                            proyecto.CodigoPresupuesto, tareas));
    });
    sizeColumns();
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
    $.each(viewModel.proyectos(), function () {
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
  $.each(viewModel.proyectos(), function () {
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