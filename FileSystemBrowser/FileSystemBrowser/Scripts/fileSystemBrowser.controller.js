(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .controller("fileSystemBrowserController", fileSystemBrowserController);

    fileSystemBrowserController.$inject = ['$scope', 'fileSystemBrowserDataService'];

    function fileSystemBrowserController($scope, fileSystemBrowserDataService) {
        var vm = this;
        vm.fileCounter;
        vm.directoryContainer;

        vm.dataLoading;
        vm.currentPath;

        vm.grabAndCount = function(root, token) {
            grabDirectoryContents(root, token);
            getFilesCount(root, token)
        }

        var activate = function () {
            vm.grabAndCount("", 'root');
        };

        activate();

        function grabDirectoryContents(root, token) {
            fileSystemBrowserDataService.grabDirectoryContents(root, token).then(function (response) {
                vm.directoryContainer = response.data;
                vm.currentPath = vm.directoryContainer.Path;
            });
        }

        function getFilesCount(root, token) {
            vm.fileCounter = null;
            vm.dataLoading = true;
            fileSystemBrowserDataService.getFilesCount(root, token).then(function (response) {
                vm.fileCounter = response.data;
            }).finally(function () {
                vm.dataLoading = false;
            });
        }

        
    }

})(angular);