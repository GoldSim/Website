/**
 * (BRAINTREE) PAYMENTS
 * @file Defines inialization and Braintree Javascript SDK (v3) communication funcationality for the Braintree Hosted Fields
 * form.
 */

function executeBraintree(clientToken) {

  var form = document.querySelector('#PaymentsForm');
  var submit = document.querySelector('button[type="submit"]');

  var clientInstance = braintree.client.create({
    authorization: clientToken
  }, function (clientError, clientInstance) {

    if (clientError) {
      console.error(clientError);
      return;
    }

    braintree.hostedFields.create({
      client: clientInstance,
      styles: {
        'input': {
          'font-family': 'Helvetica, Roboto, Arial, sans-serif',
          'font-size': '.9375rem',
          'line-height': '1.5',
          'color': 'rgb(32, 32, 32)'
        }
      },
      fields: {
        number: {
          selector: '#CardNumber',
          placeholder: '1111 1111 1111 1111'
        },
        cvv: {
          type: 'password',
          selector: '#Cvv',
          placeholder: '123',
          maskInput: {
            character: '*'
          }
        },
        expirationMonth: {
          selector: '#ExpirationMonth',
          placeholder: '01'
        },
        expirationYear: {
          selector: '#ExpirationYear',
          placeholder: '2021'
        },
        postalCode: {
          selector: '#PostalCode',
          placeholder: '12345'
        }
      }
    }, function (hostedFieldsError, hostedFieldsInstance) {

      if (hostedFieldsError) {
        console.error(hostedFieldsError);
        return;
      }

      submit.removeAttribute('disabled');

      form.addEventListener('submit', function (event) {
        event.preventDefault();

        hostedFieldsInstance.tokenize(function (tokenizeError, payload) {
          if (tokenizeError) {
            console.error(tokenizeError);

            switch (tokenizeError.code) {

              case 'HOSTED_FIELDS_FIELDS_EMPTY':
                // occurs when none of the fields are filled in
                $('label.hosted-field-label.required').addClass('is-invalid-label');
                $('span.hosted-field.required').addClass('is-invalid-input');
                $('#EmptyFieldsError').removeClass('is-hidden');
                $('#ClientSideErrors').removeClass('is-hidden');
                break;

              case 'HOSTED_FIELDS_FIELDS_INVALID':
                // occurs when certain fields do not pass client side validation
                $.each(tokenizeError.details.invalidFieldKeys, function (index, key) {
                  if (key === 'number') {
                    $('#CCNumberError').removeClass('is-hidden');
                  }
                  if (key === 'cvv') {
                    $('#CvvError').removeClass('is-hidden');
                  }
                  if (key === 'postalCode') {
                    $('#PostalCodeError').removeClass('is-hidden');
                  }
                });
                $.each(tokenizeError.details.invalidFields, function (fieldContainer, element) {
                  var containerElement = element.getAttribute('id');
                  $('label[for="' + containerElement + '"]').addClass('is-invalid-label');
                  $('span#' + containerElement).addClass('is-invalid-input');
                  if (containerElement.startsWith('Expiration')) {
                    $('label[for="Expiration"]').addClass('is-invalid-label');
                  }
                });
                break;

              case 'HOSTED_FIELDS_FAILED_TOKENIZATION':
                // occurs for any other tokenization error on the server
                console.error('Tokenization failed server side. Is the card valid?');
                break;

              case 'HOSTED_FIELDS_TOKENIZATION_NETWORK_ERROR':
                // occurs when the Braintree gateway cannot be contacted
                console.error('Network error occurred when tokenizing.');
                break;

              default:
                console.error(tokenizeError);
            }

            $('#ClientSideErrors').removeClass('is-hidden');

            return;
          }

          document.querySelector('#PaymentMethodNonce').value = payload.nonce;
          form.submit();
        });
      }, false);

    });
  });

}