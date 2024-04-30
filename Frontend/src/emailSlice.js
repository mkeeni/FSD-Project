import { createSlice } from "@reduxjs/toolkit";

const emailSlice = createSlice({
    name: "email",
    initialState: "",
    reducers:{
        updateEmail: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateEmail } = emailSlice.actions;
export default emailSlice.reducer;