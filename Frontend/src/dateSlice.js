import { createSlice } from "@reduxjs/toolkit";

const dateSlice = createSlice({
    name: "date",
    initialState: {
        "fromDate": "",
        "toDate": ""
    },
    reducers:{
        updateDate: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateDate } = dateSlice.actions;
export default dateSlice.reducer;