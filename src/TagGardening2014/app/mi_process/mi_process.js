(function ()
{
   'use strict';
   var controllerId = 'mi_process';
   angular.module('app').controller(controllerId, ['common', 'testservice', '$scope', '$routeParams', mi_process]);

   function mi_process(common, testservice, $scope, $routeParams)
   {
      var getLogFn = common.logger.getLogFn;
      var log = getLogFn(controllerId);
      var vm = this;
      vm.title = 'Media Item - Process';
      vm.test = {};
      vm.resultSet = {};
      vm.hasResultSet = false;
      vm.startWeeding = startWeeding;

      activate();

      function activate()
      {
         var promises = [getTagsForMediaItem()];
         common.activateController(promises, controllerId)
             .then(function () { log('Activated MediaItems Process View'); });
      }

      function getTagsForMediaItem()
      {
         var id = $routeParams.id;
         if (common.isNumber(id))
         {
            testservice.getTagsForMediaItem(id)
               .then(function (data)
               {
                  window.test = data.results;
                  log("Fetched Tags for MediaItemId " + id);
                  window.test = data.results;
                  return vm.test = data.results;
               });
         }
      }

      function startWeeding() {
         vm.hasResultSet = false;
         var tagArray = [];
         vm.test.forEach(function(tag) {
            var jsonObject = {};
            jsonObject.TagId = tag['TagId'];
            jsonObject.TagValue = tag['TagValue'];
            tagArray.push(jsonObject);
         });

         var jsonObjectSet = JSON.stringify({ TagSet: tagArray });
         $.ajax({
            async: true,
            contentType: 'application/json; charset=utf-8',
            dataType: 'text',
            type: "POST",
            data: jsonObjectSet,
            url: "Handlers/TagHandler.ashx",
            success: function (msg)
            {
               console.log(msg);
               vm.resultSet = JSON.parse(msg);
               window.resultSet = vm.resultSet;
               vm.hasResultSet = true;
               $scope.$apply();
            },
            error: function(msg) {
               console.log(msg);
            }
         });
      }

      return vm;
   }
})();