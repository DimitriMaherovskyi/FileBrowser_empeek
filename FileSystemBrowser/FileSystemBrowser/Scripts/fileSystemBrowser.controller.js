(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .controller("fileSystemBrowserController", fileSystemBrowserController);

    fileSystemBrowserController.$inject = ['$scope', 'fileSystemBrowserDataService'];

    function fileSystemBrowserController($scope, fileSystemBrowserDataService) {
        var vm = this;
        vm.fileCounter;
        vm.directoryContainer;

        vm.calculationLoading;
        vm.directoryLoading;
        vm.currentPath;

        // Tokens.
        vm.tokenRoot = 'root';
        vm.tokenOk = 'ok';
        vm.tokenBack = 'back';

        vm.grabAndCount = function(root, token) {
            grabDirectoryContents(root, token);
            getFilesCount(root, token)
        }

        var activate = function () {
            vm.grabAndCount("", vm.tokenRoot);
        };

        activate();

        // Ajax method callers.
        function grabDirectoryContents(root, token) {
            vm.directoryLoading = true;
            fileSystemBrowserDataService.grabDirectoryContents(root, token).then(function (response) {
                vm.directoryContainer = response.data;
                vm.currentPath = vm.directoryContainer.Path;
            }).finally(function () {
                vm.directoryLoading = false;
            });
        }

        function getFilesCount(root, token) {
            vm.fileCounter = null;
            // Displaying message to wait the response.
            vm.calculationLoading = true;
            fileSystemBrowserDataService.getFilesCount(root, token).then(function (response) {
                vm.fileCounter = response.data;
            }).finally(function () {
                vm.calculationLoading = false;
            });
        }
    }
})(angular);