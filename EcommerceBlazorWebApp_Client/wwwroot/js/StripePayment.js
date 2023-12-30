redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_ITaEfShLUJlOPcX6mP6eOKj5");
    stripe.redirectToCheckout({ sessionId: sessionId });
}