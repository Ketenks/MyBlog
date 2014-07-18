//loading the DOM into jQuery
$(document).ready(function () {
    //here is where we put our code
    $('.likes').on('click', function () {
        var id = $(this).data('id');
        alert('This is post number ' + id);
        
        var likesDiv = $(this);
        $.get('/Home/Like/' + id, function (data) {
            //select tha div in to a jquery object
            likesDiv.html(data);

        });


    });
});