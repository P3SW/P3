/* Check if an element is overflowing its parent */
export function isOverflown(element) {
    return element.scrollHeight > element.clientHeight || element.scrollWidth > element.clientWidth;
}

/* Automatically checks all elements with a given classname for overflow */
var elements = document.getElementsByClassName('overflowCheck');
for (var i = 0; i < elements.length; i++) {
    var element = elements[i];
    console.log("Element #" + i + " is " + (isOverflown(element) ? '' : 'not ') + "overflown.");
}

/*** PASTE THIS IN @Code OF A .razor COMPONONENT YOU WANT TO CHECK FOR OVERFLOW ***
JSObjectReference module;
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        module = (JSObjectReference) await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./OverflowChecker.js");
    }
}*/