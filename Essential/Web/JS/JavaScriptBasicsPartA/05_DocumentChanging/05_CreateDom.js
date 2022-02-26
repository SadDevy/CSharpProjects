function createTree(container, obj) {
    container.append(createTreeDom(obj));
  }

  function createTreeDom(obj) {
    // если нет дочерних элементов, то вызов возвращает undefined
    // и элемент <ul> не будет создан
    if (!Object.keys(obj).length) return;

    let ul = document.createElement('ul');

    for (let key in obj) {
      let li = document.createElement('li');
      li.innerHTML = key;

      let childrenUl = createTreeDom(obj[key]);
      if (childrenUl) {
        li.append(childrenUl);
      }

      ul.append(li);
    }

    return ul;
  }