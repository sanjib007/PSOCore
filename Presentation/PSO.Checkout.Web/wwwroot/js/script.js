function card_format(value) {
  var v = value.replace(/\s+/g, "").replace(/[^0-9]/gi, "");
  var matches = v.match(/\d{4,16}/g);
  var match = (matches && matches[0]) || "";
  var parts = [];
  for (i = 0, len = match.length; i < len; i += 4) {
    parts.push(match.substring(i, i + 4));
  }
  if (parts.length) {
    return parts.join(" ");
  } else {
    return value;
  }
}

document.getElementById("card_number").oninput = function () {
  this.value = card_format(this.value);
};

function ex_format(value) {
  var v = value.replace(/\s+/g, "").replace(/[^0-9]/gi, "");
  var matches = v.match(/\d{2,4}/g);
  var match = (matches && matches[0]) || "";
  var parts = [];
  for (i = 0, len = match.length; i < len; i += 2) {
    parts.push(match.substring(i, i + 2));
  }
  if (parts.length) {
    return parts.join(" / ");
  } else {
    return value;
  }
}

document.getElementById("card_date").oninput = function () {
  this.value = ex_format(this.value);
};

function checkDigit(event) {
  var code = event.which ? event.which : event.keyCode;

  if ((code < 48 || code > 57) && code > 31) {
    return false;
  }

  return true;
}


