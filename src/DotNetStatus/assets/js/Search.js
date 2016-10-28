// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

$(document).ready(function () {
    var $searchTextBox = $("#searchTextBox");
    var $searchButton = $("#searchButton");
    var $hiddenDocId = $("#docId");

    function DisableButton() {
        $searchButton.attr('disabled', 'disabled');
    }

    function EnableButton() {
        $searchButton.removeAttr('disabled');
    }

    function InvalidTextBox() {
        $searchTextBox.addClass('invalid-query');
    }

    function ValidTextBox() {
        $searchTextBox.removeClass('invalid-query');
    }

    DisableButton();

    function ValidateTextBox(url) {
        var query = $searchTextBox.val();

        // If search box is empty, invalid mark (if applied)
        if (query == '') {
            ValidTextBox();
        } else {
            // Call API to see if search box contains valid query
            $.ajax({
                type: "GET",
                url: url,
                data: "query=" + query,
                error: function (err) {
                    InvalidTextBox();
                    DisableButton();
                },
                success: function (output) {
                    ValidTextBox();
                    EnableButton();
                    $hiddenDocId.val(output.docId);
                }
            });
        }
    }

    var autocompleteUrl = $searchTextBox.data("autocomplete-url");
    var validateUrl = $searchTextBox.data("autocomplete-validate-url");

    $searchTextBox.change(function () {
        ValidateTextBox(validateUrl)
    });

    $searchTextBox.keyup(function (e) {
        // Check if ESC was pressed
        if (e.keyCode == 27) {
            $searchTextBox.val("");
        }

        ValidateTextBox(validateUrl);
    });

    // Initialize Bootstrap autocomplete
    $searchTextBox.autocomplete({
        source: autocompleteUrl,
        focus: function (event, ui) {
            $searchTextBox.val(ui.item.fullName);
            $hiddenDocId.val(ui.item.docId);

            ValidateTextBox(validateUrl);

            return false;
        },
        select: function (event, ui) {
            $hiddenDocId.val(ui.item.docId);
            $searchTextBox.val(ui.item.fullName);

            ValidateTextBox(validateUrl);

            $searchButton.click();

            return true;
        }
    })
    // Display only full name from results data
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.fullName + "</a>")
            .appendTo(ul);
    };
});
