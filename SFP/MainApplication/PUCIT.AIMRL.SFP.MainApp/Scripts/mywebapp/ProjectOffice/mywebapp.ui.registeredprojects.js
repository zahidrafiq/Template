MyWebApp.namespace("UI.RegisteredProjects");

MyWebApp.UI.RegisteredProjects = (function () {
    "use strict";
    var _isInitialized = false;

    var shareIdeaData;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            alert("initialize page");
            BindEvents();
       }
    } // initialisePage()

    //====================================================================//
    function BindEvents() {
    }// End of BindEvents();










    //====================================================================//
    return {
        readyMain: function () {
            initialisePage();
        }
    };
}());
      