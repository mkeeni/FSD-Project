import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';

function EmployeeProfile(){

    var [oldData,setOldData] = useState({})
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var [profile,setProfile] = useState(
        {
            "name": "string",
            "email": "string",
    })

    var employeeID = sessionStorage.getItem('id');

    var updateEmployee = {
        "employeeID": employeeID,
        "name": profile.name
    }

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getEmployeeDetails();
    },[])

    async function getEmployeeDetails(){
        await axios.get('http://localhost:7289/api/BankEmployees/GetBankEmployee?employeeID=' + employeeID,httpHeader)
        .then(function (response) {
            setProfile(response.data);
            setOldData(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function updateEmployeeDetails() {
        if (updateEmployee.name === "") {
            alert("Please fill all fields");
        } 
        else {
            if (areEqual(oldData, profile)) {
                alert("No changes made");
            } 
            else {
                if (updateEmployee.name.length > 2 && updateEmployee.name.length < 100) {
                    await axios.put('http://localhost:7289/api/BankEmployees/UpdateBankEmployeeName', updateEmployee, httpHeader)
                    .then(function (response) {
                        showToast();
                    })
                    .catch(function (error) {
                        console.log(error);
                    })
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
                document.getElementById("update").classList.remove("disabled");
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

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox27">
                <div className='margin3'>
                    <span className="textDecoOrange">Personal Details</span> 
                </div>
                <div>
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
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={updateEmployeeDetails}>Update</button>
                        </div>
                    </div>
                </div>
            </div>
            <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Details Updated Successfully
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
        </div>
    );
}

export default EmployeeProfile;