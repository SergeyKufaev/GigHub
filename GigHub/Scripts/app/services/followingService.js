let FollowingService = function () {
    let createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings", { followeeId: followeeId })
            .done(done)
            .fail(fail);
    };

    let deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();