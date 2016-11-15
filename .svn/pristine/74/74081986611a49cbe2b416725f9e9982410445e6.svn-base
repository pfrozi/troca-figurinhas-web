$(function () {


    $("input[type=submit], button, input[type=button]").button();

    $(".FigurinhaView").dialog({
        autoOpen: false,
        modal: true,
        width: 500,
        height: 300,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".OfertaTrocasDetalheView").dialog({
        autoOpen: false,
        modal: true,
        width: 950,
        height: 380,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".FigurinhaDetalhe").click(
        function (e) {
            $("#" + $(e.target).attr("idDetalhe")).dialog("open");
            event.preventDefault();
        });
    $(".BuscaFigurinhasTroca").click(
        function (e) {
            $("#" + $(e.target).attr("idDetalhe")).dialog("open");
            event.preventDefault();
        });

    $(".zoom").click(

        function () {
            var divZoom = $('<div />');
            $(divZoom).attr('id', 'divZoom');

            $(divZoom).css('display', 'none');
            $(divZoom).css('background-color', 'black');
            $(divZoom).css('text-align', 'center');
            $(divZoom).css('vertical-align', 'middle');
            $(divZoom).append('<img id="imgBig" class="ImagemGrande" src="' + $(this).attr('src') + '" alt="' + $(this).attr('alt') + '"></img>');

            $(divZoom).dialog({

                title: "",
                autoOpen: true,
                resizable: false,
                draggable: false,
                modal: true,
                width: 580,
                height: 580

            });

            
        });

});