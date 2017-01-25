(function ($) {
    $.portal = $.portal || {};
    $.portal.helpers = $.portal.helpers || {};
    $.portal.helpers.posts = {
        loadPost: function (actionUrl) {
            $("a#loadPost").click(function () {
                var _userId = $(this).data("userid");
                var _postId = $(this).data("postid");
                $.ajax({
                    type: "GET",
                    url: actionUrl,
                    data: { userId: _userId, postId: _postId },
                    dataType: "json",
                    error: function (xhr) {
                        alert(xhr.statusCode);
                    },
                    success: function (data) {
                        $(".modal-title").text("Full post by user " + "'" + data["Author"] + "'");
                        $(".modal-body").text(data["Content"]);
                    }
                });
            });
        }
    }
})(jQuery);