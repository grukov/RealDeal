$('#add-images-btn').click(function () {
    var imgContainer = $('#image-container');

    var counter = imgContainer.children('input[type="file"]').length;

    var inputTypeFile = $('<input />');
    inputTypeFile.attr('type', 'file');
    inputTypeFile.attr('name', 'images');
    inputTypeFile.attr('class', "form-control");


    var span = $('<span />');
    span.attr('class', 'btn btn-default btn-file');

    var br = $('<br />');

    span.append(inputTypeFile);
    imgContainer.prepend(span);
})