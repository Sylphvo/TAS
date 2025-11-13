var sortData = { sortColumnEventActual: '', sortOrderEventActual: '' }
var gridOptionsRubberGarden, ListDataFull;
var page, pageSize, gridApi, pagerApi;
var arrValueFilter = {
    statusInProgress: 0,// Đang xử lý
    statusHandle: 1,// Đã Xử lý
    statusConfirmOrder: 2,// Đã tạo đơn hàng
    contentInProgress:  'Đang xử lý',
    contentHandle: 'Đã Xử lý',
    contentConfirmOrder: 'Đã tạo đơn hàng',
	typeExcel: 1,// Xuất Excel Data
	typeSampleExcel: 2,// Xuất Excel Mẫu
};

var farmByCode = {};

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
        rowHeight: 45,//
        headerHeight: 45,// 
        rowData: [],        
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        animateRows: true,
        singleClickEdit: true,
        components: {
           /* /customFloatingFilterInput: getFloatingFilterInputComponent(),*/
            //customheader: CustomHeaderRubberGarden,
        },
        cellSelection: true,
        onGridReady: function (params) {          
            gridApi = params.api;
        },
        rowDragManaged: true,
        onRowDragEnd() {
            persistCurrentPageOrder();          // rows đã đúng thứ tự bạn vừa kéo
        },
        onCellValueChanged: e => {
            if (e.colDef.field == 'tscPercent') {
                calcDRCPercent(e.data);
                e.api.refreshCells({ rowNodes: [e.node], columns: ['drcPercent'], force: true });
            }
            if (['rubberKg', 'drcPercent'].includes(e.colDef.field)) {
                calcFinish(e.data);
                calcCentrifuge(e.data);
                e.api.refreshCells({ rowNodes: [e.node], columns: ['finishedProductKg'], force: true });
                e.api.refreshCells({ rowNodes: [e.node], columns: ['centrifugeProductKg'], force: true });
            }
            UpdateDataAfterEdit(0, e.data);
        },
        enableRangeSelection: true,
        allowContextMenuWithControlKey: true, // giữ Ctrl + click phải vẫn hiện
        suppressContextMenu: false, // cho phép hiện menu ag-Grid
        getContextMenuItems: params => {
            return [
                {
                    // custom item
                    name: 'Áp dụng cho tất cả các cột',
                    shortcut: "(Alt + a)",
                    action: () => {
                        ApplyCustomColulmn(params);
                    },
                    cssClasses: ["red", "bold"],
                    icon: '<i class="ti ti-copy f-20"></i>',
                },
                {
                    // custom item
                    name: arrValueFilter.contentInProgress,
                    shortcut: "(Alt + 1)",
                    action: () => {
                        ApproveAllData(arrValueFilter.statusHandle);
                    },
                    cssClasses: ["red", "bold"],
                    icon: '<i class="ti ti-arrow-back f-20"></i>',
                },
                {
                    // custom item
                    name: arrValueFilter.contentHandle,
                    shortcut: "(Alt + 2)",
                    action: () => {
                        ApproveAllData(arrValueFilter.statusHandle);
                    },
                    cssClasses: ["red", "bold"],
                    icon: '<i class="ti ti-check f-20"></i>',
                },
                {
                    // custom item
                    name: 'Tạo đơn hàng',
                    shortcut: "(Alt + 3)",
                    action: () => {
                        ApproveAllData(arrValueFilter.statusConfirmOrder);
                    },
                    cssClasses: ["red", "bold"],
                    icon: '<i class="ti ti-arrow-up-right-circle f-20"></i>',
                }
            ];
        },
        onCellKeyDown: function (params) {
            const keyboardEvent = params.event;
            //Đang xử lý
            if (keyboardEvent.altKey && keyboardEvent.key === "1") {
                ApproveAllData(arrValueFilter.statusInProgress);
            }
            //Đã xử lý
            if (keyboardEvent.altKey && keyboardEvent.key === "2") {
                ApproveAllData(arrValueFilter.statusHandle);
            }
            //Đã tạo đơn hàng
            if (keyboardEvent.altKey && keyboardEvent.key === "3") {
                ApproveAllData(arrValueFilter.statusConfirmOrder);
            }

            if (keyboardEvent.altKey && keyboardEvent.key === "a") {
                ApplyCustomColulmn(params);
                NotificationToast("success", "Áp dụng cho tất cả thành công");
            }
           
        }
    };
    const eGridDiv = document.querySelector(RubberGarden);
    gridApi = agGrid.createGrid(eGridDiv, gridOptionsRubberGarden);

    CreateRowDataRubberGarden();
    resizeGridRubberGarden();
}
function ApplyCustomColulmn(params) {
    gridApi.setGridOption("rowData", ListDataFull.filter(x => x[params.column.colId] = params.value));
    var idList = ListDataFull.map(x => x.intakeId);// list data intakeId 
    var colId = params.column.colId; //colId
    var valueData = params.value;

    idList.forEach(function (item, index) {
        var objData = ListDataFull.filter(x => x.intakeId == item);
        if (colId == 'tscPercent') {
            calcDRCPercent(objData[0]);
        }
        if (['rubberKg', 'drcPercent'].includes(colId)) {
            calcFinish(objData[0]);
            calcCentrifuge(objData[0]);
        }
    });
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/AddOrUpdateFull",
        contentType: 'application/json',
        data: JSON.stringify(ListDataFull),
        success: function (res) {
            RefreshAllGridWhenChangeData();
        },
        error: function () {
        }
    });
}
// Cập nhật dữ liệu sau khi chỉnh sửa
// status: 0 - chỉnh sửa, 1 - thêm mới
function UpdateDataAfterEdit(status, rowData) {
    var rowDataObj = {};
    if (status == 1) {
        rowDataObj.farmCode = $('#ListCboFarmCode').val();
        rowDataObj.farmerName = $('#ListCboFarmerName').val();
        rowDataObj.rubberKg = num($('#RubberKg').val());
        rowDataObj.tscPercent = num($('#TSCPercent').val());
        calcDRCPercent(rowDataObj);
        calcFinish(rowDataObj);
        calcCentrifuge(rowDataObj);        
        rowData = rowDataObj;
    }
    $.ajax({
        async: true,
        url: "/RubberGarden/AddOrUpdate",
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(rowData),
        success: function (res) {
            Toast.fire({
                icon: "success",
                title: (status == 1 ? "Cập nhật" : "Thành công") + " dữ liệu thành công"
            });
            RefreshAllGridWhenChangeData();
        },
        error: function () {
            Toast.fire({
                icon: "danger",
                title: (status == 1 ? "Cập nhật" : "Thành công") + " dữ liệu thất bại"
            });
        }
    });
}
// Chuyển chuỗi sang số
const num = v => {
    const x = parseFloat(String(v ?? '').replace(',', '.'));
    return Number.isFinite(x) ? x : 0;
};
// TSC %
function calcDRCPercent(row) {
    row.drcPercent = row.tscPercent - 3;
}
// Thành phẩm
function calcFinish(row) {
    row.finishedProductKg = +(num(row.rubberKg) * num(row.drcPercent) / 100).toFixed(3);
}
// Thành phẩm ly tâm
function calcCentrifuge(row) {
    row.centrifugeProductKg = +((num(row.rubberKg) * num(row.drcPercent) / 100) * 1.5).toFixed(3);
}
// Lưu thứ tự hiện tại sau khi kéo thả
function resizeGridRubberGarden() {
    setTimeout(function () {
        setWidthHeightGrid(45);
    }, 100);
}
function setWidthHeightGrid(heithlayout) {
    gridApi.sizeColumnsToFit();
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
            gridApi.setGridOption("rowData", data);
            //gridOptionsRubberGarden.api.setRowData(data);
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
            field: 'farmCode',
            headerName: 'Mã Nhà Vườn',
            width: width_Col, minWidth: width_Col,
            editable: EditRubberGarden,
            filter: true,
            cellEditor: 'agSelectCellEditor',
            cellEditorPopup: true,
            popupPosition: 'under',
            cellEditorParams: () => ({
                values: listComboFarmCode.map(f => f.farmCode),
                allowTyping: true,
                searchType: 'matchAny',
                cellRenderer: (p) => {
                    const f = farmByCode[p.value];
                    return f ? `${f.farmCode} - ${f.farmerName}` : (p.value ?? '');
                }
            }),
            filter: "agTextColumnFilter",
            cellStyle: { 'text-align': 'center' }
        },
        {
            field: 'farmerName', headerName: 'Tên Nhà Vườn', width: 150, minWidth: 150
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: EditRubberGarden
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'rubberKg', headerName: 'Khối lượng', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: EditRubberGarden
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'tscPercent', headerName: 'TSC', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: EditRubberGarden
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'drcPercent', headerName: 'DRC', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: EditRubberGarden
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'finishedProductKg', headerName: 'Thành Phẩm', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'centrifugeProductKg', headerName: 'Thành Phẩm Ly Tâm', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'timeDate_Person', headerName: 'Người cập nhật', width: width_Col, minWidth: width_Col
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'timeDate', headerName: 'Thời gian cập nhật', width: 120, minWidth: 120
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: "agTextColumnFilter"
            , headerComponent: "customHeader"
        },
        {
            field: 'status', headerName: 'Trạng thái', width: 140, minWidth: 140
            , cellStyle: cellStyle_Col_Model_EventActual
            , editable: false
            , filter: false
            , headerComponent: "customHeader"
            , cellRenderer: function (params) {
                return GetStatusRubberGarden(params);
            }
        },
        {
            field: 'action', headerName: 'Chức năng', width: 140, minWidth: 140
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
    const wrap = document.createElement('div');
    wrap.innerHTML =
    `<button class="button_action_custom avtar avtar-xs btn-light-warning js-InProgress" title="Đang xử lý">
        <i class="ti ti-arrow-back f-20"></i>
    </button>
    <button class="button_action_custom avtar avtar-xs btn-light-success js-Handle" title="Đã xử lý">
        <i class="ti ti-check f-20"></i>
    </button>
    <button class="button_action_custom avtar avtar-xs btn-light-primary js-Order" title="Tạo đơn hàng">
      <i class="ti ti-arrow-up-right-circle f-20"></i>
    </button>
    <button class="button_action_custom avtar avtar-xs btn-light-danger js-cancel" title="Xóa">
      <i class="ti ti-trash f-20"></i>
    </button>
  `;
    const btnInProgress = wrap.querySelector('.js-InProgress');
    const btnHandle = wrap.querySelector('.js-Handle');
    const btnOrder = wrap.querySelector('.js-Order');
    const btnCancel = wrap.querySelector('.js-cancel');

    btnInProgress.addEventListener('click', (e) => {
        ApproveData(params.data.intakeId, arrValueFilter.statusInProgress);
    });
    btnHandle.addEventListener('click', (e) => {
        ApproveData(params.data.intakeId, arrValueFilter.statusHandle);
    });
    btnOrder.addEventListener('click', (e) => {
        ApproveData(params.data.intakeId, arrValueFilter.statusConfirmOrder);
    });
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
                $.ajax({
                    async: true,
                    method: 'POST',
                    url: "/RubberGarden/DeleteRubber",
                    dataType: 'json',
                    data: { idRubber: params.data.intakeId },
                    success: function (res) {
                        if (res == 1) {
                            Toast.fire({
                                icon: "success",
                                title: "Phê duyệt thành công"
                            });
                        }
                        RefreshAllGridWhenChangeData();
                    },
                    error: function () {
                        Toast.fire({
                            icon: "danger",
                            title: "Phê duyệt thất bại"
                        });
                    }
                });
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
    let cellAttr = {};
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

function InputNameFile(typeExcel) {
    Swal.fire({
        title: "Nhập tên File Excel",
        input: "text",
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        confirmButtonText: "Xuất Excel",
        showLoaderOnConfirm: true,
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeExcel == arrValueFilter.typeExcel) {
                onExportExcelData(result.value);
            }
        }
    });
}
// Export Excel Data
function onExportExcelData(fileName) {
    const headers = gridApi.getColumnDefs().filter(x => x.field != 'action').map(item => item.headerName);
    const keys = gridApi.getColumnDefs().filter(x => x.field != 'action').map(item => item.field);
    const data = ListDataFull;
    // tạo mảng mới với key tiếng Việt
    const newData = data.map(obj => {
        const newObj = {};
        keys.forEach((k, i) => {
            if (k == 'status') {
                newObj[headers[i]] = obj[k] == 1 ? 'Đã xác nhận' : 'Chưa xác nhận';
            }
            else {
                newObj[headers[i]] = obj[k];
            }
        }); return newObj;
    });
    const ws = XLSX.utils.json_to_sheet(newData);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, fileName + '.xlsx');  
}
// Export Excel Mẫu
function onExportTemplateExcel() {
    var lstDataTemplate = {};
    lstDataTemplate['Số thứ tự'] = '1'; 
    lstDataTemplate['Mã Nhà Vườn'] = 'NV_1'; 
    lstDataTemplate['Tên Nhà Vườn'] = 'Nhà Vườn A'; 
    lstDataTemplate['Khối lượng'] = '9'; 
    lstDataTemplate['TSC'] = '9'; 
    lstDataTemplate['DRC'] = '9'; 
    lstDataTemplate['Thành Phẩm'] = '9'; 
    lstDataTemplate['Thành Phẩm Ly Tâm'] = '9'; 
    lstDataTemplate['Người cập nhật'] = 'admin'; 
    lstDataTemplate['Thời gian cập nhật'] = '1'; 
    lstDataTemplate['Trạng thái'] = '1'; 
	lstDataTemplate = [lstDataTemplate];
    const ws = XLSX.utils.json_to_sheet(lstDataTemplate);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    XLSX.writeFile(wb, 'Dữ Liệu Mẫu Cập Nhật Số Liệu.xlsx');
}
// Lưu thứ tự hiện tại sau khi kéo thả
var ordered;
function persistCurrentPageOrder() {
    const start = (page - 1) * pageSize;
    const n = gridApi.getDisplayedRowCount();
    ordered = [];
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
                gridApi.setGridOption("rowData", ListDataFull.slice(1, total));
                //gridApi.setRowData(ListDataFull.slice(1, total));
            }
            else {
                gridApi.setGridOption("rowData", ListDataFull.slice(s.start, last));
                //gridApi.setRowData(ListDataFull.slice(s.start, last));
            }
        }
    });
}

// Import Excel Data
function ImportExcelData(rows) {
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/ImportDataLstData",
        contentType: 'application/json',
        data: JSON.stringify(rows),
        success: function (res) {
            Toast.fire({
                icon: "success",
                title: "Import file Excel thành công"
            });
            RefreshAllGridWhenChangeData();
        },
        error: function () {
            Toast.fire({
                icon: "danger",
                title: "Lỗi khi import file Excel!"
            });
        }
	});
}
// Bắt đầu chỉnh sửa ô đang chọn
function onBtStartEditing() {
    const selectedNode = gridApi.getFocusedCell();
    if (selectedNode) {
        gridApi.startEditingCell({
            rowIndex: selectedNode.rowIndex,
            colKey: selectedNode.column.getId()
        });
    }
}
// Phê duyệt dữ liệu
function ApproveData(intakeId, status) {
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/ApproveDataRubber",
        dataType: 'json',
        data: { intakeId: intakeId, status: status },
        success: function (res) {
            if (status == 1) {
                Toast.fire({
                    icon: "success",
                    title: "Phê duyệt thành công"
                });
            }
            else {
                Toast.fire({
                    icon: "success",
                    title: "Khôi phục thành công"
                });
            }
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
function ApproveAllData(status) {
    $.ajax({
        async: true,
        method: 'POST',
        url: "/RubberGarden/ApproveAllDataRubber",
        dataType: 'json',
        data: { status: status },
        success: function (res) {
            if (res == 1) {
                NotificationToast("success", "Phê duyệt thành công");
            }
            RefreshAllGridWhenChangeData();
        },
        error: function () {
            NotificationToast("danger", "Phê duyệt thất bại");
        }
    });
}
//lấy trạng thái Rubber
function GetStatusRubberGarden(params) {
    var statusValue = params.value;
    if (statusValue == arrValueFilter.statusInProgress) {
        return '<span class="badge text-bg-info">' + arrValueFilter.contentInProgress +'</span>';
    }
    else if (statusValue == arrValueFilter.statusHandle) {
        return '<span class="badge text-bg-success">' + arrValueFilter.contentHandle +'</span>';
    }
    else if (statusValue == arrValueFilter.statusConfirmOrder) {
        return '<span class="badge text-bg-primary">' + arrValueFilter.contentConfirmOrder +'</span>';
    }
}
function EditRubberGarden(params) {
    return params.data.status != arrValueFilter.statusConfirmOrder;
}