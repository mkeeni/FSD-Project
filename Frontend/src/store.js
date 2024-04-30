import { configureStore } from "@reduxjs/toolkit";
import loanReducer from "./loanSlice";
import transactionReducer from "./transactionSlice";
import accountReducer from "./accountSlice";
import statementReducer from "./statementSlice";
import accountNumberReducer from "./accountNumberSlice";
import dateReducer from "./dateSlice";
import customerReducer from "./customerSlice";
import employeeReducer from "./employeeSlice";
import emailReducer from "./emailSlice";
import beneficiaryReducer from "./beneficiarySlice";

const store = configureStore({
    reducer: {
        loanID: loanReducer,
        transactionID: transactionReducer,
        accountID: accountReducer,
        statement: statementReducer,
        accountNumber: accountNumberReducer,
        date: dateReducer,
        customerID: customerReducer,
        employeeID: employeeReducer,
        email: emailReducer,
        beneficiaryID: beneficiaryReducer
    },
});

export default store;