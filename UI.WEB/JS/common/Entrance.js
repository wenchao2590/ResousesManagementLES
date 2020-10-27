//var Ttime = setInterval("loadEntrance()", 500);
function loadEntrance() {
    try {
        if (isValidData(pageLoadParams.entityName) && isValidData(pageLoadParams.entityMethodName)) {

            closeMessagerProgress();
            eval(pageLoadParams.entityMethodName + "()");
        }

    } catch (e) {

    }

}
loadEntrance();