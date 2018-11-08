var app = angular.module('AppVenta', []);
app.controller('CtrlVenta', function ($scope, $http, $window) {


    $scope.hora = new Date().getHours();
    $scope.minutos = new Date().getMinutes();
    $scope.segundo = new Date().getSeconds();
    $scope.date = new Date('HH:mm:ss');

    $scope.horasalida = $scope.hora+":"+$scope.minutos+":"+$scope.segundo;
    $scope.dt = [];
    $scope.horarioatencion = function () {
        var hora = $scope.horasalida.toString();
        
        if ((hora <= "00:00:00") && (hora >= '04:59:59')) {
          return  $scope.dt.detalle = "Extracurricular/s";
        }
        if ((hora >= '05:00:00') && (hora <= '11:30:59')) {
            return $scope.dt.detalle = "Desayuno/s";
        }
        if ((hora >= '11:31:00') && (hora <='17:30:59')) {
            return $scope.dt.detalle = "Almuerzo/s";
        }
        if ((hora >='17:31:00' )&& (hora <='23:59:59')) {
            return $scope.dt.detalle = "Cena/s";
        }
    }
    $scope.horarioatencion();

    $("#idbuscar").keyup(function () {
        var bus = $("#idbuscar").val();
        if (bus.length == 10) {
            $scope.busca(bus);

        }});
    $scope.perventa = [];
    $scope.busca = function (e) {
        
        $http.post("/Persona/BusPersona", { ci: e }).success(function (result) {
            $scope.perventa = result;
            $scope.horarioatencion();
            //$("#btnmodalvende").trigger('click');
            $("#Vende").modal('show');
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
                toastr.error("Datos vacios, Consulte para que los datos se llenen", "VENTA");
            }
            if (result == 2) {
                toastr.warning("Se regstró exitosamente ", "VENTA POR COBRAR");
                $("#btnmodalvendehide").trigger('click');
                $scope.perventa = [];
                //$scope.buscarp.CiRuc
                $scope.buscarp = [];
            }
            if (result == 1) {
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
