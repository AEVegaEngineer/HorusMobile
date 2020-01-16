/*
** SCRIPT PARA MENSAJERIA - NOTIFICACIONES DEL SISTEMA HORUS MOBILE
** @AUTHOR: ANDRÉS VEGA
*/
var http = require("https");
var options = {
    host: "appcenter.ms",
    path: "/api/v0.1/apps/Sulfur0/HorusMobile/push/notifications",
    method: "POST",
    headers: {
        "Content-Type": "application/json"
        "X-API-Token": "dc2fd84cfeff71d6bc9b7c1157d8d58645a268d1"
    }
};
var req = http.request(options, function (res) {
    var responseString = "";

    res.on("data", function (data) {
        responseString += data;
        // save all the data from response
    });
    res.on("end", function () {
        console.log(responseString); 
        // print to console when response ends
    });
});

process.argv.forEach(function (val, index, array) { 
  if (index < 2){}
  else if(index == 2 && val == "notify")
    var reqBody = '{"notification_content":{"name":"PruebaHorusMobile4","title":"Liquidación","body":"BuenDíaDr(a).,ustedtieneunanuevaliquidación","custom_data":{"key1":"val1","key2":"val2"}},"notification_target":null}';
    req.write(reqBody);
  else
    console.log("default");
});
