import { createSlice } from "@reduxjs/toolkit";

const customerSlice = createSlice({
    name: "customer",
    initialState: 0,
    reducers:{
        updateCustomerID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateCustomerID } = customerSlice.actions;
export default customerSlice.reducer;