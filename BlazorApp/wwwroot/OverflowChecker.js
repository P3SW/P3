/* Check if an element is overflowing its parent */
export function isOverflown(element) {
    return element.scrollHeight > element.clientHeight || element.scrollWidth > element.clientWidth;
}

/* Automatically checks all elements with a given classname for overflow */
var elements = document.getElementsByClassName('overflowCheck');
for (var i = 0; i < elements.length; i++) {
    var element = elements[i];
    element.style.color = (isOverflown(element) ? 'red' : 'green');
    console.log("Element #" + i + " is " + (isOverflown(element) ? '' : 'not ') + "overflown.");
}