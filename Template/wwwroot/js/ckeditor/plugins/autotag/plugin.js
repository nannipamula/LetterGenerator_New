CKEDITOR.plugins.add('autotag', {
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

                // Use the text match plugin which does the tricky job of performing
                // a text search in the DOM. The "matchCallback" function should return
                // a matching fragment of the text.
                return CKEDITOR.plugins.textMatch.match(range, matchCallback);
            }

            // Returns the position of the matching text.
            // It matches a word starting from the '#' character
            // up to the caret position.
            function matchCallback(text, offset) {
                console.log(text, offset);
                // Get the text before the caret.
                var left = text.slice(0, offset),
                    // Will look for a '#' character followed by a word.
                    match = left.match(/#\w*$/);

                if (!match) {
                    return null;
                }

                console.log(match);

                return {
                    start: match.index,
                    end: offset
                };
            }

            config.textTestCallback = textTestCallback;

            // The itemsArray variable is the example "database".
            var itemsArray = [
                {
                    id: 1,
                    name: '${CANDIDATE ID}',
                    type: 'feature'
                },
                {
                    id: 2,
                    name: '${Employee ID/Code}',
                    type: 'bug'
                },
                {
                    id: 3,
                    name: '${EMP First Name}',
                    type: 'task'
                },
                {
                    id: 4,
                    name: '${EMP Middle Name}',
                    type: 'feature'
                },
                {
                    id: 5,
                    name: '${EMP Last Name}',
                    type: 'bug',
                },
                {
                    id: 6,
                    name: '${Letter Date}',
                    type: 'feature'
                },
                {
                    id: 7,
                    name: '${Location}',
                    type: 'feature'
                },
                {
                    id: 8,
                    name: '${RCS Grade}',
                    type: 'bug'
                },
                {
                    id: 9,
                    name: '${Designation}',
                    type: 'feature'
                },
                {
                    id: 10,
                    name: '${Rating}',
                    type: 'task'
                },
                {
                    id: 11,
                    name: '${Service Period}',
                    type: 'feature'
                },
                {
                    id: 12,
                    name: '${DOJ}',
                    type: 'bug'
                },
                {
                    id: 13,
                    name: '${DOC}',
                    type: 'feature'
                },
                {
                    id: 14,
                    name: '${DOR}',
                    type: 'feature'
                },
                {
                    id: 15,
                    name: '${DOL}',
                    type: 'bug'
                },
                {
                    id: 16,
                    name: '${RM Code}',
                    type: 'bug'
                },
                {
                    id: 17,
                    name: '${RM Name}',
                    type: 'feature'
                },
                {
                    id: 18,
                    name: '${Employee Category}',
                    type: 'bug'
                },
                {
                    id: 19,
                    name: '${Effective Date}',
                    type: 'feature'
                },
                {
                    id: 20,
                    name: '${CTC}',
                    type: 'feature'
                },
                {
                    id: 21,
                    name: '${Old CTC}',
                    type: 'feature'
                },
                {
                    id: 22,
                    name: '${New CTC}',
                    type: 'bug'
                },
                {
                    id: 23,
                    name: '${Old Basic}',
                    type: 'feature'
                },
                {
                    id: 24,
                    name: '${New Basic}',
                    type: 'feature'
                },
                {
                    id: 25,
                    name: '${Old HRA}',
                    type: 'feature'
                },
                {
                    id: 26,
                    name: '${New HRA}',
                    type: 'feature'
                },
                {
                    id: 27,
                    name: '${Old Special}',
                    type: 'feature'
                },
                {
                    id: 28,
                    name: '${New Special}',
                    type: 'feature'
                },
                {
                    id: 29,
                    name: '${Old PF}',
                    type: 'feature'
                },
                {
                    id: 30,
                    name: '${Old CTC per month}',
                    type: 'feature'
                },
                {
                    id: 31,
                    name: '${New CTC per month}',
                    type: 'feature'
                },
                {
                    id: 32,
                    name: '${Old Incentive}',
                    type: 'feature'
                },
                {
                    id: 33,
                    name: '${New Incentive}',
                    type: 'feature'
                },
                {
                    id: 34,
                    name: '${Old Total CTC}',
                    type: 'feature'
                },
                {
                    id: 35,
                    name: '${New Total CTC}',
                    type: 'feature'
                },
                {
                    id: 36,
                    name: '${New Monthly Gross Salary}',
                    type: 'feature'
                },
                {
                    id: 37,
                    name: '${Casual Leave}',
                    type: 'feature'
                },
                {
                    id: 38,
                    name: '${Sick Leave}',
                    type: 'feature'
                },
                {
                    id: 39,
                    name: '${Name of Supervisor}',
                    type: 'feature'
                },
                {
                    id: 40,
                    name: '${Supervisor Designation}',
                    type: 'feature'
                },
                {
                    id: 41,
                    name: '${Basic Salary}',
                    type: 'feature'
                },
                {
                    id: 42,
                    name: '${House Rent Allowance}',
                    type: 'feature'
                },
                {
                    id: 43,
                    name: '${Medical Allowance}',
                    type: 'feature'
                },
                {
                    id: 44,
                    name: '${Leave Travel Allowance}',
                    type: 'feature'
                },
                {
                    id: 45,
                    name: '${Additional Benefits}',
                    type: 'feature'
                },
                {
                    id: 46,
                    name: '${Performance Incentive}',
                    type: 'feature'
                },
                {
                    id: 47,
                    name: '${PF Contribution}',
                    type: 'feature'
                },
                {
                    id: 48,
                    name: '${ESI Contribution}',
                    type: 'feature'
                },
                {
                    id: 49,
                    name: '${Stock Option}',
                    type: 'feature'
                },
                {
                    id: 50,
                    name: '${Car}',
                    type: 'feature'
                },
                {
                    id: 51,
                    name: '${Telephone}',
                    type: 'feature'
                }
            ];

            // Returns (through its callback) the suggestions for the current query.
            function dataCallback(matchInfo, callback) {
                // Remove the '#' tag and convert to lowercase for better matching.
                var query = matchInfo.query.substring(1).toLowerCase();

                // Simple search.
                // Filter the items array and normalize the names by removing `${}` and converting to lowercase.
                var suggestions = itemsArray.filter(function (item) {
                    // Remove "${" and "}" from item name and convert it to lowercase for case-insensitive comparison.
                    var normalizedName = item.name.replace("${", "").replace("}", "").toLowerCase();
                    // Check if the query is included anywhere in the name.
                    return normalizedName.includes(query);
                });

                // Pass the suggestions to the callback.
                callback(suggestions);
            }

            config.dataCallback = dataCallback;

            // Define the templates of the autocomplete suggestions dropdown and output text.
            config.itemTemplate = '<li data-id="{id}" class="issue-{type}">{name}</li>';

            config.outputTemplate = '{name}';

            // Attach autocomplete to the editor.
            new CKEDITOR.plugins.autocomplete(editor, config);
        });
    }
});
