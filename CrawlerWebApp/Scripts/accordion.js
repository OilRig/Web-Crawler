(function ($) {
    $.fn.boardSchemaAccordion = function () {
        $(document).ready(function () {
            $(".list-group-item-heading").click(function () {
                $(this).next("ul").slideToggle(duration = 100)
                    .siblings("ul:visible").slideDown(duration = 100);
            });
        });
    }
})(jQuery);
$(document).boardSchemaAccordion();
