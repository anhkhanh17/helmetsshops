

    $.ajaxSetup({ cache: false });


    $(document).ready(function () {

       
       // $('.addProduct').click(function ()

        $(document).delegate(".addProduct", "click", function () {

            pid = $(this).attr("data-id");
            var ty  = $(this).closest('.product').find('#'+pid);
           
        var img = $("#" + pid);
        soluong = $('#soluong').val();
        if (soluong == 'undefined' || soluong==null) {
            soluong = 1;
        }
        $.ajax({
            url: "/Cart/Add",
            data: { id: pid, soluong: soluong },
            success: function (response) {
                $("#nn-cart-count").html(response.Count);
                $(".nn-cart-total").html(response.Total);
            }


        }).done(function () {
            $("#cart-item").load("/Cart/PartialCart")

            flyToElement($(ty), $('#basketid'));
            $("html, body").animate({ scrollTop: 0 }, 800);
           
        });
            
       // return false;
        
     

    });


      
     

    $(document).delegate(".removeProduct", "click", function () {
        pid = $(this).attr("data-id");
         
        var img = $("#" + pid);

        flyFromElement($(img), $('#basketid'));
        return false;
          
    });

      
        
});

function flyToElement(flyer, flyingTo, callBack /*callback is optional*/) {
    var $func = $(this);

    var divider = 3;

    var flyerClone = $(flyer).clone();
    $(flyerClone).css({
        position: 'absolute',
        top: $(flyer).offset().top + "px",
        left: $(flyer).offset().left + "px",
        opacity: 1,
        'z-index': 1000
    });
    $('body').append($(flyerClone));

    var gotoX = $(flyingTo).offset().left + ($(flyingTo).width() / 2) - ($(flyer).width()/divider)/2;
    var gotoY = $(flyingTo).offset().top + ($(flyingTo).height() / 2) - ($(flyer).height()/divider)/2;

    $(flyerClone).animate({
        opacity: 0.4,
        left: gotoX,
        top: gotoY,
        width: $(flyer).width()/divider,
        height: $(flyer).height()/divider
    }, 700,
    function () {
        $(flyingTo).fadeOut('fast', function () {
            $(flyingTo).fadeIn('fast', function () {
                $(flyerClone).fadeOut('fast', function () {
                    $(flyerClone).remove();
                    if( callBack != null ) {
                        callBack.apply($func);
                    }
                });
            });
        });
    });
}

function flyFromElement(flyer, flyingTo, callBack /*callback is optional*/) {
    var $func = $(this);

    var divider = 3;

    var beginAtX = $(flyingTo).offset().left + ($(flyingTo).width() / 2) - ($(flyer).width()/divider)/2;
    var beginAtY = $(flyingTo).offset().top + ($(flyingTo).width() / 2) - ($(flyer).height()/divider)/2;

    var gotoX = $(flyer).offset().left;
    var gotoY = $(flyer).offset().top;

    var flyerClone = $(flyer).clone();

    $(flyerClone).css({
        position: 'absolute',
        top: beginAtY + "px",
        left: beginAtX + "px",
        opacity: 0.4,
        'z-index': 1000,
        width:$(flyer).width()/divider,
        height:$(flyer).height()/divider
    });
    $('body').append($(flyerClone));

    $(flyerClone).animate({
        opacity: 1,
        left: gotoX,
        top: gotoY,
        width: $(flyer).width(),
        height: $(flyer).height()
    }, 700,
    function () {
        $(flyerClone).remove();
        $(flyer).fadeOut('fast', function () {
            $(flyer).fadeIn('fast', function () {
                if (callBack != null) {
                    callBack.apply($func);
                }
            });
        });
    });
}
