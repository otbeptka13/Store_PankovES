$(document).on ('click','ul.dropdown-menu', function (e) {
    if ($(e.target).is('a') &&  e.target.hasAttribute('data-basket'))
    {
        var button = e.target;
        $(button).css("pointer-events", "none");
        $.ajax({
            url: $("#root").val() + "/Customer/DeleteBasket",
            data: { basketId: $(button).attr("data-basket") },
            success: function (response) {
                if (response.result === 0) {
                    if (response.basketCount === 0)
                        $(button).parents("ul:first").remove();
                    else
                        $(button).parents("li:first").remove();
                    $(".countBasket:first").text(response.basketCount);
                    $(".basketSumm:first").text(response.basketSum.toFixed(2));

                }
                else
                    console.log(response.message);
            }
        });
        return false;
    }
        
});


$(document).ajaxSuccess(
    function (event, xhr, settings) {
        if (xhr && xhr.responseJSON && xhr.responseJSON.result && xhr.responseJSON.result == -13)
            alert("Необходимо авторизоваться!");
        else if (xhr && xhr.responseJSON && xhr.responseJSON.result && xhr.responseJSON.result < 0)
            console.log(xhr.responseJSON);
    }
);


$(document).on("click", ".btn-add-to-wishlist", function () {
    var link = this;
    $.ajax({
        cache: false,
        type: "GET",
        url: $("#root").val() + "/Customer/AddWishList",
        data: { "goodId": $(link).closest("[data-goodid]").data("goodid") },
        beforeSend: function () {
            $(link).css("pointer-events", "none");
        },
        success: function (data) {
            if (data.result > -2)
                $("#wishCountValue").text("(" + data.wishCount + ")");
            if (data.result == 0)
                $(link).addClass("wishlist_added");
        },
        complete: function () {
            // $(link).css("event-pointer", "auto");
        },
        error: function () {
            console.log('add wishlist errorrrrr');
        }
    });
    return false;
});

$(document).on("click", ".add-basket-button", function () {
    var link = this;
    $.ajax({
        cache: false,
        type: "GET",
        url: $("#root").val() + "/Customer/AddBasket",
        data: { "goodId": $(link).closest("[data-goodid]").data("goodid") },
        beforeSend: function () {
         //   $(link).css("pointer-events", "none");
        },
        success: function (data) {
            if (data.result == 0) {
                $("span.countBasket").text(data.basketCount);
                $("span.basketSumm").text(parseFloat(data.basketSum).toFixed(2));
                var liButtons = ` <li class="checkout">
                                        <div class="basket-item">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
                                                    <a href="`+ $('#root').val()+'/Customer/Basket'+ `" class="le-button inverse">Открыть</a>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <a href="`+ $('#root').val()+'/Customer/PayBasket'+`" class="le-button">Оплатить</a>
                                                </div>
                                            </div>
                                        </div>
                                    </li>`;
                var basketMenu = $("ul.basketMenu");
                if (basketMenu.length == 0) {
                    basketMenu = $(`<ul class="dropdown-menu basketMenu"> </ul>`);
                    $('.basket').append(basketMenu);
                }


                basketMenu.html("");
                $.each(data.basket, function (ind, val) {
                    var basketRow = $(`<li>
                            <div class="basket-item" >
                            <div class="row">
                                <div class="col-xs-4 col-sm-4 no-margin text-center">
                                    <div class="thumb basketIco">
                                        <img alt="" src="`+ $('#root').val() + "/Content/images/products/" + (this.imageUrl ? this.imageUrl : "product-small-01.jpg") + `" />
                                                    </div>
                                </div>
                                <div class="col-xs-8 col-sm-8 no-margin">
                                    <div class="title">`+ this.name + `</div>
                                    <div class="price">`+ parseFloat(this.summTotal).toFixed(2) + `₽</div>
                                </div>
                            </div>
                            <a class="close-btn" data-basket="`+ this.id + `" href="#"></a>
                                        </div >
                                    </li>`);
                    console.log(basketRow);
                    basketMenu.append(basketRow);
                });
                basketMenu.append(liButtons);
            }
        },
        complete: function () {
            // $(link).css("event-pointer", "auto");
        },
        error: function () {
            console.log('add wishlist errorrrrr');
        }
    });
    return false;
});