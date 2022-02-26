var liElements = document.querySelectorAll('li');
for (var li of liElements) {
    alert(li.firstChild.data + ': ' + li.querySelectorAll('li').length);
}