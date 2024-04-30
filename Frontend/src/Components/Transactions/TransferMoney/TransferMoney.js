import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';

function TransferMoney(){

    var [allBanks, setAllBanks] = useState([]);
    var [bankID, setBankID] = useState("");
    var [allBranches, setAllBranches] = useState([]);
    var [branchID,setBranchID] = useState("");
    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");
    var [beneficiaries,setBeneficiaries] = useState([]);
    var [beneficiaryID,setBeneficiaryID] = useState("");
    var [addBeneficiary,setAddBeneficiary] = useState(false);
    var [amount,setAmount] = useState("");
    var [description,setDescription] = useState("");
    var [beneficiaryAccountNumber,setBeneficiaryAccountNumber] = useState("");
    var [beneficiaryName,setBeneficiaryName] = useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [accountsError,setAccountsError]= useState(false);

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var newTransfer = {
        "amount": amount,
        "description": description,
        "accountID": accountID,
        "beneficiaryID": beneficiaryID
    };

    var newTransferWithBeneficiary = {
        "amount": amount,
        "description": description,
        "accountID": accountID,
        "beneficiaryAccountNumber": beneficiaryAccountNumber,
        "beneficiaryName": beneficiaryName,
        "branchID": branchID,
        "customerID": customerID
    }

    useEffect(() => {
        const customerID = sessionStorage.getItem('id');
        getAllBanks();
        getAllCustomerAccounts(customerID);
        getAllCustomerBeneficiaries(customerID);
    },[])

    async function getAllCustomerAccounts(customerID){
        await axios.get('http://localhost:7289/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
        .then(function (response) {
            setAccounts(response.data);
            setAccountsError(false);
        })
        .catch(function (error) {
            console.log(error);
            setAccountsError(true);
            setErrorMessage(error.response.data);
        })
    }

    async function getAllCustomerBeneficiaries(customerID){
        await axios.get('http://localhost:7289/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=' + customerID,httpHeader).then(function (response) {
            setBeneficiaries(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })  
    }

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

    async function transferMoney(){
        if(amount === ""){
            alert("Please fill all fields");
        }
        else{
            if(amount > 0){
                if(description.length < 35){
                    if(addBeneficiary){
                        if(accountID === "" || beneficiaryAccountNumber === "" || beneficiaryName === "" || bankID === "" || branchID === ""){
                            alert("Please fill all fields");
                        }
                        else{
                            if(beneficiaryAccountNumber.length > 9 && beneficiaryAccountNumber.length < 14){
                                if(beneficiaryName.length > 2 && beneficiaryName.length < 100){
                                    await axios.post('http://localhost:7289/api/Transactions/TransferWithBeneficiary',newTransferWithBeneficiary,httpHeader).then(function (response) {
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
                    else{
                        if(accountID === "" || beneficiaryID === ""){
                            alert("Please fill all fields");
                        }
                        else{
                            await axios.post('http://localhost:7289/api/Transactions/Transfer',newTransfer,httpHeader).then(function (response) {
                                setError(false);
                                showToast();
                            })
                            .catch(function (error) {
                                console.log(error);
                                setError(true);
                                setErrorMessage(error.response.data);
                            })
                        }
                    }
                }
                else{
                    alert("Description too long");
                }
            }
            else{
                alert("Invalid Amount Entered");
            }
        }
    }

    function amountValidation(eventargs){
        var amount = eventargs.target.value;
        setAmount(amount);
        if(amount !== ""){
            if(amount > 0){
                setError(false);
                if(addBeneficiary){
                    if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                        document.getElementById("transfer").classList.remove("disabled");
                    }
                }
                else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Invalid Amount Entered");
                document.getElementById("transfer").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter an Amount");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function descriptionValidation(eventargs){
        var description = eventargs.target.value;
        setDescription(description);
        if(description.length < 35){
            setError(false);
            if(addBeneficiary){
                if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                document.getElementById("transfer").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Description too long");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function sourceAccountValidation(eventargs){
        var accountID = eventargs.target.value;
        setAccountID(accountID);
        if(accountID !== ""){
            setError(false);
            if(addBeneficiary){
                if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                document.getElementById("transfer").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select an Account");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function destinationAccountValidation(eventargs){
        var beneficiaryID = eventargs.target.value;
        setBeneficiaryID(beneficiaryID);
        if(beneficiaryID !== ""){
            setError(false);
            if(addBeneficiary){
                if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                document.getElementById("transfer").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select a Beneficiary");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function clickedAddBeneficiary(){
        setAddBeneficiary(true);
        document.getElementById("transfer").classList.add("disabled");
    }

    function beneficiaryNameValidation(eventargs){
        var beneficiaryName = eventargs.target.value;
        setBeneficiaryName(beneficiaryName);
        if(beneficiaryName !== ""){
            if(beneficiaryName.length > 2 && beneficiaryName.length < 100){
                setError(false);
                if(addBeneficiary){
                    if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                        document.getElementById("transfer").classList.remove("disabled");
                    }
                }
                else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Beneficiary Name should be between 3 and 100 characters long");
                document.getElementById("transfer").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter Beneficiary Name");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function beneficiaryAccountNumberValidation(eventargs){
        var beneficiaryAccountNumber = eventargs.target.value;
        setBeneficiaryAccountNumber(beneficiaryAccountNumber);
        if(beneficiaryAccountNumber !== ""){
            if(beneficiaryAccountNumber.length > 9 && beneficiaryAccountNumber.length < 14){
                setError(false);
                if(addBeneficiary){
                    if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                        document.getElementById("transfer").classList.remove("disabled");
                    }
                }
                else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Account number should be between 9 and 14 digits long");
                document.getElementById("transfer").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter Beneficiary Account Number");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function bankValidation(eventargs){
        changeBank(eventargs);
        var bankID = eventargs.target.value;
        setBankID(bankID);
        if(bankID !== ""){
            setError(false);
            if(addBeneficiary){
                if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                document.getElementById("transfer").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select a Bank");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function branchValidation(eventargs){
        var branchID = eventargs.target.value;
        setBranchID(branchID);
        if(branchID !== ""){
            setError(false);
            if(addBeneficiary){
                if(amount !== "" && accountID !== "" && beneficiaryAccountNumber !== "" && beneficiaryName !== "" && bankID !== "" && branchID !== ""){
                    document.getElementById("transfer").classList.remove("disabled");
                }
            }
            else if(amount !== "" && accountID !== "" && beneficiaryID !== ""){
                document.getElementById("transfer").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select a Branch");
            document.getElementById("transfer").classList.add("disabled");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox43">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoOrange" to="/menu/customerTransactions">History</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoBlack" to="/menu/transferMoney">Transfer Money</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoOrange" to="/menu/depositMoney">Deposit</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoOrange" to="/menu/withdrawMoney">Withdraw</Link>
                        </li>
                    </ul>
                    {accountsError ? 
                    <div className="smallBox48">
                        <div className="errorImage2 change-my-color2"></div>
                        <div className="clickRegisterText">{errorMessage}</div>
                    </div>
                    :
                    <div>
                        <div className="smallBox19"> 
                            <div className='phoneMargin2'>
                                <span className="clickRegisterTextBlack">Amount</span>
                                <input className="form-control enterDiv2" type="number" onChange={amountValidation}></input>
                            </div>
                            <div className='phoneMargin2'>
                                <span className="clickRegisterTextBlack">Description</span>
                                <input className="form-control enterDiv2" type="text" onChange={descriptionValidation}></input>
                            </div>
                        </div>
                        {addBeneficiary ? 
                            <div className='smallBox63'>
                                <div className='margin3'>
                                    <span className="clickRegisterTextBlack">From</span>
                                    <select className="form-control enterDiv2" value = {accountID} onChange={sourceAccountValidation}>
                                        <option value="">Select</option>
                                        {accounts.map(account => 
                                            <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                                        )}
                                    </select>
                                </div>
                                <div className="smallBox19">
                                    <div className='phoneMargin2'>
                                        <span className="clickRegisterTextBlack">Account Holder Name</span>
                                        <input className="form-control enterDiv2" type="text" onChange={beneficiaryNameValidation}></input>
                                    </div>
                                    <div className='phoneMargin2'>
                                        <span className="clickRegisterTextBlack">Holder Account Number</span>
                                        <input className="form-control enterDiv2" type="number" onChange={beneficiaryAccountNumberValidation}></input>
                                    </div>
                                </div>
                            </div> : 
                            <div className="smallBox19">
                                <div className='phoneMargin2'>
                                    <span className="clickRegisterTextBlack">From</span>
                                    <select className="form-control enterDiv2" value = {accountID} onChange={sourceAccountValidation}>
                                        <option value="">Select</option>
                                        {accounts.map(account => 
                                            <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                                        )}
                                    </select>
                                </div>
                                <div className='phoneMargin2'>
                                    <span className="clickRegisterTextBlack">To</span>
                                    <select className="form-control enterDiv2" value = {beneficiaryID} onChange={destinationAccountValidation}>
                                        <option value="">Select</option>
                                        {beneficiaries.map(beneficiary => 
                                            <option key={beneficiary.beneficiaryID} value={beneficiary.beneficiaryID}>{beneficiary.name} - {beneficiary.accountNumber}</option>
                                        )}
                                    </select>
                                </div>
                            </div>
                            }
                        {addBeneficiary ? <div className="smallBox19">
                            <div className='phoneMargin2'>
                                <span className="clickRegisterTextBlack">Bank Name</span>
                                <select className="form-control enterDiv2" value = {bankID} onChange={bankValidation}>
                                    <option value="">Select</option>
                                    {allBanks.map(bank => 
                                        <option key={bank.bankID} value={bank.bankID}>{bank.bankName}</option>
                                    )}
                                </select>
                            </div>
                            <div className='phoneMargin2'>
                                <span className="clickRegisterTextBlack">Branch Name with IFSC</span>
                                <select className="form-control enterDiv2" value = {branchID} onChange={branchValidation}>
                                    <option value="">Select</option>
                                    {allBranches.map(branch => 
                                        <option key={branch.branchID} value={branch.branchID}>{branch.ifscNumber} -- {branch.branchName}</option>
                                    )}
                                </select>
                            </div>
                        </div> : null}
                    </div>}
                    {error ? <div className='flexRow errorText'>{errorMessage}</div> : null}
                    {accountsError ? null : 
                    <div className='smallBox46'>
                        <a id="transfer" className="btn btn-outline-success smallBox44 phoneMargin2 disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                            <span>Transfer</span>
                        </a>
                        {addBeneficiary ? 
                        <a className="btn btn-outline-danger smallBox45  phoneMargin2" onClick={() => setAddBeneficiary(false)}>
                            <span>Cancel</span>
                        </a> : 
                        <a className="btn btn-outline-secondary smallBox45  phoneMargin2" onClick={clickedAddBeneficiary}>
                            <span>Add Beneficiary</span>
                        </a>}
                    </div>}
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Transfer Money</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to transfer money?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={transferMoney}>Transfer</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Transfer Successful
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
            </div>
    );
}

export default TransferMoney;