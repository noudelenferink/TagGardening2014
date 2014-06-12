(function ()
{
   'use strict';
   var controllerId = 'mi_process';
   angular.module('app').controller(controllerId, ['common', 'testservice', '$routeParams', mi_process]);

   function mi_process(common, testservice, $routeParams)
   {
      var getLogFn = common.logger.getLogFn;
      var log = getLogFn(controllerId);
      var vm = this;
      vm.title = 'Media Item - Process';
      vm.test = {};

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
         var tagArray = [];
         vm.test.forEach(function(tag) {
            var jsonObject = {};
            jsonObject.TagId = tag['TagId'];
            jsonObject.TagValue = tag['TagValue'];
            tagArray.push(jsonObject);
         });

         //example data
         var Titles = [
             { 'title': 'Steve' }, { 'title': 'John' }, { 'title': 'Andrew' }
         ];
         var myJSON = JSON.stringify({ TagSet: tagArray });
         console.log(myJSON);
         $.ajax({
            async: true,
            contentType: 'application/json; charset=utf-8',
            dataType: 'text',
            type: "POST",
            data: myJSON,
            url: "Handlers/TagHandler.ashx",
            success: function (msg)
            {
               console.log(msg);
               window.result = JSON.parse(msg);
            },
            error: function(msg) {
               console.log(msg);
            }
         });
      }

      
   }
})();