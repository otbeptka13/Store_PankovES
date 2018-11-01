$(document).on('click','.delete-basket'/*'ul.dropdown-menu'*/, function (e) {
  //  if ($(e.target).is('a') &&  e.target.hasAttribute('data-basket'))
   // {
    var button = this;//e.target;
        $(button).css("pointer-events", "none");
        $.ajax({
            url: $("#root").val() + "/Customer/DeleteBasket",
            data: { basketId: $(button).attr("data-basket") },
            success: function (response) {
                if (response.result === 0) {
                    if ($(button).hasClass('inBasket')) // delete from page Basket
                    {
                        $(button).closest('div[data-basket]').remove();
                        recalBasketAll();
                    }
                    else
                    {
                        if (response.basketCount === 0)
                            $(button).parents("ul:first").remove();
                        else
                            $(button).parents("li:first").remove();
                    }
                    $(".countBasket:first").text(response.basketCount);
                    $(".basketSumm:first").text(response.basketSum.toFixed(2));

                }
                else
                    console.log(response.message);
            }
        });
        return false;
  //  }
        
});

$(document).on('click', 'a.plus,a.minus', function () {
    recalcBasketElement(this)
});


function recalcBasketElement(button) {
    setTimeout(function () {
        var updated = $(button).closest('div[data-summone]');
        var summOne = parseFloat($(updated).attr('data-summone'));
        var count = parseFloat($(updated).find('.countBasket').val());
        var summTotal = (summOne * count).toFixed(2);
        $(updated).find('.summBasketElement').html(summTotal);
        $(updated).attr('data-summTotal', summTotal);
        recalBasketAll();
    }, 50);

}
function recalBasketAll() {
    var summTotal = 0.00;
    $.each($('[data-summTotal]'), function (ind, val) {
        summTotal += parseFloat($(val).attr('data-summTotal'));
    })
    var sum = summTotal.toFixed(2);
    $('.summ-total-basket').text(sum);
}
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

$(document).on("click", ".remove_from_wishlist", function () {
    var link = this;
    $.ajax({
        cache: false,
        type: "GET",
        url: $("#root").val() + "/Customer/DeleteWishList",
        data: { "goodId": $(link).closest("[data-goodid]").data("goodid") },
        beforeSend: function () {
            $(link).css("pointer-events", "none");
        },
        success: function (data) {
            if (data.result > -2)
                $("#wishCountValue").text("(" + data.wishCount + ")");
            if (data.result == 0)
                $(link).closest("[data-goodId]").remove();
        },
        complete: function () {
            // $(link).css("event-pointer", "auto");
        },
        error: function () {
            $(link).css("pointer-events", "auto");
            console.log('вудуеу wishlist errorrrrr');
        }
    });
    return false;
});



$(document).on("click", ".add-basket-button", function () {
    var link = this;
    var count = $(link).closest("[data-goodid]").find("[name=quantity]").val();
    count = count ? count : 1;
    $.ajax({
        cache: false,
        type: "GET",
        url: $("#root").val() + "/Customer/AddBasket",
        data: { "goodId": $(link).closest("[data-goodid]").data("goodid"), "count": count  },
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
                            <a class="close-btn  delete-basket" data-basket="`+ this.id + `" href="#"></a>
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

function payBasket(isFast) {
    var str = ''
    $.each($('div[data-basket]'), function (ind, val) {
        var id = $(val).attr('data-basket');
        var count = $(val).find('.countBasket').val();
        str += id + ':'+ count +';';
    })
    var newForm = jQuery('<form>', {
        'action': $('#root').val() + '/Customer/PayBasket',
        'method' : 'post'
    }).append(jQuery('<input>', {
        'id': 'idcount',
        'name': 'idcount',
        'type' : 'hidden',
        'value': str
        }),
        jQuery('<input>', {
            'id': 'isFast',
            'name': 'isFast',
            'type': 'hidden',
            'value': isFast ? "True" : "False"
        })
    );
    $(newForm).appendTo('body');
    newForm.submit();
    return false;
}