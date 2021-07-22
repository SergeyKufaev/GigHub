let GigDetailsController = function (followingService) {
    let followButton;

    let init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    let toggleFollowing = function (e) {
        followButton = $(e.target);

        let followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);
    };

    let done = function () {
        let text = (followButton.text().trim() == "Follow") ? "Following" : "Follow";

        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    let fail = function () {
        alert("Something failed!");
    };

    return {
        init: init
    }
}(FollowingService);