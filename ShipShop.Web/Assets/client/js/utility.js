function removeOrderDetail(elem) {
    $(elem).closest('tr').remove();
}


function AddDotsToNumber(num) {
    var num1, num2, sum;
    if (num.indexOf(',') > -1) {
        var splitNum = num.split(',');
        num1 = splitNum[0];
        num2 = splitNum[1];
    }
    else {
        num1 = num;
    }
    num1 = num1.toString().replace(/\./g, '');
    num1 = num1.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    if (typeof (num2) != 'undefined') {
        return num1 + ',' + num2;
    }
    else {
        return num1;
    }
}
function CheckPhoneNo(input) {
    if (isNaN(input) || input.length < 10) {
        return false;
    } else {
        return true;
    }
}