(function (global, $, undefined) {
    "use strict";

    //Format the date using momentJS
    $(".creationDate").each(function () {
        var $this = $(this),
            val = $this.text(),
            fromNow = moment(val).fromNow();

        $this.text(fromNow);
    });

    //Change view in index page checkbox
    $(function () {
        if($(".indexCheckAll").attr("checked")){
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }

        if ($(".indexCheckAssign").attr("checked")) {
            $(".indexCheckAll").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }

        if ($(".indexCheckNoSol").attr("checked")) {
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckAll").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }

        if ($(".indexActive").attr("checked")) {
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckAll").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
        }

    });

    $(".indexCheckAll").click(function (event) {
        var selected = this.checked;
        if(selected)
        {
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }
        else
        {
            $(".indexCheckAssign").removeAttr("disabled");
            $(".indexCheckNoSol").removeAttr("disabled");
            $(".indexActive").removeAttr("disabled");
        }
    });

    $(".indexCheckAssign").click(function (event) {
        var selected = this.checked;
        if (selected) {
            $(".indexCheckAll").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }
        else {
            $(".indexCheckAll").removeAttr("disabled");
            $(".indexCheckNoSol").removeAttr("disabled");
            $(".indexActive").removeAttr("disabled");
        }
    });

    $(".indexCheckNoSol").click(function (event) {
        var selected = this.checked;
        if (selected) {
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckAll").attr("disabled", true);
            $(".indexActive").attr("disabled", true);
        }
        else {
            $(".indexCheckAssign").removeAttr("disabled");
            $(".indexCheckAll").removeAttr("disabled");
            $(".indexActive").removeAttr("disabled");
        }
    });

    $(".indexActive").click(function (event) {
        var selected = this.checked;
        if (selected) {
            $(".indexCheckAssign").attr("disabled", true);
            $(".indexCheckAll").attr("disabled", true);
            $(".indexCheckNoSol").attr("disabled", true);
        }
        else {
            $(".indexCheckAssign").removeAttr("disabled");
            $(".indexCheckAll").removeAttr("disabled");
            $(".indexCheckNoSol").removeAttr("disabled");
        }
    });

})(window, jQuery);



