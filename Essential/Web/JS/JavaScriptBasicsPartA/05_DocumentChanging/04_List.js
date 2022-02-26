var ul = document.createElement('ul');
document.body.append(ul);

var text = '';
while ((text = prompt('Введите текст'))) {
    var li = document.createElement('li');
    li.textContent = text;

    ul.append(li);
}