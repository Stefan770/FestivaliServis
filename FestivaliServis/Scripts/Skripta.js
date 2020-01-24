$(document).ready(() => {
    const host = window.location.host;
    let token;
    let user;
    let headers = {};

    /*Main*/
    loadFestivali();
    loadSelect();

    $("body").on("click", "#btnDel", deleteItem);

    /*-----------------------------------------------------------*/

    /*Festivali--------------------------------------------------*/
    function loadFestivali() {
        $.ajax({
            url: `http://${host}/api/festivali`,
            type: "GET",
            success: setFestivali,
            beforeSend: () => { $("#loader-4").show(); },
            complete: () => { $("#loader-4").hide(); }
        });
    }

    function setFestivali(data, status) {
        let table = $("<table class='table table-bordered'></table>");
        let h1 = $("<h2 class='m3'>Festivali</h2>");

        let head = $("<thead class='bg-light'><tr><td>Naziv</td><td>Mesto</td><td>Godina</td><td>Cena</td></tr></thead>");
        if (token) { head.children().first().append("<td style='width: 10px'>Akcija</td>"); }
        table.append(head);
        let tbody = $("<tbody></tbody>");

        data.forEach((item, i) => {
            const row = $(`<tr><td>${item.Naziv}</td><td>${item.Mesto.Naziv}</td><td>${item.Godina}</td><td>${item.Cena}</td></tr>`);
            if (token) {
                row.append(`<td><button class="btn btn-default btn-sm" name=${item.Id} id="btnDel">Obrisi</button></td>`);
            }
            tbody.append(row);
        });

        table.append(tbody);
        $("#data").empty().append(h1).append(table);

    }

    $("#editForm").submit(e => {
        e.preventDefault();

        const httpAction = "POST";
        let url = `http://${host}/api/festivali/`;
        let sendData = {
            "Naziv": $("#edtNaziv").val(),
            "Godina": $("#edtGodina").val(),
            "Cena": $("#edtCena").val(),
            "MestoId": $("#edtSelect").children("option:selected").val()
        };

        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            data: sendData,
            headers: headers

        }).done((data, status) => {
            loadFestivali();
            $("#editForm")[0].reset();

        }).fail((data, status) => {
            alert("Greska prilikom dodavanja!");
        });
    });

    $("#filterForm").submit(e => {
        e.preventDefault();

        $.ajax({
            url: `http://${host}/api/festivali/pretraga`,
            type: "POST",
            headers: headers,
            data: {
                "Start": $("#filterMin").val(),
                "Kraj": $("#filterMax").val()
            },
            success: setFestivali
        }).fail(() => { alert("Greska prilikom pretrage!"); });
    });

    function deleteItem() {
        const deleteId = this.name;

        $.ajax({
            url: `http://${host}/api/festivali/${deleteId}`,
            type: "DELETE",
            headers: headers

        }).done((data, status) => {
            loadFestivali();

        }).fail((data, status) => {
            alert("Desila se greska!");
        });
    }
    /*------------------------------------------------------------*/

    /*Mesta-------------------------------------------------------*/
    function loadSelect() {
        const url = `http://${host}/api/mesta/`;
        $.getJSON(url, (data, status) => {
            $("#edtSelect").empty();
            data.forEach(e => {
                $("#edtSelect").append(`<option value="${e.Id}">${e.Naziv}</option>`);
            });
        });
    }
  
    /*------------------------------------------------------------*/

    /*Authentication----------------------------------------------*/

    $("#btnReg").click(() => {

        const sendData = {
            "Email": $("#regUser").val(),
            "Password": $("#regPass").val(),
            "ConfirmPassword": $("#regConfirm").val()
        };

        $.ajax({
            type: "POST",
            url: `http://${host}/api/Account/Register`,
            data: sendData

        }).done((data) => {
            console.log(data);
            $("#regForm")[0].reset();
            $("#logFormDiv").show();
            $("#regFormDiv").hide();

        }).fail(() => {
            alert("Greska prilikom registracije!");
        });
    });

    $("#btnLog").click(() => {

        const sendData = {
            "grant_type": "password",
            "username": $("#user").val(),
            "password": $("#pass").val()
        };

        $.ajax({
            "type": "POST",
            "url": `http://${host}/Token`,
            "data": sendData

        }).done(data => {
            console.log(data);
            token = data.access_token;
            user = data.userName;
            $("#loggedName").empty().append(user);
            $("#logForm")[0].reset();
            $("#filterForm")[0].reset();
            onLogIn();
            loadFestivali();
            headers.Authorization = 'Bearer ' + token;

        }).fail(() => {
            alert("Greska prilikom prijave!");
        });
    });

    $("#btnLogOut").click(() => {
        token = null;
        headers = {};

        onLogOut();
        loadFestivali();
    });

    /*------------------------------------------------------------*/

    $("#btnShowReg").click(() => {
        $("#logFormDiv").hide();
        $("#regFormDiv").show();
    });

    function onLogIn() {
        $("#logFormDiv").hide();
        $("#regFormDiv").hide();
        $("#logOutDiv").show();
        $("#filterFormDiv").show();
        $("#editFormDiv").show();
    }

    function onLogOut() {
        $("#logFormDiv").show();
        $("#regFormDiv").hide();
        $("#logOutDiv").hide();
        $("#filterFormDiv").hide();
        $("#editFormDiv").hide();
    }
});