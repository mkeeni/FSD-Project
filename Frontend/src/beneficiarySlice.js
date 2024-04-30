import { createSlice } from "@reduxjs/toolkit";

const beneficiarySlice = createSlice({
    name: "beneficiary",
    initialState: 0,
    reducers:{
        updateBeneficiaryID: (state, action) => {
            return action.payload;
        }
    }
})

export const { updateBeneficiaryID } = beneficiarySlice.actions;
export default beneficiarySlice.reducer;