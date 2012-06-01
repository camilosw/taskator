$(function () {
  
});
$(window).resize(sizeColumns());

function sizeColumns() {
  var contentHeight = $('html').height() - $('.header').height() - $('.header-panels').height();
  console.log($('.column'));
  $('.column').each(function () {
    
    var columnHeight = contentHeight - $(this).find('.column-header').height() - 70;
    $(this).find('.tasks-container').css('height', columnHeight + 'px');
    console.log(columnHeight);
  });
  $('.tasks-container').lionbars();
  //var newHeight = $("html").height() - $("#header").height() - $("#footer").height() + "px";
  //$("#content").css("height", newHeight);
}

//#region View Model
//----------------------------------------------------------------
// Contiene los estados de las tareas
var Estado = {
  valores: [],
  GetById: function (id) {
    for (var i = 0; i < this.valores.length; i++) {
      if (this.valores[i].Id == id)
        return this.valores[i];
    }
  },
  GetByNombre: function (nombre) {
    for (var i = 0; i < this.valores.length; i++) {
      if (this.valores[i].Nombre == nombre)
        return this.valores[i];
    }
  },
  GetColorById: function (id) {
    return "#" + this.GetById(id).Color;
  },
  tareaEsActiva: function (id) {
    return this.GetById(id).TareaActiva == 1;
  },
  IdPendiente: 1,
  IdEnProceso: 3,
  loaded: false
}

var Cliente = {
  valores: [],
  GetNombreById: function (id) {
    for (var i = 0; i < this.valores.length; i++) {
      if (this.valores[i].Id == id)
        return this.valores[i].Nombre;
    }
    return null;
  },
  loaded: false
}

var UsuarioRegistrado = {
  id: null,
  nombre: null
}

var TareaEnProceso = {
  id: null
}

//#endregion

// Basado en http://www.knockmeout.net/2011/03/guard-your-model-accept-or-cancel-edits.html
ko.revertableObservable = function (initialValue) {
  //private variables
  var _previous = null;
  var _actual = ko.observable(initialValue);

  var result = ko.computed({
    read: function () {
      return _actual();
    },
    write: function (newValue) {
      if (newValue !== _actual()) {
        _previous = _actual();
        _actual(newValue);
      }
    }
  });

  //notify subscribers to update their value with the original
  result.revert = function () {
    _actual(_previous);
  };

  result.previous = function () {
    return _previous;
  };

  return result;
};

//#region Binding Handlers
//-------------------------------------------------------------------

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = valueAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).datepicker("setDate", value);
    }
};

ko.bindingHandlers.convertLinks = {
    update: function (element, valueAccessor) {
        var options = valueAccessor();
        var text = ko.utils.unwrapObservable(options.text);
        text = text.replace(/\n/g, '<br />');
        var exp = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
        text = text.replace(exp, "<a href='$1' target='_blank'>$1</a>");
        $(element).html(text);
    }
};


