import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

function ViewCustomer(){

    var navigate = useNavigate();
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [successMessage,setSuccessMessage]= useState("");   
    var [oldData,setOldData] = useState({})
    var [profile,setProfile] = useState(
        {
            "name": "",
            "dob": "",
            "age": "",
            "phoneNumber": "",
            "address": "",
            "aadharNumber": "",
            "panNumber": "",
            "gender": "",
            "email": "",
    })

    var customerID = useSelector((state) => state.customerID);

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

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        if(customerID === 0){
            navigate("/adminMenu/allCustomers")
        }
        else{
            getCustomerDetails();
        }
    },[])

    async function getCustomerDetails(){
        await axios.get('http://localhost:7289/api/Customers/GetCustomer?customerID=' + customerID,httpHeader)
        .then(function (response) {
            convertDate(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function deactivateAccount(){
        await axios.put('http://localhost:7289/api/Validation/UpdateValidationStatus?email=' + profile.email,updateCustomer,httpHeader)
        .then(function (response) {
            setSuccessMessage("Successfully Closed Account");
            showToast();
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
        data.aadharNumber = data.aadharNumber.toString();
        setProfile(data);
        setOldData(data);
    }

    async function updateCustomerDetails() {
        if (updateCustomer.name === "" || updateCustomer.phoneNumber === "" || updateCustomer.address === "") {
            alert("Please fill all fields");
        } 
        else {
            if (areEqual(oldData, profile)) {
                alert("No changes made");
            } 
            else {
                if (updateCustomer.name.length > 2 && updateCustomer.name.length < 100) {
                    if (updateCustomer.age >= 18) {
                        if (updateCustomer.phoneNumber.length === 10) {
                            if(updateCustomer.aadharNumber.length === 12){
                                if(updateCustomer.panNumber.length === 10){
                                    if (updateCustomer.address.length > 5  && updateCustomer.address.length < 100) {
                                        await axios.put('http://localhost:7289/api/Customers/UpdateCustomerDetails', updateCustomer, httpHeader)
                                        .then(function (response) {
                                            setSuccessMessage("Details Updated Successfully");
                                            showToast();
                                        })
                                        .catch(function (error) {
                                            console.log(error);
                                        })
                                    }
                                    else{
                                        alert("Address should be between 6 and 100 characters long");
                                    }
                                }
                                else{
                                    alert("PAN number should be 10 characters long");
                                }
                            }
                            else{
                                alert("Aadhaar number should be 12 digits");
                            }
                        }
                        else{
                            alert("Phone number should be 10 digits");
                        }
                    }
                    else {
                        alert("Age should be greater than or equal to 18");
                    }
                } 
                else{
                    alert("Name should be between 3 and 100 characters long");
                }
            } 
        }    
    }

    function areEqual(a, b) {
        return JSON.stringify(a) === JSON.stringify(b);
    }

    function nameValidation(eventargs){
        var name = eventargs.target.value;
        setProfile({...profile,name:name});
        if(name !== ""){
            if(name.length > 2 && name.length < 100){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                    document.getElementById("update").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Name should be between 2 and 100 characters long");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Name cannot be empty");
        }
    }

    function dateValidation(eventargs){
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        setProfile({...profile,dob:eventargs.target.value,age:age});
        if(age >= 18){
            setError(false);
            if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                document.getElementById("update").classList.remove("disabled");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
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
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                    document.getElementById("update").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Phone number should be 10 digits");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Phone number cannot be empty");
        }
    }

    function aadhaarValidation(eventargs){
        var aadharNumber = eventargs.target.value;
        setProfile({...profile,aadharNumber:aadharNumber});
        if(aadharNumber !== ""){
            if(aadharNumber.length === 12){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                    document.getElementById("update").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Aadhaar number should be 12 digits");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Aadhaar number cannot be empty");
        }
    }

    function panValidation(eventargs){
        var panNumber = eventargs.target.value;
        setProfile({...profile,panNumber:panNumber});
        if(panNumber !== ""){
            if(panNumber.length === 10){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                    document.getElementById("update").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Pan number should be 10 characters");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Pan number cannot be empty");
        }
    }

    function genderValidation(eventargs){
        var gender = eventargs.target.value;
        setProfile({...profile,gender:gender});
        if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
            document.getElementById("update").classList.remove("disabled");
        }
    }

    function addressValidation(eventargs){
        var address = eventargs.target.value;
        setProfile({...profile,address:eventargs.target.value});
        if(address !== ""){
            if(address.length > 5 && address.length < 100){
                setError(false);
                if(updateCustomer.name !== "" && updateCustomer.dob !== "" && updateCustomer.phoneNumber !== "" && updateCustomer.address !== "" && updateCustomer.aadharNumber !== "" && updateCustomer.panNumber !== ""){
                    document.getElementById("update").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Address should be between 6 and 100 characters long");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Address cannot be empty");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox18">
            <div className="upMargin3">
                        <Link to = "/adminMenu/allCustomers">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                        <div className="closeAccount">
                            <span className="pointer" data-bs-toggle="modal" data-bs-target="#modal3">
                                <div className="delete change-my-color2"></div>
                            </span>
                            <span className="clickRegisterText">Close Account</span>
                        </div>
                    </div>
                <div>
                    <div className="smallBox19"> 
                        <div className="margin1">
                            <span className="clickRegisterText">Name</span>
                            <input className="form-control enterDiv2" type="text" value={profile.name} onChange={nameValidation}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">Email</span>
                            <input className="form-control enterDiv2" type="email" value={profile.email} readOnly></input>
                        </div>
                    </div>
                    <div className="smallBox19">
                        <div className="margin1">
                            <span className="clickRegisterText">Date of Birth</span>
                            <input className="form-control enterDiv2" type="date" value={profile.dob} onChange={dateValidation}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">Phone Number</span>
                            <input className="form-control enterDiv2" type="number" value={profile.phoneNumber} onChange={phoneValidation}></input>
                        </div>
                    </div>
                    <div className="smallBox19">
                        <div className="margin1">
                            <span className="clickRegisterText">Aadhaar Number</span>
                            <input className="form-control enterDiv2" type="number" value={profile.aadharNumber} onChange={aadhaarValidation}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">PAN Number</span>
                            <input className="form-control enterDiv2" type="text" value={profile.panNumber} onChange={panValidation}></input>
                        </div>
                    </div>
                    <div className="smallBox19">
                        <div className="margin1">
                            <span className="clickRegisterText">Gender</span>
                            <select className="form-control enterDiv2" value={profile.gender} onChange={genderValidation}>
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                                <option value="Others">Others</option>
                            </select>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">Address</span>
                            <input className="form-control enterDiv2" type="text" value={profile.address} onChange={addressValidation}></input>
                        </div>
                    </div>
                </div>
                {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                <a id="update" className="btn btn-outline-success smallBox9 margin1 disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                    <span>Update</span>
                </a>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h6 className="modal-title" id="modalEg1">Update Details</h6>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            Are you sure you want to update your details?
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={updateCustomerDetails}>Update</button>
                        </div>
                    </div>
                </div>
            </div>
            <div className="modal fade" id="modal3" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Close Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to close this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={deactivateAccount}>Close</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        {successMessage}
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
        </div>
    );
}

export default ViewCustomer;