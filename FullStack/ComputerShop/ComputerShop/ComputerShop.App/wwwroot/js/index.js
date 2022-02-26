const basketGoodsList = document.querySelectorAll('.basket__goods');

const calculateTotalPrice = () => {
    let totalSum = 0;
    for (let basketGoods of basketGoodsList) {
        const count = basketGoods.querySelector('.basket-goods__count-content').textContent;
        const priceText = basketGoods.querySelector('.basket-goods__price').textContent;
        const price = priceText.substring(0, priceText.length - 2);

        totalSum += count * price;
    }

    const totalPrice = document.querySelector('.total-price');
    totalPrice.textContent = totalSum;
}

calculateTotalPrice();
document.addEventListener('click', (event) => {
    const target = event.target;
    if (target.hasAttribute('data-min')) {
        const btnContainer = target.parentElement.parentElement.parentElement;
        const decreaseBtn = btnContainer.querySelector('.basket-goods__count-decrease-btn');

        const minData = decreaseBtn.getAttribute("data-min");
        const content = btnContainer.querySelector('.basket-goods__count-content');

        let contentValue = content.textContent;
        if (contentValue > minData) {
            content.textContent = contentValue - 1;
        }
    }

    if (target.hasAttribute('data-max')) {
        const btnContainer = target.parentElement.parentElement.parentElement;
        const increaseBtn = btnContainer.querySelector('.basket-goods__count-increase-btn');

        const maxData = increaseBtn.getAttribute("data-max");
        const content = btnContainer.querySelector('.basket-goods__count-content');

        let contentValue = content.textContent;
        if (contentValue < maxData) {
            content.textContent = +contentValue + 1;
        }
    }

    calculateTotalPrice();
});
