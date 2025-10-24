var sortData = { sortColumnEventActual: '', sortOrderEventActual: '' }
var gridOptionsInformationGarden, ListDataFull;
var page = 1;
var pageSize = 20;
var gridApi;
var pagerApi;
function CreateGridInformationGarden() {
    gridOptionsInformationGarden = {
        //pagination: true,
        paginationPageSize: 100,
        columnDefs: CreateColModelInformationGarden(),
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
            //customheader: CustomHeaderInformationGarden,
        },
        cellSelection: true,
        onGridReady: function (params) {
            gridApi = params.api;
            params.api.sizeColumnsToFit();
            renderPage();          // nạp trang đầu
            setupPager();          // tạo pager ngoài
        },
        rowDragManaged: true,
        onRowDragEnd() {
            persistCurrentPageOrder();          // rows đã đúng thứ tự bạn vừa kéo
        }

    };

    var eGridDiv = document.querySelector(InformationGarden);
    new agGrid.Grid(eGridDiv, gridOptionsInformationGarden);
    //SetButtonOnPagingForInformationGarden();
    CreateRowDataInformationGarden();
    resizeGridInformationGarden();
}
function resizeGridInformationGarden() {
    setTimeout(function () {
        setWidthHeightGrid(25);
    }, 100);
}
function setWidthHeightGrid(heithlayout) {
    //gridOptionsInformationGarden.api.sizeColumnsToFit();
    //var heigh = $(window).height() - $('.top_header').outerHeight() - $('.dm_group.dmg-shortcut').outerHeight() - ($('.col-xl-12').outerHeight() + heithlayout);
    //$(myGrid).css('height', heigh);
    //gridOptions.api.sizeColumnsToFit({
    //	defaultMinWidth: 100,
    //	columnLimits: [{ key: "DESCRIPTION", minWidth: 200 }],
    //});
}
function RefreshAllGridWhenChangeData() {
    //ShowHideLoading(true, InformationGarden);
    setTimeout(function () {
        CreateRowDataInformationGarden();
    }, 1);
}
function CreateRowDataInformationGarden() {
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
    ListDataFull = rowData;

    //gridOptionsInformationGarden.api.setRowData(rowData);
    //listTotal = [];
    //var listSearchInformationGarden = GetParamSearchInformationGarden();
    //ShowHideLoading(true, divInformationGarden);
    //$('#InformationGardenModal .ag-overlay-no-rows-center').hide();
    //$.ajax({
    //    async: !false,
    //    type: 'POST',
    //    url: "/CalendarReportUnit/GetListInformationGarden",
    //    data: listSearchInformationGarden,
    //    dataType: "json",
    //    success: function (data) {
    //        listdataInformationGarden = data;
    //        gridOptionsInformationGarden.api.setRowData(data);
    //        setTimeout(function () {
    //            ShowHideLoading(false, divInformationGarden);
    //            $('#InformationGardenModal .ag-overlay-no-rows-center').show();
    //            setWidthHeightGridInformationGarden(25, true);
    //            FocusRowInformationGarden();
    //        }, 100);
    //    }
    //});
}
function CreateColModelInformationGarden() {
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

function CustomHeaderInformationGarden() { }

CustomHeaderInformationGarden.prototype.init = function (params) {
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

    //if (rowObject.row_type == arrConstantInformationGarden.RowTypeStaff) {
    //    cellAttr['background-color'] = '#f1f182';
    //    cellAttr['color'] = 'black';
    //    cellAttr['padding-left'] = '25px !important';
    //    if (colName == 'start_date' || colName == 'end_date') {
    //        cellAttr['text-align'] = 'left';
    //        cellAttr['font-weight'] = '700';
    //    }
    //}
    //else if (rowObject.row_type == arrConstantInformationGarden.RowTypeDate) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = 'bold';
    //    }

    //    cellAttr['background-color'] = colorSortOrder_1;
    //}
    //else if (rowObject.row_type == arrConstantInformationGarden.RowTypeGroup) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = '600';
    //    }
    //    cellAttr['padding-left'] = '45px !important';
    //    cellAttr['background-color'] = colorSortOrder_4;
    //}
    //else if (rowObject.row_type == arrConstantInformationGarden.RowTypeItem && rowObject.row_status == arrConstantInformationGarden.RowStatusPast) {
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

CustomHeaderInformationGarden.prototype.onSortChanged = function () {
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

CustomHeaderInformationGarden.prototype.getGui = function () {
    return this.eGui;
};

CustomHeaderInformationGarden.prototype.onSortRequested = function () {
    RefreshAllGridWhenChangeData();
};

CustomHeaderInformationGarden.prototype.destroy = function () {
    this.eGui.removeEventListener(
        'click',
        this.onSortChangedListener
    );
};
function updateRowIndex() {
    gridApi.forEachNodeAfterFilterAndSort((node, index) => {
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
        gridApi.setRowData(rows);
        notifier.show('Thành công', 'Import file Excel thành công', 'success', '', 4000);
    } catch (err) {
        notifier.show('Thất bại', 'Lỗi khi import file Excel!', 'danger', '', 4000);
    }
});
// Export Excel
function onExportExcelData() {
    const ws = XLSX.utils.json_to_sheet(ListDataFull);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, 'datanhaplieu.xlsx');
}
// Export Example Excel
function onExportExcel() {
    const rowData_temp = [
        { STT: 1, maNhaVuon: "NV_2", tenNhaVuon: "Đoàn Thị Diệu Hiền (giang)", KG: 99, TSC: 99, DRC: 99, thanhPham: 99, thanhPhamLyTam: 99 },
    ];
    const ws = XLSX.utils.json_to_sheet(rowData_temp);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, 'mau.xlsx');
}

function persistCurrentPageOrder() {
    const start = (page - 1) * pageSize;
    const n = gridApi.getDisplayedRowCount();
    const ordered = [];
    for (let i = 0; i < n; i++) {
        ordered.push(gridApi.getDisplayedRowAtIndex(i).data);
    }
    // ghi đè đoạn trang hiện tại vào mảng gốc
    ListDataFull.splice(start, n, ...ordered);
}

// --- helpers ---
function renderPage() {
    const start = (page - 1) * pageSize;
    const slice = ListDataFull.slice(start, start + pageSize);
    gridApi.setRowData(slice);
}
function setupPager() {
    pagerApi = makePaginator({
        listEl: '#dummy',
        pagerEl: '#pager',
        page,
        pageSize,
        total: ListDataFull.length,
        render: () => '', // không render list
        onChange: ({ page: p, pageSize: sz }) => {
            // trước khi sang trang khác, lưu lại thứ tự trang hiện tại
            persistCurrentPageOrder();
            page = p;
            pageSize = sz;
            renderPage();
        }
    });
}