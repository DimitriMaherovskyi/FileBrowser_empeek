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
            var promise = $http.get('/api/FileInfo/GetInformation');
            return promise;
        }

        function getMock() {
            var promise = 
                {
                    FileUnder10MbCounter: 10,
                    File10To50MbCounter: 3,
                    FileOver100MbCounter: 2
                };

            return promise;
        }
    }

})(angular);