import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link } from 'react-router-dom';

function OpenNewAccount(){

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [accountType,setAccountType] = useState("")
    var [branchID, setBranchID] = useState("");
    var customerID = sessionStorage.getItem('id');
    var [branches,setBranches] = useState(
        [{
        "branchID": 0,
        "ifscNumber": "string",
        "branchName": "string",
        "bankID": 0,
        "banks": {
          "bankID": 0,
          "bankName": "string"
        }
      }]
    );
    
    var newAccount = {
        "accountType": accountType,
        "branchID": branchID,
        "customerID": customerID
    }

    var [profile,setProfile] = useState(
        {
            "name": "",
            "dob": "",
            "age": "",
            "phoneNumber": "",
            "address": "",
            "aadharNumber": 0,
            "panNumber": "",
            "gender": "",
            "email": "",
    })

    var updateCustomer = {
        "customerID": customerID,
        "name": profile.name,
        "dob": profile.dob,
        "age": profile.age,
        "phoneNumber": profile.phoneNumber,
        "address": profile.address,
        "aadharNumber": profile.aadharNumber,
        "panNumber": profile.panNumber,
        "gender": profile.gender
    }

    useEffect(() => {
        getCustomerDetails();
        getAllMavericksBranches();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getCustomerDetails(){
        await axios.get('http://localhost:7289/api/Customers/GetCustomer?customerID=' + customerID,httpHeader)
        .then(function (response) {
            convertDate(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function convertDate(data){
        const date = new Date(data.dob);
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 because months are zero-indexed
        const day = date.getDate().toString().padStart(2, '0');
        const formattedDate =  year + "-" + month + "-" + day;
        data.dob = formattedDate;
        data.phoneNumber = data.phoneNumber.toString();
        setProfile(data);
    }

    async function createAccount() {
        await axios.put('http://localhost:7289/api/Customers/UpdateCustomerDetails', updateCustomer, httpHeader).then(function (response) {
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
        await axios.post('http://localhost:7289/api/Accounts/AddAccount',newAccount,httpHeader).then(function (response) {
           setError(false);
           showToast();
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    async function getAllMavericksBranches(){
        await axios.get('http://localhost:7289/api/Branches/GetAllSpecificBranches?bankID=1',httpHeader)
        .then(function (response) {
            setBranches(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function nameValidation(eventargs){
        var name = eventargs.target.value;
        setProfile({...profile,name:name});
        if(name !== ""){
            if(name.length > 2 && name.length < 100){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && accountType !== "" && branchID !== ""){
                    document.getElementById("createAccount").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("createAccount").classList.add("disabled");
                setError(true);
                setErrorMessage("Name should be between 2 and 100 characters long");
            }
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
            setError(true);
            setErrorMessage("Name cannot be empty");
        }
    }

    function dateValidation(eventargs){
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        setProfile({...profile,dob:eventargs.target.value,age:age});
        if(age >= 18){
            setError(false);
            if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && accountType !== "" && branchID !== ""){
                document.getElementById("createAccount").classList.remove("disabled");
            }
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
            setError(true);
            setErrorMessage("Age should be 18 and above");
        }
    };

    function phoneValidation(eventargs){
        var phoneNumber = eventargs.target.value;
        setProfile({...profile,phoneNumber:eventargs.target.value});
        if(phoneNumber !== ""){
            if(phoneNumber.length === 10){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && accountType !== "" && branchID !== ""){
                    document.getElementById("createAccount").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("createAccount").classList.add("disabled");
                setError(true);
                setErrorMessage("Phone number should be 10 digits");
            }
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
            setError(true);
            setErrorMessage("Phone number cannot be empty");
        }
    }

    function addressValidation(eventargs){
        var address = eventargs.target.value;
        setProfile({...profile,address:eventargs.target.value});
        if(address !== ""){
            if(address.length > 5 && address.length < 100){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== ""  && accountType !== "" && branchID !== ""){
                    document.getElementById("createAccount").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("createAccount").classList.add("disabled");
                setError(true);
                setErrorMessage("Address should be between 6 and 100 characters long");
            }
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
            setError(true);
            setErrorMessage("Address cannot be empty");
        }
    }

    function accountTypeValidation(eventargs){
        var accountType = eventargs.target.value;
        setAccountType(accountType)
        if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== ""  && accountType !== "" && branchID !== ""){
            document.getElementById("createAccount").classList.remove("disabled");
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
        }
    }

    function branchValidation(eventargs){
        var branchID = eventargs.target.value;
        setBranchID(eventargs.target.value)
        if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== ""  && accountType !== "" && branchID !== ""){
            document.getElementById("createAccount").classList.remove("disabled");
        }
        else{
            document.getElementById("createAccount").classList.add("disabled");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox59">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoOrange smallBox23" to="/menu/customerAccounts">All Accounts</Link>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoBlack smallBox23" to="/menu/openAccount">Open New Account</Link>
                    </li>
                </ul>
                <div className="scrolling">
                    <div className='phoneMargin2'>
                        <div className="smallBox19"> 
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Name</span>
                                <input className="form-control enterDiv2" type="text" value={profile.name} onChange={nameValidation}></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Email</span>
                                <input className="form-control enterDiv2" type="email" value={profile.email} readOnly></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Date of Birth</span>
                                <input className="form-control enterDiv2" type="date" value={profile.dob} onChange={dateValidation}></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Phone Number</span>
                                <input className="form-control enterDiv2" type="number" value={profile.phoneNumber} onChange={phoneValidation}></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Aadhaar Number</span>
                                <input className="form-control enterDiv2" type="number" value={profile.aadharNumber} readOnly></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">PAN Number</span>
                                <input className="form-control enterDiv2" type="text" value={profile.panNumber} readOnly></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Gender</span>
                                <select className="form-control enterDiv2" value={profile.gender} onChange={(eventargs) => setProfile({...profile,gender:eventargs.target.value})}>
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                    <option value="Others">Others</option>
                                </select>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterTextBlack">Address</span>
                                <input className="form-control enterDiv2" type="text" value={profile.address} onChange={addressValidation}></input>
                            </div>
                        </div>
                    </div>
                    <div className="smallBox25">
                        <div className='phoneMargin'>
                            <span className="clickRegisterTextBlack">Account Type</span>
                            <select className="form-control enterDiv2" value={accountType} onChange={accountTypeValidation}>
                                <option value="">Select</option>
                                <option value="Savings">Savings Account</option>
                                <option value="Current">Current Account</option>
                                <option value="Business">Business Account</option>
                                <option value="Salary">Salary Account</option>
                            </select>
                        </div>
                        <div>
                            <span className="clickRegisterTextBlack">Select Branch</span>
                            <select className="form-control enterDiv2" value={branchID} onChange={branchValidation}>
                                <option value="">Select</option>
                                {branches.map(branch =>
                                    <option key={branch.branchID} value={branch.branchID}>{branch.branchName}</option>
                                )}
                            </select>
                        </div>
                    </div>
                </div>
                {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                <a id="createAccount" className="btn btn-outline-success smallBox42 phoneMargin disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                    <span>Create Account</span>
                </a>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h6 className="modal-title" id="modalEg1">Create Account</h6>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            Are you sure you want to create account?
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={createAccount}>Create</button>
                        </div>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                <div className="d-flex">
                <div className="toast-body">
                    Account Created Successfully, Please wait while we process your request and approve your account
                </div>
                <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        </div>
    );
}

export default OpenNewAccount;