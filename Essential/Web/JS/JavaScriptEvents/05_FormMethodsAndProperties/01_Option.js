let selectedItem = document.querySelector('select');
let selectedValue = selectedItem.options[selectedItem.selectedIndex].value;
let selectedText = selectedItem.options[selectedItem.selectedIndex].firstChild;

let newOption = new Option('Классика', 'classic');
selectedItem.append(newOption);
selectedItem.selectedIndex = 2;