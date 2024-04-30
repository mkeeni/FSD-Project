import { createSlice } from "@reduxjs/toolkit";

const accountNumberSlice = createSlice({
    name: "accountNumber",
    initialState: "",
    reducers:{
        updateAccountNumber: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateAccountNumber } = accountNumberSlice.actions;
export default accountNumberSlice.reducer;