(function () {
   'use strict';

   var app = angular.module('app');

   // Collect the routes
   app.constant('routes', getRoutes());

   // Configure the routes and route resolvers
   app.config(['$routeProvider', 'routes', routeConfigurator]);
   function routeConfigurator($routeProvider, routes) {

      routes.forEach(function (r) {
         $routeProvider.when(r.url, r.config);
      });
      $routeProvider.otherwise({ redirectTo: '/' });
   }

   // Define the routes 
   function getRoutes() {
      return [
         {
            url: '/',
            config:
            {
               templateUrl: 'app/dashboard/dashboard.html',
               title: 'dashboard',
               settings:
               {
                  nav: 1,
                  content: '<i class="fa fa-dashboard"></i> Dashboard'
               }
            }
         },
         {
            url: '/gallery',
            config:
            {
               title: 'gallery',
               templateUrl: 'app/gallery/gallery.html',
               settings:
               {
                     nav: 3,
                     content: '<i class="fa fa-user "></i> Gallery'
               }
            }
         },
         {
            url: '/mi_details/:id',
            config:
            {
               title: 'gallery',
               templateUrl: 'app/mi_details/mi_details.html'
            }
         },
         {
            url: '/mi_process/:id',
            config:
            {
               title: 'gallery',
               templateUrl: 'app/mi_process/mi_process.html'
            }
         }
      ];
   }
})();