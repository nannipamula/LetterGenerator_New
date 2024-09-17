CKEDITOR.plugins.add('image-dropdown', {
    icons: 'image-dropdown',
    init: function (editor) {
        console.log('init function called');
        // Add a button to the toolbar
        editor.ui.addButton('ImageDropdown', {
            label: 'Insert Image',
            command: 'imageDropdownCommand',
            toolbar: 'insert'
        });

        // Add a command for the button
        editor.addCommand('imageDropdownCommand', {
            exec: function (editor) {
                console.log('imageDropdownCommand function called'); // <-- add this line
                // Create a dropdown menu
                var dropdown = new CKEDITOR.ui.dialog.select({
                    id: 'imageDropdown',
                    label: 'Insert Image',
                    title: 'Insert Image',
                    items: [
                        { label: 'Image 1', value: 'image1.jpg' },
                        { label: 'Image 2', value: 'image2.jpg' },
                        { label: 'Image 3', value: 'image3.jpg' },
                    ],
                    onOk: function () {
                        // Insert the selected image into the editor
                        editor.insertHtml('<img src="' + this.getValue() + '">');
                    }
                });

                // Show the dropdown menu
                editor.openDialog(dropdown);
            }
        });

        // Set a keyboard shortcut for the command
        editor.setKeystroke(CKEDITOR.CTRL + 83, 'imageDropdownCommand');

        // Add a listener for the keyup event
        editor.on('keyup', function (event) {
            // Check if the user typed "$"
            if (event.data.keyCode === 52 && event.data.$.shiftKey) {
                console.log('$ detected');
                // Call the imageDropdownCommand function
                editor.execCommand('imageDropdownCommand');
            }
        });
    }


});
