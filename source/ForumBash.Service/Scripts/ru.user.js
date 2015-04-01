function init() {
}

function Refresh() {
    UpdateUsers();
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