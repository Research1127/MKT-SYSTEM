# Checklist: Automatic Payment Status with Payment History

1. Create Payment History Structure
- Create a Payments table that records every payment transaction.
- Each payment should include the student, month, year, amount paid, and payment date.
- Do not limit payments to one record per month because a student may pay multiple times.

---

2. Define Monthly Fee
- Ensure there is a defined monthly fee amount for each student or class.
- This value will be used to determine how much the student should pay each month.

---

3. Record Payments as Transactions
- Every time a user makes a payment, create a new payment record in the Payments table.
- Do not update existing payment records.
- This allows the system to keep a complete payment history.

---

4. Calculate Total Payment for a Month
- When retrieving payment information, the backend should calculate the total amount paid by summing all payment records for the same student, month, and year.

---

5. Calculate Outstanding Amount
- The outstanding amount should be calculated by subtracting the total amount paid from the monthly fee.

---

6. Determine Payment Status Automatically
- The payment status should not be manually stored in the database.
- The backend should determine the status automatically based on the payment amount:
- If no payment has been made → Status is Unpaid
- If some payment has been made but the full amount is not reached → Status is Partially Paid
- If the total payment meets or exceeds the required amount → Status is Paid

---

7. Return Calculated Values in API
- When returning payment information through the API, the backend should include:
- Due amount
- Total amount paid
- Outstanding amount
- Payment status

These values should be calculated dynamically, not stored permanently in the database.

---

8. Ensure System Handles Multiple Payments
- The system should support multiple payments within the same month.
- Each payment should automatically update the calculated totals when the data is retrieved.

---

9. Maintain Data Accuracy
- The database should only store actual payment transactions.
- All summary values such as totals, outstanding amounts, and payment status should always be calculated by the backend to prevent inconsistent data.

---

This checklist will allow the payment system to:
- Support multiple payments per month
- Automatically determine payment status
- Keep accurate payment history
- Ensure the system remains scalable and maintainable.