
/* Extensão para permitir somente números */
jQuery.fn.ForceNumericOnly =
 function () {
     return this.each(function () {
         $(this).keydown(function (e) {
             var key = e.charCode || e.keyCode || 0;
             // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
             // home, end, period, and numpad decimal
             return (
                 key == 8 ||
                 key == 9 ||
                 key == 13 ||
                 key == 46 ||
                 key == 110 ||
                 key == 190 ||
                 (key >= 35 && key <= 40) ||
                 (key >= 48 && key <= 57) ||
                 (key >= 96 && key <= 105));
         });
     });
 };

jQuery.fn.ForceNumericAndMinusOnly =
 function () {
     return this.each(function () {
         $(this).keydown(function (e) {
             var key = e.charCode || e.keyCode || 0;
             // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
             // home, end, period, and numpad decimal
             return (
                 key == 109 ||
                  key == 173 ||
                 key == 8 ||
                 key == 9 ||
                 key == 13 ||
                 key == 46 ||
                 key == 110 ||
                 key == 190 ||
                 (key >= 35 && key <= 40) ||
                 (key >= 48 && key <= 57) ||
                 (key >= 96 && key <= 105));
         });
     });
 };

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

Number.prototype.padLeft = function (n, str) {
    return Array(n - String(this).length + 1).join(str || '0') + this;
}
String.prototype.padLeft = function (n, str) {
    return Array(n - String(this).length + 1).join(str || '0') + this;
}
String.prototype.insertAt = function (index, string) {
    return this.substr(0, index) + string + this.substr(index);
}
Date.isLeapYear = function (year) {
    return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
};

Date.getDaysInMonth = function (year, month) {
    return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
};

Date.prototype.isLeapYear = function () {
    return Date.isLeapYear(this.getFullYear());
};

Date.prototype.getDaysInMonth = function () {
    return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
};

Date.prototype.addMonths = function (value) {
    var n = this.getDate();
    this.setDate(1);
    this.setMonth(this.getMonth() + value);
    this.setDate(Math.min(n, this.getDaysInMonth()));
    return this;
};

Date.monthsDiff = function (day1, day2) {
    var d1 = day1, d2 = day2;
    if (day1 < day2) {
        d1 = day2;
        d2 = day1;
    }
    var m = (d1.getFullYear() - d2.getFullYear()) * 12 + (d1.getMonth() - d2.getMonth());
    if (d1.getDate() < d2.getDate())--m;
    return m;
}

function exibirTooltip(obj, strMessage) {
    $(obj).tooltip({
        title: strMessage,
        show: '500',
        placement: 'top',
        trigger: 'manual'
    }).tooltip('show');
}

function validarData(value) {
    if (value.length !== 10) return false;
    // verificando data
    var data = value;
    var dia = data.substr(0, 2);
    var barra1 = data.substr(2, 1);
    var mes = data.substr(3, 2);
    var barra2 = data.substr(5, 1);
    var ano = data.substr(6, 4);
    if (data.length != 10 || barra1 != "/" || barra2 != "/" || isNaN(dia) || isNaN(mes) || isNaN(ano) || dia > 31 || mes > 12) return false;
    if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31) return false;
    if (mes == 2 && (dia > 29 || (dia == 29 && ano % 4 != 0))) return false;
    if (ano < 1900) return false;
    return true;
}

function convertToDate(strDDMMYYYY) {
    var parts = strDDMMYYYY.split('/');
    var mydate = new Date(parts[2], parts[1] - 1, parts[0]);
    return mydate;
}

function formattedDate(date) {
    var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    year = parseInt(year).padLeft(4, '0');

    return [day, month, year].join('/');
}

function convertStrDateBrToDateUS(strDDMMYYYY) {
    var parts = strDDMMYYYY.split('/');
    var myStrDate = parts[1] + '/' + parts[0] + '/' + parts[2];
    return myStrDate;
}

/*Utilitarios*/
function makeId(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}


/*Limpa o formulário de cadastro*/
function clearForm() {
    $(':input').not(':button, :submit, :reset, :hidden, :checkbox, :radio').val('');
    $(':checkbox, :radio').prop('checked', false);
}

function isValidMail(sEmail) {
    var validMail = false;
    var emailFilter = /^.+@.+\..{2,}$/;
    var illegalChars = /[\(\)\<\>\,\;\:\\\/\"\[\]]/

    if (!(emailFilter.test(sEmail)) || sEmail.match(illegalChars)) {
        validMail = false;
    } else {
        validMail = true;
    }
    return validMail;
}

$.strLPAD = function (i, l, s) {
    var o = i.toString();
    if (!s) { s = '0'; }
    while (o.length < l) {
        o = s + o;
    }
    return o;
};

function redirect(url) {
    var ua = navigator.userAgent.toLowerCase(),
        isIE = ua.indexOf('msie') !== -1,
        version = parseInt(ua.substr(4, 2), 10);

    // Internet Explorer 8 and lower
    if (isIE && version < 9) {
        var link = document.createElement('a');
        link.href = url;
        document.body.appendChild(link);
        link.click();
    }

        // All other browsers can use the standard window.location.href (they don't lose HTTP_REFERER like Internet Explorer 8 & lower does)
    else {
        window.location.href = url;
    }
}


