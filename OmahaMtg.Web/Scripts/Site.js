$(document).ready(function () {
    $("#EventRSVP").click(function () {
        var eventId = $(this).data("event-id");
        var isUserCurrentlyRsvpd = $(this).data("currently-rsvpd");

        var linkObject = $(this);

        var url = "/Post/RsvpForEvent";

        var eventData = {
            userIsGoing: !isUserCurrentlyRsvpd,
            eventId: eventId
        };

        $.ajax({
            url: url,
            async: true,
            dataType: "json",
            type: "POST",
            data: JSON.stringify(eventData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log(data);
                var linkText;
                if(data.IsUserGoing)
                {
                    linkText = data.TotalUsersAttending + " Members Going. Click to Remove RSVP"
                }
                else {
                    linkText = data.TotalUsersAttending + " Members Going. Click to RSVP"
                }
                linkObject.text(linkText);
                linkObject.data("currently-rsvpd", data.IsUserGoing);
            },
            error: function (data) {
                alert("error " + data);
            },
            complete: function () {
               // alert("complete");
            }
        });
    });

    $("#saveProfile").click(function() {
        
        var url = "/Profile/UpdateProfile";

        var eventData = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            websiteUrl : $("#webUrl").val(),
            twitterUser : $("#twitterUser").val(),
            gitHubUser: $("#githubUser").val(),
            emailAddress: $("#emailAddress").val()
        };

        console.log(eventData);

        $.ajax({
            url: url,
            async: true,
            dataType: "json",
            type: "POST",
            data: JSON.stringify(eventData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log(data);

                $("#profileUpdateMessage").fadeIn("fast", function() {
                    $(this).delay(1000).fadeOut("slow");
                });
            },
            error: function (data) {
                alert("error " + data);
            }
        });

        }
    );

    $("#savePassword").click(function () {

        var url = "/Profile/UpdatePassword";

        var eventData = {
            oldPassword: $("#oldPassword").val(),
            newPassword: $("#newPassword").val(),
            confirmNewPassword: $("#confirmPassword").val(),

        };

        console.log(eventData);

        $.ajax({
            url: url,
            async: true,
            dataType: "json",
            type: "POST",
            data: JSON.stringify(eventData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log(data);

                if (data.Result) {
                    $("#passwordUpdateMessage").removeClass("bg-red");
                    $("#passwordUpdateMessage").addClass("bg-green");
                    $("#passwordUpdateMessage").children().first().html("Password Updated");
                } else {
                    $("#passwordUpdateMessage").removeClass("bg-green");
                    $("#passwordUpdateMessage").addClass("bg-red");
                    $("#passwordUpdateMessage").children().first().html(data.ErrorMessage);
                }

                

                $("#passwordUpdateMessage").fadeIn("fast", function () {
                    $(this).delay(1000).fadeOut("slow");
                });
            },
            error: function (data) {
                alert("error " + data);
            }
        });

    }
    );
});