(function () {
   'use strict';
   var controllerId = 'mi_details';
   angular.module('app').controller(controllerId, ['common', 'testservice', '$routeParams', mi_details]);

   function mi_details(common, testservice, $routeParams) {
      var getLogFn = common.logger.getLogFn;
      var log = getLogFn(controllerId);
      var vm = this;
      vm.title = 'Media Item - Details';
      vm.test = {};

      activate();

      function activate() {
         var promises = [getMediaItem()];
         common.activateController(promises, controllerId)
             .then(function () { log('Activated MediaItems Details View'); });
      }

      function getMediaItem() {
         var id = $routeParams.id;
         if (common.isNumber(id)) {
            testservice.getMediaItem(id)
               .then(function (data) {
                  window.test = data.results;
                  log("Fetched MediaItem");
                  window.test = data.results[0];
                  data.results[0].FileName = "Media/" + data.results[0].FileName;
                  vm.finishedProcessing = data.results[0].MediaItemStatusId == 2;
                  return vm.test = data.results[0];
            });
         }
      }
   }
})();