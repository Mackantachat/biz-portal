String.prototype.includes = function (str) {
    var returnValue = false;

    if (this.indexOf(str) !== -1) {
        returnValue = true;
    }

    return returnValue;
}