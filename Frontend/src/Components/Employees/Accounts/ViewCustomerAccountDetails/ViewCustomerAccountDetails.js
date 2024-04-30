import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import CustomerDetails from '../ViewCustomerRequest/CustomerDetails/CustomerDetails';

function ViewCustomerAccountDetails(){

    var [clicked,setClicked] = useState([true,false,false]);
    var accountID = useSelector((state) => state.accountID);
    var navigate = useNavigate();
    
    var [account,setAccount] = useState(
        {
            "accountID": 0,
            "accountNumber": 0,
            "balance": 0,
            "accountType": "",
            "branches": {
              "ifscNumber": "",
              "branchName": "",
              "banks": {
                "bankName": ""
              }
            },
            "customers": {
              "name": "Mohnish",
            }
          }
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAccount();
    },[])

    async function getAccount(){
        if(accountID === 0){
            navigate("/employeeMenu/accounts/viewCustomerAccount");
        }
        else{
            await axios.get('http://localhost:7289/api/Accounts/GetAccount?accountID=' + accountID,httpHeader)
            .then(function (response) {
                setAccount(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    function navigateToAccountTransactions(){
        setClicked([true,false,false]);
        navigate("/employeeMenu/viewDetails/accountTransactions");
    }

    function navigateToInboundsOutbounds(){
        setClicked([false,true,false]);
        navigate("/employeeMenu/viewDetails/inboudsOutbounds");
    }

    function navigateToCustomerDetails(){
        setClicked([false,false,true]);
    }


    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <div className="upMargin3">
                        <Link to = "/employeeMenu/accounts/viewCustomerAccount">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                    </div>
                    <span className="clickRegisterText7">Balance Remaining: {account.balance}</span>
                    <span className="clickRegisterText7">Account No: {account.accountNumber} - {account.accountType} Account</span>
                    <span className="clickRegisterText7">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                    <hr className='hrS' ></hr>
                    <ul className="smallBox22 nav">
                            <li className="nav-item highlight smallBox23">
                                {clicked[0] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToAccountTransactions}>Account Transactions</span> :
                                <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToAccountTransactions}>Account Transactions</span>}
                            </li>
                            <li className="nav-item highlight smallBox23">
                                {clicked[1] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToInboundsOutbounds}>Inbouds/Outbounds</span> :
                                <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToInboundsOutbounds}>Inbouds/Outbounds</span>}
                            </li>
                            <li className="nav-item highlight smallBox23">
                                {clicked[2] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToCustomerDetails}>Customer Details</span> :
                                <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToCustomerDetails}>Customer Details</span>}
                            </li>
                    </ul>
                    {clicked[2] ? <CustomerDetails account = {account}/> : <Outlet/>}
                </div>
            </div>
    );
}

export default ViewCustomerAccountDetails;