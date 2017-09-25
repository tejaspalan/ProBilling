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
            $.ajax({
                url: "/Teams/GetAllAvailableUsers",
                dataType: "html",
                cache: false,
                async:false,
                success: function (result) {
                    $("#userTable").html(result);
                }
            });
        }

    });

    $("body").on("click",".addclass",function () {

        var userId = $(this).closest("tr").attr("id");
        var teamId = $("#teamUser").find("option:selected").val();

	    if (teamId !== "") {
		    $.ajax({
			    url: "/Teams/InsertUserToTeam",
			    data: { "teamId": teamId, "userId": userId },
			    dataType: "html",
			    cache: false,
                success: function (result) {
                    alert("User successfully added to the selected team !")
                    $("#userTable").html(result);
			    }
		    });
	    }
    });

}(window.probilling = window.probilling || {}, jQuery));