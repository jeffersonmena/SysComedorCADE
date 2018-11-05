var myApp = angular.module('AppContratoFinanciero', []);

myApp.controller('DescuentosController1', function ($scope, $http) {

    $scope.hola = "hola mundo";


    ////muestra contratos
    //$http.get("/Contrato/IndexContratosJson/").success(function (result) {
    //    $scope.descuentos = result;
    //}).error(function (result) {
    //    console.log(result);
    //});
    $scope.load = function () {
        //muestra descuentos
        $http.get("/Contrato/DescuentosAlumnoTemp").success(function (result) {
            $scope.descuentos = result;
            $scope.reloadPage = function () { window.location.reload(); }
        }).error(function (result) {
            console.log(result);
        })
    }
    $scope.load();
    //Adiciona descuentos
    var vm = this;
    vm.datos = {}
    vm.enviar = function () {
        $http.post('/Contrato/GuardarDescuento', vm.datos)
        .success(function (res) {
            $scope.Proveedores = res;
            vm.datos = {};
            $scope.load();
            $scope.reloadPage = function () { window.location.reload(); }

        }).error(function (data) {
            console.log(data);
        })
    }


    //Elimina descuentos
    $scope.eliminarDescuento = function (descuento) {
        $http.post('/Contrato/EliminarDescuento', { descuentoId: descuento })
        .success(function (res) {
            $scope.descuentos = res;
        }).error(function (data) {
            console.log(data);
        })
    }
});


myApp.controller('detallecontrato', function ($scope, $http) {

    $scope.detalle = "Detalle Contrato";

    //Adiciona descuentos
    var vm = this;
    vm.datos = {}
    vm.enviar = function () {
        $http.post('/Contrato/CalcularContrato', vm.datos)
        .success(function (res) {
            $scope.Detalles = res;

            vm.datos = {};
            $scope.load();
            $scope.tcobros();
            $scope.tdescuentos();
            $scope.tcontrato();
            $scope.reloadPage = function () { window.location.reload(); }

        }).error(function (data) {
            console.log(data);
        })
    }

    $scope.tcobros = function () {
        var total = 0;
        for (var i = 0; i < $scope.Detalles.length; i++) {
            var cobros = $scope.Detalles[i];
            if (cobros.Valor > 0) {
                total += (cobros.Valor);
            }
        }
        return total;

    }

    $scope.tdescuentos = function () {
        var total = 0;
        for (var i = 0; i < $scope.Detalles.length; i++) {
            var descuentos = $scope.Detalles[i];
            if (descuentos.Valor < 0) {
                total += (descuentos.Valor);
            }
        }
        return total;

    }

    $scope.tcontrato = function () {
        var total = 0;
        var totalcobros = 0;
        var totaldescuentos = 0;
        for (var i = 0; i < $scope.Detalles.length; i++) {
            var valor = $scope.Detalles[i];
            if (valor.Valor > 0) {
                totalcobros += (valor.Valor);
            }
            if (valor.Valor < 0) {
                totaldescuentos += (valor.Valor);
            }
        }
        return total = totalcobros + totaldescuentos;

    }


});


myApp.controller("observadorController", function ($scope) {

    $scope.operators =
    {
        "value": "suma",
        "values": ["suma", "resta", "mutliplicacion", "division"]
    };

    $scope.calcular = function () {

        switch ($scope.operators.value) {

            case "suma":
                $scope.resultado = parseInt($scope.first) + parseInt($scope.second);
                break;
            case "resta":
                $scope.resultado = parseInt($scope.first) - parseInt($scope.second);
                break;
            case "mutliplicacion":
                $scope.resultado = parseInt($scope.first) * parseInt($scope.second);
                break;
            case "division":
                $scope.resultado = parseInt($scope.first) / parseInt($scope.second);
                break;

        }

    }

    $scope.$watch($scope.calcular);

});

