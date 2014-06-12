(function () {
   'use strict';
   var controllerId = 'gallery';
   angular.module('app').controller(controllerId, ['common', 'testservice', gallery]);

   function gallery(common, testservice) {
      var getLogFn = common.logger.getLogFn;
      var log = getLogFn(controllerId);

      var vm = this;
      vm.title = 'Gallery';
      
      vm.test = [];
      activate();

      function activate() {
         var promises = [getTest()];
         common.activateController(promises, controllerId)
             .then(function () { log('Activated Gallery View'); });
      }

      function getTest() {
         testservice.getMediaItems().then(function (data) {
            window.test = data.results;
            log("Fetched MediaItems" + data.results.length);
            return vm.test = data.results;
         });
      }
   }
})();