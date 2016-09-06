(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .controller("fileSystemBrowserController", fileSystemBrowserController);

    fileSystemBrowserController.$inject = ['$scope', 'fileSystemBrowserDataService'];

    function fileSystemBrowserController($scope, fileSystemBrowserDataService) {
        var vm = this;
        vm.fileCounter;
        vm.dataLoading;

        var activate = function () {
            getFilesCount('D:\\');
        };

        activate();

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