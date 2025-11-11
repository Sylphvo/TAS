var sortData = { sortColumnEventActual: '', sortOrderEventActual: '' }
var gridOptionsTraceability, listDataFull, listRowChild;
var page = 1;
var pageSize = 20;
var gridApi;
var pagerApi;
var arrParentIds = [];

// Các hằng số cho Traceability
var arrConstantTraceability = {
	SortOrder_Lot: 1, // Order
	SortOrder_Agent: 2,// Agent
	SortOrder_Farm: 3,// Farmer
}

// Đăng ký sự kiện cho Traceability
function RegEventTraceability() {
    $('#ChkAll').on('change', function (e) {
        ShowOrHideAllRowChildren(e.target.checked);
    });
    $('.Col-orderName').on('click', function (e) {
        resizeGridPallet();
    });
}


// Tạo grid Traceability
function CreateGridTraceability() {
    gridOptionsTraceability = {
        //pagination: true,
        paginationPageSize: 100,
        columnDefs: CreateColModelTraceability(),
        defaultColDef: {
            width: 170,
            filter: true,
            floatingFilter: true,
        },
        rowHeight: 45,// chiều cao hàng
        headerHeight: 45,// chiều cao header
        rowData: [],
        rowDragManaged: true,
        rowDragMultiRow: true,
        rowSelection: 'multiple',         // cho phép chọn nhiều hàng
        suppressRowClickSelection: false, // cho phép click hàng để chọn
        animateRows: true,
        components: {
            //customFloatingFilterInput: getFloatingFilterInputComponent(),
            //customheader: CustomHeaderTraceability,
        },
        cellSelection: true,
        onGridReady: function (params) {
            gridApi = params.api;
            params.api.sizeColumnsToFit();
            //renderPage();          // nạp trang đầu
            //setupPager();          // tạo pager ngoài
        },
        rowDragManaged: true,
        onRowDragEnd() {
            persistCurrentPageOrder();          // rows đã đúng thứ tự bạn vừa kéo
        }

    };

    const eGridDiv = document.querySelector(Traceability);
    gridApi = agGrid.createGrid(eGridDiv, gridOptionsTraceability);
    //SetButtonOnPagingForTraceability();
    CreateRowDataTraceability();
    resizeGridTraceability();
}
function resizeGridTraceability() {
    setTimeout(function () {
        setWidthHeightGrid(25);
    }, 100);
}
function setWidthHeightGrid(heithlayout) {
    //gridOptionsTraceability.api.sizeColumnsToFit();
    //var heigh = $(window).height() - $('.top_header').outerHeight() - $('.dm_group.dmg-shortcut').outerHeight() - ($('.col-xl-12').outerHeight() + heithlayout);
    //$(myGrid).css('height', heigh);
    //gridOptions.api.sizeColumnsToFit({
    //	defaultMinWidth: 100,
    //	columnLimits: [{ key: "DESCRIPTION", minWidth: 200 }],
    //});
}
function RefreshAllGridWhenChangeData() {
    //ShowHideLoading(true, Traceability);
    setTimeout(function () {
        CreateRowDataTraceability();
    }, 1);
}
function CreateRowDataTraceability() {
    var listSearchTraceability = {};
    ResetValueArrParentIds();
    //ShowHideLoading(true, divTraceability);
    //$('#TraceabilityModal .ag-overlay-no-rows-center').hide();
    $.ajax({
        async: !false,
        type: 'POST',
        url: "/Traceability/Traceabilitys",
        data: listSearchTraceability,
        dataType: "json",
        success: function (data) {
            listDataFull = data;
			listRowChild = data.filter(x => x.sortOrder != 1);
            gridApi.setGridOption("rowData", data);
            //setTimeout(function () {
            //    ShowHideLoading(false, divTraceability);
            //    $('#TraceabilityModal .ag-overlay-no-rows-center').show();
            //    setWidthHeightGridTraceability(25, true);
            //    FocusRowTraceability();
            //}, 100);
        }
    });
}
function CreateColModelTraceability() {
    var columnDefs = [
        {
            field: 'orderCode', headerName: 'Mã đơn hàng', width: 90, minWidth: 90
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            //, checkboxSelection: true
            //, headerCheckbox: true
            //, headerCheckboxSelection: true // checkbox ở header để chọn tất cả
            //, rowDrag: true
            , filter: true
            , floatingFilterComponent: 'customFloatingFilterInput'
            , floatingFilterComponentParams: { suppressFilterButton: true }
            , headerComponent: "customHeader"
            //, cellRenderer: cellRender_StartDate
            //, colSpan: 2
        },
        {
            field: 'orderName', headerName: 'Tên đơn hàng', width: 90, minWidth: 90
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , cellRenderer: cellRender_ParentAndChild
            , headerComponent: "customHeader"
        },
        {
            field: 'agentName', headerName: 'Tên đại lý', width: 90, minWidth: 90
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , headerComponent: "customHeader"
            , cellRenderer: cellRender_ParentAndChild
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">params.value</div>`;
            //}
        },
        {
            field: 'farmerName', headerName: 'Tên nhà vườn', width: 90, minWidth: 90
            //, cellRenderer: cellRender_WorkStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'WeightKg', headerName: 'Số kg', width: 90, minWidth: 90
            //, cellRenderer: cellRender_RequirementStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'TotalAmount', headerName: 'Tổng', width: 90, minWidth: 90
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

function CustomHeaderTraceability() { }

CustomHeaderTraceability.prototype.init = function (params) {
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

    //if (rowObject.row_type == arrConstantTraceability.RowTypeStaff) {
    //    cellAttr['background-color'] = '#f1f182';
    //    cellAttr['color'] = 'black';
    //    cellAttr['padding-left'] = '25px !important';
    //    if (colName == 'start_date' || colName == 'end_date') {
    //        cellAttr['text-align'] = 'left';
    //        cellAttr['font-weight'] = '700';
    //    }
    //}
    //else if (rowObject.row_type == arrConstantTraceability.RowTypeDate) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = 'bold';
    //    }

    //    cellAttr['background-color'] = colorSortOrder_1;
    //}
    //else if (rowObject.row_type == arrConstantTraceability.RowTypeGroup) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = '600';
    //    }
    //    cellAttr['padding-left'] = '45px !important';
    //    cellAttr['background-color'] = colorSortOrder_4;
    //}
    //else if (rowObject.row_type == arrConstantTraceability.RowTypeItem && rowObject.row_status == arrConstantTraceability.RowStatusPast) {
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

CustomHeaderTraceability.prototype.onSortChanged = function () {
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

CustomHeaderTraceability.prototype.getGui = function () {
    return this.eGui;
};

CustomHeaderTraceability.prototype.onSortRequested = function () {
    RefreshAllGridWhenChangeData();
};

CustomHeaderTraceability.prototype.destroy = function () {
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


//// Import từ URL demo
//document.getElementById('importExcel').addEventListener('change', async e => {
//    const file = e.target.files[0];
//    if (!file) return;
//    try {
//        const buf = await file.arrayBuffer();
//        const wb = XLSX.read(buf, { type: 'array', cellDates: true });
//        const ws = wb.Sheets[wb.SheetNames[0]];
//        const rows = XLSX.utils.sheet_to_json(ws, { defval: null, raw: true });
//        gridApi.setRowData(rows);
//        notifier.show('Thành công', 'Import file Excel thành công', 'success', '', 4000);
//    } catch (err) {
//        notifier.show('Thất bại', 'Lỗi khi import file Excel!', 'danger', '', 4000);
//    }
//});
//// Export Excel
//function onExportExcelData() {
//    const ws = XLSX.utils.json_to_sheet(listDataFull);
//    const wb = XLSX.utils.book_new();
//    XLSX.utils.book_append_sheet(wb, ws, 'Data');
//    XLSX.writeFile(wb, 'datanhaplieu.xlsx');
//}
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
    listDataFull.splice(start, n, ...ordered);
}

// --- helpers ---
function renderPage() {
    const start = (page - 1) * pageSize;
    const slice = listDataFull.slice(start, start + pageSize);
    gridApi.setRowData(slice);
}
function setupPager() {
    pagerApi = makePaginator({
        listEl: '#dummy',
        pagerEl: '#pager',
        page,
        pageSize,
        total: listDataFull.length,
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

// Cell Render Parent And Child
function cellRender_ParentAndChild(params) {
    let cellValue = params.value;
    let id_list = params.data.sortIdList;
    if (params.colDef.field == "orderName" && params.data.sortOrder == 1) {
        return `<div class="text-cell-eclip">
        <button type="button" class="Col-orderName btn btn-link" data-bs-toggle="modal" data-bs-target="#myLargeModalLabel">
            ${htmlDecode(params.data.orderName)}
        </button>
        </div>` +
        '<span class="ag-group-expanded" ref="eExpanded"><span class="ag-icon ' + (params.data.isOpenChild ? ' ag-icon-tree-open' : ' ag-icon-tree-closed') + '" unselectable="on" role="presentation" id_list="' + id_list + '" onclick="ShowOrHideRowChildren(\'' + id_list + '\', this, SetValueArrParentIds, ' + params.data.sortOrder + ')"></span></span>';     
    }
    else if (params.colDef.field === 'agentName' && params.data.sortOrder == 2) {
        return `<div class="text-cell-eclip"> ${htmlDecode(cellValue)}</div>` + '<span class="ag-group-expanded" ref="eExpanded"><span class="ag-icon ' + (params.data.isOpenChild ? ' ag-icon-tree-open' : 'ag-icon-tree-closed') + '" unselectable="on" role="presentation" onclick="ShowOrHideRowChildren(\'' + id_list + '\', this, SetValueArrParentIds, ' + params.data.sortOrder +')"></span></span>';
        
    }
    return `<div class="text-cell-eclip">${htmlDecode(cellValue)}</div>`;
}
// End Cell Render Parent And Child

// Set giá trị mảng arrParentIds
function SetValueArrParentIds(arrParentIds, isOpenRow) {
    isOpenRow = ParseBool(isOpenRow);
    $.each(arrParentIds, function (parentIdIndex, parentIdValue) {
        if (isOpenRow) {
            arrParentIds.push(parentIdValue);           
        } else {
            arrParentIds = arrParentIds.filter(x => x != parentIdValue);           
        }
    });
}

// Reset giá trị mảng arrParentIds
function ResetValueArrParentIds() {
    arrParentIds = [];
}

// Hiển thị hoặc ẩn các hàng con dựa trên việc chọn hoặc bỏ chọn checkbox "Chọn tất cả"
function ShowOrHideAllRowChildren(isCheckAll) {
    
    let listDataChildToAdd = listDataFull.filter(x => x.sortOrder != arrConstantTraceability.SortOrder_Lot);
    if (isCheckAll)
    {
        $('div[col-id="orderName"] span.ag-icon').removeClass('ag-icon-tree-closed').addClass('ag-icon-tree-open');
        gridApi.applyTransaction({ add: listDataChildToAdd });
    }
    else {
        $('div[col-id="orderName"] span.ag-icon').removeClass('ag-icon-tree-open').addClass('ag-icon-tree-closed');
        gridApi.applyTransaction({ remove: listDataChildToAdd });
    }
    listDataFull.filter(x => x.isOpenChild = isCheckAll);
}