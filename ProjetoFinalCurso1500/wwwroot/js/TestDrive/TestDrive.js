$(document).ready(function () {
    getCars();
    getSalesmen();
}
)

function getCars() {
    $.ajax({
        type: "GET",
        url: "/Cars/GetCarsForConcessionaire",

        data: {
            idConcessionaire: $("#Concessionairies").val(),
        },
        success: function (result) {
            changeSelectOptions('Cars', result, 'Selecione um carro...');

        },
        error: function () {
            console.log("failure on getting cars");
        }
    })
}

function getSalesmen() {
    $.ajax({
        type: "GET",
        url: "/Salesmen/GetSalesmenForConcessionaire",

        data: {
            idConcessionaire: $("#Concessionairies").val(),
        },
        success: function (result) {
     
            changeSelectOptions('Salesmen', result, 'Selecione um vendedor...');


        },
        error: function () {
            console.log("failure on getting salesmen");
        }
    })
}

function changeSelectOptions(selectId, result, selectPlaceHolder) {
    let destinationStationSelect = $(`#${selectId}`);
    const oldValue = destinationStationSelect.val();

    destinationStationSelect.empty();
    let optionTemplate = $('<option></option>');
    let optionDefault = optionTemplate.clone();
    optionDefault.append(selectPlaceHolder);
    optionDefault.attr('value', "");
    destinationStationSelect.append(optionDefault);

    $.each(result, (index, element) => {
        let option = optionTemplate.clone();
        option.append(element.text);
        option.attr('value', element.value);
        destinationStationSelect.append(option);
    });
    destinationStationSelect.val(oldValue);
}