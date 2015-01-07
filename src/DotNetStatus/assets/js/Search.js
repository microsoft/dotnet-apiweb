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
                    $hiddenDocId.val(output.DocId);
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
            $searchTextBox.val(ui.item.FullName);
            $hiddenDocId.val(ui.item.DocId);

            ValidateTextBox(validateUrl);

            return false;
        },
        select: function (event, ui) {
            $hiddenDocId.val(ui.item.DocId);
            $searchTextBox.val(ui.item.FullName);

            ValidateTextBox(validateUrl);

            $searchButton.click();

            return true;
        }
    })
    // Display only full name from results data
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.FullName + "</a>")
            .appendTo(ul);
    };
});