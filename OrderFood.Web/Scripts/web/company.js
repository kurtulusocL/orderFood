
$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyCreate/SelectList/',
        dataType: 'JSON'
    });
    $.extend({
        GetCountry: function () {
            $.ajax({
                data: { "tip": "GetCountry" },
                success: function (sonuc) {
                    if (sonuc.ok) {
                        $.each(sonuc.text, function (CountryCity, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#country").append(optionhtml);
                        });

                    } else {
                        $.each(sonuc.text, function (CountryCity, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#country").append(optionhtml);
                        });
                    }
                }
            });
        },
        GetCity: function (countryId) {
            $.ajax({
                data: { "countryId": countryId, "tip": "GetCity" },
                success: function (sonuc) {
                    $("#city option").remove();
                    if (sonuc.ok) {
                        $("#city").prop("disabled", false);
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#city").append(optionhtml);
                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#city").append(optionhtml);
                        });
                    }
                }
            });
        }
    });
    $.GetCountry();
    $("#country").on("change", function () {
        var countryId = $(this).val();
        $.GetCity(countryId);
    });
});