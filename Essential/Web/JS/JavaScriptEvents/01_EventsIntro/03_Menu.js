var title = document.getElementById('menu-title');
title.addEventListener('click', () => {
    var ulElement = document.querySelector('ul');
    ulElement.hidden = !ulElement.hidden;
});