/// <reference path="../node_modules/karma-jasmine/lib/jasmine.js" />
/// <reference path="../node_modules/angular/angular.min.js" />
/// <reference path="../node_modules/angular-mocks/angular-mocks.js" />
/// <reference path="../lib/Model.js" />
describe('Testing the Controller', function () {
    var http, SampleApi;
    var status = true;
    var httpData = {
        user: {
            'Id': 1,
            'Name': 'lakshman',
            'Email': 'lpeethan@xya.com',
            'Age': 0,
            'DateOfBirth': '0001-01-01T00:00:00',
            'Addresses': null,
            'Intrests': null,
            'IsAlive': false,
            'MaritalStatus': 0
        }
    };
    var data = {
        id: 1,
        user: {
            'Id': 1,
            'Name': 'lakshman',
            'Email': 'lpeethan@xya.com',
            'Age': 0,
            'DateOfBirth': '0001-01-01T00:00:00',
            'Addresses': null,
            'Intrests': null,
            'IsAlive': false,
            'MaritalStatus': 0
        }
    };
    beforeEach(module('Model'));
    beforeEach(inject(function (_$controller_, _$httpBackend_, _$rootScope_, _$http_, _SampleApi_) {
        $scope = _$rootScope_.$new();
        SampleApi = _SampleApi_;
        http = _$http_;
        $controller = _$controller_;
        $httpBackend = _$httpBackend_;
    }));

    beforeEach(function () {
        var Controller = $controller('ModelController', { $scope: $scope, $http: http, SampleApi: SampleApi });
    });

    it('should get the contacts data', function () {
        $httpBackend.when('GET', '/api/Contacts').respond(httpData);
        $scope.Get_Contacts();
        $httpBackend.flush();
        expect($scope.contacts).toEqual(httpData);
    });

    it('get should throw error', function () {
        $httpBackend.when('GET', '/api/Contacts').respond(500);
        $scope.Get_Contacts();
        $httpBackend.flush();
        expect($scope.contacts).toBe(undefined);
    });

    it('post should add the contact', function () {
        $httpBackend.when('POST', '/api/Contacts', httpData).respond(status);
        $scope.Add_Contacts(httpData);
        $httpBackend.flush();
        expect($scope.user).toEqual(true);
    });

    it('post should throw error', function () {
        $httpBackend.when('POST', '/api/Contacts', httpData).respond(500);
        $scope.Add_Contacts(httpData);
        $httpBackend.flush();
        expect($scope.user).toBe(undefined);
    });

    it('should update the contact', function () {
        $httpBackend.when('PUT', '/api/Contacts/' + data.id, data).respond(status);
        $scope.Update_Contacts(data);
        $httpBackend.flush();
        expect($scope.updateStatus).toEqual(true);
    });

    it('put should throw the error', function () {
        $httpBackend.when('PUT', '/api/Contacts/' + data.id, data).respond(500);
        $scope.Update_Contacts(data);
        $httpBackend.flush();
        expect($scope.updateStatus).toBe(undefined);
    });

    it('should delete the contact', function () {
        $httpBackend.when('DELETE', '/api/Contacts/' + data.id, data).respond(status);
        $scope.Delete_Contacts(data);
        $httpBackend.flush();
        expect($scope.deleteStatus).toEqual(true);
    });

    it('should delete the contact', function () {
        $httpBackend.when('DELETE', '/api/Contacts/' + data.id, data).respond(500);
        $scope.Delete_Contacts(data);
        $httpBackend.flush();
        expect($scope.deleteStatus).toBe(undefined);
    });
});
