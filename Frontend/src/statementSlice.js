import { createSlice } from "@reduxjs/toolkit";

const statementSlice = createSlice({
    name: "statement",
    initialState: {
        "accountID": 0,
        "fromDate": "2024-01-01",
        "toDate": "2024-02-01"
    },
    reducers:{
        updateStatement: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateStatement } = statementSlice.actions;
export default statementSlice.reducer;