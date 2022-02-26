var table = document.body.firstElementChild;
var tableRows = table.rows;

for (var i = 0; i < tableRows.length; i++) {
    var cell = tableRows[i].cells[i];
    cell.style.background = 'red';
}