(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .factory("fileSystemBrowserDataService", fileSystemBrowserDataService);

    fileSystemBrowserDataService.$inject = ["$http"];

    function fileSystemBrowserDataService($http) {

        var service = {
            getFilesCount: getFilesCount,
            grabDirectoryContents: grabDirectoryContents
        };

        return service;

        function getFilesCount(root) {
            var promise = $http({
                url: '/api/FileCounter/CountFilesFromDirectory',
                method: "GET",
                params: { root: root }
            });
            return promise;
        }

        function grabDirectoryContents(root) {
            var promise = $http({
                url: '/api/FileExplorer/GrabDirectoryContents',
                method: "GET",
                params: { root: root }
            });
            return promise;
        }
    }

})(angular);