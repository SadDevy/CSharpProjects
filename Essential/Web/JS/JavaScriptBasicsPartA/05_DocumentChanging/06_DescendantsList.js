var liElements = document.querySelectorAll('li');
for (var elem of liElements) {
  if (elem.querySelectorAll('li').length) {
    elem.firstChild.data += ` [${elem.querySelectorAll('li').length}]`;
  }
}