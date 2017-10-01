// Write your Javascript code.
(function (probilling, $) {

    $("#selectTeam").on("change",function () {

        var teamId = $(this).find("option:selected").val();
        $.ajax({
            url: "/Teams/GetUsersForTheTeam",
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

            $.ajax({
                url: "/Teams/GetUsersForTheTeam",
                dataType: "html",
                data: { "teamId": teamId },
                cache: false,
                async: false,
                success: function (result) {
                    $("#teamUserTable").html(result);
                }
            });

            $.ajax({
                url: "/Teams/GetAllAvailableUsers",
                dataType: "html",
                cache: false,
                async: false,
                success: function (result) {
                    $("#userTable").html(result);
                }
            });
        }

    });

    $("body").on("click", ".addclass", function () {

        var userId = $(this).closest("tr").attr("id");
        var teamId = $("#teamUser").find("option:selected").val();

        if (teamId !== "") {
            $.ajax({
                url: "/Teams/InsertUserToTeam",
                data: { "teamId": teamId, "userId": userId },
                dataType: "html",
                cache: false,
                success: function (result) {
                    alert("User successfully added to the selected team !");
                    $("#userTable").html(result);
                    $.ajax({
                        url: "/Teams/GetUsersForTheTeam",
                        dataType: "html",
                        data: { "teamId": teamId },
                        cache: false,
                        async: false,
                        success: function (newResult) {
                            $("#teamUserTable").html(newResult);
                        }
                    });
                }
            });
        }
    });

    $("body").on("click", ".removeClass", function () {

        var userId = $(this).closest("tr").attr("id");
        var teamId = $("#teamUser").find("option:selected").val();

        if (teamId !== "") {
            $.ajax({
                url: "/Teams/RemoveUserFromTeam",
                data: { "teamId": teamId, "userId": userId },
                dataType: "html",
                cache: false,
                success: function (result) {
                    alert("User successfully removed from the selected team !");
                    $("#userTable").html(result);
                    $.ajax({
                        url: "/Teams/GetUsersForTheTeam",
                        dataType: "html",
                        data: { "teamId": teamId },
                        cache: false,
                        async: false,
                        success: function (newResult) {
                            $("#teamUserTable").html(newResult);
                        }
                    });
                }
            });
        }
    });

    $("#teamForSprint").change(function () {
        var teamId = $("#teamForSprint").find("option:selected").val();
        if (teamId !== "") {
            $.ajax({
                url: "/Sprints/Index",
                dataType: "html",
                data: { "teamId": teamId },
                cache: false,
                async: false,
                success: function (result) {
                    $("#sprintTable").html(result);
                }
            });
        }
    });

	$("#selectTeamForCurrentSprint").change(function() {
        var teamId = $("#selectTeamForCurrentSprint").find("option:selected").val();
        if (teamId !== "") {
	        $.ajax({
		        url: "/Sprints/LoadSprintDates",
		        dataType: "html",
		        data: { "teamId": teamId },
		        cache: false,
		        async: false,
		        success: function (result) {
		        }
	        });
		}
	});

	$("select").each(function () {

		var length = $(this).find("option").length;
        if (length === 2) {
	        var id = $(this).attr("id");
			$("#" + id +" option:last").attr("selected", "selected");
			$("#"+id).trigger("change");
		}
	});

}(window.probilling = window.probilling || {}, jQuery));