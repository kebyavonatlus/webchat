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
