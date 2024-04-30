import axios from 'axios';
import { useState } from 'react';
import '../../style.css';
import { Link } from 'react-router-dom';

function RegisterCustomer(){

    var [email,setEmail]= useState("");
    var [password,setPassword]= useState("");
    var [confirmPassword,setConfirmPassword]= useState("");
    var [name,setName]= useState("");
    var [dob,setDob]= useState("");
    var [age,setAge]= useState(0);
    var [phoneNumber,setPhoneNumber]= useState("");
    var [address,setAddress]= useState("");
    var [aadharNumber,setAadharNumber]= useState("");
    var [panNumber,setPanNumber]= useState("");
    var [gender,setGender]= useState("Male");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var newCustomer = {
        "email": email,
        "password": password,
        "name": name,
        "dob": dob,
        "age": age,
        "phoneNumber": phoneNumber,
        "address": address,
        "aadharNumber": aadharNumber,
        "panNumber": panNumber,
        "gender": gender
    }

    async function registerCustomer(){
        await axios.post('http://localhost:7289/api/Validation/RegisterCustomers',newCustomer).then(function (response) {
            setError(false);
            showToast();
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    function checkEmailValidation(eventargs){
        var email = eventargs.target.value;
        setEmail(email)
        if(email !== ""){
            if(email.includes("@") && email.includes(".")){
                if(email.length > 5){
                    setError(false);
                    if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                        document.getElementById("registerButton").classList.remove("disabled");
                    }
                }
                else{
                    document.getElementById("registerButton").classList.add("disabled");
                    setError(true);
                    setErrorMessage("Email Length should be greater than 5 characters");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Invalid Email");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Email cannot be empty");
        }
    }

    function checkPasswordValidation(eventargs){
        var password = eventargs.target.value;
        setPassword(password);
        if(password !== ""){
            if(password.length >= 8 && password.length <= 15){
                setError(false);
                if(password === confirmPassword){
                    if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                        document.getElementById("registerButton").classList.remove("disabled");
                    }
                }
                else{
                    document.getElementById("registerButton").classList.add("disabled");
                    setError(true);
                    setErrorMessage("Confirm Your Password");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Password length should be between 8-15 characters");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Password cannot be empty");
        }
    }

    function confirmPasswordValidation(eventargs){
        var confirmPassword = eventargs.target.value;
        setConfirmPassword(confirmPassword);
        if(confirmPassword !== ""){
            if(password === confirmPassword){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Passwords do not match");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Confirm Password cannot be empty");
        }
    }

    function nameValidation(eventargs){
        var name = eventargs.target.value;
        setName(name);
        if(name !== ""){
            if(name.length > 2 && name.length < 100){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Name should be between 2 and 100 characters long");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Name cannot be empty");
        }
    }

    function dateValidation(eventargs){
        setDob(eventargs.target.value);
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        setAge(age);
        if(age >= 18){
            setError(false);
            if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                document.getElementById("registerButton").classList.remove("disabled");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Age should be 18 and above");
        }
    };

    function phoneValidation(eventargs){
        var phoneNumber = eventargs.target.value;
        setPhoneNumber(phoneNumber);
        if(phoneNumber !== ""){
            if(phoneNumber.length === 10){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Phone number should be 10 digits");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Phone number cannot be empty");
        }
    }

    function addressValidation(eventargs){
        var address = eventargs.target.value;
        setAddress(address);
        if(address !== ""){
            if(address.length > 5 && address.length < 100){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Address should be between 6 and 100 characters long");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Address cannot be empty");
        }
    }

    function aadhaarValidation(eventargs){
        var aadharNumber = eventargs.target.value;
        setAadharNumber(aadharNumber);
        if(aadharNumber !== ""){
            if(aadharNumber.length === 12){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Aadhaar number should be 12 digits");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Aadhaar number cannot be empty");
        }
    }

    function panValidation(eventargs){
        var panNumber = eventargs.target.value;
        setPanNumber(panNumber);
        if(panNumber !== ""){
            if(panNumber.length === 10){
                setError(false);
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== "" && dob !== "" && phoneNumber !== "" && address !== "" && aadharNumber !== "" && panNumber !== ""){
                    document.getElementById("registerButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("registerButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Pan number should be 10 characters");
            }
        }
        else{
            document.getElementById("registerButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Pan number cannot be empty");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="container">
            <div className="orginalRow">
                <div className="smallBox1 col-sm-5">
                    <div className="topcorner2 flexRow">
                        <div className="logoImage change-my-color3"></div>
                        <span className="logo">Mavericks Bank</span>
                    </div>
                    <div  className="smallBox13">
                        <Link to="/">
                            <div className="back change-my-color"></div>
                        </Link>
                    </div>
                    <div className="smallBox11">
                        <span className="welcomeText">Just One more step</span>
                        <div className="flexRow">
                            <span className="clickRegisterText">To Join Maverick Family</span>
                        </div>
                    </div>
                </div>
                <div className="smallBox4 col-sm-7">
                    <div className="smallBox7">
                        <div className="smallBox10">
                            <div className="smallBox14">
                                <div  className="smallBox13">
                                    <Link to="/">
                                        <div className="back change-my-color"></div>
                                    </Link>
                                </div>
                                <span className="welcomeText2">1 step away!</span>
                                <div className="flexRow">
                                    <span className="clickRegisterText">From becoming a Maverick</span>
                                </div>
                            </div>
                            <span className="welcomeText3 marginRegisterCustomer">New Account</span>
                            <div className="scrolling">
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Email</span>
                                    <input className="form-control enterDiv" type="email" value={email} onChange={checkEmailValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Password</span>
                                    <input className="form-control enterDiv" type="password" onChange={checkPasswordValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Confirm Password</span>
                                    <input className="form-control enterDiv" type="password" onChange={confirmPasswordValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Name</span>
                                    <input className="form-control enterDiv" type="text" value={name} onChange={nameValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Date of Birth</span>
                                    <input className="form-control enterDiv" type="date" value={dob} onChange={dateValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Phone Number</span>
                                    <input className="form-control enterDiv" type="number"  value={phoneNumber} onChange={phoneValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Address</span>
                                    <input className="form-control enterDiv" type="text" value={address} onChange={addressValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Aadhaar Number</span>
                                    <input className="form-control enterDiv" type="number" value={aadharNumber} onChange={aadhaarValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Pan Number</span>
                                    <input className="form-control enterDiv" type="text" value={panNumber} onChange={panValidation}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterTextBlack">Gender</span>
                                    <select className="form-control enterDiv" onChange={(eventargs) => setGender(eventargs.target.value)}>
                                        <option value="Male">Male</option>
                                        <option value="Female">Female</option>
                                        <option value="Others">Others</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                        <div className="smallBox8">
                            <a id="registerButton" onClick = {registerCustomer} className="btn btn-outline-success smallBox9 disabled">
                                <span>Register</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                <div className="d-flex">
                <div className="toast-body">
                    Registration Successful, Please Login
                </div>
                <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
    </div>
    )
}

export default RegisterCustomer;