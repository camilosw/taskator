function Tarea(id, idProyecto, idEstado, idAsignado, usuarioCrea, descripcion, fechaEntrega, fechaCreada, tiempoEstimado) {
  var self = this;
  // Datos cliente-servidor
  self.Id = ko.observable(id);
  self.IdProyecto = ko.observable(idProyecto);
  idEstado = idEstado == null ? undefined : idEstado.toString();
  self.IdEstado = ko.revertableObservable(idEstado);
  idAsignado = idAsignado == null ? undefined : idAsignado;
  self.IdAsignado = ko.revertableObservable(idAsignado);
  self.UsuarioCrea = ko.observable(usuarioCrea);
  self.Descripcion = ko.observable(descripcion);
  self.FechaEntrega = ko.observable(fechaEntrega);
  self.FechaCreada = ko.observable(fechaCreada);
  self.TiempoEstimado = ko.observable(tiempoEstimado);
  self.TiempoEjecutado = ko.observable();

  // Datos solo del cliente
  self.editando = ko.observable(false);
  self.editandoTiempoEstimado = ko.observable(false);

  self.colorEstado = ko.computed(function () {
    return Estado.GetColorById(self.IdEstado());
  });

  self.colorFecha = ko.computed(function () {
    if (self.FechaEntrega() == null) {
      return 'transparent';
    }
    var hoy = new Date().setHours(0, 0, 0, 0);
    var fechaEntrega = self.FechaEntrega().setHours(0, 0, 0, 0);
    if (fechaEntrega < hoy) {
      return '#fa3e3e';
    } else if (fechaEntrega == hoy) {
      return '#ffea00';
    } else if (fechaEntrega < new Date().setDate(new Date().getDate() + 2)) {
      return '#ff970f';
    } else {
      return '#ccc';
    }
  });

  self.diasDiferencia = ko.computed(function () {
    if (self.FechaEntrega() == null) {
      return '';
    }
    var hoy = Date.today();
    var fechaEntrega = self.FechaEntrega();
    var diferencia = daysBetween(hoy, fechaEntrega);
    var texto = Math.abs(diferencia) == 1 ? ' dia' : ' dias';
    if (diferencia > 0) {
      return 'Retrasado ' + diferencia + texto;
    }
    if (diferencia == 0) {
      return 'Para hoy';
    }
    if (diferencia < 0) {
      return 'Queda ' + Math.abs(diferencia) + texto;
    }
  });

  self.diasCreacion = ko.computed(function () {
    var hoy = Date.today();
    var fechaCreada = self.FechaCreada();
    var diferencia = daysBetween(hoy, fechaCreada);
    var texto = Math.abs(diferencia) == 1 ? ' dia' : ' dias';
    if (diferencia == 0) {
      return 'hoy';
    }
    return 'hace ' + Math.abs(diferencia) + texto;
  });

  self.IdEstado.subscribe(function () {
    var estado = Estado.GetByNombre("En proceso");
    if (self.IdEstado() == estado.Id && self.IdAsignado() == null) {
      alert("No se puede poner en proceso si no está asignada.");
      self.IdEstado.revert();
    }
    if (self.IdEstado() == estado.Id && self.IdAsignado() != UsuarioRegistrado.id) {
      alert("No se puede poner en proceso las tareas de otro usuario.");
      self.IdEstado.revert();
    }
    if (self.Id() != null) {
      actualizarTarea(self);
    }
  });

  self.IdAsignado.subscribe(function () {
    var estado = Estado.GetByNombre("En proceso");
    if (self.IdEstado() == estado.Id && self.IdAsignado() == null) {
      alert("Una tarea en proceso no se puede dejar sin asignar");
      self.IdAsignado.revert();
    }
    if (self.Id() != null) {
      actualizarTarea(self);
    }
  });

  self.FechaEntrega.subscribe(function () {
    if (self.Id() != null) {
      actualizarTarea(self); 
    } 
  });

  self.TiempoEstimado.subscribe(function () {
    self.editandoTiempoEstimado(false);
    corregirTiempo(self.TiempoEstimado);
    if (self.Id() != null) {
      actualizarTarea(self);
    }
  });

  // Métodos
  self.editar = function () {
    actualizando = true;
    self.editando(true);
  }
  self.guardar = function () {
    self.editando(false);
    actualizarTarea(self);
  }
  self.cancelar = function (parentList) {
    actualizando = false;
    self.editando(false);
    if (self.Id() == null) {
      parentList.Tareas.remove(self);
    }
  }
}

Tarea.prototype = {
  setNombreProyecto: function (nombreProyecto) {
    this.NombreProyecto = ko.observable(nombreProyecto);
  }
}