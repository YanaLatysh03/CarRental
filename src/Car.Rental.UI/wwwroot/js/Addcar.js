$('.custom-file-input').on('change', function () {
    var fileName = document.getElementById("exampleInputFile").files[0].name;
    $(this).next('.form-control-file').addClass("selected").html(fileName);
})