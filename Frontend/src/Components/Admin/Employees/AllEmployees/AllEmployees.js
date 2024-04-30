import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Employee from '../Employee/Employee';
import { useDispatch, useSelector } from 'react-redux';
import { updateEmployeeID } from '../../../../employeeSlice';
import { updateEmail } from '../../../../emailSlice';


function AllEmployees(){

    var fetchedEmail = useSelector((state) => state.email);
    var [email,setEmail] = useState(fetchedEmail);
    var [errorMessage,setErrorMessage] = useState("");
    var [error,setError] = useState(false);
    var [employee,setEmployee] = useState({});
    var [employees,setEmployees] = useState([]);
    var [searched,setSearched] = useState(false);

    var dispatch = useDispatch();
    var navigate = useNavigate();

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        search();
    },[]);

    function search(){
        if(email === ""){
            setSearched(false);
            allEmployees();
            updateEmailID();
        }
        else{
            setSearched(true);
            getEmployee();
            updateEmailID();
        }
    }

    function clear(){
        setSearched(false);
        allEmployees();
        setEmail("");
        dispatch(
            updateEmail("")
        )
    }

    async function allEmployees(){
        await axios.get('http://localhost:7289/api/BankEmployees/GetAllBankEmployees',httpHeader)
        .then(function (response) {
            setEmployees(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getEmployee(){
        await axios.get('http://localhost:7289/api/BankEmployees/GetEmployeeByEmail?email=' + email,httpHeader)
        .then(function (response) {
            setError(false);
            setEmployee(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    function updateEmployeeId(){
        navigate("/adminMenu/viewEmployee");
        dispatch(
            updateEmployeeID(employee.employeeID)
        );
    }

    function updateEmailID(){
        dispatch(
            updateEmail(email)
        );
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox43">
            <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoBlack smallBox23" to="/adminMenu/allEmployees">All Employees</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/adminMenu/createEmployee">Create Employee</Link>
                        </li>
                </ul>
                <div className="widthBox3">
                    <div className="marginRegisterCustomer flexRow2">
                        <input className="form-control enterDiv7" required placeholder='Enter Employee Email' type="email" value={email} onChange={(eventargs) => setEmail(eventargs.target.value)}></input>
                        {searched ? 
                        <span className="pointer" onClick={clear}>
                            <div className="cancel change-my-color2"></div>
                        </span>:
                        null}
                        <span className="pointer" onClick={search}>
                            <div className="search change-my-color"></div>
                        </span>
                    </div>
                </div>
                {searched ? 
                <div>
                    {error ?
                    <div className="smallBox48">
                        <div className="errorImage2 change-my-color2"></div>
                        <div className="clickRegisterText">{errorMessage}</div>
                    </div> : 
                    <div className="heigthBox2">
                        <div className="scrolling">
                            <div className="whiteOutlineBox7">
                                <div className="whiteOutlineBoxMargin">
                                    <span className="clickRegisterText">Name: {employee.name}</span>
                                    <div className="smallBox23">
                                        <span className="clickRegisterText">Email: {employee.email}</span>
                                        <span className="pointer" onClick={updateEmployeeId}>
                                            <div className="rightArrow2 change-my-color"></div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>}
                </div> : 
                <div className="scrolling">
                    {employees.map(employee => 
                        <Employee key = {employee.employeeID} employee={employee}/>
                    )}
                </div>}
            </div>
    </div>
    );
}

export default AllEmployees;