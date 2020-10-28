
$(document).ready(function () {

    // podaci od interesa
    var host = window.location.host;
    var token = null;
    var headers = {};
    var hoteliEndpoint = "/api/hoteli/";

    var formAction = "Create";

    // okidanje ucitavanja proizvoda
    loadHoteli();

    // posto inicijalno nismo prijavljeni, sakrivamo odjavu
    $("#odjavise").css("display", "none");

    // pripremanje dogadjaja 
    $("body").on("click", "#btnDelete", deleteHoteli);    
    $("body").on("click", "#btnReset", loadHoteli);
    $("body").on("click", "#btnRegistracija", registracijaForma);
    $("body").on("click", "#btnPrijava", prijavaForma);

    function registracijaForma() {
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "block");
        $("#btnRegistracija").css("display", "none");
        $("#btnPrijava").css("display", "none");
    }

    function prijavaForma() {
        $("#prijava").css("display", "block");
        $("#registracija").css("display", "none");
        $("#btnRegistracija").css("display", "none");
        $("#btnPrijava").css("display", "none");
    }

    $("#Odustajanje").click(function (e) {
        e.preventDefault();
        $("#lanac").val('');
        $("#naziv").val('');
        $("#godinaOtvaranja").val('');
        $("#brojSoba").val('');
        $("#brojZaposlenih").val('');
        loadHoteli();
        $("#formHoteliDiv").css("display", "none");
    });

    $("#Odustajanje1").click(function (e) {
        e.preventDefault();
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#btnPrijava").css("display", "block");
        $("#btnRegistracija").css("display", "block");
        $("#prijavaNova").css("display", "none");
        $("#info2").css("display", "none");
    });

    $("#Odustajanje2").click(function (e) {
        e.preventDefault();
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#btnPrijava").css("display", "block");
        $("#btnRegistracija").css("display", "block");

    });

    $("#prijavaNova").click(function () {
        $("#prijava").css("display", "block");
        $("#registracija").css("display", "none");
        $("#info2").css("display", "none");
    });


    // ucitavanje hotela
    function loadHoteli() {
        var requestUrl = 'http://' + host + hoteliEndpoint;
        $.getJSON(requestUrl, setHoteli);
    }

    // metoda za postavljanje hotela u tabelu
    function setHoteli(data, status) {

        var $container = $("#hoteli");
        $container.empty();

        if (status === "success") {
            // ispis naslova
            var div = $("<div></div>");
            var h1 = $("<h1 style ='position: center; margin-left: 30% ; margin-right:30%'>Hoteli</h1>");

            div.append(h1);
            // ispis tabele
            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead ><tr style='background-color:yellow'><td>Naziv</td><td>Godina Otvaranja</td><td>Broj soba</td><td>Broj zaposlenih</td><td>Lanac</td><td>Akcija</td></tr></thead>");
            } else {
                header = $("<thead><tr style='background-color:yellow'><td>Naziv</td><td>Godina Otvaranja</td><td>Broj soba</td><td>Broj zaposlenih</td><td>Lanac</td></tr></thead>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Naziv + "</td><td>" + data[i].GodinaOtvaranja + "</td><td>" + data[i].BrojSoba + "</td><td>" + data[i].BrojZaposlenih + "</td><td>" + data[i].LanacHotel.Naziv + "</td>";

                // prikaz dugmadi za izmenu i brisanje
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";

                // prikaz samo ako je korisnik prijavljen
                if (token) {
                    row += displayData + displayDelete + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                // dodati red
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);

            $container.append(div);
        }
        else {
            div = $("<div></div>");
            h1 = $("<h1>Greška prilikom preuzimanja Hotela!</h1>");
            div.append(h1);
            $container.append(div);
        }
    }
    // registracija korisnika
    $("#registracija").submit(function (e) {
        e.preventDefault();


        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info2").css("display", "block");
            $("#regEmail").val("");
            $("#regLoz").val("");
            $("#regLoz2").val("");


        }).fail(function (data) {
            alert('Greška prilikom registracije! Proverite unos!');
        });
    });

    // prijava korisnika
    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljeni korisnik: " + data.userName);
            token = data.access_token;
            $("#prijava").css("display", "none");
            $("#registracija").css("display", "none");
            $("#formaDodavanja").css("display", "block");
            $("#odjavise").css("display", "block");
            $("#tradicija").css("display", "block");
            $("#formsearch").css("display", "block");
            $("#priEmail").val('');
            $("#priLoz").val('');
            refreshTable();

            //i uvucena forma za selektovanje lanaca prilikom dodavanja istog
            var selectList = $("#lanac");
            selectList.empty();
            $.ajax({
                url: 'http://' + host + '/api/lanci',
                type: "GET",
                headers: headers
            }).done(function (lanci, status) {
                console.log('wwwwwwwwwwwwww', lanci);
                for (i = 0; i < lanci.length; i++) {
                    var displayData = '<option selected value=' + lanci[i].Id + '>' + lanci[i].Naziv + '</option>';

                    selectList.append(displayData);
                }
            })
                .fail(function (lanci, status) {
                    formAction = "Create";
                    alert("Desila se greška!");
                });

        }).fail(function (data) {
            alert('Greška prilikom prijave!');
        });
    });

    // odjava korisnika sa sistema
    $("#odjavise").click(function () {
        token = null;
        headers = {};

        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#odjavise").css("display", "none");
        $("#info").empty();
        $("#formHoteliDiv").css("display", "none");
        $("#formaDodavanja").css("display", "none");
        $("#formsearch").css("display", "none");
        $("#tradicija").css("display", "none");
        $("#btnPrijava").css("display", "block");
        $("#btnRegistracija").css("display", "block");

        refreshTable();
    });

    //Dodavane novog hotela
    $("#hotelNova").submit(function (e) {
        console.log('ggggggggggg');
        // sprecavanje default akcije forme
        e.preventDefault();

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var lanac = $("#lanac").val();
        var naziv = $("#naziv").val();
        var godinaOtvaranja = $("#godinaOtvaranja").val();
        var brojSoba = $("#brojSoba").val();
        var brojZaposlenih = $("#brojZaposlenih").val();

        var httpAction;
        var sendData;
        var url;

        // u zavisnosti od akcije pripremam objekat
        formAction === "Create";
        console.log('usaoooooo u create');
        httpAction = "POST";
        url = 'http://' + host + hoteliEndpoint;
        sendData = {
            "LanacHotelId": lanac,
            "Naziv": naziv,
            "GodinaOtvaranja": godinaOtvaranja,
            "BrojSoba": brojSoba,
            "BrojZaposlenih": brojZaposlenih
        };

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        })
            .done(function (data, status) {
                formAction = "Create";
                refreshTable();
                $("#formaDodavanja").css("display", "none");
                alert("Hotel je uspešno dodat!");

            })
            .fail(function (data, status) {
                alert("Greška prilikom dodavanja!");
            });
    });

    //pretraga
    $("#searchForm").submit(function (e) {
        e.preventDefault();

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var najmanje = $("#najmanje").val();
        var najvise = $("#najvise").val();

        var httpAction;
        var sendData;
        var url;

        httpAction = "POST";
        url = 'http://' + host + "/api/kapacitet/";
        sendData = {
            "Najmanje": najmanje,
            "Najvise": najvise
        };

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        })
            .done(function (data, status) {
                $("#najmanje").val('');
                $("#najvise").val('');
                setHoteli(data, status);
            })
            .fail(function (data, status) {
                alert("Greška prilikom pretrage!");
            });
    });

    $("#ucitavanje").click(function () {
        $("#ucitavanje").css("display", "none");
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        $.ajax({
            type: "GET",
            url: 'http://' + host + "/api/tradicija/",
            headers: headers
        })
            .done(function (data, status) {
                if (data[0] !== null) {
                    $("#tradicijaDiv").append("<br/> <p> <strong> 1. " + data[0].Naziv + " </strong> (osnovan <strong>" + data[0].GodinaOsnivanja + "</strong> godine) </p>");
                }
                if (data[1] !== null) {
                    $("#tradicijaDiv").append("<p> <strong> 2. " + data[1].Naziv + " </strong> (osnovan <strong>" + data[1].GodinaOsnivanja + "</strong> godine) </p>");
                }
            });
    });

    // brisanje hotela
    function deleteHoteli() {
        // izvlacimo {id}
        var deleteID = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev 
        $.ajax({
            url: 'http://' + host + hoteliEndpoint + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greška!");
            });
    }

    // osvezi prikaz tabele
    function refreshTable() {
        // cistim formu
        $("#lanac").val('');
        $("#naziv").val('');
        $("#godinaOtvaranja").val('');
        $("#brojSoba").val('');
        $("#brojZaposlenih").val('');
        loadHoteli();
    }
});



