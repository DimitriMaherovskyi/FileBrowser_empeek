(function (angular) {

    angular
        .module("fileSystemBrowserModule")
        .controller("fileSystemBrowserController", fileSystemBrowserController);

    fileSystemBrowserController.$inject = ['$scope', 'fileSystemBrowserDataService', 'filesInfo'];

    function fileSystemBrowserController($scope, fileSystemBrowserDataService, filesInfo) {
        var vm = this;
        vm.filesInfo = filesInfo;

        //vm.filesHeaders = [
        //    {
        //        name: '<= 10 mb',
        //        field: 'FileUnder10MbCounter'
        //    },
        //    {
        //        name: '> 10, <= 50 mb',
        //        field: 'File10To50MbCounter'
        //    },
        //    {
        //        name: '>= 100 mb',
        //        field: 'FileOver100MbCounter'
        //    }
        //];

        var activate = function () {
            fileSystemBrowserDataService.getInfo().then(function (response) {
                vm.filesInfo = response.data;
            });
        };

        activate();
    }

})(angular);