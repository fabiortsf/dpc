
$(document).ready(function (evente) {

    $("#txtQuantidade").ForceNumericOnly();

    $('#txtPreco').priceFormat({ prefix: '', centsSeparator: ',', thousandsSeparator: '' });

    listarComboTipoProduto();

    $("#btnSalvar").click(function (event) {
        event.preventDefault();
        iniciarValidator();
        if ($('#frmCadastro').valid())
            salvar(this);
    });

});



function listarComboTipoProduto() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/api/produto/ListarTipos",
        data: null,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: montarComboTipoProduto,
        error: function (xhr, err) {
            $('#drpTipo').empty();
            $('#drpTipo').append('<option value="">ERRO</option>');
        }
    });
}
function montarComboTipoProduto(data) {
    $('#drpTipo').empty();
    $('#drpTipo').append('<option value="">-SELECIONE-</option>');

    $.each(data, function (index, value) {
        $('#drpTipo').append('<option value="' + value["Id"] + '" >' + value["Descricao"] + '</option>');
    });
}



function iniciarValidator() {

    $.validator.addMethod(
          "validDate",
          function (value, element) {
              var check = false;
              var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
              if (re.test(value)) {
                  var adata = value.split('/');
                  var gg = parseInt(adata[0], 10);
                  var mm = parseInt(adata[1], 10);
                  var aaaa = parseInt(adata[2], 10);
                  var xdata = new Date(aaaa, mm - 1, gg);
                  if ((xdata.getFullYear() == aaaa) && (xdata.getMonth() == mm - 1) && (xdata.getDate() == gg))
                      check = true;
                  else
                      check = false;
              } else
                  check = false;
              return this.optional(element) || check;
          },
          "Insira uma data válida"
    );

    $.validator.setDefaults({
        showErrors: function (errorMap, errorList) {
            this.defaultShowErrors();
            //alert("." + this.settings.validClass)
            // destroy tooltips on valid elements                              
            $("." + this.settings.validClass).tooltip("destroy");

            // add/update tooltips 
            for (var i = 0; i < errorList.length; i++) {
                var error = errorList[i];

                $("#" + error.element.id)
                    .tooltip("show")
                    .attr("data-original-title", error.message)
            }
        }
    });

    $("#frmCadastro").validate({
        debug: true,
        rules: {
        },
        messages: {
        },
        errorElement: 'label',
        errorClass: 'error',
        validClass: 'valid',
        errorPlacement: function (error, element) {
            //element.parent("div").parent("div").addClass("has-error");
            //if (element.parent('.input-group').length) {
            //    error.insertAfter(element.parent());
            //} else {
            //    error.insertAfter(element);
            //}
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
            $(element).addClass("valid");
            $('body').tooltip('destroy');
        }
    });

}



/* Cadastro */
function salvar(botao) {
    var textoBotao = $(botao).html();

    var ProdutoPreco = {
        CodigoBarras: $("#txtCodigoDeBarras").val(),
        CodTabelaPreco: 'VA', //FIXO
        Preco: $("#txtPreco").val().replace(',', '.'),
        Ativo: 'S'
    }
    var ProdutoSKU = {
        CodigoDeBarras: $("#txtCodigoDeBarras").val(),
        CodigoProduto: $("#txtCodigoDeBarras").val()
    }

    var Estoque = {
        CodigoFilial: '0001',
        CodigoDeBarras: $("#txtCodigoDeBarras").val(),
        Quantidade: $("#txtQuantidade").val()
    }

    var Produto = {
        Codigo: $("#txtCodigoDeBarras").val(),
        Nome: $("#txtDescricao").val(),
        IdTipo: $("#drpTipo option:selected").val(),
        Grades: [ProdutoSKU],
        Precos: [ProdutoPreco],
        Estoque: Estoque
    }

    $(botao).prop('disabled', true);
    $(botao).text("Salvando....");

    $.ajax({
        type: "POST",
        async: true,
        url: "/api/produto/Salvar",
        data: JSON.stringify(Produto),
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert("Produto salvo com sucesso");
        },
        error: function (xhr, err) {
            alert("Erro ao efetuar cadastro");
            $(botao).text("Salvar");
            $(botao).prop('disabled', false);
        },
        beforeSend: function () {

            setTimeout(function () {
                $(botao).prop('disabled', true);
                $(botao).html("<i class=\"fa fa-spinner fa-spin fa-fw\" style=\"margin-top:3px;\" ></i>&nbsp;Processando....");
            }, 1);

        },
        complete: function () {

            setTimeout(function () {
                $(botao).prop('disabled', false);
                $(botao).html(textoBotao);
            }, 1000);
        }
    });
}
