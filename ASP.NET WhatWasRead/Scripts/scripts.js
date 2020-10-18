$(function () {
    $("#btn-load-more").click(btnLoadMoreClick);
    $("#filter-apply-btn").click(btnApplyFilterClick);
});

function btnLoadMoreClick() {
    $("#btn-load-more").prop('disabled', true); //prevent multiclick
    var selectedPageEl = $(".page-link.selected").last();
    var lastSelectedPage = parseInt(selectedPageEl.text());
    if (lastSelectedPage) {
        var page = ++lastSelectedPage;
    }
    else {
        return false;
    }
    var moreAllowed = parseInt($(".page-link").last().text()) > page || false;

    var category = null, tag = null;
    var c = $("#left-panel #list .li-selected");
    if (c.length > 0) {
        category = c[0].dataset.target;
    }
    var t = $(".tag-span.selected");
    if (t.length > 0) {
        tag = t[0].dataset.target;
    }
    var curUrl = window.location;


    $.ajax({
        url: "/books/ListToAppend",
        type: "GET",
        data: "page=" + page + (category ? "&category=" + category : "") + (tag ? "&tag=" + tag : ""),
        success: onSuccess
    });
    function onSuccess(html) {
        var parentDiv = $("#books-list-wrapper");
        parentDiv.append(html);
        selectedPageEl.next().addClass("selected");
        if (moreAllowed) {
            $("#btn-load-more").prop('disabled', false);
        }
    }
}
function btnApplyFilterClick() {
    var queryStringAr = [];
    var langQWord = $("#filter-lang")[0].dataset.target;
    var elements = $("#filter-lang .filter-checkbox:checked");
    var ar = [];
    for (let i = 0; i < elements.length; i++) {
        ar.push($(elements[i]).attr('id'));
    }
    var qLang = "";
    if (ar.length > 0) {
        qLang = langQWord + "=" + ar.join(",");
        queryStringAr.push(qLang);
    }

    var langAuthors = $("#filter-author")[0].dataset.target;
    elements = $("#filter-author .filter-checkbox:checked");
    ar.length = 0;
    for (let i = 0; i < elements.length; i++) {
        ar.push($(elements[i]).attr('id'));
    }
    var qAuthors = "";
    if (ar.length > 0) {
        qAuthors = langAuthors + "=" + ar.join(",");
        queryStringAr.push(qAuthors);
    }

    var pagesData = $("#filter-pages")[0].dataset;
    var pagesQWord = pagesData.target;
    var minExpected = parseInt(pagesData.min);
    var maxExpected = parseInt(pagesData.max);
    var minActual = parseInt($("#min-pages").val());
    var maxActual = parseInt($("#max-pages").val());
    if (minActual && maxActual && (minActual !== minExpected || maxActual !== maxExpected) && minActual <= maxActual) /*correct values*/ {
        var qPages = pagesQWord + "=" + minActual + "-" + maxActual;
        queryStringAr.push(qPages);
    }
    var qString = queryStringAr.join("&");

    $.ajax({
        url: "/books/List?" + qString,
        type: "GET",
        success: onSuccess
    });
    function onSuccess(data) {
        var parentDiv = $("#center");
        parentDiv.html(data);
        $("#btn-load-more").click(btnLoadMoreClick);
        $("#filter-apply-btn").click(btnApplyFilterClick);
        if (window.history.pushState) {
            window.history.pushState(null, document.title, "/books/List?" + qString);
        } else {
            document.location.href = "/books/List?" + qString;
        }
    }
    
}