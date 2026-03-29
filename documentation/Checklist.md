1. Add Logic To handle isFullyPaid and CanSubmitForm
   var outstanding = totalFee - totalPaid;
   var isFullyPaid = outstanding <= 0;
   var canSubmitForm = isFullyPaid;
2. Edit PaymentResponseDto