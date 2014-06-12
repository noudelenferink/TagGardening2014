(function () {

	angular.module('app').factory('testservice',
	['$http', '$q', '$timeout', 'breeze', 'logger', testservice]);

	function testservice($http, $q, $timeout, breeze, logger) {

		var serviceName = 'breeze/tagGardening'; // route to the same origin Web Api controller

		// *** Cross origin service example  ***
		// When data server and application server are in different origins
		//var serviceName = 'http://todo.breezejs.com/breeze/todos'; 

		var manager = new breeze.EntityManager(serviceName);
		//manager.enableSaveQueuing(true);

		var service = {
			getMediaItems: getMediaItems,
			getMediaItem: getMediaItem,
			getTagsForMediaItem: getTagsForMediaItem,
		};
		return service;

		/*** implementation details ***/

		function getMediaItems() {
			var query = breeze.EntityQuery
					.from("MediaItems");

			var promise = manager.executeQuery(query).catch(queryFailed);
			return promise;

			function queryFailed(error) {
				logger.error(error.message, "Query failed");
				return $q.reject(error); // so downstream promise users know it failed
			}
		}

		function getMediaItem(id) {
			var query = breeze.EntityQuery
					.from("MediaItems")
					.where("MediaItemId", "==", id);

			var promise = manager.executeQuery(query).catch(queryFailed);
			return promise;

			
		}

      function getTagsForMediaItem(id) {
         var query = breeze.EntityQuery
               .from("Tags")
               .where("MediaItemId", "==", id);
         var promise = manager.executeQuery(query).catch(queryFailed);
         return promise;
      }

      function queryFailed(error)
      {
         logger.error(error.message, "Query failed");
         return $q.reject(error); // so downstream promise users know it failed
      }

		function getTodos(includeArchived) {
			var query = breeze.EntityQuery
				 .from("Todos")
				 .orderBy("CreatedAt");

			if (!includeArchived) { // if excluding archived Todos ...
				// add filter clause limiting results to non-archived Todos
				query = query.where("IsArchived", "==", false);
			}

			var promise = manager.executeQuery(query).catch(queryFailed);
			return promise;

			function queryFailed(error) {
				logger.error(error.message, "Query failed");
				return $q.reject(error); // so downstream promise users know it failed
			}
		}

		//#region demo operations
		function purge(callback) {
			return $http.post(serviceName + '/purge')
			.then(function () {
				logger.success("database purged.");
				manager.clear();
				if (callback) callback();
			})
			.catch(function (error) {
				logger.error("database purge failed: " + error);
				return $q.reject(error); // so downstream promise users know it failed
			});
		}

		function reset(callback) {
			return $http.post(serviceName + '/reset')
			.then(function () {
				logger.success("database reset.");
				manager.clear();
				if (callback) callback();
			})
			.catch(function (error) {
				logger.error("database reset failed: " + error);
				return $q.reject(error); // so downstream promise users know it failed
			});
		}
		//#endregion
	}
})();