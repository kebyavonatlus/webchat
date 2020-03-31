function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function AddUser(userId, name) {
    $("#onlineUsers").append('<li id="' + userId + '"><b>' + name + '</b></li>');
}

function AddMessage(name, message, messageDate) {
    $('#Chats').append('<div  class="cont darker"><strong>' +
        htmlEncode(name) +
        '</strong>: ' +
        htmlEncode(message) +
        '<span class="time-right">' +
        moment(messageDate).format('DD.MM.YYYY HH:mm:ss') +
        '</span>' +
        '</div>');
}

function AddMessages(data) {
    for (var i = 0; i < data.length; i++) {
        $('#Chats').append('<div  class="cont darker"><strong>' +
            htmlEncode(data[i].UserName) +
            '</strong>: ' +
            htmlEncode(data[i].Content) +
            '<span class="time-right">' +
            moment(data[i].MessageDate).format('DD.MM.YYYY HH:mm:ss') +
            '</span>' +
            '</div>');
    }
}

//////////////////////////////
//function GetDate(timestamp) {
//    if (timestamp.includes('Date')) {
//        var anotherTime = timestamp.replace(/\/Date\((-?\d+)\)\//, '$1');
//        anotherTime = new Date(parseInt(anotherTime));
//        var anotherDay = anotherTime.getDate();
//        var anotherMonth = anotherTime.getMonth();
//        var anotherYear = anotherTime.getFullYear();

//        anotherDay = checkZero(anotherDay);
//        anotherMonth = checkZero(anotherMonth);
//        anotherYear = checkZero(anotherYear);

//        return anotherYear + "-" + anotherMonth + "-" + anotherDay;
//    }
//    var dateTime = new Date(timestamp);
//    var dd = dateTime.getDate();
//    var mm = dateTime.getMonth() + 1;
//    var yy = dateTime.getFullYear();

//    dd = checkZero(dd);
//    mm = checkZero(mm);
//    yy = checkZero(yy);

//    return yy + "-" + mm + "-" + dd;
//}

//function GetTime(timestamp) {
//    if (timestamp.includes('Date')) {
//        var anotherTime = timestamp.replace(/\/Date\((-?\d+)\)\//, '$1');
//        anotherTime = new Date(parseInt(anotherTime));
//        var anotherHour = anotherTime.getHours();
//        var anotherMin = anotherTime.getMinutes();
//        var anotherSecond = anotherTime.getSeconds();

//        anotherHour = checkZero(anotherHour);
//        anotherMin = checkZero(anotherMin);
//        anotherSecond = checkZero(anotherSecond);

//        return anotherHour + "-" + anotherMin + "-" + anotherSecond;
//    }
//    var dateTime = new Date(timestamp);
//    var hour = dateTime.getHours();
//    var min = dateTime.getMinutes();
//    var second = dateTime.getSeconds();

//    hour = checkZero(hour);
//    min = checkZero(min);
//    second = checkZero(second);

//    return hour + ":" + min + ":" + second;
//}

//function checkZero(data) {
//    if (data === 1) {
//        data = "0" + data;
//    }
//    return data;
//}