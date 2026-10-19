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
            window.location.href = "index.html";
            localStorage.setItem("token",data.accessToken);
        }else{
            alert(data.message);
        }
    })
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
               html += "<td>" + item.productCode + "</td>";
               html += "<td>" + item.price + "</td>";
               html += "<td>" + item.stock + "</td>";
               html += "<td><a href='product-detail.html'>Detail</a></td>";
               html += "</tr>";
            });
            $("#listproduct").html(html);
        })    

    }else{
        window.location.href = "login.html";
    }
}
