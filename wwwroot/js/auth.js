function login() {
    var requestLogin = {
        username: $("#username").val(),
        password: $("#password").val(),
    };

    var request = $.ajax({
        url: '/api/auth/login',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(requestLogin)
    });

    request.done(function(data){
        if ( data.accessToken != "" ){
            window.location.href = "/";
            localStorage.setItem("token",data.accessToken);
        }else{
            alert(data.message);
        }
    })

    request.fail(function(data){
       alert("Invalid Login");
    });
}

function getProduct(){
    if (localStorage.getItem("token") !== null) {
        var request = $.ajax({
            url: '/api/product/all',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: {
                page : 1
            },
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
        });

        request.done(function(data){
            var html = "";
            $.each(data.data, function(i, item) {
               html += "<tr>";
               html += "<td>" + item.productCode + "</td>";
               html += "<td>" + item.name + "</td>";
               html += "<td>" + item.kategori.name + "</td>";
               html += "<td>" + item.price + "</td>";
               html += "<td>" + item.stock + "</td>";
               html += "<td><a href='product/" + item.productCode+ "'>Detail</a></td>";
               html += "</tr>";
            });
            $("#listproduct").html(html);
        });

        request.fail(function(){
           window.location.href = "/login";
        });

    }else{
        window.location.href = "login";
    }
}

function getKategori(){
    if (localStorage.getItem("token") !== null) {
        var request = $.ajax({
            url: '/api/product/kategori',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: {
                page : 1
            },
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
        });

        request.done(function(data){
            var html = "";
            $.each(data.data, function(i, item) {
               html += "<tr>";
               html += "<td>" + item.name + "</td>";
               html += "</tr>";
            });
            $("#listkategori").html(html);
        });

        request.fail(function(){
           window.location.href = "/login";
        });

    }else{
        window.location.href = "/login";
    }
}

function getProductDetail(product_code){
    if (localStorage.getItem("token") !== null) {
        var request = $.ajax({
            url: '/api/product/'+ product_code,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: {
                page : 1
            },
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
        });

        request.done(function(response){
            $("#product-title").text(response.data.name);
            $("#product-title-2").text(response.data.name);
            $("#product-code").text(response.data.productCode);
            $("#product-code-2").text(response.data.productCode);
            $("#product-code-3").text(response.data.productCode);
            $("#product-categori").text(response.data.kategori.name);
            $("#current-stock").text(response.data.stock)
            $("#product_id").val(response.data.id);

            var htmlStock = "";
            $.each(response.data.productStocks,function(i, item){
                htmlStock += "<tr>";
                htmlStock += "<td>" + item.createdAt + "</td>";
                htmlStock += "<td>" + item.stock + "</td>";
                htmlStock += "</tr>";
            });
            $("#stock").html(htmlStock);
        });

        request.fail(function(){
           //window.location.href = "/login";
        });

    }else{
        window.location.href = "/login";
    }
}

function submitStock(){
    if ( $("#latest-stock").val() <= 0 ){
        alert("Tidak bisa dibawah 0")
    }else{
        var requestStock = {
            stock : $("#latest-stock").val(),
            product_id : $("#product_id").val()
        };

        var request = $.ajax({
            url: '/api/product/stock',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(requestStock),
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
        });

        request.done(function(data){
            alert("Berhasil");
            window.location.reload();
        });
    }
}
