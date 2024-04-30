import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import CustomerTransactions from '../../Accounts/ViewCustomerAccountDetails/CustomerTransactions/CustomerTransactions';
import CustomerAccounts from '../../Accounts/ViewCustomerRequest/CustomerAccounts/CustomerAccounts';
import CustomerAppliedLoans from '../../Loans/LoanApplicationDetails/CustomerAppliedLoans/CustomerAppliedLoans';
import CreditWorthiness from '../../Loans/LoanApplicationDetails/CreditWorthiness/CreditWorthiness';

function ViewCustomerDetails(){

    var [clicked,setClicked] = useState([true,false,false,false,false]);
    var customerID = useSelector((state) => state.customerID);
    var navigate = useNavigate();
    
    var [customer,setCustomer] = useState({});

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    
    useEffect(() => {
        getCustomer();
    },[])

    async function getCustomer(){
        if(customerID === 0){
            navigate("/employeeMenu/allCustomers");
        }
        else{
            await axios.get('http://localhost:7289/api/Customers/GetCustomer?customerID=' + customerID,httpHeader)
            .then(function (response) {
                setCustomer(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    function navigateToCustomerTransactions(){
        setClicked([true,false,false,false,false]);
    }

    function navigateToCustomerAccounts(){
        setClicked([false,true,false,false,false]);
    }

    function navigateToCustomerLoans(){
        setClicked([false,false,true,false,false]);
    }

    function navigateToCustomerDetails(){
        setClicked([false,false,false,true,false]);
    }

    function navigateToCreditWorthiness(){
        setClicked([false,false,false,false,true]);
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <div className="upMargin3">
                    <Link to = "/employeeMenu/allCustomers">
                        <div className="leftArrow change-my-color"></div>
                    </Link>
                </div>
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        {clicked[0] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToCustomerTransactions}>Transactions</span> :
                        <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToCustomerTransactions}>Transactions</span>}
                    </li>
                    <li className="nav-item highlight smallBox23">
                        {clicked[1] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToCustomerAccounts}>Accounts</span> :
                        <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToCustomerAccounts}>Accounts</span>}
                    </li>
                    <li className="nav-item highlight smallBox23">
                        {clicked[2] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToCustomerLoans}>Loans</span> :
                        <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToCustomerLoans}>Loans</span>}
                    </li>
                    <li className="nav-item highlight smallBox23">
                        {clicked[3] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToCustomerDetails}>Details</span> :
                        <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToCustomerDetails}>Details</span>}
                    </li>
                    <li className="nav-item highlight smallBox23">
                        {clicked[4] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToCreditWorthiness}>Credit</span> :
                        <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToCreditWorthiness}>Credit</span>}
                    </li>
                </ul>
                {clicked[0] ? <CustomerTransactions/> : null}
                {clicked[1] ? <CustomerAccounts account = {customer}/> : null}
                {clicked[2] ? <CustomerAppliedLoans loan = {customer}/> : null}
                {clicked[3] ? 
                    <div className='smallBox40 scrolling margin4'>
                        <span className="clickRegisterText7">Name: {customer.name}</span>
                        <span className="clickRegisterText7">Email: {customer.email}</span>
                        <span className="clickRegisterText7">Date of Birth: {customer.dob}</span>
                        <span className="clickRegisterText7">Phone Number: {customer.phoneNumber}</span>
                        <span className="clickRegisterText7">Aadhaar Number: {customer.aadharNumber}</span>
                        <span className="clickRegisterText7">PAN Number: {customer.panNumber}</span>
                        <span className="clickRegisterText7">Address: {customer.address}</span>
                        <span className="clickRegisterText7">Gender: {customer.gender}</span>
                    </div>:
                    null}
                {clicked[4] ? <CreditWorthiness loan = {customer}/> : null}
            </div>
        </div>
    );
}

export default ViewCustomerDetails;