// Permite ejecutar varios métodos cada 30 segundos
function TimeRunner() {
  this._listeners = [];
}
TimeRunner.prototype = {
  add: function (listener) {
    this._listeners.push(listener);
  },
  fire: function () {
    for (var i = 0; i < this._listeners.length; i++) {
      this._listeners[i].call();
    }    
  }
}

// http://www.developertutorials.com/tutorials/javascript/javascript-event-handlers-callback-functions-060817-1003/
//function Manager() {
//  this.callback = function () { }; // do nothing
//  this.registerCallback = function (callbackFunction) {
//    this.callback = (this.callback).andThen(callbackFunction);
//  }
//}
var timeRunner = new TimeRunner();
setInterval(function () { timeRunner.fire(); }, 5000);

// Todo: mirar esta página http://ajaxpatterns.org/Periodic_Refresh

function daysBetween(date1, date2) {

    // The number of milliseconds in one day
    var ONE_DAY = 1000 * 60 * 60 * 24;

    // Convert both dates to milliseconds
    var date1_ms = date1.getTime();
    var date2_ms = date2.getTime();

    // Calculate the difference in milliseconds
    var difference_ms = date1_ms - date2_ms;

    // Convert back to days and return
    return Math.round(difference_ms / ONE_DAY);
}