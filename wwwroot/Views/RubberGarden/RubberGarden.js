var sortData = { sortColumnEventActual: '', sortOrderEventActual: '' }
var gridOptionsEventsActual;
function CreateGridEventsActual() {
    gridOptionsEventsActual = {
        //pagination: true,
        columnDefs: CreateColModelEventsActual(),
        defaultColDef: {
            width: 170,
            filter: true,
            floatingFilter: true,
        },
        height: 45,
        rowData: [],
        rowDragManaged: true,
        rowDragMultiRow: true,
        rowSelection: 'multiple',         // cho phép chọn nhiều hàng
        suppressRowClickSelection: false, // cho phép click hàng để chọn
        animateRows: true,
        components: {
            customFloatingFilterInput: getFloatingFilterInputComponent(),
            //customheader: CustomHeaderEventsActual,
        },
        cellSelection: true,
        onGridReady: function (params) {
            params.api.sizeColumnsToFit();
        },
    };
    //gridOptionsEventsActual = {
    //    //pagination: true,
    //    ////overlayPagingTemplate: CustomPaging(),
    //    //paginationPageSize: 100000,
    //    //rowBuffer: 1, //Biến để điều chỉnh số lượng row render trước
    //    //paginationPageSizeSelector: false,
    //    //defaultColDef: {
    //    //    resizable: true,
    //    //    filterParams: {
    //    //        debounceMs: 300
    //    //    }, suppressMovable: true
    //    //},
    //    headerHeight: 35,
    //    //onFilterChanged: updateRowIndex,
    //    //onSortChanged: updateRowIndex,
    //    //components: {
    //    //    customLoadingOverlay: CustomLoadingOverlay,
    //    //    //customHeader: CustomHeaderEventsActual,
    //    //},
    //    //loadingOverlayComponent: 'customLoadingOverlay',
    //    //loadingOverlayComponentParams: {
    //    //    loadingMessage: 'loading',
    //    //},
    //    height:45,
    //    columnDefs: CreateColModelEventsActual(),
    //    //onRowSelected: onRowSelected,
    //    //rowData: [],
    //    rowDragManaged: false,
    //    rowDragMultiRow: true,
    //    rowSelection: { mode: "multiRow", headerCheckbox: false },
    //    onGridReady: function (params) {
    //        params.api.sizeColumnsToFit();
    //    },
    //    //processRowPostCreate: (params) => {
    //    //    if ((params.node.data.tag_div_id == tagDivIdByUser) && !IsNullOrEmpty(params.node.data.id) && !params.node.data.is_view) {
    //    //        notificationIndex++;
    //    //        UpdateViewTaskNotification(params.node.data, params.rowIndex, notificationIndex);
    //    //    }
    //    //},
    //    //getRowStyle: function (params) {
    //    //    if (params.data.STATUS === 2) {
    //    //        return { color: 'red' };
    //    //    }
    //    //    if (params.data.row_type == arrConstantEventsActual.RowTypeItem && params.data.row_status == arrConstantEventsActual.RowStatusActual) {
    //    //        return { color: 'blue' };
    //    //    }
    //    //},
    //    //onCellDoubleClicked: function (params) {
    //    //    if (!isWriteEvent || CheckPermisstionAndStatus(params.data)) { return; }
    //    //    if ((!isEditLockedEventActualPast && params.data.row_status == arrConstantEventsActual.RowStatusPast) || (params.data.row_type == arrConstantEventsActual.RowTypeStaff || params.data.row_type == arrConstantEventsActual.RowTypeGroup || params.data.row_type == arrConstantEventsActual.RowTypeDate)) {
    //    //        return;
    //    //    }
    //    //    EditEventsActual();
    //    //},
    //    //getRowClass: params => {
    //    //    let rowClass = [];
    //    //    if ((params.data.tag_div_id == tagDivIdByUser) && !IsNullOrEmpty(params.data.id) && !params.data.is_view) {
    //    //        rowClass.push('is_not_view_event_actual');
    //    //    }
    //    //    var id_list = GetIdListEventActual(params.data.id);
    //    //    if (id_list == PFN_readCookie('id_list')) {
    //    //        setTimeout(function () {
    //    //            PFN_createCookie('row_index', '', -1);
    //    //        }, 100)
    //    //        rowClass.push('ag-row-selected');
    //    //    }
    //    //    if (params.rowIndex == PFN_readCookie('focus_row')) {
    //    //        setTimeout(function () {
    //    //            PFN_createCookie('focus_row', '', -1);
    //    //        }, 100)
    //    //        rowClass.push('ag-row-selected');
    //    //    }

    //    //    return rowClass;
    //    //},
    //    //onCellClicked: function (event) { //HungAnh: Gọi click cell dùng để focus row
    //    //    if (listdataEventsActual.length > 0) {
    //    //        if ((!isEditLockedEventActualPast && event.data.row_status == arrConstantEventsActual.RowStatusPast) || (event.data.row_type == arrConstantEventsActual.RowTypeStaff || event.data.row_type == arrConstantEventsActual.RowTypeGroup || event.data.row_type == arrConstantEventsActual.RowTypeDate)) {
    //    //            if (!$('#btnEditEventsActual').hasClass('disabled')) {
    //    //                $('#btnEditEventsActual').attr('disabled', true);
    //    //            }
    //    //            return;
    //    //        }
    //    //        $('#btnEditEventsActual').attr('disabled', CheckPermisstionAndStatus(event.data));
    //    //    }
    //    //},
    //    //localeText: {
    //    //    to: arrMsgEventActual.To,
    //    //    of: arrMsgEventActual.Of,
    //    //    noRowsToShow: arrMsgEventActual.NoRowsToShow
    //    //}
    //};

    var eGridDiv = document.querySelector(EventsActual);
    new agGrid.Grid(eGridDiv, gridOptionsEventsActual);
    //SetButtonOnPagingForEventsActual();
    CreateRowDataEventsActual();
    resizeGridEventsActual();
}
function resizeGridEventsActual() {
    setTimeout(function () {
        setWidthHeightGrid(25);
    }, 100);
}
function setWidthHeightGrid(heithlayout) {
    //gridOptionsEventsActual.api.sizeColumnsToFit();
    //var heigh = $(window).height() - $('.top_header').outerHeight() - $('.dm_group.dmg-shortcut').outerHeight() - ($('.col-xl-12').outerHeight() + heithlayout);
    //$(myGrid).css('height', heigh);
    //gridOptions.api.sizeColumnsToFit({
    //	defaultMinWidth: 100,
    //	columnLimits: [{ key: "DESCRIPTION", minWidth: 200 }],
    //});
}
function RefreshAllGridWhenChangeData() {
    //ShowHideLoading(true, EventsActual);
    setTimeout(function () {
        CreateRowDataEventsActual();
    }, 1);
}
function CreateRowDataEventsActual() {
    const rowData = [
        { STT: 1, maNhaVuon: "NV_1", tenNhaVuon: "Phan Thị Dự", KG: null, TSC: null, DRC: null, thanhPham: null, thanhPhamLyTam: null },
        { STT: 2, maNhaVuon: "NV_2", tenNhaVuon: "Đoàn Thị Diệu Hiền (giang)", KG: 532, TSC: 34.9, DRC: 31.9, thanhPham: 170, thanhPhamLyTam: 255 },
        { STT: 3, maNhaVuon: "NV_3", tenNhaVuon: "Hoàng Thị Long (C4)", KG: 721, TSC: 32.2, DRC: 29.2, thanhPham: 211, thanhPhamLyTam: 316 },
        { STT: 4, maNhaVuon: "NV_4", tenNhaVuon: "Nguyễn Văn Hải 01 (Thành)", KG: 220, TSC: 33.2, DRC: 30.2, thanhPham: 66, thanhPhamLyTam: 100 },
        { STT: 5, maNhaVuon: "NV_5", tenNhaVuon: "Nguyễn Văn Hải 02 (Thành)", KG: 324, TSC: 27.6, DRC: 24.6, thanhPham: 80, thanhPhamLyTam: 120 },
        { STT: 6, maNhaVuon: "NV_6", tenNhaVuon: "Nguyễn Văn Hà (Fong)", KG: 275, TSC: 41.2, DRC: 38.2, thanhPham: 105, thanhPhamLyTam: 158 },
        { STT: 7, maNhaVuon: "NV_7", tenNhaVuon: "Hồ Thị Hội (nhí)", KG: 47, TSC: 33.1, DRC: 30.1, thanhPham: 14, thanhPhamLyTam: 21 },
        { STT: 8, maNhaVuon: "NV_8", tenNhaVuon: "Hồ Thị Hội 2 (nhí)", KG: null, TSC: null, DRC: -3, thanhPham: null, thanhPhamLyTam: null },
        { STT: 9, maNhaVuon: "NV_9", tenNhaVuon: "Trần Văn Hương (Quốc)", KG: 477, TSC: 32.6, DRC: 29.6, thanhPham: 141, thanhPhamLyTam: 212 },
    ];

    gridOptionsEventsActual.api.setRowData(rowData);
    //listTotal = [];
    //var listSearchEventsActual = GetParamSearchEventsActual();
    //ShowHideLoading(true, divEventsActual);
    //$('#EventsActualModal .ag-overlay-no-rows-center').hide();
    //$.ajax({
    //    async: !false,
    //    type: 'POST',
    //    url: "/CalendarReportUnit/GetListEventsActual",
    //    data: listSearchEventsActual,
    //    dataType: "json",
    //    success: function (data) {
    //        listdataEventsActual = data;
    //        gridOptionsEventsActual.api.setRowData(data);
    //        setTimeout(function () {
    //            ShowHideLoading(false, divEventsActual);
    //            $('#EventsActualModal .ag-overlay-no-rows-center').show();
    //            setWidthHeightGridEventsActual(25, true);
    //            FocusRowEventsActual();
    //        }, 100);
    //    }
    //});
}
function CreateColModelEventsActual() {
    var columnDefs = [
        {
            field: 'maNhaVuon', headerName: 'Mã Nhà Vườn', width: 110, minWidth: 110
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , checkboxSelection: true
            , headerCheckbox: true
            , headerCheckboxSelection: true // checkbox ở header để chọn tất cả
            , rowDrag: true
            , filter: true
            , floatingFilterComponent: 'customFloatingFilterInput'
            , floatingFilterComponentParams: { suppressFilterButton: true }
            , headerComponent: "customHeader"
            //, cellRenderer: cellRender_StartDate
            //, colSpan: 2
            , headerComponent: "customHeader"
        },
        {
            field: 'tenNhaVuon', headerName: 'Nhà Vườn', width: 110, minWidth: 110
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            //, cellRenderer: cellRender_StartDate
            , headerComponent: "customHeader"
        },
        {
            field: 'KG', headerName: 'Khối lượng', width: 210, minWidth: 210
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">params.value</div>`;
            //}
        },
        {
            field: 'TSC', headerName: 'TSC', width: 100, minWidth: 100
            //, cellRenderer: cellRender_WorkStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'DRC', headerName: 'DRC', width: 100, minWidth: 100
            //, cellRenderer: cellRender_RequirementStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'thanhPham', headerName: 'Thành Phẩm', width: 140, minWidth: 140
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        },
        {
            field: 'thanhPhamLyTam', headerName: 'Thành Phẩm Ly Tâm', width: 140, minWidth: 140
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        }
    ]
    return columnDefs;
}
function onRowSelected(event) {
	//if (event.node.isSelected()) {
	//	var id_list = GetIdListEventActual(event.data.id);
	//	if (id_list !== PFN_readCookie('id_list')) {
	//		$('.ag-body-viewport div.ag-row').removeClass('ag-row-selected');
 //           $('.ag-body-viewport div[row-index="' + $('.' + PFN_readCookie('id_list')).parent().attr('row-index') + '"]').removeClass('ag-row-selected');
	//	}
	//	if (IsNullOrEmpty(PFN_readCookie('focus_row')) && event.type == 'rowSelected' && event.source == 'rowClicked') {
	//		$('.ag-body-viewport div[row-index="' + event.rowIndex + '"]').addClass('ag-row-selected');
	//	}
 //   }
}

function CustomHeaderEventsActual() { }

CustomHeaderEventsActual.prototype.init = function (params) {
    this.params = params;
    var strHiddenAsc = params.sortOrderDefault == 'asc' ? '' : 'ag-hidden';
    var strHiddenDesc = params.sortOrderDefault == 'desc' ? '' : 'ag-hidden';
    this.eGui = document.createElement('div');
    this.eGui.className = "ag-header-cell-label";
    this.eGui.innerHTML =
        '' + '<span class="ag-header-cell-text">' +
        this.params.displayName +
        '</span>' +
        '<span class="ag-header-icon ag-header-label-icon ag-sort-ascending-icon ' + strHiddenAsc + '"><span class="ag-icon ag-icon-asc"></span></span>' +
        '<span class="ag-header-icon ag-header-label-icon ag-sort-descending-icon ' + strHiddenDesc + '"><span class="ag-icon ag-icon-desc"></span></span>';

    //if (!IsNullOrEmpty(this.params.style)) {
    //    this.eGui.style = this.params.style;
    //}
    if (this.params.style != null && this.params.style != undefined) {
        this.eGui.style = this.params.style;
    }

    this.eSortDownButton = this.eGui.querySelector('.ag-sort-descending-icon');
    this.eSortUpButton = this.eGui.querySelector('.ag-sort-ascending-icon');

    //if (!IsNullOrEmpty(params.sortOrderDefault)) {
    if (params.sortOrderDefault != null && params.sortOrderDefault != undefined) {
        sortData.sortColumnEventActual = params.column.colId;
        sortData.sortOrderEventActual = params.sortOrderDefault;
    }

    if (this.params.enableSorting) {
        this.onSortChangedListener = this.onSortChanged.bind(this);
        this.eGui.addEventListener(
            'click',
            this.onSortChangedListener
        );
    } else {
        this.eGui.removeChild(this.eSortDownButton);
        this.eGui.removeChild(this.eSortUpButton);
    }
};
function cellStyle_Col_Model_EventActual(params) {
    //let colName = params.colDef.field;
    //let rowObject = params.data;
    let cellAttr = {};

    //if (rowObject.row_type == arrConstantEventsActual.RowTypeStaff) {
    //    cellAttr['background-color'] = '#f1f182';
    //    cellAttr['color'] = 'black';
    //    cellAttr['padding-left'] = '25px !important';
    //    if (colName == 'start_date' || colName == 'end_date') {
    //        cellAttr['text-align'] = 'left';
    //        cellAttr['font-weight'] = '700';
    //    }
    //}
    //else if (rowObject.row_type == arrConstantEventsActual.RowTypeDate) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = 'bold';
    //    }

    //    cellAttr['background-color'] = colorSortOrder_1;
    //}
    //else if (rowObject.row_type == arrConstantEventsActual.RowTypeGroup) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = '600';
    //    }
    //    cellAttr['padding-left'] = '45px !important';
    //    cellAttr['background-color'] = colorSortOrder_4;
    //}
    //else if (rowObject.row_type == arrConstantEventsActual.RowTypeItem && rowObject.row_status == arrConstantEventsActual.RowStatusPast) {
    //    cellAttr['background-color'] = colorSortOrder_3;
    //    if (colName == 'start_date' || colName == 'end_date') {
    //        cellAttr['text-align'] = 'center';
    //    }
    //}
    //else {
    //    if (colName == 'start_date' || colName == 'end_date' || colName == 'check_start_actual' || colName == 'start_date_actual' || colName == 'check_end_actual'
    //        || colName == 'end_date_actual') {
    //        cellAttr['text-align'] = 'center';
    //    }
    //}
    cellAttr['text-align'] = 'center';
    return cellAttr;
}

CustomHeaderEventsActual.prototype.onSortChanged = function () {
    //Remove tất cả icon sort ở col khác
    $('.ag-sort-ascending-icon').not($(this.eSortUpButton)).addClass('ag-hidden');
    $('.ag-sort-descending-icon').not($(this.eSortDownButton)).addClass('ag-hidden');
    //
    if (!$(this.eSortUpButton).hasClass('ag-hidden') || ($(this.eSortDownButton).hasClass('ag-hidden') && $(this.eSortUpButton).hasClass('ag-hidden'))) {
        $(this.eSortDownButton).removeClass('ag-hidden');
        $(this.eSortUpButton).addClass('ag-hidden');
        sortData.sortOrderEventActual = 'desc';
    }
    else if (!$(this.eSortDownButton).hasClass('ag-hidden')) {
        $(this.eSortUpButton).removeClass('ag-hidden');
        $(this.eSortDownButton).addClass('ag-hidden');
        sortData.sortOrderEventActual = 'asc';
    }
    sortData.sortColumnEventActual = this.params.column.colId;
    this.onSortRequested();
};

CustomHeaderEventsActual.prototype.getGui = function () {
    return this.eGui;
};

CustomHeaderEventsActual.prototype.onSortRequested = function () {
    RefreshAllGridWhenChangeData();
};

CustomHeaderEventsActual.prototype.destroy = function () {
    this.eGui.removeEventListener(
        'click',
        this.onSortChangedListener
    );
};
function updateRowIndex() {
    gridOptionsEventsActual.api.forEachNodeAfterFilterAndSort((node, index) => {
        node.setDataValue('STT', index + 1);
    });
}


// Import từ URL demo
document.getElementById('importExcel').addEventListener('change', async e => {
    const file = e.target.files[0];
    if (!file) return;
    try {
        const buf = await file.arrayBuffer();
        const wb = XLSX.read(buf, { type: 'array', cellDates: true });
        const ws = wb.Sheets[wb.SheetNames[0]];
        const rows = XLSX.utils.sheet_to_json(ws, { defval: null, raw: true });
        gridOptionsEventsActual.api.setRowData(rows);
        notifier.show('Thành công', 'Import file Excel thành công', 'success', '', 4000);
    } catch (err) {
        notifier.show('Thất bại', 'Lỗi khi import file Excel!', 'danger', '', 4000);
    }
});
// Export Excel
function onExportExcel() {
    const rowData_temp = [
        { STT: 1, maNhaVuon: "NV_1", tenNhaVuon: "Phan Thị Dự", KG: null, TSC: null, DRC: null, thanhPham: null, thanhPhamLyTam: null },
        { STT: 2, maNhaVuon: "NV_2", tenNhaVuon: "Đoàn Thị Diệu Hiền (giang)", KG: 532, TSC: 34.9, DRC: 31.9, thanhPham: 170, thanhPhamLyTam: 255 },
        { STT: 3, maNhaVuon: "NV_3", tenNhaVuon: "Hoàng Thị Long (C4)", KG: 721, TSC: 32.2, DRC: 29.2, thanhPham: 211, thanhPhamLyTam: 316 },
        { STT: 4, maNhaVuon: "NV_4", tenNhaVuon: "Nguyễn Văn Hải 01 (Thành)", KG: 220, TSC: 33.2, DRC: 30.2, thanhPham: 66, thanhPhamLyTam: 100 },
        { STT: 5, maNhaVuon: "NV_5", tenNhaVuon: "Nguyễn Văn Hải 02 (Thành)", KG: 324, TSC: 27.6, DRC: 24.6, thanhPham: 80, thanhPhamLyTam: 120 },
        { STT: 6, maNhaVuon: "NV_6", tenNhaVuon: "Nguyễn Văn Hà (Fong)", KG: 275, TSC: 41.2, DRC: 38.2, thanhPham: 105, thanhPhamLyTam: 158 },
        { STT: 7, maNhaVuon: "NV_7", tenNhaVuon: "Hồ Thị Hội (nhí)", KG: 47, TSC: 33.1, DRC: 30.1, thanhPham: 14, thanhPhamLyTam: 21 },
        { STT: 8, maNhaVuon: "NV_8", tenNhaVuon: "Hồ Thị Hội 2 (nhí)", KG: null, TSC: null, DRC: -3, thanhPham: null, thanhPhamLyTam: null },
        { STT: 9, maNhaVuon: "NV_9", tenNhaVuon: "Trần Văn Hương (Quốc)", KG: 477, TSC: 32.6, DRC: 29.6, thanhPham: 141, thanhPhamLyTam: 212 },
    ];
    const ws = XLSX.utils.json_to_sheet(rowData_temp);         // [{col1:..., col2:...}]
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, 'datanhaplieu.xlsx');  
}
// Export Example Excel
function onExportExcel() {
    const rowData_temp = [
        { STT: 1, maNhaVuon: "NV_2", tenNhaVuon: "Đoàn Thị Diệu Hiền (giang)", KG: 99, TSC: 99, DRC: 99, thanhPham: 99, thanhPhamLyTam: 99 },
    ];
    const ws = XLSX.utils.json_to_sheet(rowData_temp);         // [{col1:..., col2:...}]
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, 'mau.xlsx');
}