import axios from 'axios';
import { useState } from 'react';
import '../../../style.css';
import { Link } from 'react-router-dom';

function CreateCustomer() {

    var [email, setEmail] = useState("");
    var [password, setPassword] = useState("");
    var [confirmPassword, setConfirmPassword] = useState("");
    var [name, setName] = useState("");
    var [dob, setDob] = useState("");
    var [age, setAge] = useState(0);
    var [phoneNumber, setPhoneNumber] = useState("");
    var [address, setAddress] = useState("");
    var [aadharNumber, setAadharNumber] = useState("");
    var [panNumber, setPanNumber] = useState("");
    var [gender, setGender] = useState("Male");
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
    };

    var addCustomer = async () => await axios.post('http://localhost:7289/api/Validation/RegisterCustomers', newCustomer)
        .then(function (response) {
            setError(false);
            showToast();
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        });

    var registerCustomer = () => {
        if (email === "" || password === "" || confirmPassword === "" || name === "" || dob === "" || phoneNumber === "" || address === "" || aadharNumber === "" || panNumber === "") {
            alert("Please fill in all fields");
        } else {
            if (email.includes("@") && email.includes(".") && email.length > 5 && email.length < 50) {
                if (password.length >= 8 && password.length <= 15) {
                    if (password === confirmPassword) {
                        if (name.length > 2 && name.length < 100) {
                            if (age >= 18) {
                                if (phoneNumber.length === 10) {
                                    if (address.length > 5 && address.length < 100) {
                                        if (aadharNumber.length === 12) {
                                            if (panNumber.length === 10) {
                                                addCustomer();
                                            } else {
                                                alert("Pan number should be 10 characters");
                                            }
                                        } else {
                                            alert("Aadhaar number should be 12 digits");
                                        }
                                    } else {
                                        alert("Address should be between 6 and 100 characters long");
                                    }
                                } else {
                                    alert("Phone number should be 10 digits");
                                }
                            } else {
                                alert("Age should be 18 and above");
                            }
                        } else {
                            alert("Name should be between 6 and 100 characters long");
                        }
                    } else {
                        alert("Passwords do not match");
                    }
                } else {
                    alert("Password length should be between 8-15 characters");
                }
            } else {
                alert("Invalid email");
            }
        }
    };

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
                    setErrorMessage("Passwords do not match");
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
        <div className="smallBox17 col-md-9">
            <div className="smallBox40 widthBox">
            <ul className="smallBox22 nav">
            <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/adminMenu/allCustomers">All Customers</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoBlack  smallBox23" to="/adminMenu/createCustomer">Create Customer</Link>
                        </li>
                </ul>
                <div className="smallBox54">
                    <div className="scrolling">
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Email</span>
                            <input className="form-control enterDiv7" type="email" value={email} onChange={checkEmailValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Password</span>
                            <input className="form-control enterDiv7" type="password" onChange={checkPasswordValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Confirm Password</span>
                            <input className="form-control enterDiv7" type="password" onChange={confirmPasswordValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Name</span>
                            <input className="form-control enterDiv7" type="text" value={name} onChange={nameValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Date of Birth</span>
                            <input className="form-control enterDiv7" type="date" value={dob} onChange={dateValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Phone Number</span>
                            <input className="form-control enterDiv7" type="number" value={phoneNumber} onChange={phoneValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Address</span>
                            <input className="form-control enterDiv7" type="text" value={address} onChange={addressValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Aadhaar Number</span>
                            <input className="form-control enterDiv7" type="number" value={aadharNumber} onChange={aadhaarValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Pan Number</span>
                            <input className="form-control enterDiv7" type="text" value={panNumber} onChange={panValidation}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterTextBlack">Gender</span>
                            <select className="form-control enterDiv7" onChange={(eventargs) => setGender(eventargs.target.value)}>
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                                <option value="Others">Others</option>
                            </select>
                        </div>
                    </div>
                </div>
                {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                <div className="smallBox55 phoneMargin2">
                    <a id="registerButton" onClick={registerCustomer} className="btn btn-outline-success smallBox9 disabled">
                        <span>Create</span>
                    </a>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Successfully Created Customer
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
        </div>
    );
}

export default CreateCustomer;