import { createSlice } from "@reduxjs/toolkit";

const loanSlice = createSlice({
    name: "loan",
    initialState: 0,
    reducers:{
        updateLoanID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateLoanID } = loanSlice.actions;
export default loanSlice.reducer;