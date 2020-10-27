///加载收藏夹
function loadUserFavorites() {
    document.getElementById("menuItem").innerHTML = '';
    var dataParams = 'method=getUserFavorites&ENTITY_NAME=UserFavorites&AN=SYS';
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:userFavoritesLoaded', false);
}
///加载收藏夹数据成功
function userFavoritesLoaded(data) {
    if (!alertMessage(data)) return;
    data = eval('(' + data + ')');
    var divMenuItem = document.getElementById("menuItem");
    for (var i = 0; i < data.rows.length; i++) {
        var fid = data.rows[i].MenuFid;
        createMenu(fid, divMenuItem);
    }
    $(".menuClose").click(function (event) {
        var fid = this.id.replace('a_', '');
        ///TODO:中英文
        $.messager.confirm("提示", "是否删除该收藏", function (ev) {
            if (ev) {
                delFavorite(fid);
            }
        });
        event.stopPropagation();
    })
}
///
function createMenu(fid, divMenuItem) {
    var menuObj = parent.menuMap.get(fid);
    if (!isValidData(menuObj)) return;

    var divMenu = document.createElement("div");
    divMenu.id = "div_" + fid;
    divMenu.className = "easyui menuDiv";

    var pic = menuObj.FavoritePic;
    pic = pic == null ? "default" : pic;
    divMenu.setAttribute("pic", pic);
    divMenu.style.backgroundImage = 'url("CSS/HomePageIcon/' + pic + '.png")';
    divMenu.setAttribute("clickId", menuObj.domId);
    divMenu.setAttribute("onclick", "Menulocation(this)");
    divMenuItem.appendChild(divMenu);

    var divTitle = document.createElement("div");
    divTitle.className = "menuTitle";
    divMenu.appendChild(divTitle);

    var divContent = document.createElement("div");
    divContent.className = "menuContent";
    divMenu.appendChild(divContent);

    var close = document.createElement("a");
    close.className = "menuClose";
    close.innerHTML = "";
    close.setAttribute("onmouseover", "showX(this)");
    close.setAttribute("onmouseout", "hidX(this)");

    close.id = "a_" + fid;
    divTitle.appendChild(close);
    divContent.innerHTML += XR.language == true ? menuObj.MenuName : menuObj.MenuNameCn;
}
///
function showX(obj) {
    obj.innerHTML = "×";
}
///
function hidX(obj) {
    obj.innerHTML = "";
}
///删除收藏夹
function delFavorite(menuFid) {
    var dataParams = 'method=delUserFavorite&ENTITY_NAME=UserFavorites&AN=SYS&menuFid=' + menuFid;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:favoriteDeleted', false);
}
///收藏夹删除后
function favoriteDeleted(data) {
    if (!alertMessage(data)) return;
    loadUserFavorites();
}
///
function clickButton(id) {
    if (document.all) {
        parent.document.getElementById(id).click();
    }
    else {
        var evt = document.createEvent("MouseEvent");
        evt.initEvent("click", true, true);
        parent.document.getElementById(id).dispatchEvent(evt);
    }
}
///
function Menulocation(obj) {
    var clickID = obj.getAttribute("clickId");
    clickButton(clickID);
}
