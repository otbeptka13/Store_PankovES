$('ul.dropdown-menu').click(function (e) {
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
