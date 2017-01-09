
var devBlog = function () {
    this.init = function () {
        $('#tags').tooltip();
        this.bindEvents();
    };

    this.bindEvents = function () {
        $('.delete-icon').on('click', function () {
            var postId = $(this).data('post-id');

            $('#postId').val(postId);
            $('#confirmation-dialog').modal({ 'backdrop': 'static' });
        });

        $('button.delete-confirmed').on('click', function () {
            window.location = '/Blog/Delete/?postId=' + $('#postId').val();
        });
    };
};

$(document).ready(function () {
    new devBlog().init();
});
