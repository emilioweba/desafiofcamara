var interval;

$().ready(function () {
    ko.applyBindings(new HomeIndexViewModel());
});

function HomeIndexViewModel() {
    var self = this;

    self.productList = ko.observableArray(([
        { ProductId: "Label", ProductName: "Label", ProductDescricao: "Label", ProductPrice: "Label" },
        { ProductId: "Label", ProductName: "Label", ProductDescricao: "Label", ProductPrice: "Label" },
        { ProductId: "Label", ProductName: "Label", ProductDescricao: "Label", ProductPrice: "Label" },
        { ProductId: "Label", ProductName: "Label", ProductDescricao: "Label", ProductPrice: "Label" },
    ]));

    self.ProductId = ko.observable();
    self.ProductName = ko.observable();
    self.ProductDescricao = ko.observable();
    self.ProductPrice = ko.observable();

    self.getProductListAjax = function () {
        $('#message').text('');

        var paramData = new MyToken($('#tokenValue').text(),
            $("#tokenExpirationDate").val()); // passando token e sua data

        $.ajax({
            type: "POST",
            url: "http://www.emilioweba.com/api/product/", // Web API
            data: JSON.stringify(paramData),
            contentType: 'application/json'
        }).success(function (data) {
            self.productList.removeAll(); // zera o array

            if (data != null) {
                $('#table').fadeOut(100).fadeIn(100);

                $.each(data, function (i, item) {
                    self.productList.push(new productViewModel(item)); // popula observable array
                });
            }
            else { // caso nao retorne nada, popular com valores default
                for (var i = 0; i < 4; i++) {
                    self.productList.push(new productViewModel({
                        ProductId: "Label",
                        ProductName: "Label",
                        ProductDescricao: "Label",
                        ProductPrice: "Label"
                    }));

                    $('#message').text('Token expirado, favor atualizar');
                }
            }
        });
    }

    self.getProductListMVC = function () {
        self.productList.removeAll(); // zera o array
        $('#message').text('');

        var paramData = new MyToken($('#tokenValue').text(),
            $("#tokenExpirationDate").val()); // passando token e sua data

        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'http://www.emilioweba.com/api/product/'); // Web API
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.send(JSON.stringify(paramData));

        xhr.onreadystatechange = function (data) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                self.productList.removeAll(); // zera o array

                if (data.srcElement.response != "null") {
                    $('#table').fadeOut(100).fadeIn(100);

                    $.each(JSON.parse(data.srcElement.response), function (i, item) {
                        self.productList.push(new productViewModel(item)); // popula observable array
                    });
                }
                else { // caso nao retorne nada, popular com valores default
                    for (var i = 0; i < 4; i++) {
                        self.productList.push(new productViewModel({
                            ProductId: "Label",
                            ProductName: "Label",
                            ProductDescricao: "Label",
                            ProductPrice: "Label"
                        }));
                    }
                    $('#message').text('Token expirado, favor atualizar');
                }
            }
        }
    }
}

function productViewModel(data) {
    var self = this;

    self.ProductId = data.ProductId;
    self.ProductName = data.ProductName;
    self.ProductDescricao = data.ProductDescricao;
    self.ProductPrice = data.ProductPrice;
}

$('#gerarToken').click(function () {
    clearInterval(interval);
    $('#message').text('');

    $.get("http://www.emilioweba.com/Home/GenerateToken", function (data) {
        $('#tokenValue').text(data.Token); // atribui o valor do token na View
        $("#tokenExpirationDate").val(data.TimeCreated); // atribui o valor da data de expiracao na View

        $('#time').text('59');
        $('#unit').text('segundos');

        interval = setInterval(function () { // cronometro diminuindo ate 1 segundo
            $('#time').text(parseInt($('#time').text()) - 1);
            if ($('#time').text() == 0) { // quando chegar a zero, zera as informacoes do cronometro
                $('#tokenValue').text("XXXXX-XXXXX-XXXXX-XXXX");
                $('#time').text('1');
                $('#unit').text('minuto');
                clearInterval(interval);
            }
        }, 1000);
    });
});

function MyToken(token, time) {
    var self = this;

    self.Token = token;
    self.TimeCreated = time;
}