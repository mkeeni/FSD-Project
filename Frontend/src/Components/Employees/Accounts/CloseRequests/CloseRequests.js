import { Link, useNavigate } from "react-router-dom";
import '../../../style.css';
import axios from 'axios';
import Account from "../../../Accounts/Account/Account";
import { useEffect, useState } from "react";
import Request from "../Request/Request";

function CloseRequests(){

    var [accounts,setAccounts] = useState([]);
    var [errorMessage,setErrorMessage] = useState("");

    useEffect(() => {
        allAccounts();
    },[]);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function allAccounts(){
        await axios.get('http://localhost:7289/api/Accounts/GetAllAccountsStatus?status=Close%20Account%20Request%20Pending',httpHeader)
        .then(function (response) {
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setErrorMessage(error.response.data);
        })
    }

    return (
        <div>
            <ul className="smallBox22 nav">
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoOrange smallBox23" to="/employeeMenu/accounts/viewCustomerAccount">View Account</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoOrange smallBox23" to="/employeeMenu/accounts/openRequests">Open Account Requests</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoBlack smallBox23" to="/employeeMenu/accounts/closeRequests">Close Account Requests</Link>
                </li>
            </ul>
            {errorMessage === "No Close Account Request Pending Accounts Available" ?
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterTextBlack">No Pending Close Account Request Available</div>
                </div> : 
            <div className="heigthBox3">
                <div className="scrolling">
                        {accounts.map(account => 
                            <Request key = {account.accountID} account={account}/>
                        )}
                </div>
            </div>}
        </div>
    );  
}

export default CloseRequests;