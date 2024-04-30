import { createSlice } from "@reduxjs/toolkit";

const employeeSlice = createSlice({
    name: "employee",
    initialState: 0,
    reducers:{
        updateEmployeeID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateEmployeeID } = employeeSlice.actions;
export default employeeSlice.reducer;