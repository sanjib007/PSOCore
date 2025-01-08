
function focusElementById (id) {
    const element = document.getElementById(id);
    element.focus();
}

function FormSubmitWithRedirect(url, parameters) {
    // Create a form element
    var form = document.createElement('form');
    form.setAttribute('method', 'post'); // Assuming you want to submit via POST method
    form.setAttribute('action', url); // Set your action URL

    // Iterate over the dictionary and create hidden input fields
    for (var key in parameters) {
        if (parameters.hasOwnProperty(key)) {
            var input = document.createElement('input');
            input.setAttribute('type', 'hidden');
            input.setAttribute('name', key);
            input.setAttribute('value', parameters[key]);
            form.appendChild(input);
        }
    }

    // Append the form to the document body and submit it
    document.body.appendChild(form);
    form.submit();

}

function completeCallback(response) {
    console.log('completeCallback response: ', response);
    console.log('resultIndicator: ', response.resultIndicator);
    console.log('sessionVersion: ', response.sessionVersion);
}
function beforeRedirectCallback(response) {
    console.log('beforeRedirectCallback response: ', response);
}
function afterRedirectCallback(response) {
    console.log('afterRedirectCallback response: ', response);
}
function cancelCallback(response) {
    confirm('Are you sure you want to cancel?');
    console.log('cancelCallback response: ', response);
}

function MastercardRedirect(sessionId) {

    Checkout.configure({
        session: {
            id: sessionId
        }
    });
    Checkout.showPaymentPage();
};