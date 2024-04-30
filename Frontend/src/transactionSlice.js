import { createSlice } from "@reduxjs/toolkit";

const transactionSlice = createSlice({
    name: "transaction",
    initialState: 0,
    reducers:{
        updatetransactionID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updatetransactionID } = transactionSlice.actions;
export default transactionSlice.reducer;