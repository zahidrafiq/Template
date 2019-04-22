# StudentFacultyPortal

Steps to Setup 

1) Create a new Database "SFP" for example
2) Execute "\SFP\DB Scripts\DB_Script.txt" on above DB to create base objects + Database
3) Change connection string "SFP\MainApplication\PUCIT.AIMRL.SFP.MainApp\web.config"

4) Create another database "NotificationServer"
5) Execute "\SFP\NotificationServer\DBScripts\DB_Script.txt" on above DB to create objects for notification.
6) Change connection string "SFP\NotificationServer\PUCIT.AIMRL.NotificationServer\web.config"

-----------------

7) Right click on solution -> Properties -> Choose Multiple Startup Projects -> Select "Start" against "PUCIT.AIMRL.NotificationServer" & against "PUCIT.AIMRL.SFP.MainApp"

8) Rebuild the solution & Run the application.

-----------------

To send realtime notification, you can use following JS function

Signature of sendMessage: (empTo, message, showDesktopNotif, extraData)
Here empTo: UserID to whom you want to send notification.
-------
message: Message you want to show in notificaiton panel.
-------
showDesktopNotif: Do you want to show message in Desktop notification.
-------
extraData: If you want to send any data & want to use it on other side.

-----------------

You may check in _Headerlayout.cshtml, Following example is available

PUCIT.AIMRL.NotificationServerHandler.sendMessage(1, "hello world", false,{});

