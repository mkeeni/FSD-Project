import { createSlice } from "@reduxjs/toolkit";

const accountSlice = createSlice({
    name: "account",
    initialState: 0,
    reducers:{
        updateAccountID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateAccountID } = accountSlice.actions;
export default accountSlice.reducer;