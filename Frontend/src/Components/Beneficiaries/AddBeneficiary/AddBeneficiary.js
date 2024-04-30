import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link } from 'react-router-dom';

function AddBeneficiary(){

    var [allBanks, setAllBanks] = useState([]);
    var [bankID, setBankID] = useState("");
    var [allBranches, setAllBranches] = useState([]);
    var [branchID,setBranchID] = useState("");
    var [beneficiaryAccountNumber,setBeneficiaryAccountNumber] = useState("");
    var [beneficiaryName,setBeneficiaryName] = useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    const customerID = sessionStorage.getItem('id');

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var newBeneficiary = {
        "accountNumber": beneficiaryAccountNumber,
        "name": beneficiaryName,
        "branchID": branchID,
        "customerID": customerID
    }

    useEffect(() => {
        getAllBanks();
    },[])

    async function getAllBanks(){
        await axios.get('http://localhost:7289/api/Banks/GetAllBanks',httpHeader)
        .then(function (response) {
            setAllBanks(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function changeBank(eventargs){
        if(eventargs.target.value === ""){
            setBankID(eventargs.target.value);
            setAllBranches([]);
            setBranchID("");
        }
        else{
            setBankID(eventargs.target.value);
            getAllBranches(eventargs.target.value);
        }
    }

    async function getAllBranches(changedBankID){
        await axios.get('http://localhost:7289/api/Branches/GetAllSpecificBranches?bankID=' + changedBankID,httpHeader)
        .then(function (response) {
            setAllBranches(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function addBeneficiary(){
        if(beneficiaryAccountNumber === "" || beneficiaryName === "" || bankID === "" || branchID === ""){
            alert("Please fill all the fields");
        }
        else{
            if(beneficiaryAccountNumber.length > 9 && beneficiaryAccountNumber.length < 14){
                if(beneficiaryName.length > 2 && beneficiaryName.length < 100){
                    await axios.post('http://localhost:7289/api/Beneficiaries/AddBeneficiary', newBeneficiary, httpHeader)
                    .then(function (response) {
                        setError(false);
                        showToast();
                    })
                    .catch(function (error) {
                        console.log(error);
                        setError(true);
                        setErrorMessage(error.response.data);
                    })
                }
                else{
                    alert("Beneficiary Name should be between 3 and 100 characters long");
                
                }
            }
            else{
                alert("Account number should be between 9 and 14 digits long")
            }
        }
    }

    function beneficiaryNameValidation(eventargs){
        var beneficiaryName = eventargs.target.value;
        setBeneficiaryName(beneficiaryName);
        if(beneficiaryName !== ""){
            if(beneficiaryName.length > 2 && beneficiaryName.length < 100){
                setError(false);
                if(beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("addBeneficiary").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Beneficiary Name should be between 3 and 100 characters long");
                document.getElementById("addBeneficiary").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter Beneficiary Name");
            document.getElementById("addBeneficiary").classList.add("disabled");
        }
    }

    function beneficiaryAccountNumberValidation(eventargs){
        var beneficiaryAccountNumber = eventargs.target.value;
        setBeneficiaryAccountNumber(beneficiaryAccountNumber);
        if(beneficiaryAccountNumber !== ""){
            if(beneficiaryAccountNumber.length > 9 && beneficiaryAccountNumber.length < 14){
                setError(false);
                if(beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("addBeneficiary").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Account number should be between 9 and 14 digits long");
                document.getElementById("addBeneficiary").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter Beneficiary Account Number");
            document.getElementById("addBeneficiary").classList.add("disabled");
        }
    }

    function bankValidation(eventargs){
        changeBank(eventargs);
        var bankID = eventargs.target.value;
        setBankID(bankID);
        if(bankID !== ""){
            setError(false);
            if(beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                document.getElementById("addBeneficiary").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select a Bank");
            document.getElementById("addBeneficiary").classList.add("disabled");
        }
    }

    function branchValidation(eventargs){
        var branchID = eventargs.target.value;
        setBranchID(branchID);
        if(branchID !== ""){
            setError(false);
            if(beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                document.getElementById("addBeneficiary").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select a Branch");
            document.getElementById("addBeneficiary").classList.add("disabled");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox41">
                    <ul className="smallBox49 nav">
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoOrange" to="/menu/customerBeneficiaries">All Beneficiaries</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoBlack" to="/menu/addBeneficiary">Add Beneficiary</Link>
                        </li>
                    </ul>
                    <div>
                        <span className="clickRegisterTextBlack">Beneficiary Name</span>
                        <input className="form-control enterDiv3" type="text" onChange={beneficiaryNameValidation}></input>
                    </div>
                    <div>
                        <span className="clickRegisterTextBlack">Account Number</span>
                        <input className="form-control enterDiv3" type="number" onChange={beneficiaryAccountNumberValidation}></input>
                    </div>
                    <div>
                        <span className="clickRegisterTextBlack">Bank Name</span>
                        <select className="form-control enterDiv3" value = {bankID} onChange={bankValidation}>
                            <option value="">Select</option>
                            {allBanks.map(bank => 
                                <option key={bank.bankID} value={bank.bankID}>{bank.bankName}</option>
                            )}
                        </select>
                    </div>
                    <div>
                        <span className="clickRegisterTextBlack">Branch Name with IFSC</span>
                        <select className="form-control enterDiv3" value = {branchID} onChange={branchValidation}>
                            <option value="">Select</option>
                            {allBranches.map(branch => 
                                <option key={branch.branchID} value={branch.branchID}>{branch.ifscNumber} -- {branch.branchName}</option>
                            )}
                        </select>
                    </div>
                    {error ? <div className='flexRow errorText'>{errorMessage}</div> : null}
                    <a id="addBeneficiary" className="btn btn-outline-success smallBox9 disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Add</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Add Beneficiary</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to add this beneficiary?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={addBeneficiary}>Add</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Beneficiary Added Successfully
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
            </div>
    );
}

export default AddBeneficiary;