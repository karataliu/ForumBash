﻿$(function () {
    UpdateIssuesCount();
    UpdateIssues();
    $("#o5").click(OpenTop5);
    $("#navSO").click(function() {
        Clear();
        $("#o5").show();
        UpdateIssues();
    });
    $("#navU").click(function () {
        Clear();
        $("#o5").hide();
        UpdateUsers();
    });
});

var top5 = [];

function OpenTop5() {
    for (var i = 0; i < top5.length; i++) {
        window.open(top5[i], '_blank');
    }
}

function Clear() {
    $("#board").empty();
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
    $.get("/fb.svc/SOIssues?$orderby=CreationDate%20desc", function (response) {
        var text = "";
        var data = response.value;
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

            //sin +=
            //    "<div class=\"tile half double bg-green fg-white opacity\"> \
            //       <div class=\"text-itemtitle\">" + owner + "</div> \
            //     </div>";

            var color = data[i].Status == "Active" ? "bg-red" : "bg-green";

            sin +=
                "<div class=\"tile half " + color + " fg-white opacity\"> \
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

