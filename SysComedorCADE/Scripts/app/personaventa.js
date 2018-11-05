var app = angular.module('AppVenta', []);
app.controller('CtrlVenta', function ($scope, $http, $window) {



    $("#idbuscar").keyup(function () {
        var bus = $("#idbuscar").val();
        $scope.busca(bus);
    });
    $scope.perventa = [];
    $scope.busca = function (e) {
        $http.post("/Persona/BusPersona", { ci: e }).success(function (result) {
            $scope.perventa = result;
            $("#btnmodalvende").trigger('click');
        }).error(function (result) {
            console.log(result);
        });

    }

    $scope.dt = [];
    $scope.dt.cant = 1;
    $scope.dt.vunit = 2.50;
    $scope.calculoventa = function () {
        var cant = 0.00;
        var vunit = 0.00;
        var total = 0.00;
        cant = $scope.dt.cant;
        vunit = $scope.dt.vunit;
        $scope.dt.total = (cant * vunit);
        return $scope.dt.total;

    }
    $scope.calculoventa();

    var tipopago = 0;
    $(document).ready(function () {
        checkcta = document.getElementById('CheckCuenta');
        checkcdo = document.getElementById('CkeckContado');        
        if (checkcta.checked) {
            document.getElementById('CkeckContado').removeAttribute('checked');
            tipopago = 1;
            //$('#CkeckContado').prop('checked', false);
        }
        $('#CheckCuenta').click(function () {
            if (checkcta.checked) {
                document.getElementById('CkeckContado').checked = false;
                tipopago = 1;
            } else {
                document.getElementById('CkeckContado').checked = true;
                tipopago = 2;
            }
            return tipopago;
        });

        $('#CkeckContado').click(function () {
            if (checkcdo.checked) {
                document.getElementById('CheckCuenta').checked = false;
                tipopago = 2;
            } else {
                document.getElementById('CheckCuenta').checked = true;
                tipopago = 1;
            }
            return tipopago;
        });

    });

    $scope.perventa = {};
    $scope.buscarp = [];
    $scope.perventa = [];
    $scope.guardaventa = function (dt) {
        $http.post("/Venta/GuardaVenta?cant=" + dt.cant + "&des=" + dt.detalle + "&vunit=" + dt.vunit + "&vt=" + dt.total + "&tpago=" + tipopago, $scope.perventa).success(function (result) {
            if(result == 0){
                toastr.error("Error al registrar", "VENTA POR COBRAR");
            }
            if (result == 1) {
                toastr.warning("Se regstró exitosamente ", "VENTA POR COBRAR");
                $("#btnmodalvendehide").trigger('click');
                $scope.perventa = [];
                //$scope.buscarp.CiRuc
                $scope.buscarp = [];
            }
            if (result == 2) {
                toastr.success("Se regstró exitosamente", "VENTA AL CONTADO");
                $("#btnmodalvendehide").trigger('click');
                $scope.perventa = [];
                $scope.buscarp = [];
            }
        }).error(function (result) {
            console.log(result);
        })
    }








});
