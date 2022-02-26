container.onclick = (event) => {
    var target = event.target;
    if (target.className === 'remove-button')
        target.parentElement.hidden = true;
};