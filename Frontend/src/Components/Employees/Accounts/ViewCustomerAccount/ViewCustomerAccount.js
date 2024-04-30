import { Link, useNavigate } from "react-router-dom";
import axios from 'axios';
import '../../../style.css';
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { updateAccountID } from "../../../../accountSlice";
import { updateAccountNumber } from "../../../../accountNumberSlice";
import Account from "../../../Accounts/Account/Account";
import { updateDate } from "../../../../dateSlice";

function ViewCustomerAccount(){

    var fetchedAccountNumber = useSelector((state) => state.accountNumber);
    var [accountNumber,setAccountNumber]= useState(fetchedAccountNumber);
    var [errorMessage,setErrorMessage] = useState("");
    var [error,setError] = useState(false);
    var [account,setAccount] = useState({});
    var [accounts,setAccounts] = useState([]);
    var [searched,setSearched] = useState(false);

    var dispatch = useDispatch();
    var navigate = useNavigate();

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        dispatch(
            updateDate({
                "fromDate": "",
                "toDate": ""
            })
        )
        search();
    },[])

    function search(){
        if(accountNumber === ""){
            setSearched(false);
            getAllAccounts();
            updateAccountNo();
        }
        else{
            setSearched(true);
            getAccount();
            updateAccountNo();
        }
    }

    function clear(){
        setSearched(false);
        getAllAccounts();
        setAccountNumber("");
        dispatch(
            updateAccountNumber("")
        );
    }

    async function getAllAccounts(){
        await axios.get('http://localhost:7289/api/Accounts/GetAllAccounts',httpHeader)
        .then(function (response) {
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getAccount(){
        await axios.get('http://localhost:7289/api/Accounts/GetAccountByAccountNumber?accountNumber=' + accountNumber,httpHeader)
        .then(function (response) {
            setError(false);
            setAccount(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    function updateAccountId(){
        navigate("/employeeMenu/viewDetails");
        dispatch(
            updateAccountID(account.accountID)
        );
        updateAccountNo();
    }


    function updateAccountNo(){
        dispatch(
            updateAccountNumber(accountNumber)
        );
    }

    return (
        <div>
            <ul className="smallBox60 nav">
            <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoBlack smallBox23" to="/employeeMenu/accounts/viewCustomerAccount">View Account</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoOrange smallBox23" to="/employeeMenu/accounts/openRequests">Open Account Requests</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoOrange smallBox23" to="/employeeMenu/accounts/closeRequests">Close Account Requests</Link>
                </li>
            </ul>
            <div className="widthBox2">
                <div className="marginRegisterCustomer flexRow2">
                    <input className="form-control enterDiv8" required placeholder='Account Number' type="number" value={accountNumber} onChange={(eventargs) => setAccountNumber(eventargs.target.value)}></input>
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
                        <div className="whiteOutlineBox8">
                            <div className="whiteOutlineBoxMargin">
                                <span className="clickRegisterText">Account No: {account.accountNumber}</span>
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Account type: {account.accountType}</span>
                                    <span className="pointer" onClick={updateAccountId}>
                                        <div className="rightArrow2 change-my-color"></div>
                                    </span>
                                </div>
                                <span className="clickRegisterText">Balance: {account.balance}</span>
                            </div>
                        </div>
                    </div>
                </div>}
            </div> : 
            <div className="heigthBox2">
            <div className="scrolling">
                {accounts.map(account => 
                    <Account key = {account.accountID} account={account}/>
                )}
            </div>
            </div>}
        </div>
    );  
}

export default ViewCustomerAccount;