import axios from 'axios';
import { useState } from 'react';
import '../../../style.css';
import { Link } from 'react-router-dom';

function CreateEmployee() {

    var [email, setEmail] = useState("");
    var [password, setPassword] = useState("");
    var [confirmPassword, setConfirmPassword] = useState("");
    var [name, setName] = useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var newEmployee = {
        "email": email,
        "password": password,
        "name": name
    }

    async function addEmployee(){
        if (email === "" || password === "" || confirmPassword === "" || name === "") {
            alert("Please fill in all fields");
        } else {
            if (email.includes("@maverick.in") && email.length > 5 && email.length < 50) {
                if (password.length >= 8 && password.length <= 15) {
                    if (password === confirmPassword) {
                        if (name.length > 2 && name.length < 100) {
                            await axios.post('http://localhost:7289/api/Validation/RegisterBankEmployees', newEmployee)
                            .then(function (response) {
                                setError(false);
                                showToast();
                            })
                            .catch(function (error) {
                                console.log(error);
                                setError(true);
                                setErrorMessage(error.response.data);
                            });
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
    }

    function checkEmailValidation(eventargs){
        var email = eventargs.target.value;
        setEmail(email)
        if(email !== ""){
            if(email.includes("@maverick.in") && email.includes(".")){
                if(email.length > 5){
                    setError(false);
                    if(email !== "" && password !== "" && confirmPassword !== "" && name !== ""){
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
                    if(email !== "" && password !== "" && confirmPassword !== "" && name !== ""){
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
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== ""){
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
                if(email !== "" && password !== "" && confirmPassword !== "" && name !== ""){
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
    
    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40 widthBox">
            <ul className="smallBox22 nav">
            <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/adminMenu/allEmployees">All Employees</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoBlack smallBox23" to="/adminMenu/createEmployee">Create Employee</Link>
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
                    </div>
                    {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                    <div className="smallBox55">
                        <a id="registerButton" onClick={addEmployee} className="btn btn-outline-success smallBox9 disabled">
                            <span>Create</span>
                        </a>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Successfully Created Employee
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
        </div>
    );
}

export default CreateEmployee;