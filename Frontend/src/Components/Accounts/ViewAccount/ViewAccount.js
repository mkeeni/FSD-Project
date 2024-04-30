import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

function ViewAccount(){

    var [clicked,setClicked] = useState([false,false,false]);
    var accountID = useSelector((state) => state.accountID);
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

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
              "name": "Debjit",
            }
          }
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    
    useEffect(() => {
        if(accountID === 0){
            navigate("/menu/customerAccounts");
        }
        else{
            getAccount();
        }
    },[])

    async function getAccount(){
        await axios.get('http://localhost:7289/api/Accounts/GetAccount?accountID=' + accountID,httpHeader)
        .then(function (response) {
            setAccount(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function closeAccount(){
        await axios.put('http://localhost:7289/api/Accounts/CloseAccount?accountID=' + accountID,account,httpHeader)
        .then(function (response) {
            setError(false);
            showToast();
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    function navigateToRecentTransactions(){
        setClicked([true,false,false]);
        navigate("/menu/viewAccount/recentTransaction");
    }

    function navigateToLastMonthTransactions(){
        setClicked([false,true,false]);
        navigate("/menu/viewAccount/lastMonthTransaction");
    }

    function navigateToFilterTransactions(){
        setClicked([false,false,true]);
        navigate("/menu/viewAccount/filterTransaction");
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <div className="upMargin3">
                        <Link to = "/menu/customerAccounts">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                        <div className="closeAccount">
                            <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                <div className="delete change-my-color2"></div>
                            </a>
                            <span className="clickRegisterTextBlack">Close Account</span>
                        </div>
                    </div>
                    <span className="clickRegisterText7">Balance Remaining: {account.balance}</span>
                    <span className="clickRegisterText7">Account No: {account.accountNumber} - {account.accountType} Account</span>
                    <span className="clickRegisterText7">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                    {error ? <div className='flexRow errorText'>{errorMessage}</div> : null}
                    <hr className='hrS' ></hr>
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            {clicked[0] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToRecentTransactions}>Recent Transactions</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToRecentTransactions}>Recent Transactions</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[1] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToLastMonthTransactions}>Last Month</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToLastMonthTransactions}>Last Month</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[2] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={navigateToFilterTransactions}>Filter Transactions</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={navigateToFilterTransactions}>Filter Transactions</span>}
                        </li>
                    </ul>
                    <Outlet/>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Close Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to close this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={closeAccount}>Close</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Account Closed Successfully, Please wait while we process your request and close your account
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
            </div>
    );
}

export default ViewAccount;