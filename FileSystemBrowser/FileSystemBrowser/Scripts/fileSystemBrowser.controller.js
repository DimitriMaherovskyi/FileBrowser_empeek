(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .controller("fileSystemBrowserController", fileSystemBrowserController);

    fileSystemBrowserController.$inject = ['$scope', 'fileSystemBrowserDataService'];

    function fileSystemBrowserController($scope, fileSystemBrowserDataService) {
        var vm = this;
        vm.filesInfo = 'filesInfo';

        var activate = function () {
            fileSystemBrowserDataService.getInfo().then(function (response) {
                vm.filesInfo = response.data;
            });
        };

        activate();
    }

})(angular);