let tableName = "";
let excelColNames = [];
let excelData = [];
var columnDefs = [
    { headerName: "Excel Column", checkboxSelection: true, headerCheckboxSelection: true, field: "excelColumn", width: 500 }
];
var gridOptionsMap = {
    columnDefs: columnDefs,
    rowData: [],
    rowSelection: 'multiple',
    rowMultiSelectWithClick: true,
    onSelectionChanged: onSelectionChanged 
};
var eGridDiv = document.querySelector('#mappingGrid');
new agGrid.Grid(eGridDiv, gridOptionsMap);

gridOptionsExcel = {
    rowData: []
};
var eGridDiv1 = document.querySelector('#excelGrid');
new agGrid.Grid(eGridDiv1, gridOptionsExcel);
function onTablesDdlChange() {
    $.ajax({
        url: '@Url.Action("GetTableColNames", "Home")',
        type: 'GET',
        contentType: 'application/json',
        success: result => { console.log(result); },
        error: err => { console.log(err); }
    });
}
$("#tableNames").change(function (event) {
    tableName = $(this).val();
    $.ajax({
        type: "GET",
        url: "Home/GetTableColNames?tableName=" + tableName,
        dataType: "json",
        success: values => {
            const sqlColumn = {
                headerName: 'Sql Column', field: 'sqlColumn', cellEditor: 'agSelectCellEditor'
                , cellEditorParams: { values },
                editable: true, width: 500
            };
            if (columnDefs.length > 1) {
                columnDefs.shift();
            }
            columnDefs.push(sqlColumn);
            gridOptionsMap.api.setColumnDefs(columnDefs.reverse());
            console.log(sqlColumn);
        },
        error: err => { console.log(err); }
    });
});

$("#fileUpload").on('change', function () {
    var files = this.files;
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append("files", files[i]);
    }
    for (i = 0, f = files[i]; i < files.length; ++i) {
        var reader = new FileReader();
        var name = f.name;
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, { type: 'binary' });

            var sheet_name_list = workbook.SheetNames;
            sheet_name_list.forEach(function (y) { /* iterate through sheets */
                //Convert the cell value to Json
                var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                if (roa.length > 0) {
                    excelData = roa;
                    excelColNames = Object.keys(excelData[0]);

                }
            });
            if (excelColNames.filter(r => r === 'undefined').length > 0) {
                excelData = [];
                excelColNames = [];
                gridOptionsMap.api.setRowData([]);
                gridOptionsExcel.api.setColumnDefs([]);
                gridOptionsExcel.api.setRowData([]);

                alert('Please define proper column name in excel file');
            } else {
                gridOptionsMap.api.setRowData(excelColNames.map((colName, i) => ({ excelColumn: colName })));
                gridOptionsExcel.api.setColumnDefs(excelColNames.map((colName, i) => ({ headerName: colName, field: colName })));


            }


        };
        reader.readAsArrayBuffer(f);
    }
});


function onSelectionChanged(event) {
    var colNames = event.api.getSelectedNodes().map(r => r.data.excelColumn);
    gridOptionsExcel.api.setColumnDefs(colNames.map((colName, i) => ({ headerName: colName, field: colName })));
    gridOptionsExcel.api.setRowData(excelData);
}

function submitExcelData() {
    if (!gridOptionsMap || !gridOptionsExcel) {
        return;
    }
    const excelToSqlMapping = gridOptionsMap.api.getSelectedNodes().map(r => r.data).filter(r => r.sqlColumn);
    if (!tableName) {
        alert("Please Select Sql Table");
        return;
    }
    if (excelToSqlMapping.length <= 0) {
        alert("Please Map Excel Column with Sql Columns");
        return;
    } else {
        gridOptionsExcel.api.setColumnDefs(excelToSqlMapping.map((colName, i) => ({ headerName: colName.excelColumn, field: colName.excelColumn })));
    }
    const excelData = [];
    this.gridOptionsExcel.api.forEachNode(node => excelData.push(node.data));
    if (excelData.length <= 0) {
        alert("No data Found in Excel");
        return;
    }
    const bodyData = {
        tableName,
        excelToSqlMapping,
        excelData
    };
    $.post("Home/PostExcelData", JSON.stringify(bodyData), function (data) {
        alert(data);
    });

}