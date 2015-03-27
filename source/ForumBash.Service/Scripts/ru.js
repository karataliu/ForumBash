/***********************
    ForumBash Display
***********************/

// Constans
var IssueUriBase = "/fb.svc/SOIssues";

var TopLinks = [];
var CurrentView = "SO"; // SO USER STAT
var StatusFilter = "All"; // All, Active, Assigned, Resolved,Closed, Irrelevant
var IssuesQueryUri = null;

function UpdateIssueQueryUri() {
    IssuesQueryUri = IssueUriBase + "?$orderby=CreationDate%20desc";
    if (StatusFilter != "All") {
        IssuesQueryUri += "&$filter=Status%20eq%20'" + StatusFilter + "'";
    }
}

// Entrance func
$(function () {
    Refresh();
    $("#o5").click(OpenTopLinks);
    $("#navSO").click(function () {
        CurrentView = "SO"; Refresh();
    });
    $("#navUSER").click(function () {
        CurrentView = "USER"; Refresh();
    });

    $("#refresh").click(Refresh);

    $("#status-All").click(function () { StatusFilter = "All"; Refresh(); });
    $("#status-Active").click(function () { StatusFilter = "Active"; Refresh(); });
    $("#status-Assigned").click(function () { StatusFilter = "Assigned"; Refresh(); });
    $("#status-Resolved").click(function () { StatusFilter = "Resolved"; Refresh(); });
    $("#status-Closed").click(function () { StatusFilter = "Closed"; Refresh(); });
    $("#status-Irrelevant").click(function () { StatusFilter = "Irrelevant"; Refresh(); });

    $(window).scroll(function () {
        if (CurrentView == "SO" && $(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            UpdateIssues();
        }
    });
});

function Refresh() {
    switch (CurrentView) {
        case "SO":
            Clear();
            $("#o5").show();
            $(".statusfilter").show();
            UpdateIssueQueryUri();
            UpdateIssuesCount();
            UpdateIssues();
            UpdateStatusFilter();
            break;
        case "USER":
            Clear();
            $("#o5").hide();
            $(".statusfilter").hide();
            UpdateUsers();
            break;
        default:
    }
}

function UpdateStatusFilter() {
    $("#statusfilter div").removeClass("selected");
    $("#status-" + StatusFilter).addClass("selected");
}

function OpenTopLinks() {
    for (var i = 0; i < TopLinks.length; i++) {
        window.open(TopLinks[i], '_blank');
    }
}

function Clear() {
    $("#board").empty();
    TopLinks = [];
    IssuesQueryUri = null;
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
                     <span class=\"badge bg-red\">" + data[i].IssueCount + "</span> \
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
    if (IssuesQueryUri == null) {
        return;
    }

    $.get(IssuesQueryUri, function (response) {
        var text = "";
        var data = response.value;
        IssuesQueryUri = response['@odata.nextLink'];

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
            if (TopLinks.length < 5) {
                TopLinks.push(data[i].URL);
            }

            sin +=
                "<div onclick=\"ShowMenu(" + data[i].Id + ")\" class=\"tile half status-" +
                    data[i].Status + " fg-white opacity\"> \
                   <div class=\"text-itemtitle\">" + data[i].Status + "</div> \
                   <div class=\"brand bg-black opacity\"> \
                     <span class=\"fg-white\">" + owner + "</span> \
                   </div> \
                 </div>";

            sin += "<div id=\"_qo" + data[i].Id + "\" class=\"statuspanel fg-white\"></div>";

            text += sin;
        }

        $("#board").append(text);
    });
}

function UpdateStatus(id, status) {
    var owner = $("#owner").val();

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/fb.svc/SOIssues(' + id + ')',
        type: 'PATCH',
        data: JSON.stringify({ Status: status, Owner: owner }),
        success: function (response, textStatus, jqXhr) {
            console.log("Venue Successfully Patched!");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // log the error to the console
            console.log("The following error occured: " + textStatus, errorThrown);
        },
        complete: function () {
            console.log("Venue Patch Ran");
            Refresh();
        }
    });
}

function ShowMenu(id) {
    $("#_qo" + id).append(" \
                    <div onclick=UpdateStatus("+ id + ",\"Active\") id=\"status-Active\" class=\"tile half opacity status-Active\">Active</div> \
                    <div onclick=UpdateStatus(" + id + ",\"Assigned\") id=\"status-Assigned\" class=\"tile half opacity status-Assigned\">Assigned</div> \
                    <div onclick=UpdateStatus(" + id + ",\"Resolved\") id=\"status-Resolved\" class=\"tile half opacity status-Resolved\">Resolved</div> \
                    <div onclick=UpdateStatus(" + id + ",\"Closed\") id=\"status-Closed\" class=\"tile half opacity status-Closed\">Closed</div> \
                    <div onclick=UpdateStatus(" + id + ",\"Irrelevant\") id=\"status-Irrelevant\" class=\"tile half opacity status-Irrelevant\">Irrelevant</div> \
                    <input id=\"owner\" type=\"text\" name=\"fname\">");
}