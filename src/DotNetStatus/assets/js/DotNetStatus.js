// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

"use strict";
var sortArrowSpan = "Sort-Arrow-Span";

/// <summary>Remove the sort-Arrow-Span from all elements in the header section of an HTML table</summary>
/// <param name="thead">The header section object of an HTML table</param>
function removeSortArrowSpan(thead) {
    thead.find("#" + sortArrowSpan).remove();
}

/// <summary>Sets the Sort-Arrow-Span on the Table Header table cell</summary>
/// <param name="headCell">The table cell object from the header section of an HTML table</param>
/// <param name="order">Order by ascending or descending</param>
function setSortArrowSpan(headCell, order) {
    if (order === 1) {
        $(headCell).append("<span id='" + sortArrowSpan + "' style='color:green' class='glyphicon glyphicon-arrow-up'></span>");
    } else {
        $(headCell).append("<span id='" + sortArrowSpan + "' style='color:green' class='glyphicon glyphicon-arrow-down'></span>");
    }
}

/// <summary>Sets the data-SortDirection attribute on a table cell object from the header section of an HTML table</summary>
/// <param name="headCell">HTMLTableHeader cell object</param>
/// <param name="order">Order by ascending or descending</param>
function setDataSortAttribute(headCell, order) {
    if (order === 1) {
        headCell.setAttribute("data-SortDirection", "ascending");
    } else {
        headCell.setAttribute("data-SortDirection", "descending");
    }
}

/// <summary>Fills an array with the rows from the body section of an HTML table</summary>
/// <param name="rows">The rows from the body section of an HTML table</param>
function fillArrayWithCellsFromRows(rows) {
    var cells, i, j;
    var arr = [];

    for (i = 0; i < rows.length; i += 1) {
        cells = rows[i].cells;
        arr[i] = [];
        for (j = 0; j < cells.length; j += 1) {
            arr[i][j] = cells[j].innerHTML;
        }
    }

    return arr;
}

/// <summary>Returns an array sorted by the specified column number and ordered by the orderending/descending parameter</summary>
/// <param name="arr">An array of items to sort</param>
/// <param name="order">Order by ascending or descending</param>
/// <param name="colToSortOn">The column number to sort on</param>
/// <param name="isColumnANumber">Boolean representing whether column is a number or not</param>
function sortArray(arr, order, colToSortOn, isColumnANumber) {
    return arr.sort(function (a, b) {
        var x = a[colToSortOn];
        var y = b[colToSortOn];

        //if x and y are ints then compare by the value rather then by text
        if (isColumnANumber) {
            x = parseInt(x);
            y = parseInt(y);
        }

        if (x === y) {
            return 0;
        } else {
            if (x > y) {
                return order;
            } else {
                return -1 * order;
            }
        }
    });
}

/// <summary>Sorts an rows of an HTML table body</summary>
/// <param name="thead">The HTMLelement object representing the header section of an HTML table</param>
/// <param name="tbody">The HTMLelement object representing the body section of an HTML table</param>
/// <param name="colToSortOn">Integer of the column to sort on</param>
/// <param name="isColumnANumber">Boolean representing whether column is a number or not</param>
function tableSort(thead, tbody, colToSortOn, isColumnANumber) {
    var order, arr, i;
    var rows = tbody[0].rows;

    //Compute whether to sort ascending or descending based on the the data-SortDirection attribute
    var headCell = thead[0].rows[0].cells[colToSortOn];
    if (headCell.hasAttribute("data-SortDirection") && headCell.getAttribute("data-SortDirection") === "descending") {
        order = 1;
    } else {
        order = -1;
    }

    //set sorting arrow and sortdirection attribute
    removeSortArrowSpan(thead);
    setDataSortAttribute(headCell, order);
    setSortArrowSpan(headCell, order);

    //fill and sort the array of table rows
    arr = sortArray(fillArrayWithCellsFromRows(rows), order, colToSortOn, isColumnANumber);

    // replace existing rows with new rows created from the sorted array
    for (i = 0; i < rows.length; i += 1) {
        rows[i].innerHTML = "<td>" + arr[i].join("</td><td>") + "</td>";
    }
}

/// <summary>Refreshes the contentPlaceHolder HTMLElement with new data from the data service</summary>
/// <param name="contentPlaceHolder">The HTMLelement object to replace the content of</param>
/// <param name="route">The route to call on the data service</param>
function refreshChart(contentPlaceHolder, route) {

    contentPlaceHolder.hide();
    $.get('/Data/' + route, function (data) {
        contentPlaceHolder.html(data);
        contentPlaceHolder.fadeIn(1000);
    });
}