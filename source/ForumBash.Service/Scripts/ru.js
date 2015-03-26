$(function () {
    UpdateIssuesCount();
    UpdateIssues();
    $("#o5").click(OpenTop5);
    $("#navSO").click(function() {
        Clear();
        $("#o5").show();
        UpdateIssues();
        current = "SO";
        nl = "/fb.svc/SOIssues?$orderby=CreationDate%20desc";
    });
    $("#navU").click(function () {
        Clear();
        $("#o5").hide();
        UpdateUsers();
        current = "U";
    });

    $(window).scroll(function () {
        if (current=="SO" && $(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            UpdateIssues();
        }
    });
});

var top5 = [];
var nl = "/fb.svc/SOIssues?$orderby=CreationDate%20desc";
var current = "SO";

function OpenTop5() {
    for (var i = 0; i < top5.length; i++) {
        window.open(top5[i], '_blank');
    }
}

function Clear() {
    $("#board").empty();
    top5 = [];
    nl = null;
}

function UpdateUsers() {
    $.get("/fb.svc/Users", function (response) {
        var text = "";
        var data = response.value;
        for (var i = 0; i < data.length; i++) {
            var sin =
                "<a class=\"tile bg-darkBlue fg-white opacity\" href=\"#\"> \
                   <div class=\"text-itemtitle\">" + data[i].Name + "</div> \
                   <div class=\"brand bg-black opacity\"> \
                     <span class=\"label fg-white\">" + "" + "</span> \
                     <span class=\"badge bg-red\">" + "1" + "</span> \
                   </div> \
                 </a>\n";

            text += sin;
        }

        $("#board").append(text);
    });
}

function UpdateIssuesCount() {
    $("#issuesCount").load("/fb.svc/SOIssues/$count?$filter=Owner%20eq%20null");
}

function UpdateIssues() {
    if (nl == null) {
        return;
    }

    $.get(nl, function (response) {
        var text = "";
        var data = response.value;
        nl = response['@odata.nextLink'];

        for (var i = 0; i < data.length; i++) {
            var owner = data[i].Owner == null ? "None" : data[i].Owner;

            var sin =
                "<a class=\"tile half long bg-darkBlue fg-white opacity\" href=\"" + data[i].URL + "\"> \
                   <div class=\"text-itemtitle\">" + data[i].Title + "</div> \
                   <div class=\"brand bg-black opacity\"> \
                     <span class=\"label fg-white\">" + data[i].CreationDate + "</span> \
                     <span class=\"badge bg-red\">" + data[i].AnswerNumber + "</span> \
                   </div> \
                 </a>\n";
            if (top5.length < 5) {
                top5.push(data[i].URL);
            }

            var color = data[i].Status == "Active" ? "bg-red" : "bg-green";

            sin +=
                "<div onclick=\"UpdateStatus("+data[i].Id+","+"'Assigned'"+")\" class=\"tile half " + color + " fg-white opacity\"> \
                   <div class=\"text-itemtitle\">" + data[i].Status + "</div> \
                   <div class=\"brand bg-black opacity\"> \
                     <span class=\"fg-white\">" + owner + "</span> \
                   </div> \
                 </div>";

            text += sin;
        }

        $("#board").append(text);
    });
}

function UpdateStatus(id, status) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/fb.svc/SOIssues(' + id + ')',
        type: 'PATCH',
        data: JSON.stringify({ Status: status , Owner : "SomeOne1"}),
        success: function (response, textStatus, jqXhr) {
            console.log("Venue Successfully Patched!");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // log the error to the console
            console.log("The following error occured: " + textStatus, errorThrown);
        },
        complete: function () {
            console.log("Venue Patch Ran");
        }
    });
}