// Write your Javascript code.
(function (probilling, $) {

    $("#selectTeam").change(function () {

        var teamId = $(this).find("option:selected").val();
        $.ajax({
            url: "/Home/TeamTable/",
            data: { "teamId": teamId },
            dataType: "html",
            cache: false,
            success: function (result) {
                $("#teamTable").html(result);
            }
        });
    });

    $("#viewPreviousReport").click(function (e) {

        e.preventDefault();
        var teamId = $("#selectTeam").find("option:selected").val();
        var sprintId = $("#selectSprint").find("option:selected").val();

        if (teamId !== "" && sprintId !== "") {
            $.ajax({
                url: "/Home/ViewPreviousReport/",
                data: { "teamId": teamId, "sprintId": sprintId },
                dataType: "html",
                cache: false,
                success: function (result) {
                    $("#previousSprintReport").html(result);
                }
            });
        }
    });

    $("#teamUser").change(function () {
        var teamId = $(this).find("option:selected").val();
        if (teamId !== "") {
            $("#addNewUser").css("display", "block");
            $.ajax({
                url: "/Teams/GetUserForTeam",
                data: { "teamId": parseInt(teamId,10) },
                dataType: "html",
                cache: false,
                success: function (result) {
                    $("#teamUserTable").html(result);
                }
            });
        } else {
            $("#addNewUser").css("display", "none");
        }

    });

}(window.probilling = window.probilling || {}, jQuery));