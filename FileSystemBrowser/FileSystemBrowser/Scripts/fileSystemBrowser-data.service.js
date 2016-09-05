(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .factory("fileSystemBrowserDataService", fileSystemBrowserDataService);

    fileSystemBrowserDataService.$inject = ["$http"];

    function fileSystemBrowserDataService($http) {

        var service = {
            getInfo: getInfoAjax,
        };

        return service;

        function getInfoAjax() {
            var promise = $http.get('/FileInfo/GetInformation');
            return promise;
        }
    }

})(angular);