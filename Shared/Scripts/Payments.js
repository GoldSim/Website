/**
 * (BRAINTREE) PAYMENTS
 * @file Defines inialization and Braintree Javascript SDK (v3) communication funcationality for the Braintree Hosted Fields
 * form.
 */

function executeBraintree(clientToken) {

  /**
   * Establish variables
   */
  var
    form        = document.querySelector('#PaymentsForm'),
    submit      = document.querySelector('button[type="submit"]');

  /**
   * Initialize Braintree client instance
   */
  var clientInstance = braintree.client.create({
    authorization: clientToken
  },
  function (clientError, clientInstance) {

    /**
      * Exit if there is an error creating the client instance
      */
    if (clientError) {
      console.error(clientError);
      return;
    }

    /**
     * Initialize Braintree Hosted Fields configuration
     */
    braintree.hostedFields.create({
      client: clientInstance,
      styles: {
        'input': {
          'font-family'         : 'Helvetica, Roboto, Arial, sans-serif',
          'font-size'           : '.9375rem',
          'line-height'         : '1.5',
          'color'               : 'rgb(32, 32, 32)'
        }
      },
      fields: {
        number: {
          selector              : '#CardNumber',
          placeholder           : '1111 1111 1111 1111'
        },
        cvv: {
          selector              : '#Cvv',
          placeholder           : '123',
          maskInput             : {
            character           : '*'
          }
        },
        expirationMonth: {
          selector              : '#ExpirationMonth',
          placeholder           : '01'
        },
        expirationYear: {
          selector              : '#ExpirationYear',
          placeholder           : '2021'
        },
        postalCode: {
          selector              : '#PostalCode',
          placeholder           : '12345'
        }
      }
    },
    function (hostedFieldsError, hostedFieldsInstance) {

      /**
       * Exit if there is an error creating the Hosted Fields
       */
      if (hostedFieldsError) {
        console.error(hostedFieldsError);
        return;
      }

      /**
       * Enable submit button once Hosted Fields are created
       */
      submit.removeAttribute('disabled');

      /**
       * Handle form submission, including errors
       */
      form.addEventListener('submit', function (event) {
        event.preventDefault();

        hostedFieldsInstance.tokenize(function (tokenizeError, payload) {
          if (tokenizeError) {
            console.error(tokenizeError);

            switch (tokenizeError.code) {

              // Occurs when ALL fields are empty
              case 'HOSTED_FIELDS_FIELDS_EMPTY':
                $('label.hosted-field-label.required').addClass('is-invalid-label');
                $('span.hosted-field.required').addClass('is-invalid-input');
                $('#EmptyFieldsError').removeClass('is-hidden');
                $('#ClientSideErrors').removeClass('is-hidden');
                break;

              // Occurs when certain fields do not pass client side validation
              case 'HOSTED_FIELDS_FIELDS_INVALID':
                // Expose client-side error messaging depending on the erring field(s)
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
                // Set error styles for erring fields and their labels
                $.each(tokenizeError.details.invalidFields, function (fieldContainer, element) {
                  var containerElement = element.getAttribute('id');
                  $('label[for="' + containerElement + '"]').addClass('is-invalid-label');
                  $('span#' + containerElement).addClass('is-invalid-input');
                  if (containerElement.startsWith('Expiration')) {
                    $('label[for="Expiration"]').addClass('is-invalid-label');
                  }
                });
                break;

              // Occurs for any other tokenization error on the server
              case 'HOSTED_FIELDS_FAILED_TOKENIZATION':
                console.error('Tokenization failed server side. Is the card valid?');
                break;

              // Occurs when the Braintree gateway cannot be contacted
              case 'HOSTED_FIELDS_TOKENIZATION_NETWORK_ERROR':
                console.error('Network error occurred when tokenizing.');
                break;

              default:
                console.error(tokenizeError);
            }

            // Expose the error messages
            $('#ClientSideErrors').removeClass('is-hidden');

            return;
          }

          // Set the payment nonce hidden field value
          document.querySelector('#PaymentMethodNonce').value   = payload.nonce;

          // Submit the form
          form.submit();

        });
      }, false);

    });
  });

}