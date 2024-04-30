import axios from 'axios';
import { useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';

function Landing(){

    var [email,setEmail]= useState("");
    var [password,setPassword]= useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var navigate = useNavigate();

    var loginValidation = {
        "email": email,
        "password": password,
        "userType": "",
        "token": ""
    }

    async function loginUser(){
        await axios.post('http://localhost:7289/api/Validation/Login',loginValidation)
        .then(function (response) {
            setError(false);
            sessionStorage.setItem("email",response.data.email);
            sessionStorage.setItem("token",response.data.token);
            if(response.data.userType === "Customer"){
                getCustomerID();
            }
            else if(response.data.userType === "Employee"){
                getEmployeeID();
            }
            else if(response.data.userType === "Admin"){
                getAdminID();
            }
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    async function getCustomerID(){
        const email = sessionStorage.getItem('email');
        const token = sessionStorage.getItem('token');
        const httpHeader = { 
            headers: {'Authorization': 'Bearer ' + token}
        };
        await axios.get('http://localhost:7289/api/Customers/GetCustomerByEmail?email=' + email,httpHeader)
        .then(function (response) {
            sessionStorage.setItem("id",response.data.customerID);
            navigate("/menu/dashboard");
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getEmployeeID(){
        const email = sessionStorage.getItem('email');
        const token = sessionStorage.getItem('token');
        const httpHeader = { 
            headers: {'Authorization': 'Bearer ' + token}
        };
        await axios.get('http://localhost:7289/api/BankEmployees/GetEmployeeByEmail?email=' + email,httpHeader)
        .then(function (response) {
            sessionStorage.setItem("id",response.data.employeeID);
            navigate("/employeeMenu/allCustomers");
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getAdminID(){
        const email = sessionStorage.getItem('email');
        const token = sessionStorage.getItem('token');
        const httpHeader = { 
            headers: {'Authorization': 'Bearer ' + token}
        };
        await axios.get('http://localhost:7289/api/Admin/GetAdminByEmail?email=' + email,httpHeader)
        .then(function (response) {
            sessionStorage.setItem("id",response.data.adminID);
            navigate("/adminMenu/allCustomers");
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function checkEmailValidation(eventargs){
        var email = eventargs.target.value;
        setEmail(email)
        if(email !== ""){
            if(email.includes("@") && email.includes(".")){
                if(email.length > 5){
                    setError(false);
                    if(password !== ""){
                        document.getElementById("loginButton").classList.remove("disabled");
                    }
                }
                else{
                    document.getElementById("loginButton").classList.add("disabled");
                    setError(true);
                    setErrorMessage("Email Length should be greater than 5 characters");
                }
            }
            else{
                document.getElementById("loginButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Invalid Email");
            }
        }
        else{
            document.getElementById("loginButton").classList.add("disabled");
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
                if(email !== ""){
                    document.getElementById("loginButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("loginButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Password length should be between 8-15 characters");
            }
        }
        else{
            document.getElementById("loginButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Password cannot be empty");
        }
    }

    return(
        <div className="container">
            <div className="orginalRow">
                <div className="smallBox1 col-sm-5">
                    <div className="topcorner2 flexRow">
                        <div className="logoImage change-my-color3"></div>
                        <span className="logo">Mavericks Bank</span>
                    </div>
                    <div className="smallBox2">
                        <span className="welcomeText">Welcome!</span>
                        <span className="clickRegisterText">New? Click below to create account</span>
                        <Link className="btn btn-outline-success smallBox3" to="/registerCustomer">
                            <span>Become a Maverick</span>
                        </Link>
                    </div>
                </div>
                <div className="smallBox4 col-sm-7">
                    <div className="smallBox7">
                        <div className="smallBox5">
                            <span className="welcomeText2">Welcome!</span>
                            <span className="welcomeText3">Sign In</span>
                            <div>
                                <span className="clickRegisterTextBlack">Email</span>
                                <input className="form-control enterDiv" type="email" value={email} onChange={checkEmailValidation}></input>
                            </div>
                            <div>
                                <span className="clickRegisterTextBlack">Password</span>
                                <input className="form-control enterDiv" type="password" value={password} onChange={checkPasswordValidation}></input>
                            </div>
                        </div>
                        <div className="smallBox6">
                            <Link className="clickRegisterTextBlack" to="/forgotPassword">Forgot Password?</Link>
                        </div>
                        {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                        <div className="smallBox8">
                            <Link className="btn btn-outline-success smallBox9 noDisplay" to="/registerCustomer">
                                <span>Register</span>
                            </Link>
                            <div className="pad">
                                <a id="loginButton" onClick = {loginUser} className="btn btn-outline-success smallBox9 disabled">
                                    <span>Login</span>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Landing;