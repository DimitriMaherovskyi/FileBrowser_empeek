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

        vm.grabAndCount = function(root) {
            grabDirectoryContents(root);
            getFilesCount(root)
        }

        var activate = function () {
            vm.grabAndCount("E:\\");
        };

        activate();

        function grabDirectoryContents(root) {
            vm.currentPath = root;
            fileSystemBrowserDataService.grabDirectoryContents(root).then(function (response) {
                vm.directoryContainer = response.data;
            });
        }

        function getFilesCount(root) {
            vm.dataLoading = true;
            fileSystemBrowserDataService.getFilesCount(root).then(function (response) {
                vm.fileCounter = response.data;
            }).finally(function () {
                vm.dataLoading = false;
            });
        }

        
    }

})(angular);