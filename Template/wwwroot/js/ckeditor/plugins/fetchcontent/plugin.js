// The itemsArray variable is the example "database" of templates or table content.
var itemsArray = [
    {
        id: 1,
        name: '@template1',
        content: '<p>This is the content of template 1.</p>'
    },
    {
        id: 2,
        name: '@template2',
        content: '<p>This is the content of template 2.</p>'
    },
    {
        id: 3,
        name: '@table1',
        content: '<table align="center" border="1" cellpadding="1" cellspacing="1"><tbody><tr><td>&nbsp; <strong>Pay Elements</strong></td><td><strong>MONTHLY</strong></td><td><strong>ANNUAL</strong></td></tr><tr><td>Basic</td><td>${Monthly Basic}</td><td>${New Basic}</td></tr><tr><td>Flexi Benefit</td><td>${Monthly Special}</td><td>${New Special}</td></tr><tr><td>Annual Wage Supplement</td><td>${Annual Wage Supplement Monthly}</td><td>${Annual Wage Supplement}</td></tr><tr><td><strong>Annual Base Salary (Total Guaranteed Payment)</strong></td><td><strong>${Annual Base Salary Monthly}</strong></td><td><strong>${Annual Base Salary}</strong></td></tr></tbody></table>'
    },
    {
        id: 4,
        name: '@table2',
        content: '<table align="center" border="1" cellpadding="1" cellspacing="1"><tbody><tr><td>&nbsp; <strong>Pay Elements</strong></td><td><strong>MONTHLY</strong></td><td><strong>ANNUAL</strong></td></tr><tr><td>Basic</td><td>${Monthly Basic}</td><td>${New Basic}</td></tr><tr><td>Flexi Benefit</td><td>${Monthly Special}</td><td>${New Special}</td></tr><tr><td>Annual Wage Supplement</td><td>${Annual Wage Supplement Monthly}</td><td>${Annual Wage Supplement}</td></tr><tr><td><strong>Annual Base Salary (Total Guaranteed Payment)</strong></td><td><strong>${Annual Base Salary Monthly}</strong></td><td><strong>${Annual Base Salary}</strong></td></tr></tbody></table>'
    },
    // Add more templates or table content objects as needed.
];

// Register the plugin in the editor.
CKEDITOR.plugins.add('fetchcontent', {
    requires: 'autocomplete,textmatch',

    init: function (editor) {
        editor.on('instanceReady', function () {
            var config = {};

            // Called when the user types in the editor or moves the caret.
            // The range represents the caret position.
            function textTestCallback(range) {
                // You do not want to autocomplete a non-empty selection.
                if (!range.collapsed) {
                    return null;
                }

                return CKEDITOR.plugins.textMatch.match(range, matchCallback);
            }

            // Returns the position of the matching text.
            // It matches a word starting from the '@' character
            // up to the caret position.
            function matchCallback(text, offset) {
                console.log(editor.insertHtml(text), offset);
                // Get the text before the caret.
                var left = text.slice(0, offset),
                    // Will look for an '@' character followed by a template identifier.
                    match = left.match(/@\w*$/);

                if (!match) {
                    return null;
                }
                console.log(match);
                return {
                    start: match.index,
                    end: offset
                    
                };
            }

            function getItemById(id) {
                return itemsArray.find(function (item) {
                    return item.id === id;
                });
            }

            config.textTestCallback = textTestCallback;

            editor.config.allowedContent = true;
            editor.config.extraAllowedContent = 'table tr th td'; // Specify the allowed tags and attributes

            config.itemTemplate = '<li data-id="{id}" class="template">{name}</li>';
            config.outputTemplate = '<div>{content}</div>';

            function dataCallback(matchInfo, callback) {
                // Remove the '@' symbol.
                var query = matchInfo.query.substring(1);
                // Simple search.
                // Filter the entire items array so only the items that start
                // with the query remain.
                var suggestions = itemsArray.filter(function (item) {
                    return String(item.name).indexOf(query) === 0;
                });
                // Modify the callback function to insert HTML content.
                callback(suggestions, function (selectedItem) {
                    editor.insertHtml(selectedItem.content);

                    // Add a breakpoint here to catch after clicking on the selected item.
                    console.log('Selected Item:', selectedItem);
                });
            }

            config.dataCallback = dataCallback;

            // Attach autocomplete to the editor.
            new CKEDITOR.plugins.autocomplete(editor, config);
        });
    }
});
