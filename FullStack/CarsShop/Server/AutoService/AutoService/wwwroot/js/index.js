let mobileMenuLink = document.querySelector('.menu__borders');
let mobileMenu = document.querySelector('.mobile-menu__items');

mobileMenuLink.addEventListener('click', (event) => {
    mobileMenu.classList.toggle('mobile-menu__items--hidden');

    event.preventDefault();
});

let mobileMenuItems = document.querySelector('.mobile-menu__items');

mobileMenuItems.addEventListener('click', (event) => {
    if (event.target.classList.contains('mobile-menu__link')){
        let mobileMenuLink = event.target;

        let mobileSubmenu = mobileMenuLink.parentElement.lastElementChild;
        if (mobileSubmenu.classList.contains('item__mobile-submenu'))
            mobileSubmenu.classList.toggle('item__mobile-submenu--hidden');
    }

    if (event.target.classList.contains('mobile-submenu-item__link')) {
        let mobileMenuLink = event.target;

        let mobileSubmenu = mobileMenuLink.parentElement.lastElementChild;
        if (mobileSubmenu.classList.contains('item__mobile-side-submenu'))
            mobileSubmenu.classList.toggle('item__mobile-side-submenu--hidden');
    }
});