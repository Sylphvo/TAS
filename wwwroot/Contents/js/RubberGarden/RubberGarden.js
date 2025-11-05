var sortData = { sortColumnEventActual: '', sortOrderEventActual: '' }
var gridOptionsRubberGarden, ListDataFull;
var page, pageSize, gridApi, pagerApi;
const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    }
});
function CreateGridRubberGarden() {
    gridOptionsRubberGarden = {
        //pagination: true,
        paginationPageSize: 100,
        columnDefs: CreateColModelRubberGarden(),
        defaultColDef: {
            width: 170,
            filter: true,
            floatingFilter: true,
        },
        height: 45,
        rowData: [],        
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        animateRows: true,
        singleClickEdit: true,
        components: {
            customFloatingFilterInput: getFloatingFilterInputComponent(),
            //customheader: CustomHeaderRubberGarden,
        },
        cellSelection: true,
        onGridReady: function (params) {          
            gridApi = params.api;
            params.api.sizeColumnsToFit();           
        },
        rowDragManaged: true,
        onRowDragEnd() {
            persistCurrentPageOrder();          // rows đã đúng thứ tự bạn vừa kéo
        }
     
    };

    var eGridDiv = document.querySelector(RubberGarden);
    new agGrid.Grid(eGridDiv, gridOptionsRubberGarden);
    CreateRowDataRubberGarden();
    resizeGridRubberGarden();
}
function resizeGridRubberGarden() {
    setTimeout(function () {
        setWidthHeightGrid(25);
    }, 100);
}
function setWidthHeightGrid(heithlayout) {
    gridOptionsRubberGarden.api.sizeColumnsToFit();
}
function RefreshAllGridWhenChangeData() {
    ShowHideLoading(true, RubberGarden);
    setTimeout(function () {
        CreateRowDataRubberGarden();
    }, 1);
}

// Lấy tham số tìm kiếm
function GetParamSearch() {
    return {
        companys: $('#ListCboAgent').val(),
        COMPANY_ID: $('#ag-filter-COMPANY_ID').val(),
        STAFF_NO: $('#ag-filter-STAFF_NO').val(),
        DEPARTMENT: $('#ag-filter-DEPARTMENT').val(),
        POSITION: $('#ag-filter-POSITION').val(),
        STAFF_ID: $('#ag-filter-STAFF_ID').val(),
        STAFF_NAME: $('#ag-filter-STAFF_NAME').val(),
        sortColumn: sortData.sortColumn,
        sortOrder: sortData.sortOrder,
    }
}
function CreateRowDataRubberGarden() {
    var listSearchRubberGarden = {};
    ShowHideLoading(true, RubberGarden);
    $('#RubberGardenModal .ag-overlay-no-rows-center').hide();
    $.ajax({
        async: !false,
        type: 'POST',
        url: "/RubberGarden/RubberGardens",
        data: listSearchRubberGarden,
        dataType: "json",
        success: function (data) {
            ListDataFull = data;
            gridOptionsRubberGarden.api.setRowData(data);
            renderPage();
            $('.ag-header-select-all:not(.ag-hidden)').on('click', function (e) {
                let IsChecked = $(this).find('.ag-input-field-input');
                if (IsChecked.prop('checked')) {
                    IsChecked.prop('checked', false);
                    gridApi.deselectAll();
                }
                else {
                    IsChecked.prop('checked', true);
                    gridApi.selectAll();           // chọn tất cả
                }
            });     
            setTimeout(function () {
                ShowHideLoading(false, RubberGarden);
            }, 100);
        }
    });
}
function CreateColModelRubberGarden() {
    var width_Col = 80;
    var columnDefs = [
        {
            field: 'rowNo', headerName: 'Số thứ tự', width: 100, minWidth: 100
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , checkboxSelection: true
            , headerCheckbox: true
            , headerCheckboxSelection: true // checkbox ở header để chọn tất cả
            , rowDrag: true
            , filter: false
            , floatingFilterComponent: 'customFloatingFilterInput'
            , floatingFilterComponentParams: { suppressFilterButton: true }
            , headerComponent: "customHeader"
            //, cellRenderer: cellRender_StartDate
            //, colSpan: 2
        },
        {
            field: 'farmCode', headerName: 'Mã Nhà Vườn', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , filter: true
            , floatingFilterComponent: 'customFloatingFilterInput'
            , floatingFilterComponentParams: { suppressFilterButton: true }
            , headerComponent: "customHeader"
            //, cellRenderer: cellRender_StartDate
        },
        {
            field: 'farmerName', headerName: 'Tên Nhà Vườn', width: 150, minWidth: 150
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            //, cellRenderer: cellRender_StartDate
            , headerComponent: "customHeader"
        },
        {
            field: 'rubberKg', headerName: 'Khối lượng', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">params.value</div>`;
            //}
        },
        {
            field: 'tscPercent', headerName: 'TSC', width: width_Col, minWidth: width_Col
            //, cellRenderer: cellRender_WorkStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'drcPercent', headerName: 'DRC', width: width_Col, minWidth: width_Col
            //, cellRenderer: cellRender_RequirementStatus
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
        },
        {
            field: 'finishedProductKg', headerName: 'Thành Phẩm', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        },
        {
            field: 'centrifugeProductKg', headerName: 'Thành Phẩm Ly Tâm', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        },
        {
            field: 'timeDate_Person', headerName: 'Người cập nhật', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        },
        {
            field: 'timeDate', headerName: 'Thời gian cập nhật', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: true
            , headerComponent: "customHeader"
            //, cellRenderer: function (params) {
            //    return `<div class="text-cell-eclip">${params.value}</div>`;
            //}
        },
        {
            field: 'status', headerName: 'Trạng thái', width: 140, minWidth: 140
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , headerComponent: "customHeader"
            , cellRenderer: function (params) {
                if (params.value == 0) {
                    return '<span class="badge text-bg-primary">Chưa xác nhận</span>';
                }
                if (params.value == 1) {                  
                    return '<span class="badge text-bg-success">Đã xác nhận</span>';
                }
            }
        },
        {
            field: 'action', headerName: 'Chức năng', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: false
            , editType: 'fullRow'
            , headerComponent: "customHeader"
			, cellRenderer: ActionRenderer
        }
    ]
    return columnDefs;
}
function ActionRenderer(params) {
    console.log(params.data.status);
    const wrap = document.createElement('div');
    wrap.innerHTML =
    (params.data.status == 0 ?
    ` <button class="button_action_custom avtar avtar-xs btn-light-success js-Approve" title="Phê duyệt">
            <i class="ti ti-check f-20"></i>
        </button>` 
    :
    `<button class="button_action_custom avtar avtar-xs btn-light-warning js-Restore" title="Phê duyệt">
            <i class="ti ti-arrow-back f-20"></i>
        </button>`)
    +
    `<button class="button_action_custom avtar avtar-xs btn-light-danger js-cancel" title="Xóa">
      <i class="ti ti-trash f-20"></i>
    </button>
  `;
    if (params.data.status == 0) {
        const btnApprove = wrap.querySelector('.js-Approve');
        btnApprove.addEventListener('click', (e) => {
            ApproveData(params.data.intakeId, params.data.status);            
        });
    }
    else {
        const btnRestore = wrap.querySelector('.js-Restore');
        btnRestore.addEventListener('click', (e) => {
            ApproveData(params.data.intakeId, params.data.status);
        });
    }
   

    const btnCancel = wrap.querySelector('.js-cancel');
    btnCancel.addEventListener('click', (e) => {
       // e.stopPropagation();
        Swal.fire({
            title: 'Xóa dòng này?',
            icon: "error",
            showDenyButton: false,
            showCancelButton: true,
            confirmButtonText: `Lưu`,
            showCloseButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                // remove theo đúng object data của node
                params.api.applyTransaction({ remove: [params.node.data] });
                Toast.fire({
                    icon: "success",
                    title: "Xóa thành công"
                });
            }
        });
    });

  

    return wrap;
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

function CustomHeaderRubberGarden() { }

CustomHeaderRubberGarden.prototype.init = function (params) {
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

    //if (rowObject.row_type == arrConstantRubberGarden.RowTypeStaff) {
    //    cellAttr['background-color'] = '#f1f182';
    //    cellAttr['color'] = 'black';
    //    cellAttr['padding-left'] = '25px !important';
    //    if (colName == 'start_date' || colName == 'end_date') {
    //        cellAttr['text-align'] = 'left';
    //        cellAttr['font-weight'] = '700';
    //    }
    //}
    //else if (rowObject.row_type == arrConstantRubberGarden.RowTypeDate) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = 'bold';
    //    }

    //    cellAttr['background-color'] = colorSortOrder_1;
    //}
    //else if (rowObject.row_type == arrConstantRubberGarden.RowTypeGroup) {
    //    if (colName == 'start_date') {
    //        cellAttr['font-weight'] = '600';
    //    }
    //    cellAttr['padding-left'] = '45px !important';
    //    cellAttr['background-color'] = colorSortOrder_4;
    //}
    //else if (rowObject.row_type == arrConstantRubberGarden.RowTypeItem && rowObject.row_status == arrConstantRubberGarden.RowStatusPast) {
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

CustomHeaderRubberGarden.prototype.onSortChanged = function () {
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

CustomHeaderRubberGarden.prototype.getGui = function () {
    return this.eGui;
};

CustomHeaderRubberGarden.prototype.onSortRequested = function () {
    RefreshAllGridWhenChangeData();
};

CustomHeaderRubberGarden.prototype.destroy = function () {
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
    pagerApi = makePaginator({
        data: ListDataFull,
        listEl: '#pager',
        pagerEl: '#list-paging',
        page: 1,
        pageSize: $('.selector-paging').val(),
        renderItem: x => ``,
        onChange: s => {
            let total = s.total;
            let start = (s.start == 0 ? 1 : s.start);
            let last = s.start + parseInt(s.pageSize);
            if (last > total) {
				last = total;
            }
            $('#total-entries').text(total);
            $('#start-entries').text(start);
            $('#last-entries').text(last);
            if (IsOptionAll) {
                gridApi.setRowData(ListDataFull.slice(1, total));
            }
            else {
                gridApi.setRowData(ListDataFull.slice(s.start, last));
            }
        }
    });
}
function addNewRowBody() {
    const res = gridApi.applyTransaction({ add: [{ maNhaVuon: null, tenNhaVuon: null, KG: null, TSC: null, DRC: null, thanhPham: null, thanhPhamLyTam: null }] });
    const node = res.add[0]; // là dòng cuối nếu không đặt addIndex

    gridApi.ensureNodeVisible(node, "bottom");
    gridApi.setFocusedCell(node.rowIndex, "symbol");
    gridApi.startEditingCell({ rowIndex: node.rowIndex, colKey: "symbol" });
}

function ImportExcelData(rows) {
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/ImportDataLstData",
        contentType: 'application/json',
        data: JSON.stringify(rows),
        success: function (res) {
            if (res == 1) {
                Toast.fire({
                    icon: "success",
                    title: "Import file Excel thành công"
                });
                RefreshAllGridWhenChangeData();
            }
        },
        error: function () {
            Toast.fire({
                icon: "danger",
                title: "Lỗi khi import file Excel!"
            });
        }
	});
}

function onBtStartEditing() {
    const selectedNode = gridApi.getFocusedCell();
    if (selectedNode) {
        gridApi.startEditingCell({
            rowIndex: selectedNode.rowIndex,
            colKey: selectedNode.column.getId()
        });
    }
}

function ApproveData(intakeId, status) {
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/ApproveDataRubber",
        dataType: 'json',
        data: { intakeId: intakeId, status: status },
        success: function (res) {
            Toast.fire({
                icon: "success",
                title: "Approve thành công"
            });
            RefreshAllGridWhenChangeData();
        },
        error: function () {
            Toast.fire({
                icon: "danger",
                title: "Approve thất bại"
            });
        }
    });
}