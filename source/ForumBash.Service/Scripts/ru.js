/***********************
    ForumBash Display
***********************/

// Entrance func
$(function () {
    init();
    Refresh();
});


function UpdateIssuesCount() {
    $("#issuesCount").load("/fb.svc/SOIssues/$count?$filter=Owner%20eq%20null");
}
