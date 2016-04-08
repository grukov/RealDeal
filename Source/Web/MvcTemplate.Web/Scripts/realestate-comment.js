$('#add-comment-form').hide();
var isHidden = true;

$('#add-comment-btn').click(function () {
    if (isHidden) {
        $('#add-comment-form').show();
        isHidden = !isHidden
    }
    else {
        $('#add-comment-form').hide();
        isHidden = !isHidden
    }
})