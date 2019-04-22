MyWebApp.namespace("UI.Project");

MyWebApp.UI.Project = (function () {
    "use strict";

    var basePath = window.MyWebAppBasePath;
    var _isInitialized = false;
    var projectsData;
    function initializePage()
    {
        if (_isInitialized == false)
            _isInitialized = true;
        getAllProjects();
    }

    //============================================================================//
    function getAllProjects() {
        debugger;
        var url = "Project/GetAllProjects";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                debugger;
                displayAllProjects(result.data);
                projectsData = result.data;
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Projects: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }  // End of getAllprojects()

    //============================================================================//
    function displayAllProjects(ProjectList) {
        debugger;
        $("#project-div").html("");

        if (!ProjectList)
            return;

        try {
            debugger;
            var source = $("#ProjectTemplate").html();
            var template = Handlebars.compile(source);
            
            var html = template(ProjectList);
        } catch (e) {
            debugger;
        }

        $("#project-div").append(html);
        //BindGridEvents();
    }// end of displayAllprojects()

    //============================================================================//
    function BindGridEvents() {
       
        //$("._DLoadComments").click(function (e) {
        //    e.preventDefault();
        //    var ID = $(this).attr("id");
        //    getComments(ID);
        //});
    }
    
    //============================================================================//
    function getComments(projId) {
        debugger;
        var cmtAlreadyDisplayed = ($("._DCommentBox" + projId).children().length);
        if(cmtAlreadyDisplayed>0){}
        var cmtCountToLoad = cmtAlreadyDisplayed + 5;
        
        var url = "Project/GetCommentByProjectId";
        MyWebApp.Globals.MakeAjaxCall("GET", url, { id: projId, count: cmtCountToLoad }, function (result) {
            if (result.success == true) {
                debugger;
                console.log(result.data);
                var projectComments = result.data.Comments;
                if (projectComments.length <= cmtCountToLoad)
                {

                }
                showComments(projectComments, projId);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Comments for Projects: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }// End of getComments()

    //============================================================================//
    function showComments(projectComments, projId) {
        debugger;
        $("._DCommentBox" + projId).empty();
        for (var i = 0; i < projectComments.length ; i++) {
            var time = projectComments[i].CreatedOn;
            var b = time.slice(0, 25).replace('T', ':');
            var $cmtDiv = $("#_DCommentBoxStruct").clone().attr('hidden', false).attr('id', projectComments[i].CommentId);
            $cmtDiv.find("._DName").text(projectComments[i].UserName);
            $cmtDiv.find("._DName").closest("a").attr('href', basePath + "/UserWall/UserWall?UserId=" + projectComments[i].UserId);
            $cmtDiv.find("._DUsrImage").attr('src', basePath + "/images/gallery/" + projectComments[i].ProfilePicName);
            $cmtDiv.find("._Ddate").append('<b data-utc-time=' + b + '></b>');
            $cmtDiv.find("._DCommentText").text(projectComments[i].CommentText);
            $("._DCommentBox" + projId).append($cmtDiv);
        }
     }// End of showComments()

    //============================================================================//
    function saveComment($btnComment) {
        debugger;
        var projId = $btnComment.closest("._DContainer").find("#ProjectId").val();
        var $cmtBox = $btnComment.closest("._DCommentDiv").find("._DUserCommentBox");
        var cmtText = $cmtBox.val();
        if (cmtText.trim() == "") 
        {
           alert("Please enter comment text");
            return;
        }
        $cmtBox.val("");
        var url = "Project/SaveComment";
        var obj = {
                "ProjectId": projId,
                "CommentText": cmtText
        };
        var dataTosend = JSON.stringify(obj);
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataTosend, function (result) {
            if(result.success==true)
            {
                debugger;
                var CommentTime = result.data.CreatedOn;
                var $cmtDiv = $("#_DCommentBoxStruct").clone().attr('hidden', false).attr('id', result.data.CommentId);
                $cmtDiv.find("._DName").text(result.data.UserName);
                $cmtDiv.find("._DName").closest("a").attr('href', basePath + "/UserWall/UserWall?UserId=" + result.data.UserId);
                $cmtDiv.find("._DUsrImage").attr('src', basePath + "/images/gallery/" + result.data.PicName);
                $cmtDiv.find("._Ddate").append('<b>few second ago</b>');
                $cmtDiv.find("._DCommentText").text(cmtText);
                $("._DCommentBox" + projId).prepend($cmtDiv);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred in saving Comment: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        },false);
    }// end of saveComments()

    //============================================================================//
    //          This function is to update vote status in database                //
    //============================================================================//
    function updateVoteInDB(btnClicked)
    {
        var $this = $(btnClicked);
        var Upvote;
        var Downvote;
        var name = $this.attr("name");
        // alert(name);
        if ($this.attr("name") == "DownVote") {
            Upvote = false;
            Downvote = true;
        }
        if ($this.attr("name") == "UpVote") {
            Upvote = true;
            Downvote = false;
        }
        var projectId = parseInt($this.closest("._DContainer").find("#ProjectId").val());
        var resultCode;
        var url = "Project/VoteProject";
        debugger;
        MyWebApp.Globals.MakeAjaxCall("GET", url, {
            "ProjectId": projectId, "UpVote": Upvote, "DownVote": Downvote
        },
          function (result) {
              if (result.success == true) {
                  resultCode = result.data.ResultCode;
                  updateVotes(resultCode,$this);
              }
          }, function (xhr, ajaxOptions, thrownError) {
              MyWebApp.UI.showRoasterMessage('A problem has occurred while voting: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
          }, false);
        return false;
    }

    //============================================================================//
    //          This function is to update votes on UI screen                     //
    //============================================================================//
    function updateVotes(resultCode,$this)
    {
        debugger;
        if (resultCode == 0) {
            var voteCount = parseInt($this.val()) - 1;
            $this.val(voteCount);
            var textToShowOnBtn = $this.attr("name");
            $this.text(textToShowOnBtn + " (" + voteCount + ")");
        }
        else if (resultCode == 1) {
            var voteCount = parseInt($this.val()) + 1;
            $this.val(voteCount);
            var textToShowOnBtn = $this.attr("name");
            $this.text(textToShowOnBtn + " (" + voteCount + ")");
        }
        else if (resultCode == -1) {
            var voteCount = parseInt($this.val()) + 1;
            $this.val(voteCount);
            var textToShowOnBtn = $this.attr("name");
            $this.text(textToShowOnBtn + " (" + voteCount + ")");
            if ($this.attr("name") == "DownVote") {
                var HandleVotes = $this.closest(".UpDownContainer").find("#btnUpvote");
                var oppositeVote = parseInt(HandleVotes.val()) - 1;
                //debugger;
                HandleVotes.val(oppositeVote);
                var textToShowOnBtn = HandleVotes.attr("name");
                HandleVotes.text(textToShowOnBtn + " (" + oppositeVote + ")");
             }
             else if ($this.attr("name") == "UpVote") {
                 var HandleVotes = $this.closest(".UpDownContainer").find("#btnDownVote");
                 var oppositeVote = parseInt(HandleVotes.val() - 1);
                 //debugger;
                 HandleVotes.val(oppositeVote);
                    var textToShowOnBtn = HandleVotes.attr("name");
                    HandleVotes.text(textToShowOnBtn + " (" + oppositeVote + ")");
             } 
        }//End of else if (resultCode == -1)

    }// End of updateVotes(resultCode).

    //============================================================================//
    //                          Public functions                                  //
    //============================================================================//
    return {
        readyMain: function () {
            initializePage();
        },

       Vote: function (btnClicked) {
            debugger;
                updateVoteInDB(btnClicked);
       },
        Bid: function (btnClicked) {
        debugger;
        updateBidInDB(btnClicked);
    },
    
        Comment:function(btnClicked,e)
        {
            var btnType=$(btnClicked).hasClass("_DbtnComment");
            if (btnType == true)   //When Comment is to save
            {
                saveComment($(btnClicked));
            }
            else
            {  //When Load Comment is clicked ( will have class="_DLoadComments")
                e.preventDefault();
                var ProjId = $(btnClicked).closest("._DContainer").find("#ProjectId").val();
                getComments(ProjId);
            }
        }
    }; //End of return

    //============================================================================//
    //          This function is to update bid in database                //
    //============================================================================//
    function updateBidInDB(btnClicked) {
        var $this = $(btnClicked);
        var projectId = parseInt($this.closest("._DContainer").find("#ProjectId").val());
        var resultCode;
        var url = "Project/BidOnProject";
        debugger;
        MyWebApp.Globals.MakeAjaxCall("GET", url, {
            "ProjectId": projectId
        },
          function (result) {
              if (result.success == true) {
                  resultCode = result.data.ResultCode;
                  updateBid(resultCode, $this);
              }
          }, function (xhr, ajaxOptions, thrownError) {
              MyWebApp.UI.showRoasterMessage('A problem has occurred while biding: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
          }, false);
        return false;
    }


    //============================================================================//
    //          This function is to update Bids on UI screen                     //
    //============================================================================//
    function updateBid(resultCode, $this) {
        debugger;
        if (resultCode == -1) {

            var BidCount = parseInt($this.val()) - 1;
            $this.val(BidCount);
            var textToShowOnBtn = $this.attr("name");
            $this.text(textToShowOnBtn + "(" + BidCount + ")");
        }
        else if (resultCode == 1) {
            var BidCount = parseInt($this.val()) + 1;
            $this.val(BidCount);
            var textToShowOnBtn = $this.attr("name");
            $this.text(textToShowOnBtn + "(" + BidCount + ")");
        }

    }// End of updateBid(resultCode).


}()); //End of Outermost immediately invoked function.