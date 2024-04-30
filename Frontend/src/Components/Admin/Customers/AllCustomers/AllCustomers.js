import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Customer from '../Customer/Customer';
import { useDispatch, useSelector } from 'react-redux';
import { updateEmail } from '../../../../emailSlice';
import { updateCustomerID } from '../../../../customerSlice';

function AllCustomers(){

    var fetchedEmail = useSelector((state) => state.email);
    var [email,setEmail] = useState(fetchedEmail);
    var [errorMessage,setErrorMessage] = useState("");
    var [error,setError] = useState(false);
    var [customer,setCustomer] = useState({});
    var [customers,setCustomers] = useState([]);
    var [searched,setSearched] = useState(false);

    var dispatch = useDispatch();
    var navigate = useNavigate();

    var sessionEmail = sessionStorage.getItem('email');

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
            allCustomers();
            updateEmailID();
        }
        else{
            setSearched(true);
            getCustomer();
            updateEmailID();
        }
    }

    function clear(){
        setSearched(false);
        allCustomers();
        setEmail("");
        dispatch(
            updateEmail("")
        )
    }

    function updateCustomerId(){
        if(sessionEmail.includes("@maverick.in")){
            navigate("/employeeMenu/viewCustomerDetails");
            dispatch(
                updateCustomerID(customer.customerID)
            );
            updateEmailID();
        }
        else{
            navigate("/adminMenu/viewCustomer");
            dispatch(
                updateCustomerID(customer.customerID)
            );
            updateEmailID();
        }
    }

    function updateEmailID(){
        dispatch(
            updateEmail(email)
        );
    }

    async function allCustomers(){
        await axios.get('http://localhost:7289/api/Customers/GetAllCustomers',httpHeader)
        .then(function (response) {
            setCustomers(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getCustomer(){
        await axios.get('http://localhost:7289/api/Customers/GetCustomerByEmail?email=' + email,httpHeader)
        .then(function (response) {
            setError(false);
            setCustomer(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox43">
            {sessionEmail.includes("@maverick.in") ? null :
            <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoBlack smallBox23" to="/adminMenu/allCustomers">All Customers</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/adminMenu/createCustomer">Create Customer</Link>
                        </li>
                </ul>}
                <div className="widthBox3">
                    <div className="marginRegisterCustomer flexRow2">
                        <input className="form-control enterDiv7" required placeholder='Enter Customer Email' type="email" value={email} onChange={(eventargs) => setEmail(eventargs.target.value)}></input>
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
                                    <span className="clickRegisterText">Name: {customer.name}</span>
                                    <div className="smallBox23">
                                        <span className="clickRegisterText">Email: {customer.email}</span>
                                        <span className="pointer" onClick={updateCustomerId}>
                                            <div className="rightArrow2 change-my-color"></div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>}
                </div> : 
                <div className="scrolling">
                    {customers.map(customer => 
                        <Customer key = {customer.customerID} customer={customer}/>
                    )}
                </div>}
                
            </div>
    </div>
    );
}

export default AllCustomers;