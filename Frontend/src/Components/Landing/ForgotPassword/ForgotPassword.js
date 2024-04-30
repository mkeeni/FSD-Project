import axios from 'axios';
import { useState } from 'react';
import '../../style.css';
import { Link } from 'react-router-dom';

function ForgotPassword(){

    var [email,setEmail]= useState("");
    var [password,setPassword]= useState("");
    var [confirmPassword,setConfirmPassword]= useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var newPassword = {
        "email": email,
        "password": password,
        "userType": "",
        "token": ""
    }

    async function changeExistingPassword(){
        await axios.post('http://localhost:7289/api/Validation/ForgotPassword',newPassword).then(function (response) {
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
                    if(email !== "" && password !== "" && confirmPassword !== ""){
                        document.getElementById("changeButton").classList.remove("disabled");
                    }
                }
                else{
                    document.getElementById("changeButton").classList.add("disabled");
                    setError(true);
                    setErrorMessage("Email Length should be greater than 5 characters");
                }
            }
            else{
                document.getElementById("changeButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Invalid Email");
            }
        }
        else{
            document.getElementById("changeButton").classList.add("disabled");
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
                    if(email !== "" && password !== "" && confirmPassword !== ""){
                        document.getElementById("changeButton").classList.remove("disabled");
                    }
                }
                else{
                    document.getElementById("changeButton").classList.add("disabled");
                    setError(true);
                    setErrorMessage("confirm password");
                }
            }
            else{
                document.getElementById("changeButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Password length should be between 8-15 characters");
            }
        }
        else{
            document.getElementById("changeButton").classList.add("disabled");
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
                if(email !== "" && password !== "" && confirmPassword !== ""){
                    document.getElementById("changeButton").classList.remove("disabled");
                }
            }
            else{
                document.getElementById("changeButton").classList.add("disabled");
                setError(true);
                setErrorMessage("Passwords do not match");
            }
        }
        else{
            document.getElementById("changeButton").classList.add("disabled");
            setError(true);
            setErrorMessage("Confirm Password cannot be empty");
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
                        <span className="welcomeText">Keep your password secure</span>
                        <div className="flexRow">
                            <span className="clickRegisterText">Banking is Easier Now</span>
                        </div>
                    </div>
                </div>
                <div className="smallBox4 col-sm-7">
                    <div className="smallBox7">
                        <div className="smallBox35">
                            <div className="smallBox14">
                                <div  className="smallBox13">
                                    <Link to="/">
                                        <div className="back change-my-color"></div>
                                    </Link>
                                </div>
                                <span className="welcomeText2">Secured!</span>
                                <div className="flexRow">
                                    <span className="clickRegisterText">Banking made</span>
                                    <span className="clickRegisterText2">Perfect</span>
                                </div>
                            </div>
                            <span className="welcomeText3 marginRegisterCustomer">Change Password</span>
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
                            </div>
                        </div>
                        {error ? <div className='flexRow errorText'>{errorMessage}</div> : null}
                        <div className="smallBox36">
                            <a id="changeButton" onClick = {changeExistingPassword} className="btn btn-outline-success smallBox9 disabled">
                                <span>Change</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                <div className="d-flex">
                <div className="toast-body">
                    Password Reset Successful, Please Login
                </div>
                <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        </div>
    );
}

export default ForgotPassword;