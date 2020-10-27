function SynchronizationKanBanCardInfos() {
    openMessagerProgress();
    var dataParam = 'key=' + 'AA'
        + "&method=" + "SynchronizationKanBanCardInfos"
        + "&ENTITY_NAME=" + pageLoadParams.entityName
        + "&AN=BLL." + pageLoadParams.bllProjectName;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:SynchronizationResult');
    search();
}


function SynchronizationResult(data) {
    if (alertMessage(data)) {
        search();
    }
}