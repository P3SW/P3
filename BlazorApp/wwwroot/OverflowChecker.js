function isOverflown(element) {
    return element.scrollHeight > element.clientHeight || element.scrollWidth > element.clientWidth;
}

var els = document.getElementsByClassName('demos');
for (var i = 0; i < els.length; i++) {
    var el = els[i];
    el.style.borderColor = (isOverflown(el) ? 'red' : 'green');
    console.log("Element #" + i + " is " + (isOverflown(el) ? '' : 'not ') + "overflown.");
}