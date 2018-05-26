$(document).ready(function () {
    umgDatepicker();
    $("#busqueda").on('input', function () {
        var valor = $("#busqueda").val();
        var url = $("#busqueda").attr("url");
        var url_perfil = $("#busqueda").attr("url_perfil");
        $.get(url, { valor: valor }, function (response) {
            $("#resultado").html(response);
        }, 'html');
    })
});
function umgDatepicker(){
    $(".b-datepicker").datepicker();
}