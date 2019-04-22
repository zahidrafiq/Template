MyWebApp.namespace("UI.UserWall");

MyWebApp.UI.UserWall = (function () {
    var basePath = window.MyWebAppBasePath;

    function displayUserInfo(UserInfo) {
        debugger;
        
        $("#UserName").text(UserInfo.data.StudentInfo.Name);
        $("#UserRegistrationNumber").text(UserInfo.data.StudentInfo.RegistrationNumber);
        $("#UserEmail").text(UserInfo.data.StudentInfo.Email);
        $("#UserImage").attr("src", "../images/gallery/" + UserInfo.data.StudentInfo.ProfilePicName);
            debugger;
            getAndDisplayUserProjects(UserInfo.data.StudentInfo.UserId);
       
    }
function getUserInfo(UserId) {
    var userId = UserId;
    debugger;
    var url = "UserWall/getUserInfo";
    MyWebApp.Globals.MakeAjaxCall("GET", url, {"UserId":UserId}, function (result) {
        
        if (result.success === true) {
            var UserInfo = result;
            debugger;
            displayUserInfo(UserInfo);
        }
       
    }, function (xhr, ajaxoptions, thrownerror) {
        alert("---Some Error has occuerd---");
        //MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while getting student info: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
    });

}
function getAndDisplayUserProjects(userId)
{
    debugger;
    var url = "UserWall/GetProjectsByUserId?userId=" + userId;
    MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
        var UserProjectsList = result.data;
        debugger;
        displayUserProjects(UserProjectsList);
    }, function (xhr, ajaxoptions, thrownerror) {
        alert("---Some Error has occuerd---");
        //MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while getting student info: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
    });

}
function displayUserProjects(ProjectList) {
    debugger;
    $("#project-div").html("");
   
    if ($.isEmptyObject(ProjectList.ProjectList)) {
        $("#No_Projects_available").text("Nothing to show!");
        return;
    }

    try {
        debugger;
        var source = $("#ProjectTemplate").html();
        var template = Handlebars.compile(source);
        var html = template(ProjectList);
    } catch (e) {
    }

    $("#project-div").append(html);

    //BindGridEvents();
}
function BindEvents()
{    
    $(document).on('click', '#submitEditRequest', function () {
        debugger;
        var userName = $("#TBUserName").val();
        //var userRegNo = $("#TBUserRegNo").val();
        var userEmail = $("#TBUserEmail").val();
        if ($.trim(userName) == "" || $.trim(userEmail) == "") {
            alert("All the fields are mandatory");
            return false;
        }

        var userId = $("#editUserProfile").val();
        $("#TBUserName").remove();
        $("#TBUserRegNo").remove();
        $("#TBUserEmail").remove();
        $("#submitEditRequest").remove();
        var dataToSend = {
            Name: userName,
            Email: userEmail,
            UserId: userId
        };
        var dts = JSON.stringify(dataToSend);
        var url = "UserWall/updateUserProfile";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dts, function (result) {
            debugger;
            $("#UserName").text(userName);
            //SUserRegistrationNumber.text(userRegNo);
            
            $("#UserName").show();
            $("#UserEmail").text(userEmail).show();
            $("#UserRegistrationNumber").show();
            $("#editUserProfile").show();

            //MyWebApp.UI.showRoasterMessage(result.msg, Enums.MessageType.Success, 2000);
        }, function () {
            alert("---Some Error has occuerd---");
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while updating user profile: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });
    });
}// End of BindEvents

    function UpdateUserProfie() {
        var SuserName = $("#UserName");                                  //Span where user name is displayed
        //var SUserRegistrationNumber = $("#UserRegistrationNumber");     //Span where user registration Number is displayed
        var SUserEmail = $("#UserEmail");
        var userName = SuserName.text();                               //string user name 
        var userEmail = SUserEmail.text();
        SuserName.hide();
        SUserEmail.hide();
        var $textBoxName = $("<input>").attr("type", "text").attr("id", "TBUserName");
        $textBoxName.val($.trim(userName));
        $("._DeditUserName").append($textBoxName);

        // Hide Edit Profile Button and Display Update Button
        var $textBoxEmail = $("<input>").attr("type", "text").attr("id", "TBUserEmail");
        $textBoxEmail.val($.trim(userEmail));
        $("._DeditUserEmail").append($textBoxEmail);
        var BtneditUserProfile = $("#editUserProfile");
        BtneditUserProfile.hide();
        var button = $("<button>").attr("id", "submitEditRequest").text("Update");
        $(".editButton").append(button);
    } //End of UpdateUserProfile

    function UpdateProfilePic() {
        var webapiurlbase = basePath + "aapi/";
        var urltocall = webapiurlbase + "UserWall/updateUserProfilePic";
        var data = new FormData();
        var files = $("#uploadProfilePic").get(0).files;
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
        }

        //append other form data.
        data.append("Username", "abds");
        // Make Ajax request with the contentType = false, and procesDate = false

        var ajaxRequest = $.ajax({
            type: "POST",
            url: urltocall,
            contentType: false,
            processData: false,
            data: data,
            success: function uploadedSuccess(result) {
                $("#UserImage").attr("src", "../images/gallery/" + result.data);
            }
        });

    }// End of UpdateProfilePic()

return {

    readyMain: function (UserId) {
        
        debugger;
        
        getUserInfo(UserId);
        BindEvents();
    },
    editUserProfile: function () {
        UpdateUserProfie();
    },
    editProfilePic : function()
    {
        UpdateProfilePic();
    }    
};
}());//End of Outermost immediately invoked function.
