function highlightLink(link) {
    // Remove the "selected" class from all links
    var links = document.querySelectorAll('.nav-link');
    for (var i = 0; i < links.length; i++) {
        links[i].classList.remove('selected');
    }

    // Add the "selected" class to the clicked link
    link.classList.add('selected');

    // Store the ID of the selected link in local storage
    localStorage.setItem('selectedLink', link.getAttribute('id'));
}
