function MessageBox(text, type, modal, location) {

    if (type == null)
        type = "alert";
    if (modal == null)
        modal = false;
    if (location == null)
        location = "topCenter";

    var n = noty({
        text: text,
        type: type,
        dismissQueue: true,
        closeWith: ['click', 'backdrop'],
        modal: true,
        layout: location,
        theme: 'defaultTheme',
        maxVisible: 10
    });
    console.log('html: ' + n.options.id);
}