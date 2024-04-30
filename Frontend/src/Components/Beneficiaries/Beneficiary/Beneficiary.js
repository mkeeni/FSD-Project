import axios from 'axios';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { updateBeneficiaryID } from '../../../beneficiarySlice';

function Beneficiary(props){

    var dispatch = useDispatch();
    var beneficiaryID = useSelector((state) => state.beneficiaryID);

    var beneficiary = {
        "beneficiaryID": 0,
        "accountNumber": 0,
        "name": "string",
        "status": "string",
        "branchID": 0,
        "customerID": 0
    }

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function deleteBeneficiary(){
        await axios.put('http://localhost:7289/api/Beneficiaries/UpdateDeleteBeneficiary?beneficiaryID=' + beneficiaryID,beneficiary,httpHeader)
        .then(function (response) {
            console.log(response.data);
            showToast();
        })
        .catch(function (error) {
            console.log(error);
        }) 
    }

    function updateBeneficiaryid(){
        dispatch(
            updateBeneficiaryID(props.beneficiary.beneficiaryID)
        )
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="whiteOutlineBox1">
            <div className="whiteOutlineBoxMargin">
                <div className="smallBox23">
                    <span className="clickRegisterText">{props.beneficiary.name}</span>
                    <span className="pointer" data-bs-toggle="modal" data-bs-target="#modal1" onClick={updateBeneficiaryid}>
                        <div className="delete change-my-color2"></div>
                    </span>
                </div>
                <span className="clickRegisterText">Acc No {props.beneficiary.accountNumber}</span>
                <span className="clickRegisterText">{props.beneficiary.branches.banks.bankName}</span>
                <span className="clickRegisterText">IFSC: {props.beneficiary.branches.ifscNumber}</span>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h6 className="modal-title" id="modalEg1">Delete Beneficiary</h6>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            Are you sure you want to delete this beneficiary?
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal"  onClick={deleteBeneficiary}>Delete</button>
                        </div>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                <div className="d-flex">
                    <div className="toast-body">
                        Beneficiary Deleted Successfully
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        </div>
    );
}

export default Beneficiary;