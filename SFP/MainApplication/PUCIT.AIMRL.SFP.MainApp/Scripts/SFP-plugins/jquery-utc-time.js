// Temp put jquery-utc-time here.
$(document).ready(function () {
    var timeSince = function (date) {
		//debugger;
		var dateInSeconds=new Date(date)/1000;
		var seconds = Math.floor((((new Date())/1000) - dateInSeconds));
        var interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return date.toLocaleDateString();;
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " days ago";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " hours ago";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " minutes ago";
        }
        if (interval >= 0) {
           // return Math.floor(seconds) + "few seconds ago";
		   return "few seconds ago";
        } else {
            return date.toLocaleDateString();;
        }
    };
    
    var initTime = function () {
        $('*[data-utc-time]').each(function (t) {
            var timefield = $(this);
            var timevalue = timefield.attr('data-utc-time');
           // if (!timevalue.endsWith(' UTC')) {
             //   timevalue = timevalue+ ' UTC';
            //}
            var date = new Date(timevalue);
            var text = timeSince(date);
            timefield.html(text);
              if (timefield.tooltip) {
                timefield.attr('data-toggle', 'tooltip');
                timefield.attr('data-trigger', 'hover');
                timefield.attr('data-title', date.toLocaleString());
                timefield.tooltip();
            } 
        });
    };
    
    var loop = function () {
        initTime();
        setTimeout(loop, 10000);
    };
    loop();
});