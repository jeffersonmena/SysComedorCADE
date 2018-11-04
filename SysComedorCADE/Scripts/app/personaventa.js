var app = angular.module('AppVenta', []);
app.controller('CtrlVenta', function ($scope, $http, $window) {

    $scope.hora = new Date().getHours().toString()
    $scope.minutos = new Date().getMinutes().toString();
    $scope.segundo = new Date().getSeconds().toString();
    $scope.date = new Date().getTime();

    $scope.horasalida = $scope.hora+":"+$scope.minutos+":"+$scope.segundo;
    $scope.dt = [];
    $scope.horarioatencion = function () {
        var hora = $scope.horasalida.toString();
        var hdinicio = "05:00:00";
        var hdfin = "11:30:00";
        if (hora > hdinicio && hora < hdfin) {
            $scope.dt.detalle = "Desayuno/s";
        }
        if (hora > '11:31:00' && hora <'16:30:00') {
             $scope.dt.detalle = "Almuerzo/s";
        }
        if (hora >'16:31:00' && hora <'20:30:00') {
             $scope.dt.detalle = "Cena/s";
        }
        if (hora >"20:06:00" && hora <'04:59:00') {
             $scope.dt.detalle = "Extracurricular/s";
        }

    }
    $scope.horarioatencion();

    $("#idbuscar").keyup(function () {
        var bus = $("#idbuscar").val();
        $scope.busca(bus);
    });
    $scope.perventa = [];
    $scope.busca = function (e) {
        $http.post("/Persona/BusPersona", { ci: e }).success(function (result) {
            $scope.perventa = result;
            $scope.horarioatencion();
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
